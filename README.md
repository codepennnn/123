DateTime oldCreatedOn =
    Convert.ToDateTime(dsExist.Tables[0].Rows[0]["CreatedOn"]);

string oldRef =
    dsExist.Tables[0].Rows[0]["RefNo"].ToString();

for (int i = 0; i < PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows.Count; i++)
{
    DataRow row = PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i];

    row["VCode"] = Session["Username"].ToString();
    row["Year"] = Year.SelectedValue;
    row["Period"] = SearchPeriod.SelectedValue;
    row["RefNo"] = oldRef;

    // ✅ Keep original CreatedOn
    row["CreatedOn"] = oldCreatedOn;

    // ✅ Set only resubmission date
    row["ResubmittedOn"] = DateTime.Now;

    row["CreatedBy"] = Session["UserName"].ToString();

    row.AcceptChanges();
    row.SetAdded();
}
