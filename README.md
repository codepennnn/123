DECLARE @DeptName NVARCHAR(MAX) = 'Administration & Event Management,Angul Business';

SELECT DISTINCT Ema_perno
FROM SAPHRDB.dbo.T_Empl_All
WHERE ema_dept_desc IN (
    SELECT value FROM STRING_SPLIT(@DeptName, ',')
)
ORDER BY Ema_perno;
