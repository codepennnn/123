string Remarks_CC = ((TextBox)Half_Yearly_Record.Rows[0]
                    .FindControl("Remarks")).Text;

if (PageRecordDataSet.Tables["App_Half_Yearly_Details"]
    .Rows[0]["Status"].ToString() == "Approved")
{
    foreach (DataRow r in PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows)
    {
        r["CC_UpdatedOn"] = System.DateTime.Now;
        r["CC_UpdatedBy"] = Session["UserName"].ToString();
        r["Status"] = "Approved";

        string oldRemarks = r["Remarks"] == DBNull.Value
            ? ""
            : r["Remarks"].ToString();

        r["Remarks"] =
            oldRemarks +
            "( CC --" +
            System.DateTime.Now.ToString("dd/MM/yyyy") +
            " -- " +
            Remarks_CC +
            ")|";
    }
}
else
{
    foreach (DataRow r in PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows)
    {
        r["CC_UpdatedOn"] = System.DateTime.Now;
        r["CC_UpdatedBy"] = Session["UserName"].ToString();
        r["Status"] = "Return";

        string oldRemarks = r["Remarks"] == DBNull.Value
            ? ""
            : r["Remarks"].ToString();

        r["Remarks"] =
            oldRemarks +
            "( CC --" +
            System.DateTime.Now.ToString("dd/MM/yyyy") +
            " -- " +
            Remarks_CC +
            ")|";
    }
}
