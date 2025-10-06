WITH YearRange AS ( 
    SELECT 
        VW.WO_NO, 
        VW.V_CODE,  
        VM.V_NAME,   
        VW.START_DATE,  
        VW.END_DATE,    
        YEAR(VW.START_DATE) AS Calendar_Year   
    FROM App_Vendorwodetails VW   
    LEFT JOIN App_VendorMaster VM ON VW.V_CODE = VM.V_CODE 
    WHERE VW.WO_NO = '4700023110' AND VW.V_CODE = '14494'  

    UNION ALL 

    SELECT      
        VW.WO_NO,       
        VW.V_CODE,        
        VW.V_NAME,     
        VW.START_DATE, 
        VW.END_DATE,    
        Calendar_Year + 1  
    FROM YearRange VW   
    WHERE Calendar_Year + 1 <= YEAR(VW.END_DATE) 
)
SELECT 
    FORMAT(GETDATE(), 'dd-MM-yyyy') AS CURR_MONTH,  
    Calendar_Year AS Leave_Year,   
    YR.WO_NO AS WorkOrder,    
    YR.V_CODE AS VendorCode,   
    YR.V_NAME AS VendorName,     
    'N' AS Leave_compliance,   
    FORMAT(YR.START_DATE, 'dd-MM-yyyy') AS start_date, 
    FORMAT(YR.END_DATE, 'dd-MM-yyyy') AS end_date,
    CASE 
        WHEN R.WO_NO IS NOT NULL THEN 'Y' 
        ELSE 'N' 
    END AS Recognized_Status   -- ✅ Recognition flag added
FROM YearRange YR
LEFT JOIN APP_RECOGNIZED_WO R ON R.WO_NO = YR.WO_NO   -- ✅ Check recognition
ORDER BY Calendar_Year 
OPTION (MAXRECURSION 1000);
