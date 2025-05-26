DECLARE @VendorCode NVARCHAR(50) = '17201';
DECLARE @WorkOrder NVARCHAR(50) = '4700321';

SELECT TOP 1
    VendorCode,
    WorkOrderNo,
    CASE 
        WHEN DATEDIFF(DAY, Approved_On, GETDATE()) <= Exemption_CC THEN 'YES'
        ELSE 'NO'
    END AS IsExemption
FROM App_WorkOrder_Exemption
WHERE VendorCode = @VendorCode
  AND Status = 'Approved'
  AND (
        WorkOrderNo = @WorkOrder
        OR WorkOrderNo LIKE @WorkOrder + ',%'
        OR WorkOrderNo LIKE '%,' + @WorkOrder + ',%'
        OR WorkOrderNo LIKE '%,' + @WorkOrder
      )
ORDER BY Approved_On DESC;
