string newRemark =
    "CC -- " +
    DateTime.Now.ToString("dd/MM/yyyy") +
    " -- " +
    Remarks_CC.Trim();

foreach (DataRow r in PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows)
{
    string oldRemarks = r["Remarks"] == DBNull.Value
        ? string.Empty
        : r["Remarks"].ToString();

    // Append correctly
    r["Remarks"] = oldRemarks + newRemark + " ||";

    r["CC_UpdatedOn"] = DateTime.Now;
    r["CC_UpdatedBy"] = Session["UserName"].ToString();

    if (PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[0]["Status"].ToString() == "Approved")
        r["Status"] = "Approved";
    else
        r["Status"] = "Return";
}
