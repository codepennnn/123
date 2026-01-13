public bool AreAllWorkOrdersApproved(string masterId)
{
    string sql = @"
        SELECT COUNT(*) 
        FROM App_Bocw_details_workorder d
        INNER JOIN App_WorkOrder_Reg w
            ON d.WO_NO = w.WO_NO
        WHERE d.Master_ID = @Master_ID
          AND ISNULL(w.Status,'') <> 'Approved'";

    Dictionary<string, object> param = new Dictionary<string, object>();
    param.Add("@Master_ID", masterId);

    DataHelper dh = new DataHelper();
    DataSet ds = dh.GetDataset(sql, "chk", param);

    int notApprovedCount = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

    // true = all approved
    return notApprovedCount == 0;
}


string masterId =
    PageRecordDataSet.Tables["App_BOCW_Summary_wo"]
    .Rows[0]["ID"].ToString();

bool allApproved = blobj.AreAllWorkOrdersApproved(masterId);

if (!allApproved)
{
    MyMsgBox.show(
        CLMS.Control.MyMsgBox.MessageType.Warning,
        "Some work orders are not approved. Please get them approved before proceeding.");

    BtnSave.Visible = false;
}
else
{
    BtnSave.Visible = true;
}

