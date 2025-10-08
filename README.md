public List<(string WorkOrderNo, DateTime ApprovedOn, int ExemptionCC)> GetWorkOrdersInExemptionPeriod(string workOrder)
{
    string sql = @"
        SELECT w.WorkOrderNo, w.Approved_On, w.Exemption_CC 
        FROM App_WorkOrder_Exemption AS w 
        WHERE w.Status = 'Approved' 
          AND DATEDIFF(DAY, w.Approved_On, GETDATE()) <= w.Exemption_CC
          AND (
               w.WorkOrderNo = @workOrder
            OR w.WorkOrderNo LIKE @workOrder + ',%'
            OR w.WorkOrderNo LIKE '%,' + @workOrder + ',%'
            OR w.WorkOrderNo LIKE '%,' + @workOrder
          );
    ";

    Dictionary<string, object> objParam = new Dictionary<string, object>
    {
        { "workOrder", workOrder }
    };

    DataHelper dh = new DataHelper();
    DataSet ds = dh.GetDataset(sql, "App_WorkOrder_Exemption", objParam);

    return ds.Tables[0]
             .AsEnumerable()
             .Select(r => (
                 WorkOrderNo: r.Field<string>("WorkOrderNo"),
                 ApprovedOn: r.Field<DateTime>("Approved_On"),
                 ExemptionCC: r.Field<int>("Exemption_CC")
             ))
             .ToList();
}







string[] workOrders = PageRecordDataSet.Tables["App_WorkOrder_Exemption"]
                             .Rows[0]["WorkOrderNo"]
                             .ToString()
                             .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

foreach (string wo in workOrders)
{
    var conflicts = blobj.GetWorkOrdersInExemptionPeriod(wo.Trim());

    if (conflicts.Any())
    {
        string joined = string.Join(Environment.NewLine, 
            conflicts.Select(c => 
                $"{c.WorkOrderNo} (Approved On: {c.ApprovedOn:dd-MMM-yyyy}, Exemption CC: {c.ExemptionCC} days)"
            )
        );

        MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors,
            $"The following work orders are already approved and still within the exemption period:\n\n{joined}\n\nDuplicate not allowed!");
        return;
    }
}
