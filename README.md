public bool CheckExist(string VendorCode, string workOrderNo)
{
    string StrSQL = @"
        SELECT 1
        FROM App_WorkOrder_Exemption
        WHERE VendorCode = @VendorCode
          AND ',' + WorkOrderNo + ',' LIKE '%,' + @WorkOrderNo + ',%'
          AND Status IN ('Pending With CC', 'Approved')
    ";

    Dictionary<string, object> objParam = new Dictionary<string, object>();
    objParam.Add("@VendorCode", VendorCode);
    objParam.Add("@WorkOrderNo", workOrderNo);

    DataHelper dh = new DataHelper();
    DataSet ds = dh.GetDataset(StrSQL, "App_WorkOrder_Exemption", objParam);

    return ds != null 
        && ds.Tables.Count > 0 
        && ds.Tables[0].Rows.Count > 0;
}


  string vendorCode = Session["UserName"].ToString();

string[] selectedWorkOrders = PageRecordDataSet.Tables["App_WorkOrder_Exemption"]
                               .Rows[0]["WorkOrderNo"]
                               .ToString()
                               .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

foreach (string wo in selectedWorkOrders)
{
    bool exists = blobj.IsWorkOrderAlreadySubmitted(vendorCode, wo.Trim());

    if (exists)
    {
        MyMsgBox.show(
            CLMS.Control.MyMsgBox.MessageType.Errors,
            $"Application already exists for Work Order No: {wo}. Re-submission is not allowed."
        );
        return;
    }
}

