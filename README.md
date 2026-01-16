SET DATEFIRST 7;

;WITH d AS (
    SELECT CAST('2025-07-01' AS DATE) AS dt
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

SELECT
    NEWID() AS ID,
    l.LicNo AS LabourLicNo,
    l.FromDate,
    l.ToDate,
    CAST(l.workerno AS INT) AS workerno,

    CONVERT(VARCHAR(10),
        DATEDIFF(DAY,
            CONVERT(DATETIME, l.FromDate, 103),
            CONVERT(DATETIME, l.ToDate, 103)
        )
    ) AS Duration_Of_Contract,

    (SELECT CONCAT(R1.V_NAME, ', ', R1.ADDRESS)
     FROM App_Vendor_Reg R1
     WHERE R1.V_CODE = l.VCode
    ) AS Name_Address_Of_Contractor,

    'JHARKHAND' AS State,

    c3.wo_no AS WorkOrderNo,
    R.From_Date AS Workorder_FromDate,
    R.To_Date AS Workorder_ToDate,

    (wds.Establishment_Of_Principal_Emp_Worked - 2)
        AS Establishment_Of_Principal_Emp_Worked,

    /* ================= MALE COUNT ================= */
    ISNULL((
        SELECT COUNT(DISTINCT w.AadharNo)
        FROM App_WagesDetailsJharkhand w
        WHERE w.workorderno = c3.wo_no
          AND w.vendorcode = '15235'
          AND w.yearwage = '2025'
          AND CHARINDEX(',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,') > 0
          AND EXISTS (
                SELECT 1
                FROM App_EmployeeMaster em
                WHERE em.AadharCard = w.AadharNo
                  AND em.Sex = 'M'
          )
    ), 0) AS sex_M,

    /* ================= FEMALE COUNT ================= */
    ISNULL((
        SELECT COUNT(DISTINCT w.AadharNo)
        FROM App_WagesDetailsJharkhand w
        WHERE w.workorderno = c3.wo_no
          AND w.vendorcode = '15235'
          AND w.yearwage = '2025'
          AND CHARINDEX(',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,') > 0
          AND EXISTS (
                SELECT 1
                FROM App_EmployeeMaster em
                WHERE em.AadharCard = w.AadharNo
                  AND em.Sex = 'F'
          )
    ), 0) AS sex_F,

    /* ================= MANDAYS MEN ================= */
    ISNULL((
        SELECT SUM(tab.TotalMandays)
        FROM (
            SELECT w.AadharNo,
                   SUM(ISNULL(w.TotPaymentDays, 0)) AS TotalMandays
            FROM App_WagesDetailsJharkhand w
            WHERE w.workorderno = c3.wo_no
              AND w.vendorcode = '15235'
              AND w.yearwage = '2025'
              AND CHARINDEX(',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,') > 0
              AND EXISTS (
                    SELECT 1
                    FROM App_EmployeeMaster em
                    WHERE em.AadharCard = w.AadharNo
                      AND em.Sex = 'M'
              )
            GROUP BY w.AadharNo
        ) tab
    ), 0) AS No_Of_Mandays_Worked_Men,

    /* ================= MANDAYS WOMEN ================= */
    ISNULL((
        SELECT SUM(tab.TotalMandays)
        FROM (
            SELECT w.AadharNo,
                   SUM(ISNULL(w.TotPaymentDays, 0)) AS TotalMandays
            FROM App_WagesDetailsJharkhand w
            WHERE w.workorderno = c3.wo_no
              AND w.vendorcode = '15235'
              AND w.yearwage = '2025'
              AND CHARINDEX(',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,') > 0
              AND EXISTS (
                    SELECT 1
                    FROM App_EmployeeMaster em
                    WHERE em.AadharCard = w.AadharNo
                      AND em.Sex = 'F'
              )
            GROUP BY w.AadharNo
        ) tab
    ), 0) AS No_Of_Mandays_Worked_Women,

    0.00 AS No_Of_Mandays_Worked_Children,

    /* ================= DEDUCTION MEN ================= */
    ISNULL((
        SELECT SUM(tab.Male_Deduction)
        FROM (
            SELECT w.AadharNo,
                   SUM(ISNULL(w.PFAmt,0) + ISNULL(w.ESIAmt,0)) AS Male_Deduction
            FROM App_WagesDetailsJharkhand w
            WHERE w.workorderno = c3.wo_no
              AND w.vendorcode = '15235'
              AND w.yearwage = '2025'
              AND CHARINDEX(',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,') > 0
              AND EXISTS (
                    SELECT 1
                    FROM App_EmployeeMaster em
                    WHERE em.AadharCard = w.AadharNo
                      AND em.Sex = 'M'
              )
            GROUP BY w.AadharNo
        ) tab
    ), 0) AS Amt_Of_Deduct_From_Wages_Men,

    /* ================= DEDUCTION WOMEN ================= */
    ISNULL((
        SELECT SUM(tab.Female_Deduction)
        FROM (
            SELECT w.AadharNo,
                   SUM(ISNULL(w.PFAmt,0) + ISNULL(w.ESIAmt,0)) AS Female_Deduction
            FROM App_WagesDetailsJharkhand w
            WHERE w.workorderno = c3.wo_no
              AND w.vendorcode = '15235'
              AND w.yearwage = '2025'
              AND CHARINDEX(',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,') > 0
              AND EXISTS (
                    SELECT 1
                    FROM App_EmployeeMaster em
                    WHERE em.AadharCard = w.AadharNo
                      AND em.Sex = 'F'
              )
            GROUP BY w.AadharNo
        ) tab
    ), 0) AS Amt_Of_Deduct_From_Wages_Women,

    0.00 AS Amt_Of_Deduct_From_Wages_Children,

    /* ================= GROSS MEN ================= */
    ISNULL((
        SELECT SUM(tab.Male_Gross)
        FROM (
            SELECT w.AadharNo,
                   SUM(ISNULL(w.TotalWages,0)) AS Male_Gross
            FROM App_WagesDetailsJharkhand w
            WHERE w.workorderno = c3.wo_no
              AND w.vendorcode = '15235'
              AND w.yearwage = '2025'
              AND CHARINDEX(',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,') > 0
              AND EXISTS (
                    SELECT 1
                    FROM App_EmployeeMaster em
                    WHERE em.AadharCard = w.AadharNo
                      AND em.Sex = 'M'
              )
            GROUP BY w.AadharNo
        ) tab
    ), 0) AS Amount_Of_Wages_Paid_Men,

    /* ================= GROSS WOMEN ================= */
    ISNULL((
        SELECT SUM(tab.Female_Gross)
        FROM (
            SELECT w.AadharNo,
                   SUM(ISNULL(w.TotalWages,0)) AS Female_Gross
            FROM App_WagesDetailsJharkhand w
            WHERE w.workorderno = c3.wo_no
              AND w.vendorcode = '15235'
              AND w.yearwage = '2025'
              AND CHARINDEX(',' + CAST(w.monthwage AS VARCHAR(10)) + ',', ',7,8,9,10,11,12,') > 0
              AND EXISTS (
                    SELECT 1
                    FROM App_EmployeeMaster em
                    WHERE em.AadharCard = w.AadharNo
                      AND em.Sex = 'F'
              )
            GROUP BY w.AadharNo
        ) tab
    ), 0) AS Amount_Of_Wages_Paid_Women,

    0.00 AS Amount_Of_Wages_Paid_Children

FROM App_LabourLicenseSubmission l
LEFT JOIN App_vendor_form_c3_dtl c3
    ON c3.ll_no = l.LicNo
LEFT JOIN App_WorkOrder_Reg R
    ON c3.WO_NO = R.WO_NO
CROSS JOIN WorkDays wds
WHERE c3.status = 'Approved'
  AND l.FromDate < '2025-12-31'
  AND l.ToDate >= '2025-07-01'
  AND l.VCode = '15235'
  AND l.WorkLocation IN ('Jamshedpur', 'Seraikela')
ORDER BY l.LicNo
OPTION (MAXRECURSION 0);
