DECLARE @DynamicColumns NVARCHAR(MAX); 
DECLARE @SQLQuery NVARCHAR(MAX);

-- Generate dynamic column names (DayOfMonth)
SELECT @DynamicColumns = STRING_AGG(QUOTENAME(DayOfMonth), ',') 
FROM ( 
    SELECT DISTINCT DATEPART(DAY, ML.Dates) AS DayOfMonth 
    FROM dbo.ListOfDaysByEngagementType('09', '2024') AS ML 
    LEFT JOIN App_AttendanceDetails AS ad ON ML.Dates = AD.Dates 
    WHERE AD.VendorCode = '17201' 
    AND DATEPART(MONTH, AD.Dates) = '09' 
    AND DATEPART(YEAR, AD.Dates) = '2024' 
) AS Days;

-- Create the dynamic SQL query
SET @SQLQuery = '
WITH AttendanceData AS (
    SELECT 
        DATEPART(DAY, ML.Dates) AS DayOfMonth, 
        ad.WorkManSl AS WorkManSLNo, 
        ad.WorkManName AS WorkManName,
        CASE 
            WHEN (ML.EngagementType = AD.EngagementType AND Present = ''True'') THEN ''P'' 
            ELSE ''A'' 
        END AS Present, 
        ad.EngagementType AS Eng_Type, 
        DATEPART(MONTH, ML.Dates) AS Month,
        DATEPART(YEAR, ML.Dates) AS Year 
    FROM dbo.ListOfDaysByEngagementType(''09'', ''2024'') AS ML 
    LEFT JOIN App_AttendanceDetails AS ad ON ML.Dates = AD.Dates 
    WHERE AD.VendorCode = ''17201'' 
    AND DATEPART(MONTH, AD.Dates) = ''09'' 
    AND DATEPART(YEAR, AD.Dates) = ''2024''
)

-- Final SELECT statement with the dynamic PIVOT
SELECT 
    WorkManSLNo, 
    WorkManName, 
    Eng_Type, 
    Month, 
    Year, 
    ' + @DynamicColumns + ', 
    -- Calculating total present
    ( 
        SELECT COUNT(*) 
        FROM AttendanceData AS ad2
        WHERE ad2.WorkManSLNo = ad1.WorkManSLNo
        AND ad2.Present = ''P''
    ) AS totalpresent
FROM AttendanceData AS ad1
PIVOT ( 
    MAX(Present) FOR DayOfMonth IN (' + @DynamicColumns + ') 
) AS PivotTable
ORDER BY WorkManSLNo;
';

-- Execute the dynamic SQL query
EXEC sp_executeSQL @SQLQuery;
