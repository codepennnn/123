using (SqlConnection con = new SqlConnection(
       ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString))
{
    con.Open();
    SqlTransaction tran = con.BeginTransaction();

    try
    {
        foreach (DataRow r in PageRecordDataSet.Tables["App_BOCW_details_workorder"].Rows)
        {
            if (r.RowState == DataRowState.Modified ||
                r.RowState == DataRowState.Added)
            {
                string woNo = r["WorkOrderNo"].ToString();
                string bocwValue = r["BOCW_APPLICABLE_AS_PER_CC"].ToString();

                SqlCommand cmd = new SqlCommand(
                    @"UPDATE App_WorkOrder_Reg
                      SET BOCW_APPLICABLE_AS_PER_CC = @BOCW
                      WHERE WO_NO = @WO_NO",
                    con, tran);

                cmd.Parameters.AddWithValue("@BOCW", bocwValue);
                cmd.Parameters.AddWithValue("@WO_NO", woNo);

                int rows = cmd.ExecuteNonQuery();

                if (rows <= 0)
                {
                    tran.Rollback();
                    MyMsgBox.show(
                        CLMS.Control.MyMsgBox.MessageType.Errors,
                        "Work Order update failed. Transaction rolled back.");
                    return;
                }
            }
        }

        tran.Commit();
    }
    catch (Exception ex)
    {
        tran.Rollback();
        MyMsgBox.show(
            CLMS.Control.MyMsgBox.MessageType.Errors,
            "Error occurred while updating Work Order.");
        return;
    }
}
