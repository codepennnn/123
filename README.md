SELECT  
    ID,
    LTRIM(RTRIM(value)) AS Remarks
FROM App_Half_Yearly_Details
CROSS APPLY STRING_SPLIT(
    REPLACE(Remarks, '||', '|'),
    '|'
)
WHERE Id = '053EAF4D-044D-4F61-BD71-28F6D01AF04D'
AND LTRIM(RTRIM(value)) <> '';
