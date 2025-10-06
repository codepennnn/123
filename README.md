;WITH BonusData AS (
    SELECT DISTINCT 
        D.Vcode AS VendorCode,
        D.Year AS BONUS_YEAR,
        D.WorkOrderNo AS WorkOrder,
        ISNULL(CASE WHEN S.Status = 'Request Closed' THEN 'Y' ELSE 'N' END, 'N') AS Bonus_compliance
    FROM App_Bonus_Comp_Details D
    LEFT JOIN App_Bonus_Comp_Summary S ON S.ID = D.MasterID
    WHERE D.WorkOrderNo = '4700023110' AND D.Vcode = '14494'

    UNION

    SELECT DISTINCT 
        RIGHT(V_CODE,5) AS VendorCode,
        BONUS_YEAR,
        WO_NO AS WorkOrder,
        'Y' AS Bonus_compliance
    FROM JCMS_ONLINE_TEMP_BONUS
    WHERE STATUS = 'Approved' AND WO_NO = '4700023110' AND RIGHT(V_CODE,5) = '14494'

    UNION

    SELECT 
        RIGHT(V_CODE,5) AS VendorCode,
        LEFT(proc_month,4) AS BONUS_YEAR,
        WO_NO AS WorkOrder,
        'Y' AS Bonus_compliance
    FROM JCMS_C_ENTRY_DETAILS
    WHERE C_NO = '8' AND WO_NO = '4700023110' AND RIGHT(V_CODE,5) = '14494'
),

WorkOrderRange AS (
    SELECT 
        VW.WO_NO,
        VW.V_CODE,
        VM.V_NAME,
        VW.START_DATE,
        VW.END_DATE,
        CASE 
            WHEN MONTH(VW.START_DATE) >= 4 THEN YEAR(VW.START_DATE)
            ELSE YEAR(VW.START_DATE) - 1 
        END AS Financial_Year_Start
    FROM App_Vendorwodetails VW
    LEFT JOIN App_VendorMaster VM ON VW.V_CODE = VM.V_CODE
    WHERE VW.WO_NO = '4700023110' AND VW.V_CODE = '14494'
),

YearSeries AS (
    SELECT 
        WO_NO,
        V_CODE,
        V_NAME,
        START_DATE,
        END_DATE,
        Financial_Year_Start AS BONUS_YEAR
    FROM WorkOrderRange
    UNION ALL
    SELECT 
        WO_NO,
        V_CODE,
        V_NAME,
        START_DATE,
        END_DATE,
        BONUS_YEAR + 1
    FROM YearSeries
    WHERE BONUS_YEAR + 1 <= 
          CASE 
              WHEN MONTH(END_DATE) >= 4 THEN YEAR(END_DATE)
              ELSE YEAR(END_DATE) - 1 
          END
),

Recognised AS (
    SELECT DISTINCT WO_NO FROM APP_RECOGNIZED_WO
)

SELECT 
    FORMAT(GETDATE(), 'dd-MM-yyyy') AS CURR_MONTH,
    YS.BONUS_YEAR AS BONUS_YEAR,
    YS.WO_NO AS WorkOrder,
    YS.V_CODE AS VendorCode,
    YS.V_NAME AS VendorName,
    CASE 
        WHEN R.WO_NO IS NOT NULL THEN 'Y'
        WHEN MAX(B.Bonus_compliance) = 'Y' THEN 'Y'
        ELSE 'N'
    END AS Bonus_compliance,
    FORMAT(YS.START_DATE, 'dd-MM-yyyy') AS start_date,
    FORMAT(YS.END_DATE, 'dd-MM-yyyy') AS end_date
FROM YearSeries YS
LEFT JOIN BonusData B ON 
    B.WorkOrder = YS.WO_NO 
    AND B.VendorCode = RIGHT(YS.V_CODE, 5)
    AND B.BONUS_YEAR = YS.BONUS_YEAR
LEFT JOIN Recognised R ON R.WO_NO = YS.WO_NO
GROUP BY 
    YS.BONUS_YEAR, YS.WO_NO, YS.V_CODE, YS.V_NAME, YS.START_DATE, YS.END_DATE, R.WO_NO
ORDER BY YS.BONUS_YEAR
OPTION (MAXRECURSION 1000);
