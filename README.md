SELECT 
    e.Pno,
    e.Name,
    ep.Position,
    STRING_AGG(l.Work_Site, ', ') AS Worksites
FROM App_Empl_Master e
LEFT JOIN App_Emp_Position ep ON e.Pno = ep.Pno
LEFT JOIN App_Position_Worksite pw ON ep.Position = pw.Position
OUTER APPLY (
    SELECT l.Work_Site
    FROM STRING_SPLIT(pw.Worksite, ',') AS split
    JOIN App_LocationMaster l ON TRY_CAST(split.value AS INT) = l.ID
) AS sites
WHERE e.DepartmentName = @DeptName
GROUP BY e.Pno, e.Name, ep.Position
ORDER BY e.Pno
