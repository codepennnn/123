if (isExist)
{
    DataSet ds = blobj.GetDelete(vc, year, Period);

    string oldRef = dsExist.Tables[0].Rows[0]["RefNo"].ToString();

    for (int i = 0; i < PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows.Count; i++)
    {
        DataRow dr = PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i];

        dr["VCode"] = Session["Username"].ToString();
        dr["Year"] = Year.SelectedValue;
        dr["Period"] = SearchPeriod.SelectedValue;
        dr["RefNo"] = oldRef;
        dr["CreatedBy"] = Session["UserName"].ToString();
        dr["CreatedOn"] = DateTime.Now;

        // ❗ DO NOT CALL AcceptChanges()
        // ❗ DO NOT CALL SetAdded()
        // Let it stay Modified
    }
}
