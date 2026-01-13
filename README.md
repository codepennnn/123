bool result = Save();

if (result)
{
    // =================== NEW CODE START ===================
    DataHelper dh = new DataHelper();
    SqlTransaction tran = null;

    try
    {
        dh.OpenConnection();
        tran = dh.BeginTransaction();

        foreach (DataRow r in PageRecordDataSet.Tables["App_BOCW_details_workorder"].Rows)
        {
            if (r.RowState == DataRowState.Modified ||
                r.RowState == DataRowState.Added)
            {
                string woNo = r["WorkOrderNo"].ToString();
                string bocwValue = r["BOCW_APPLICABLE_AS_PER_CC"].ToString();

                string sql = @"UPDATE App_WorkOrder_Reg
                               SET BOCW_APPLICABLE_AS_PER_CC = @BOCW
                               WHERE WO_NO = @WO_NO";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@BOCW", bocwValue);
                param.Add("@WO_NO", woNo);

                int rows = dh.ExecuteNonQuery(sql, param, tran);

                if (rows <= 0)
                {
                    tran.Rollback();
                    MyMsgBox.show(
                        CLMS.Control.MyMsgBox.MessageType.Errors,
                        "Work Order update failed. Changes rolled back.");
                    return;
                }
            }
        }

        tran.Commit();
    }
    catch
    {
        if (tran != null)
            tran.Rollback();

        MyMsgBox.show(
            CLMS.Control.MyMsgBox.MessageType.Errors,
            "Error while updating Work Order. Transaction rolled back.");
        return;
    }
    finally
    {
        dh.CloseConnection();
    }
    // =================== NEW CODE END ===================


    // 🔹 YOUR EXISTING UI RESET CODE (UNCHANGED)
    PageRecordDataSet.Clear();
    summary_Record.BindData();
    PageRecordsDataSet.Clear();
    summary_Entry_Record.BindData();
    GetRecords(GetFilterCondition(), summary_Records.PageSize, 10, "");
    summary_Records.BindData();
    btnSave.Visible = false;
    div_Remarks.Visible = false;
    WODetails_Record.BindData();
    ContainerDiv.Style["overflow"] = "";
    ContainerDiv.Style["height"] = "";

    MyMsgBox.show(
        CLMS.Control.MyMsgBox.MessageType.Success,
        "Record saved successfully !");
}
else
{
    MyMsgBox.show(
        CLMS.Control.MyMsgBox.MessageType.Errors,
        "Error While Saving !");
}
