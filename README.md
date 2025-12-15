string oldRef = dsExist.Tables[0].Rows[0]["RefNo"].ToString();

for (int i = 0; i < PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows.Count; i++)
{
    DataRow row = PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i];

    row["VCode"] = Session["Username"].ToString();
    row["Year"] = Year.SelectedValue;
    row["Period"] = SearchPeriod.SelectedValue;
    row["RefNo"] = oldRef;

    // 🔥 THIS IS WHAT YOU MISSED
    row["ResubmitOn"] = DateTime.Now;

    row["CreatedBy"] = Session["UserName"].ToString();
    row["CreatedOn"] = DateTime.Now;

    // IMPORTANT ORDER
    row.AcceptChanges();
    row.SetAdded();
}
