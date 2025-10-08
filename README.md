      //validation - Approved work orders should not be repeated within the exempted period defined by the CC team

      string[] workOrders = PageRecordDataSet.Tables["App_WorkOrder_Exemption"]
                                 .Rows[0]["WorkOrderNo"]
                                 .ToString()
                                 .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

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

           public List<string> GetWorkOrdersInExemptionPeriod(string workOrder)
     {

         string sql = @"SELECT w.WorkOrderNo FROM App_WorkOrder_Exemption AS w WHERE w.Status = 'Approved' AND DATEDIFF(DAY, w.Approved_On, GETDATE()) <= w.Exemption_CC AND
      (
       w.WorkOrderNo = @workOrder
          OR w.WorkOrderNo LIKE @workOrder + ',%'
          OR w.WorkOrderNo LIKE '%,' + @workOrder + ',%'
          OR w.WorkOrderNo LIKE '%,' + @workOrder
       );";

         Dictionary<string, object> objParam = new Dictionary<string, object>();
         objParam.Add("workOrder", workOrder);

         DataHelper dh = new DataHelper();
         DataSet ds = dh.GetDataset(sql, "App_WorkOrder_Exemption", objParam);


         return ds.Tables[0]
                  .AsEnumerable()
                  .Select(r => r.Field<string>("WorkOrderNo"))
                  .ToList();
     }
