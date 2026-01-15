private void BindLeaveOutsideAttachments()
{
    if (PageRecordDataSet.Tables["App_Leave_Comp_Summary"].Rows.Count == 0)
        return;

    DataRow dr = PageRecordDataSet.Tables["App_Leave_Comp_Summary"].Rows[0];

    BindAttachments(
        dr["Leave_Register_Upload"].ToString(),
        Leave_Register_Upload1
    );

    BindAttachments(
        dr["Bank_Statement_Upload"].ToString(),
        Bank_Statement_Upload1
    );
}


Leavecomplience.BindData();
BindLeaveOutsideAttachments();
