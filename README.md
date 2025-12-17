protected void gvAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
{
    if (e.Row.RowType != DataControlRowType.DataRow)
        return;

    var dtWorkOrders = ViewState["WorkOrders"] as DataTable;
    var dtLocations  = ViewState["Locations"] as DataTable;
    var dtEmployees  = ViewState["Employees"] as DataTable;
    var dtExisting   = ViewState["ExistingAttendance"] as DataTable;

    var hfDate       = e.Row.FindControl("hfDateIso") as HiddenField;
    var lblAadhar    = e.Row.FindControl("lblRowAadhar") as Label;
    var lblName      = e.Row.FindControl("lblRowName") as Label;
    var ddlWO        = e.Row.FindControl("ddlRowWorkOrder") as DropDownList;
    var ddlLoc       = e.Row.FindControl("ddlRowLocation") as DropDownList;
    var ddlDayDef    = e.Row.FindControl("ddlDayDef") as DropDownList;
    var ddlEng       = e.Row.FindControl("ddlEngagementType") as DropDownList;
    var txtOT        = e.Row.FindControl("txtOT") as TextBox;
    var chkPresent   = e.Row.FindControl("chkPresent") as CheckBox;

    // ----------------------------
    // Row Date
    // ----------------------------
    DateTime rowDate = DateTime.MinValue;
    if (hfDate != null)
        DateTime.TryParseExact(hfDate.Value, "yyyy-MM-dd",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out rowDate);

    // ----------------------------
    // Bind WorkOrders
    // ----------------------------
    if (ddlWO != null)
    {
        ddlWO.Items.Clear();
        ddlWO.Items.Add(new ListItem("-- Select --", ""));

        if (dtWorkOrders != null)
        {
            foreach (DataRow r in dtWorkOrders.Rows)
            {
                var li = new ListItem(
                    Convert.ToString(r["wo_no"]),
                    Convert.ToString(r["wo_no"])
                );

                li.Attributes["data-loc"] = Convert.ToString(r["LOC_CODE"]);
                li.Attributes["data-eng"] = NormalizeEngagement(
                                                Convert.ToString(r["EngagementType"])
                                            );

                ddlWO.Items.Add(li);
            }
        }
    }

    // ----------------------------
    // Bind Locations
    // ----------------------------
    if (ddlLoc != null)
    {
        ddlLoc.Items.Clear();
        ddlLoc.Items.Add(new ListItem("-- Select --", ""));

        foreach (DataRow r in dtLocations.Rows)
            ddlLoc.Items.Add(new ListItem(
                Convert.ToString(r["Location"]),
                Convert.ToString(r["LocationCode"])
            ));
    }

    // ----------------------------
    // Default DayDef
    // ----------------------------
    if (ddlDayDef != null && rowDate != DateTime.MinValue)
    {
        ddlDayDef.SelectedValue =
            rowDate.DayOfWeek == DayOfWeek.Sunday ? "OD" : "WD";
        ddlDayDef.Enabled = rowDate.DayOfWeek != DayOfWeek.Sunday;
    }

    // ----------------------------
    // Existing Attendance
    // ----------------------------
    DataRow matched = null;
    if (dtExisting != null && rowDate != DateTime.MinValue)
    {
        matched = dtExisting.AsEnumerable()
            .FirstOrDefault(r => Convert.ToDateTime(r["Dates"]).Date == rowDate);
    }

    if (matched != null)
    {
        lblAadhar.Text = Convert.ToString(matched["AadharNo"]);
        lblName.Text   = Convert.ToString(matched["WorkManName"]);

        // WorkOrder
        string wo = Convert.ToString(matched["WorkOrderNo"]);
        if (ddlWO.Items.FindByValue(wo) != null)
            ddlWO.SelectedValue = wo;

        // Location
        string loc = Convert.ToString(matched["LocationCode"]);
        if (ddlLoc.Items.FindByValue(loc) != null)
            ddlLoc.SelectedValue = loc;

        // Engagement
        string eng = matched["EngagementType"] == DBNull.Value
                     ? "ManPowerSupply"
                     : NormalizeEngagement(Convert.ToString(matched["EngagementType"]));
        ddlEng.SelectedValue = eng;

        // OT / Present
        txtOT.Text = Convert.ToString(matched["OT_Hrs"]);
        chkPresent.Checked = Convert.ToBoolean(matched["Present"]);
    }
    else
    {
        // ----------------------------
        // NEW ROW → AUTO DEFAULTS
        // ----------------------------
        lblAadhar.Text = ddlAadhar.SelectedValue;

        var emp = dtEmployees?.Select($"AadharCard = '{ddlAadhar.SelectedValue}'");
        if (emp != null && emp.Length > 0)
            lblName.Text = Convert.ToString(emp[0]["Name"]);

        chkPresent.Checked = false;
    }

    // ----------------------------
    // FORCE DEFAULT WORKORDER
    // ----------------------------
    if (ddlWO != null && string.IsNullOrEmpty(ddlWO.SelectedValue)
        && ddlWO.Items.Count > 1)
    {
        ddlWO.SelectedIndex = 1; // first valid WO

        var li = ddlWO.SelectedItem;
        if (ddlLoc.Items.FindByValue(li.Attributes["data-loc"]) != null)
            ddlLoc.SelectedValue = li.Attributes["data-loc"];

        ddlEng.SelectedValue = li.Attributes["data-eng"];
    }

    // ----------------------------
    // FORCE DEFAULT ENGAGEMENT
    // ----------------------------
    if (ddlEng != null && string.IsNullOrEmpty(ddlEng.SelectedValue))
        ddlEng.SelectedValue = "ManPowerSupply";
}
