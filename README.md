SELECT SUM(ISNULL(TotPaymentDays,0)) AS total
FROM App_WagesDetailsJharkhand
WHERE VendorCode = '15235'
  AND MonthWage IN (7,8,9,10,11,12)
  AND YearWage = '2025';




SELECT w.AadharNo,w.TotPaymentDays,w.monthwage
FROM App_WagesDetailsJharkhand w
JOIN App_EmployeeMaster em
  ON w.AadharNo = em.AadharCard AND em.Sex = 'M'
WHERE w.VendorCode = '15235'
  AND w.MonthWage IN (7,8,9,10,11,12)
  AND w.YearWage = '2025' order by AadharNo;
