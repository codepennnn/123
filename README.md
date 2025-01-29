
  if (e.Row.RowType.ToString().ToUpper() == "DATAROW")
            {
                if (e.Row.Cells[7].Text.Trim() != "" && e.Row.Cells[7].Text.Trim() != null && e.Row.Cells[7].Text.Trim() != "&nbsp;")
                {
                    e.Row.Cells[7].Text = (Convert.ToDateTime(e.Row.Cells[7].Text.Substring(0, 10))).ToString("MM/dd/yyyy");
                }
 
 
 Select ''as WO_NO,'' as wo_date from App_WorkOrder_Reg union
 select WO_NO,(WO_NO+' - '+ Convert(varchar,TO_DATE,103)) as wo_date from App_WorkOrder_Reg where V_CODE='12307'
 and Status='Approved' and WO_NO Not in (select  WO_NO  from App_Wo_Nil where  NO_WORK='Permanent')

