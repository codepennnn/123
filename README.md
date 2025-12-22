SELECT COUNT(1)
FROM App_WorkOrder_Exemption
WHERE VendorCode = @VendorCode
  AND ',' + WorkOrderNo + ',' LIKE '%,' + @WorkOrderNo + ',%'
  AND Status IN ('Pending With CC', 'Approved', 'Pending')

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

