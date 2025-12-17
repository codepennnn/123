protected void gvAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
{
    if (e.Row.RowType != DataControlRowType.DataRow) return;

    var dtWorkOrders = ViewState["WorkOrders"] as DataTable;
    var dtLocations = ViewState["Locations"] as DataTable;
    var dtEmployees = ViewState["Employees"] as DataTable;
    var dtExisting = ViewState["ExistingAttendance"] as DataTable;

    //var lblDate = e.Row.FindControl("lblDate") as Label;
    var hfDate = e.Row.FindControl("hfDateIso") as HiddenField;

    var lblAadhar = e.Row.FindControl("lblRowAadhar") as Label;
    var lblName = e.Row.FindControl("lblRowName") as Label;
    var ddlRowWorkOrder = e.Row.FindControl("ddlRowWorkOrder") as DropDownList;
    var ddlRowLocation = e.Row.FindControl("ddlRowLocation") as DropDownList;
    var txtOT = e.Row.FindControl("txtOT") as TextBox;
    var ddlDayDef = e.Row.FindControl("ddlDayDef") as DropDownList;
    var chkPresent = e.Row.FindControl("chkPresent") as CheckBox;
    var ddlEngType = e.Row.FindControl("ddlEngagementType") as DropDownList;

    // ----------------------------
    // Determine row date
    // ----------------------------


    //DateTime rowDate = DateTime.MinValue;
    //if (lblDate != null && DateTime.TryParse(lblDate.Text, out DateTime tmp))
    //    rowDate = tmp.Date;

    DateTime rowDate = DateTime.MinValue;
    if (hfDate != null &&
        DateTime.TryParseExact(hfDate.Value, "yyyy-MM-dd",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tmp))
    {
        rowDate = tmp.Date;
    }




    // ----------------------------
    // Bind Work Orders
    // ----------------------------
    

    //3rd
    if (ddlRowWorkOrder != null)
    {
        ddlRowWorkOrder.Items.Clear();
        ddlRowWorkOrder.Items.Add(new ListItem("-- Select --", ""));

        if (dtWorkOrders != null)
        {
            foreach (DataRow r in dtWorkOrders.Rows)
            {
                string wo = Convert.ToString(r["wo_no"]);
                //string loc = r.Table.Columns.Contains("Location_Code") && r["Location_Code"] != DBNull.Value
                //                ? Convert.ToString(r["Location_Code"])
                //                : "";

                string loc = r.Table.Columns.Contains("Loc_Code") && r["Loc_Code"] != DBNull.Value
                                ? Convert.ToString(r["Loc_Code"])
                                : "";
                string eng = r.Table.Columns.Contains("EngagementType") && r["EngagementType"] != DBNull.Value
                                ? Convert.ToString(r["EngagementType"])
                                : "";

                var li = new ListItem(wo, wo);

                if (!string.IsNullOrEmpty(loc))
                    li.Attributes["data-loc"] = loc;        // already used for Location

                if (!string.IsNullOrEmpty(eng))
                    li.Attributes["data-eng"] = NormalizeEngagement(eng);        // NOTE: normalized

                ddlRowWorkOrder.Items.Add(li);
            }
        }
    }



    // ----------------------------
    // Bind Locations
    // ----------------------------
    if (ddlRowLocation != null)
    {
        ddlRowLocation.Items.Clear();
        ddlRowLocation.Items.Add(new ListItem("-- Select --", ""));
        if (dtLocations != null)
        {
            foreach (DataRow r in dtLocations.Rows)
            {
                ddlRowLocation.Items.Add(new ListItem(
                    Convert.ToString(r["Location"]),
                    Convert.ToString(r["LocationCode"])
                ));
            }
        }
    }

    // ----------------------------
    // Default DayDef: Sunday = OD (disabled), Weekday = WD (enabled)
    // ----------------------------
    if (ddlDayDef != null && rowDate != DateTime.MinValue)
    {
        if (rowDate.DayOfWeek == DayOfWeek.Sunday)
        {
            if (ddlDayDef.Items.FindByValue("OD") != null)
                ddlDayDef.SelectedValue = "OD";

            ddlDayDef.Enabled = false;
        }
        else
        {
            if (ddlDayDef.Items.FindByValue("WD") != null)
                ddlDayDef.SelectedValue = "WD";

            ddlDayDef.Enabled = true;
        }
    }

    // ----------------------------
    // Load existing attendance if available
    // ----------------------------
    if (dtExisting != null && rowDate != DateTime.MinValue)
    {
        DataRow matched = null;
        string topAadhar = ddlAadhar.SelectedValue;

        foreach (DataRow r in dtExisting.Rows)
        {
            if (r["Dates"] == DBNull.Value) continue;


            DateTime dbDate = Convert.ToDateTime(r["Dates"]).Date; 
            string dbAadhar = Convert.ToString(r["AadharNo"]).Trim();

            if (dbDate == rowDate &&
                (string.IsNullOrEmpty(topAadhar) ||
                 dbAadhar.Equals(topAadhar.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                matched = r;
                break;
            }
        }

        if (matched != null)
        {
            lblAadhar.Text = Convert.ToString(matched["AadharNo"]);
            lblName.Text = Convert.ToString(matched["WorkManName"]);

            // Work Order
            if (ddlRowWorkOrder != null && matched["WorkOrderNo"] != DBNull.Value)
            {
                string wo = Convert.ToString(matched["WorkOrderNo"]).Trim();
                if (ddlRowWorkOrder.Items.FindByValue(wo) != null)
                    ddlRowWorkOrder.SelectedValue = wo;
            }

            // Location
            if (ddlRowLocation != null && matched["LocationCode"] != DBNull.Value)
            {
                string loc = Convert.ToString(matched["LocationCode"]).Trim();
                if (ddlRowLocation.Items.FindByValue(loc) != null)
                    ddlRowLocation.SelectedValue = loc;
            }

            // OT
            if (txtOT != null && matched["OT_Hrs"] != DBNull.Value)
                txtOT.Text = Convert.ToString(matched["OT_Hrs"]);

            // DayDef
            if (ddlDayDef != null && matched["DayDef"] != DBNull.Value)
            {
                string dbDay = Convert.ToString(matched["DayDef"]).Trim().ToUpper();
                if (ddlDayDef.Items.FindByValue(dbDay) != null)
                    ddlDayDef.SelectedValue = dbDay;
            }

            // Present
            if (chkPresent != null && matched["Present"] != DBNull.Value)
                chkPresent.Checked = Convert.ToBoolean(matched["Present"]);

            // Engagement Type (NEW)                   

            if (ddlEngType != null && matched.Table.Columns.Contains("EngagementType"))
            {
                string dbE = Convert.ToString(matched["EngagementType"]).Trim();
                string normalized = NormalizeEngagement(dbE);
                if (!string.IsNullOrEmpty(normalized) && ddlEngType.Items.FindByValue(normalized) != null)
                    ddlEngType.SelectedValue = normalized;
            }





        }
        else
        {
            // Prefill from top dropdown
            if (!string.IsNullOrEmpty(ddlAadhar.SelectedValue))
            {
                lblAadhar.Text = ddlAadhar.SelectedValue;

                var em = dtEmployees?.Select($"AadharCard = '{ddlAadhar.SelectedValue}'");
                if (em != null && em.Length > 0)
                    lblName.Text = Convert.ToString(em[0]["Name"]);
            }

            if (chkPresent != null) chkPresent.Checked = false;
        }
    }
    else
    {
        // Fresh month with no saved data
        if (!string.IsNullOrEmpty(ddlAadhar.SelectedValue))
        {
            lblAadhar.Text = ddlAadhar.SelectedValue;

            var em = dtEmployees?.Select($"AadharNo = '{ddlAadhar.SelectedValue}'");
            if (em != null && em.Length > 0)
                lblName.Text = Convert.ToString(em[0]["Name"]);
        }

        if (chkPresent != null) chkPresent.Checked = false;
    }

    // Client-side JS will apply present/absent row colors
}
