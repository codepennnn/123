@DeptName = 'Administration & Event Management,Angul Business'

SELECT DISTINCT Ema_perno FROM SAPHRDB.dbo.T_Empl_All WHERE ema_dept_desc = @DeptName ORDER BY Ema_perno
