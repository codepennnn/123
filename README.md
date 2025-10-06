WITH YearRange AS (
    -- Generate years from START_DATE to END_DATE
    SELECT 
        VW.WO_NO AS WorkOrder,
        VW.V_CODE AS VendorCode,
        VM.V_NAME AS VendorName,
        YEAR(VW.START_DATE) AS Leave_Year,
        VW.START_DATE,
        VW.END_DATE
    FROM App_Vendorwodetails VW
    LEFT JOIN App_VendorMaster VM ON VW.V_CODE = VM.V_CODE
    WHERE VW.WO_NO = '4700022119' AND VW.V_CODE = '17077'

    UNION ALL

    SELECT 
        YR.WorkOrder,
        YR.VendorCode,
        YR.VendorName,
        YR.Leave_Year + 1,
        YR.START_DATE,
        YR.END_DATE
    FROM YearRange YR
    WHERE YR.Leave_Year + 1 <= YEAR(YR.END_DATE)
),

LeaveData AS (
    -- Your existing leave union logic
    select distinct 
        D.V_Code as VendorCode,
        D.year as Leave_Year,
        D.WorkOrderNo as WorkOrder,
        ISNULL(case when S.Status = 'Request Closed' then 'Y' else 'N' end, 'N') as Leave_compliance
    from App_Leave_Comp_Details D
    left join App_Leave_Comp_Summary S on S.ID = D.MasterID
    where D.WorkOrderNo = '4700022119' and D.V_Code = '17077'

    union

    select distinct 
        right(V_CODE,5) as VendorCode,
        LEAVE_YEAR as Leave_Year,
        WO_NO as WorkOrder,
        'Y' as Leave_compliance
    from JCMS_ONLINE_TEMP_LEAVE
    where STATUS = 'Approved' and WO_NO = '4700022119' and right(V_CODE,5) = '17077'

    union  

    select 
        RIGHT(V_CODE, 5) as VendorCode,
        LEFT(proc_month, 4) as Leave_Year,
        WO_NO as WorkOrder,
        'Y' as Leave_compliance
    from JCMS_C_ENTRY_DETAILS
    where C_NO = '7' and WO_NO = '4700022119' and RIGHT(V_CODE,5) = '17077'
)

SELECT 
    FORMAT(GETDATE(), 'dd-MM-yyyy') as CURR_MONTH,
    YR.Leave_Year,
    YR.WorkOrder,
    YR.VendorCode,
    YR.VendorName,
    -- ✅ if leave data exists for year → Y else N
    ISNULL(MAX(LD.Leave_compliance), 'N') as Leave_compliance,
    FORMAT(YR.START_DATE, 'dd-MM-yyyy') as start_date,
    FORMAT(YR.END_DATE, 'dd-MM-yyyy') as end_date
FROM YearRange YR
LEFT JOIN LeaveData LD 
    ON LD.WorkOrder = YR.WorkOrder 
    AND LD.VendorCode = YR.VendorCode 
    AND LD.Leave_Year = YR.Leave_Year
GROUP BY 
    YR.Leave_Year, 
    YR.WorkOrder, 
    YR.VendorCode, 
    YR.VendorName, 
    YR.START_DATE, 
    YR.END_DATE
ORDER BY YR.Leave_Year
OPTION (MAXRECURSION 1000);
