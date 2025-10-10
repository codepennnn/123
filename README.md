
;WITH BonusData AS (
    SELECT DISTINCT 
        D.Vcode AS VendorCode,
        D.Year AS BONUS_YEAR,
        D.WorkOrderNo AS WorkOrder,
        ISNULL(CASE WHEN S.Status = 'Request Closed' THEN 'Y' ELSE 'N' END, 'N') AS Bonus_compliance
    FROM App_Bonus_Comp_Details D
    LEFT JOIN App_Bonus_Comp_Summary S ON S.ID = D.MasterID
    WHERE D.WorkOrderNo = '2500012468' AND D.Vcode = '29401'

    UNION

    SELECT DISTINCT 
        RIGHT(V_CODE,5) AS VendorCode,
        BONUS_YEAR,
        WO_NO AS WorkOrder,
        'Y' AS Bonus_compliance
    FROM JCMS_ONLINE_TEMP_BONUS
    WHERE STATUS = 'Approved' AND WO_NO = '2500012468' AND RIGHT(V_CODE,5) = '29401'

    UNION

    SELECT 
        RIGHT(V_CODE,5) AS VendorCode,
        LEFT(proc_month,4) AS BONUS_YEAR,
        WO_NO AS WorkOrder,
        'Y' AS Bonus_compliance
    FROM JCMS_C_ENTRY_DETAILS
    WHERE C_NO = '8' AND WO_NO = '2500012468' AND RIGHT(V_CODE,5) = '29401'
),


BonusDataNorm AS (
    SELECT 
        VendorCode,
        WorkOrder,
        Bonus_compliance,
       
        TRY_CAST(LEFT(CAST(BONUS_YEAR AS VARCHAR(20)), 4) AS INT) AS BONUS_YEAR_INT
    FROM BonusData
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
    WHERE VW.WO_NO = '2500012468' AND VW.V_CODE = '29401'
),

YearSeries AS (
    SELECT 
        WO_NO,
        V_CODE,
        V_NAME,
        START_DATE,
        END_DATE,
        Financial_Year_Start
    FROM WorkOrderRange

    UNION ALL

    SELECT 
        WO_NO,
        V_CODE,
        V_NAME,
        START_DATE,
        END_DATE,
        Financial_Year_Start + 1
    FROM YearSeries
    WHERE Financial_Year_Start + 1 <= 
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

    CONCAT(CAST(YS.Financial_Year_Start AS VARCHAR(4)), '-', CAST(YS.Financial_Year_Start + 1 AS VARCHAR(4))) AS BONUS_YEAR,
    YS.WO_NO AS WorkOrder,
    YS.V_CODE AS VendorCode,
    YS.V_NAME AS VendorName,
    CASE 
        WHEN R.WO_NO IS NOT NULL THEN 'Y'                          
        WHEN MAX(BN.Bonus_compliance) = 'Y' THEN 'Y'                
        ELSE 'N'
    END AS Bonus_compliance,
    FORMAT(YS.START_DATE, 'dd-MM-yyyy') AS start_date,
    FORMAT(YS.END_DATE, 'dd-MM-yyyy') AS end_date
FROM YearSeries YS
LEFT JOIN BonusDataNorm BN 
    ON BN.WorkOrder = YS.WO_NO
   
    AND RIGHT(CAST(YS.V_CODE AS VARCHAR(20)),5) = RIGHT(CAST(BN.VendorCode AS VARCHAR(20)),5)

    AND BN.BONUS_YEAR_INT = YS.Financial_Year_Start
LEFT JOIN Recognised R ON R.WO_NO = YS.WO_NO
GROUP BY 
    YS.Financial_Year_Start, YS.WO_NO, YS.V_CODE, YS.V_NAME, YS.START_DATE, YS.END_DATE, R.WO_NO
ORDER BY YS.Financial_Year_Start
OPTION (MAXRECURSION 1000);
