SELECT ISNULL(SUM(tab.TotalMandays), 0.00)
FROM (
    SELECT w.AadharNo,
           SUM(ISNULL(w.TotPaymentDays, 0)) AS TotalMandays
    FROM app_wagesdetailsjharkhand w
    WHERE w.workorderno = c3.wo_no
      AND w.vendorcode = '15235'
      AND w.yearwage = '2025'
      AND CHARINDEX(',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,') > 0
      AND EXISTS (
            SELECT 1
            FROM app_employeemaster em
            WHERE em.aadharcard = w.aadharno
              AND em.sex = 'M'
      )
    GROUP BY w.AadharNo
) tab
