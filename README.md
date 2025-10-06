      select FORMAT(GETDATE(),'dd-MM-yyyy') as CURR_MONTH,
             tab.BONUS_YEAR,
             tab.WorkOrder,
             tab.VendorCode,
             VM.V_NAME as VendorName,
             tab.Bonus_compliance,
             FORMAT(VW.START_DATE,'dd-MM-yyyy') as start_date,
             FORMAT(VW.END_DATE,'dd-MM-yyyy') as end_date
      from (
          select distinct D.Vcode as VendorCode,
                          D.year as BONUS_YEAR,
                          D.WorkOrderNo as WorkOrder,
                          ISNULL(case when S.Status = 'Request Closed' then 'Y' else 'N' end,'N') as Bonus_compliance
          from App_Bonus_Comp_Details D
          left join App_Bonus_Comp_Summary S on S.ID = D.MasterID
          where D.WorkOrderNo = '4700023110' and D.Vcode = '14494'

          union

          select distinct right(V_CODE,5) as VendorCode,
                          BONUS_YEAR as BONUS_YEAR,
                          WO_NO as WorkOrder,
                          'Y' as Bonus_compliance
          from JCMS_ONLINE_TEMP_BONUS
          where STATUS = 'Approved'
            and WO_NO = '4700023110'
            and right(V_CODE,5) = '14494'

          union

          select RIGHT(V_CODE,5) as VendorCode,
                 LEFT(proc_month,4) as BONUS_YEAR,
                 WO_NO as WorkOrder,
                 'Y' as Bonus_compliance
          from JCMS_C_ENTRY_DETAILS
          where C_NO = '8'
            and WO_NO = '4700023110'
            and RIGHT(V_CODE,5) = '14494'
      ) tab
      left join App_Vendorwodetails VW on VW.WO_NO = tab.WorkOrder
      left join App_VendorMaster VM on VM.V_CODE = tab.VendorCode
      order by tab.BONUS_YEAR
