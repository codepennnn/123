DataSet ds = blobj.Get_tab1_detail(
    from,
    to,
    Session["UserName"].ToString(),
    PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["ID"].ToString()
);

if (ds == null || ds.Tables.Count == 0 ||
    ds.Tables["App_Bocw_details_workorder"].Rows.Count == 0)
{
    // Clear grid
    PageRecordDataSet.Tables["App_Bocw_details_workorder"].Clear();
    WODetails_Record.BindData();

    // Show message
    MyMsgBox.show(
        CLMS.Control.MyMsgBox.MessageType.Warning,
        "No Work Order details found for the selected Period and Year."
    );

    // Hide Save button
    BtnSave.Visible = false;

    return; // ⛔ stop further processing
}
else
{
    PageRecordDataSet.Tables["App_Bocw_details_workorder"].Clear();
    PageRecordDataSet.Merge(ds);
}
