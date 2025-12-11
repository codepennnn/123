SELECT top 1
    WorkManCategory,
    MAX(BasicRate) + MAX(DARate) AS Rate
        
FROM App_WagesDetailsJharkhand
WHERE YearWage = '2024'
  AND MonthWage = (
        SELECT DISTINCT TOP (1) MonthWage
        FROM App_WagesDetailsJharkhand
        WHERE WorkOrderNo IN ('4700020228','4700022348','4700017345','4700020508','4700019066','4700020215')
          AND VendorCode = '10517'
          AND MonthWage IN (1,2,3,4,5,6)
          AND YearWage = '2024'
        ORDER BY MonthWage DESC
    )
  AND WorkOrderNo IN ('4700020228','4700022348','4700017345','4700020508','4700019066','4700020215')
  AND VendorCode = '10517'
GROUP BY WorkManCategory
ORDER BY
    CASE
        WHEN WorkManCategory = 'Unskilled'      THEN 1
        WHEN WorkManCategory = 'Semi Skilled'   THEN 2
        WHEN WorkManCategory = 'Skilled'        THEN 3
        WHEN WorkManCategory = 'Highly Skilled' THEN 4
        ELSE 5
        end
