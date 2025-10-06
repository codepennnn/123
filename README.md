select FORMAT(GETDATE(), 'dd-MM-yyyy') as CURR_MONTH,tab.Leave_Year,tab.WorkOrder,tab.VendorCode,VM.V_NAME as VendorName,
    MAX(tab.Leave_compliance) as Leave_compliance,   
    FORMAT(VW.START_DATE, 'dd-MM-yyyy') as start_date,
    FORMAT(VW.END_DATE, 'dd-MM-yyyy') as end_date
from
(select distinct D.V_Code as VendorCode,D.year as Leave_Year,D.WorkOrderNo as WorkOrder,
   ISNULL(case when S.Status = 'Request Closed' then 'Y' else 'N' end,'N') as Leave_compliance
    from App_Leave_Comp_Details D
    left join App_Leave_Comp_Summary S on S.ID = D.MasterID where D.WorkOrderNo = '4700022119'  and D.V_Code = '17077'  
    union
    select distinct right(V_CODE,5) as VendorCode,LEAVE_YEAR as Leave_Year,WO_NO as WorkOrder,'Y' as Leave_compliance from JCMS_ONLINE_TEMP_LEAVE
    where STATUS = 'Approved'and WO_NO = '4700022119' and right(V_CODE,5)= '17077'  
    union  
    select RIGHT(V_CODE, 5) as VendorCode,LEFT(proc_month, 4) as Leave_Year,WO_NO as WorkOrder,'Y' as Leave_compliance from JCMS_C_ENTRY_DETAILS where C_NO = '7' and WO_NO = '4700022119' and RIGHT(V_CODE,5)= '17077'  
) tab
left join App_Vendorwodetails VW on VW.WO_NO = tab.WorkOrder   
left join App_VendorMaster VM on VM.V_CODE = tab.VendorCode  
group by tab.Leave_Year, tab.WorkOrder, tab.VendorCode, VM.V_NAME, VW.START_DATE, VW.END_DATE
order by tab.Leave_Year;

my workorder validity 01-01-2023 to 31-03-2026 so row must be 23,24,25,26 - 5 records but its showing 2 records only 23,25 because leave data only in this two year
so i want all data as per workorder validity and if data in leave then Y otherwise N
