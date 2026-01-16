                    SET DATEFIRST 7;  

;WITH d AS (
    SELECT CAST( '2025-07-01' AS date) AS dt
    UNION ALL
    SELECT DATEADD(DAY, 1, dt)
    FROM d
    WHERE dt < '2025-12-31' 
),
WorkDays AS (
    SELECT COUNT(*) AS Establishment_Of_Principal_Emp_Worked
    FROM d
    WHERE DATEPART(WEEKDAY, dt) <> 1  
)





    SELECT NEWID() AS ID,l.LicNo AS LabourLicNo,l.FromDate,l.ToDate,CAST(l.workerno AS INT) AS workerno,
            CONVERT(varchar(10),

         
        
            


        
            DATEDIFF(DAY, CONVERT(datetime, l.FromDate, 103), CONVERT(datetime, l.ToDate, 103))) AS Duration_Of_Contract,
        (SELECT CONCAT(R.V_NAME, ', ', R.ADDRESS) FROM App_Vendor_Reg AS R WHERE R.V_CODE = l.VCode
        ) AS Name_Address_Of_Contractor,

           'JHARKHAND' as State,
            




        c3.wo_no As WorkOrderNo,
        R.From_Date as Workorder_FromDate,
        R.TO_DATE as Workorder_ToDate,

        (wds.Establishment_Of_Principal_Emp_Worked - 2 ) as Establishment_Of_Principal_Emp_Worked ,
   

    
     



        ISNULL((
            SELECT TOP 1 
                COUNT(DISTINCT w.AadharNo)
            FROM App_Wagesdetailsjharkhand w
            INNER JOIN App_EmployeeMaster em 
                ON em.AadharCard = w.AadharNo 
               AND em.Sex = 'M'
            WHERE w.workorderno = c3.wo_no
              AND w.vendorcode = '15235'
              AND w.yearwage = '2025'
              AND CHARINDEX( ',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,' ) > 0
            GROUP BY MonthWage
            ORDER BY COUNT(DISTINCT w.AadharNo) DESC
        ), 0) AS sex_M,

        
     ISNULL((
            SELECT TOP 1 
                COUNT(DISTINCT w.AadharNo)
            FROM App_Wagesdetailsjharkhand w
            INNER JOIN App_EmployeeMaster em 
                ON em.AadharCard = w.AadharNo 
               AND em.Sex = 'F'
            WHERE w.workorderno = c3.wo_no
              AND w.vendorcode = '15235'
              AND w.yearwage = '2025'
              AND CHARINDEX( ',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,' ) > 0
            GROUP BY MonthWage
            ORDER BY COUNT(DISTINCT w.AadharNo) DESC
        ), 0) AS sex_F,

              
        (SELECT ISNULL(SUM(tab.TotalMandays), 0.00) FROM ( SELECT w.AadharNo, SUM(ISNULL(w.TotPaymentDays, 0)) AS TotalMandays
                FROM app_wagesdetailsjharkhand AS w
                INNER JOIN app_employeemaster AS em
                    ON em.aadharcard = w.aadharno AND em.sex = 'M'
                WHERE w.workorderno = c3.wo_no
                  AND w.vendorcode = '15235'
                  AND CHARINDEX( ',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,' ) > 0

                  AND w.yearwage = '2025'
                GROUP BY w.AadharNo
            ) AS tab
        ) AS No_Of_Mandays_Worked_Men,

        (SELECT ISNULL(SUM(tab.TotalMandays), 0.00) FROM ( SELECT w.AadharNo, SUM(ISNULL(w.TotPaymentDays, 0)) AS TotalMandays
                FROM app_wagesdetailsjharkhand AS w
                INNER JOIN app_employeemaster AS em
                    ON em.aadharcard = w.aadharno AND em.sex = 'F'
                WHERE w.workorderno = c3.wo_no
                  AND w.vendorcode = '15235'
                 AND CHARINDEX( ',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,' ) > 0

                  AND w.yearwage = '2025'
                GROUP BY w.AadharNo
            ) AS tab
        ) AS No_Of_Mandays_Worked_Women,
            
         0.00 as No_Of_Mandays_Worked_children,
                   
                 

   
        (SELECT ISNULL(SUM(tab.Male_Deduction), 0.00) FROM ( SELECT w.AadharNo, SUM(ISNULL(w.PFAmt,0) + ISNULL(w.ESIAmt,0)) AS Male_Deduction
                FROM app_wagesdetailsjharkhand AS w
                INNER JOIN app_employeemaster AS em
                    ON em.aadharcard = w.aadharno AND em.sex = 'M'
                WHERE w.workorderno = c3.wo_no
                  AND w.vendorcode = '15235'
                  AND CHARINDEX( ',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,' ) > 0

                  AND w.yearwage = '2025'
                GROUP BY w.AadharNo
            ) AS tab
        ) AS Amt_Of_Deduct_From_Wages_Men,

        (SELECT ISNULL(SUM(tab.Female_Deduction), 0.00) FROM ( SELECT w.AadharNo, SUM(ISNULL(w.PFAmt,0) + ISNULL(w.ESIAmt,0)) AS Female_Deduction
                FROM app_wagesdetailsjharkhand AS w
                INNER JOIN app_employeemaster AS em
                    ON em.aadharcard = w.aadharno AND em.sex = 'F'
                WHERE w.workorderno = c3.wo_no
                  AND w.vendorcode = '15235'
                 AND CHARINDEX( ',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,' ) > 0

                  AND w.yearwage = '2025'
                GROUP BY w.AadharNo
            ) AS tab
        ) AS Amt_Of_Deduct_From_Wages_Women,


               0.00 as Amt_Of_Deduct_From_Wages_Children,
               

      
        (SELECT ISNULL(SUM(tab.Male_Gross), 0.00) FROM ( SELECT w.AadharNo, SUM(ISNULL(w.TotalWages,0)) AS Male_Gross
                FROM app_wagesdetailsjharkhand AS w
                INNER JOIN app_employeemaster AS em
                    ON em.aadharcard = w.aadharno AND em.sex = 'M'
                WHERE w.workorderno = c3.wo_no
                  AND w.vendorcode = '15235'
                  AND CHARINDEX( ',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,' ) > 0

                  AND w.yearwage = '2025'
                GROUP BY w.AadharNo
            ) AS tab
        ) AS Amount_Of_Wages_Paid_Men,

        (SELECT ISNULL(SUM(tab.Female_Gross), 0.00) FROM (SELECT w.AadharNo, SUM(ISNULL(w.TotalWages,0)) AS Female_Gross
                FROM app_wagesdetailsjharkhand AS w
                INNER JOIN app_employeemaster AS em
                    ON em.aadharcard = w.aadharno AND em.sex = 'F'
                WHERE w.workorderno = c3.wo_no
                  AND w.vendorcode = '15235'
                 AND CHARINDEX( ',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,' ) > 0

                  AND w.yearwage = '2025'
                GROUP BY w.AadharNo
            ) AS tab
        ) AS Amount_Of_Wages_Paid_Women,


    0.00 as Amount_Of_Wages_Paid_Children









    FROM App_LabourLicenseSubmission AS l
    LEFT JOIN App_vendor_form_c3_dtl AS c3
        ON c3.ll_no = l.LicNo
     left join App_WorkOrder_Reg R 
        on c3.WO_NO=R.WO_NO
        CROSS JOIN WorkDays wds
    WHERE
        c3.status = 'Approved'
        AND l.FromDate < '2025-12-31'
        AND l.ToDate >= '2025-07-01'
        AND l.VCode = '15235'
        AND l.WorkLocation IN ('Jamshedpur', 'Seraikela')
        order by l.LicNo 
        OPTION (MAXRECURSION 0);
