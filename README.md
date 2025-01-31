

my workman present 7 days according to attendance table but why its count 14 days in this query
  select distinct  (select top 1 EMP_ESI_EXEMPTED from App_EmployeeMaster where VendorCode = AttDtl.VendorCode and 
      AadharCard = AttDtl.AadharNo order by CreatedOn desc) EMP_ESI_EXEMPTED, 
      
      (select top 1 EMP_PF_EXEMPTED from App_EmployeeMaster
      where VendorCode = AttDtl.VendorCode and AadharCard = AttDtl.AadharNo order by CreatedOn desc) EMP_PF_EXEMPTED,  masterID MasterID, 
      sum(OT_hrs) OT_hrs, 2024 YearWage,2 MonthWage,AttDtl.AadharNo AadharNo, AttDtl.VendorCode VendorCode, VR.V_NAME VendorName, SN.STATE_NAME StateName, 
      AttDtl.LocationCode LocationCode, LM.Location LocationNM,'' SiteID,'' WorkSite ,WorkOrderNo WorkOrderNo, WorkManSl WorkManSl,WorkManName WorkManName,
      AttDtl.WorkManCategory WorkManCategory,
     
     (select top 1 PFNo from App_EmployeeMaster where VendorCode = AttDtl.VendorCode
      and AadharCard = AttDtl.AadharNo order by CreatedOn desc) PFNo,
      
      (select top 1 ESINo from App_EmployeeMaster 
      where VendorCode = AttDtl.VendorCode and AadharCard = AttDtl.AadharNo order by CreatedOn desc) ESINo,
      (select top 1 UANNo from App_EmployeeMaster where VendorCode = AttDtl.VendorCode and AadharCard = AttDtl.AadharNo order by CreatedOn desc)
      UANNo,DAY(EOMONTH('01/2/2024')) TotDaysInMonth,  DATEDIFF(ww, CAST('20240201' AS datetime) - 1, '20240229') AS TotSunDays, 
    
    (select(Count(ah.Hdate)) from App_HolidayMaster ah where DATEPART(month, ah.Hdate) = '2'and Location = LM.Location  and DATEPART(year, ah.Hdate) = '2024') as TotHoliDays, 
      DAY(EOMONTH('01/2/2024')) - DATEDIFF(ww, CAST('20241001' AS datetime) - 1, '20241031') TotWorkingDays,   
     
     case when AttDtl.EngagementType = 'ManPowerSupply' then Convert(decimal(18,2), sum(convert(INT, AttDtl.Present)))
      end NoOfDaysWorkedMP,  
      
      case when AttDtl.EngagementType <> 'ManPowerSupply' then Convert(decimal(18,2), 
      sum(convert(INT, AttDtl.Present))) end NoOfDaysWorkedRate,  0.0 NoOfDaysAbsMP,CAST(1 as BIT) Approve,null 
      Flag,0.0 PieceRate,  0.0 OverTimeAmt,0.0 CashPaymentAmt,0.0 OtherDeduAmt,0.0 WeeklyAllowance  from App_AttendanceDetails AttDtl 
      inner join App_EmployeeMaster EM on AttDtl.AadharNo = em.AadharCard and AttDtl.VendorCode = em.VendorCode 
      inner join App_LocationMaster LM on AttDtl.LocationCode = LM.LocationCode  inner join App_Vendor_Reg VR on VR.V_CODE = AttDtl.VendorCode 
      inner join STATE_NAME SN on SN.ID = LM.State  where  year(AttDtl.Dates) = '2024' 
      and AttDtl.LocationCode = 'L_45' and month(AttDtl.Dates) = '2' and AttDtl.VendorCode = '17277' and AadharNo='799039612073'  and AttDtl.WorkOrderNo IN  ('4700023666')  
      group by AttDtl.AadharNo,masterID,WorkManSl,WorkOrderNo,AttDtl.VendorCode,AttDtl.LocationCode,WorkManSl,WorkManName, 
      AttDtl.WorkManCategory,LM.Location,EngagementType,Basic_rate,VR.V_NAME,SN.STATE_NAME order by WorkManName  


-----------------------------------------------------------------
 Select ''as WO_NO,'' as wo_date from App_WorkOrder_Reg union
 select WO_NO,(WO_NO+' - '+ Convert(varchar,TO_DATE,103)) as wo_date from App_WorkOrder_Reg where V_CODE='12307'
 and Status='Approved' and WO_NO Not in (select  WO_NO  from App_Wo_Nil where  NO_WORK='Permanent')

