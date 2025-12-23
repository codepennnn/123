  string strSQL = "select * ,DATEDIFF(day, CreatedOn,getdate()) as dayscountCreatedOn,DATEDIFF(day, ResubmittedOn, getdate()) as dayscountResub from App_WorkOrder_Exemption  ";
  DataHelper dh = new DataHelper();
  Dictionary<string, object> objParam = null;



         



  if (FilterConditions != null && FilterConditions.Count > 0)
  {
      strSQL += " where ";

      if (FilterConditions["From_COMPLAINT_DATE"] != null && FilterConditions["To_COMPLAINT_DATE"] != null)
      {
       

          string ftdt = FilterConditions["From_COMPLAINT_DATE"].Substring(6, 4) + "-" + FilterConditions["From_COMPLAINT_DATE"].Substring(3, 2) + "-" + FilterConditions["From_COMPLAINT_DATE"].Substring(0, 2);
          string eddt = FilterConditions["To_COMPLAINT_DATE"].Substring(6, 4) + "-" + FilterConditions["To_COMPLAINT_DATE"].Substring(3, 2) + "-" + FilterConditions["To_COMPLAINT_DATE"].Substring(0, 2);
          strSQL += "CreatedOn >= Convert(datetime, '" + ftdt + "') and CreatedOn <= Convert(datetime, '" + eddt + "')";
      }




      strSQL += " and Status='" + FilterConditions["Status"] + "'     ";

      if (FilterConditions["Search_With"] != null)
          strSQL += "and " + FilterConditions["Search_With"] + " like '%" + FilterConditions["Enter_Detail"] + "'   ";

  }
  if (sortExpression != "")
      strSQL += "order by " + sortExpression;
  return dh.GetDataset(strSQL, "App_WorkOrder_Exemption", objParam);





  select * ,DATEDIFF(day, CreatedOn,getdate()) as dayscountCreatedOn,DATEDIFF(day, ResubmittedOn, getdate()) as dayscountResub 
from App_WorkOrder_Exemption   where COMPLAINT_DATE >= Convert(datetime, '2025-12-10') and
COMPLAINT_DATE <= Convert(datetime, '2025-12-23') and Status='Pending with CC'  

where CreatedOn >= Convert(datetime, '2025-12-10') 
and CreatedOn <= Convert(datetime, '2025-12-23') and Status='Pending with CC'   
