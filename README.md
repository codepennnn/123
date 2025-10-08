public List<string> GetWorkOrdersInExemptionPeriod(string workOrder)
{
    string sql = @"
        SELECT w.WorkOrderNo, 
               CONVERT(varchar(10), w.Approved_On, 105) AS Approved_On, 
               w.Exemption_CC
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

    Dictionary<string, object> objParam = new Dictionary<string, object>();
    objParam.Add("workOrder", workOrder);

    DataHelper dh = new DataHelper();
    DataSet ds = dh.GetDataset(sql, "App_WorkOrder_Exemption", objParam);

    // ✅ Return combined string (work order + date + cc)
    return ds.Tables[0]
             .AsEnumerable()
             .Select(r => $"{r.Field<string>("WorkOrderNo")} (Approved On: {r.Field<string>("Approved_On")}, Exemption CC: {r.Field<int>("Exemption_CC")})")
             .ToList();
}




foreach (string wo in workOrders)
{
    List<string> conflicts = blobj.GetWorkOrdersInExemptionPeriod(wo.Trim());

    if (conflicts.Any())
    {
        string joined = string.Join(", ", conflicts);
        MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors,
            $"The following work orders are already approved and still within the exemption period: {joined}. Duplicate not allowed!");
        return;
    }
}
