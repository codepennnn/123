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
 WHERE Calendar_Year + 1 <= YEAR(VW.END_DATE) )  SELECT 
 FORMAT(GETDATE(), 'dd-MM-yyyy') AS CURR_MONTH,  
 Calendar_Year as Leave_Year,   
 WO_NO AS WorkOrder,    
 V_CODE AS VendorCode,   
 V_NAME AS VendorName,     
 'N' AS Leave_compliance,   
 FORMAT(START_DATE, 'dd-MM-yyyy') AS start_date, 
 FORMAT(END_DATE, 'dd-MM-yyyy') AS end_date  
 FROM YearRange ORDER BY Calendar_Year OPTION (MAXRECURSION 1000);   

 -- in this query i want to also check wo has recognised or not 
 select case when count(*)>0 then 'Y' else 'N' end as REC from APP_RECOGNIZED_WO where WO_NO='" + wo_no + "'
