string Remarks_CC = ((TextBox)Half_Yearly_Record.Rows[0]
                    .FindControl("Remarks")).Text.Trim();

foreach (DataRow r in PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows)
{
    string oldRemarks = r["Remarks"] == DBNull.Value
        ? string.Empty
        : r["Remarks"].ToString().Trim();

    // 🔹 Ensure separator exists
    if (!string.IsNullOrEmpty(oldRemarks) && !oldRemarks.EndsWith("||"))
    {
        oldRemarks += " || ";
    }

    // 🔹 Build new remark (always consistent)
    string newRemark =
        "CC -- " +
        DateTime.Now.ToString("dd/MM/yyyy") +
        " -- " +
        Remarks_CC;

    // 🔹 Append properly
    r["Remarks"] = oldRemarks + newRemark + " ||";

    // Other fields (unchanged)
    r["CC_UpdatedOn"] = DateTime.Now;
    r["CC_UpdatedBy"] = Session["UserName"].ToString();

    if (PageRecordDataSet.Tables["App_Half_Yearly_Details"]
        .Rows[0]["Status"].ToString() == "Approved")
        r["Status"] = "Approved";
    else
        r["Status"] = "Return";
}

