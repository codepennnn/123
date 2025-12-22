string vendorCode = Session["UserName"].ToString();

foreach (string wo in workOrders)
{
    DataRow existingRow = blobj.GetLatestApplication(vendorCode, wo.Trim());

    if (existingRow != null)
    {
        string status = existingRow["Status"].ToString();

        // ❌ Block
        if (status == "Pending With CC" || status == "Approved")
        {
            MyMsgBox.show(
                CLMS.Control.MyMsgBox.MessageType.Errors,
                $"Application already exists for Work Order {wo} and is under process."
            );
            return;
        }

        // ✅ Returned → Update same application
        if (status == "Returned")
        {
            PageRecordDataSet.Tables["App_WorkOrder_Exemption"]
                .Rows[0]["ID"] = existingRow["ID"];

            PageRecordDataSet.Tables["App_WorkOrder_Exemption"]
                .Rows[0]["Status"] = "Pending With CC";

            PageRecordDataSet.Tables["App_WorkOrder_Exemption"]
                .Rows[0]["ResubmittedOn"] = DateTime.Now;

            PageRecordDataSet.Tables["App_WorkOrder_Exemption"]
                .Rows[0]["ResubmittedBy"] = vendorCode;
        }
    }
}

public DataRow GetLatestApplication(string vendorCode, string workOrderNo)
{
    string sql = @"
        SELECT TOP 1 *
        FROM App_WorkOrder_Exemption
        WHERE VendorCode = @VendorCode
          AND ',' + WorkOrderNo + ',' LIKE '%,' + @WorkOrderNo + ',%'
        ORDER BY CreatedOn DESC";

    Dictionary<string, object> param = new Dictionary<string, object>();
    param.Add("@VendorCode", vendorCode);
    param.Add("@WorkOrderNo", workOrderNo);

    DataHelper dh = new DataHelper();
    DataSet ds = dh.GetDataset(sql, "App_WorkOrder_Exemption", param);

    if (ds != null && ds.Tables[0].Rows.Count > 0)
        return ds.Tables[0].Rows[0];

    return null;
}

