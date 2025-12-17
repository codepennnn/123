DataTable dtAttended = new DataTable();

string attSql = @"
    SELECT DISTINCT AadharNo
    FROM App_AttendanceDetails
    WHERE Dates >= @FromDate
      AND Dates <  @ToDate
";

using (var da = new SqlDataAdapter(attSql, cn))
{
    da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.Date).Value = fromDate;
    da.SelectCommand.Parameters.Add("@ToDate", SqlDbType.Date).Value = toDate;
    da.Fill(dtAttended);
}
ViewState["AttendedAadhars"] = dtAttended;


private void BindAadharDropdown()
{
    var dtEmployees = ViewState["Employees"] as DataTable;
    var dtAttended  = ViewState["AttendedAadhars"] as DataTable;

    ddlAadhar.Items.Clear();
    ddlAadhar.Items.Add(new ListItem("-- Select --", ""));

    foreach (DataRow r in dtEmployees.Rows)
    {
        string aadhar = r["AadharCard"].ToString();
        string name   = r["Name"].ToString();

        var li = new ListItem($"{name} ({aadhar})", aadhar);

        // MARK GREEN IF ATTENDANCE EXISTS
        if (dtAttended != null &&
            dtAttended.Select($"AadharNo = '{aadhar}'").Length > 0)
        {
            li.Attributes["class"] = "att-done";
        }

        ddlAadhar.Items.Add(li);
    }
}


select option.att-done {
    background-color: #dcfce7;
    color: #166534;
    font-weight: 600;
}

