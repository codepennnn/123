if (PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[0].RowState.ToString() == "Modified")
{
    foreach (DataRow dr in ds.Tables[0].Rows)
    {
        dr["Final_Attachment"] = attachments;
        dr["Status"] = "Pending With CC";
        dr["ResubmitedOn"] = System.DateTime.Now;


    }
}

else
{
    foreach (DataRow dr in ds.Tables[0].Rows)
    {
        dr["Final_Attachment"] = attachments;
        dr["Status"] = "Pending With CC";
        


    }
}
