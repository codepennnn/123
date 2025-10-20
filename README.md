// 🔹 Create lists to track totals and selected records
List<string> selectedWorkOrders = new List<string>();
double totalCheckedAmount = 0;

// 🔹 Loop through every row in your grid
foreach (GridViewRow row in WorkOder_wise_records.Rows)
{
    CheckBox chk = (CheckBox)row.FindControl("chkSelect");
    HiddenField hfKONNR = (HiddenField)row.FindControl("hfKONNR");
    HiddenField hfAmount = (HiddenField)row.FindControl("hfAmount");

    // Defensive null checks
    if (hfKONNR == null || hfAmount == null || chk == null)
        continue;

    string konnr = hfKONNR.Value;
    double amount = 0;
    Double.TryParse(hfAmount.Value, out amount);
    bool isChecked = chk.Checked;

    // 🔹 Find if this work order already exists in dataset
    DataRow[] existing = PageRecordDataSet.Tables["App_BOCW_Details"].Select("KONNR = '" + konnr + "'");
    DataRow rowData;

    if (existing.Length > 0)
    {
        // Update existing
        rowData = existing[0];
        rowData["Checked"] = isChecked;
        rowData["Amount"] = amount;
        rowData["CreatedOn"] = DateTime.Now;
        rowData["CreatedBy"] = Session["UserName"].ToString();
    }
    else
    {
        // Add new row if not exists
        rowData = PageRecordDataSet.Tables["App_BOCW_Details"].NewRow();
        rowData["KONNR"] = konnr;
        rowData["Amount"] = amount;
        rowData["Checked"] = isChecked;
        rowData["CreatedOn"] = DateTime.Now;
        rowData["CreatedBy"] = Session["UserName"].ToString();
        rowData["Master_Id"] = PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Id"].ToString();
        PageRecordDataSet.Tables["App_BOCW_Details"].Rows.Add(rowData);
    }

    // 🔹 Totals — only count checked rows
    if (isChecked)
    {
        totalCheckedAmount += amount;
        selectedWorkOrders.Add(konnr);
    }
}

// 🔹 Update summary totals
var summary = PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0];
summary["Selected_WorkOrders"] = string.Join(",", selectedWorkOrders);
summary["Total_Base_Amount"] = totalCheckedAmount;
summary["Cess_Amount"] = totalCheckedAmount * 0.01; // example: 1% cess
summary["Status"] = "Pending with CC";

// 🔹 Accept all changes before final save
PageRecordDataSet.Tables["App_BOCW_Details"].AcceptChanges();
PageRecordDataSet.Tables["App_BOCW_Summary"].AcceptChanges();
