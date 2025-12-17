private void BindAadharDropdown()
{
    var dtEmployees = ViewState["Employees"] as DataTable;
    var dtPresent   = ViewState["PresentAadhars"] as DataTable;

    ddlAadhar.Items.Clear();
    ddlAadhar.Items.Add(new ListItem("-- Select --", ""));

    foreach (DataRow r in dtEmployees.Rows)
    {
        string aadhar = r["AadharCard"].ToString();
        string name   = r["Name"].ToString();

        var li = new ListItem($"{name} ({aadhar})", aadhar);

        // ✅ COLOR GREEN IF PRESENT=1 EXISTS
        if (dtPresent != null &&
            dtPresent.Select($"AadharNo = '{aadhar}'").Length > 0)
        {
            li.Attributes["class"] = "att-present";
        }

        ddlAadhar.Items.Add(li);
    }
}
