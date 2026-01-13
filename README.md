public DataSet GetNotApprovedWorkOrders(string woCsv)
{
    string sql = @"
        SELECT WO_NO
        FROM App_WorkOrder_Reg
        WHERE WO_NO IN (SELECT value FROM STRING_SPLIT(@WO_LIST, ','))
          AND ISNULL(Status,'') <> 'Approved'";

    Dictionary<string, object> param = new Dictionary<string, object>();
    param.Add("@WO_LIST", woCsv);

    DataHelper dh = new DataHelper();
    return dh.GetDataset(sql, "NotApprovedWO", param);
}


public void get_wo()
{
    string period = ((DropDownList)summary_Record.Rows[0].FindControl("PERIOD")).SelectedValue;
    string year = ((DropDownList)summary_Record.Rows[0].FindControl("YEAR")).SelectedValue;

    DataSet ds_dup_chk = blobj.get_dup_chk(year, period, Session["UserName"].ToString());

    if (ds_dup_chk.Tables[0].Rows.Count > 0)
    {
        MyMsgBox.show(
            CLMS.Control.MyMsgBox.MessageType.Errors,
            "Data with selected Period & Year already exists, cannot proceed.");

        BtnSave.Visible = false;
        return;
    }

    BtnSave.Visible = true;

    string from = null, to = null;

    if (period == "April to June")
    {
        from = year + "-04-01 00:00:00.000";
        to = year + "-06-30 00:00:00.000";
    }
    else if (period == "July to September")
    {
        from = year + "-07-01 00:00:00.000";
        to = year + "-09-30 00:00:00.000";
    }
    else if (period == "October to December")
    {
        from = year + "-10-01 00:00:00.000";
        to = year + "-12-31 00:00:00.000";
    }
    else if (period == "January to March")
    {
        from = year + "-01-01 00:00:00.000";
        to = year + "-03-31 00:00:00.000";
    }

    DataSet ds = blobj.Get_tab1_detail(
        from,
        to,
        Session["UserName"].ToString(),
        PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["ID"].ToString());

    if (ds != null && ds.Tables["App_Bocw_details_workorder"].Rows.Count > 0)
    {
        PageRecordDataSet.Tables["App_Bocw_details_workorder"].Clear();
        PageRecordDataSet.Merge(ds);
    }

    // ===================== NEW LOGIC START =====================

    // Build comma-separated WorkOrder list
    string woList = "";

    foreach (DataRow r in PageRecordDataSet.Tables["App_Bocw_details_workorder"].Rows)
    {
        woList += r["WO_NO"].ToString() + ",";
    }

    woList = woList.TrimEnd(',');

    if (!string.IsNullOrEmpty(woList))
    {
        DataSet dsNotApproved = blobj.GetNotApprovedWorkOrders(woList);

        if (dsNotApproved.Tables[0].Rows.Count > 0)
        {
            // Build message with WO numbers
            string notApprovedWOs = "";

            foreach (DataRow r in dsNotApproved.Tables[0].Rows)
            {
                notApprovedWOs += r["WO_NO"].ToString() + ", ";
            }

            notApprovedWOs = notApprovedWOs.TrimEnd(',', ' ');

            MyMsgBox.show(
                CLMS.Control.MyMsgBox.MessageType.Warning,
                "The following Work Orders are NOT approved:\n" +
                notApprovedWOs +
                "\n\nPlease get them approved before proceeding.");

            BtnSave.Visible = false;
        }
        else
        {
            BtnSave.Visible = true;
        }
    }

    // ===================== NEW LOGIC END =====================
}

