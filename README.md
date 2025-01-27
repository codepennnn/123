DECLARE @DynamicColumns NVARCHAR(MAX); 
DECLARE @SQLQuery NVARCHAR(MAX);

-- Generate the dynamic columns list
SELECT @DynamicColumns = STRING_AGG(QUOTENAME(DayOfMonth), ',') 
FROM ( 
    SELECT DISTINCT DATEPART(DAY, ML.Dates) AS DayOfMonth 
    FROM dbo.ListOfDaysByEngagementType('09', '2024') AS ML 
    LEFT JOIN App_AttendanceDetails AS ad ON ML.Dates = AD.Dates 
    WHERE AD.VendorCode = '17201' 
    AND DATEPART(MONTH, AD.Dates) = '09' 
    AND DATEPART(YEAR, AD.Dates) = '2024' 
) AS Days;

-- Create the temporary table for pivoted data
SET @SQLQuery = '
IF OBJECT_ID(''tempdb..#PivotTable'') IS NOT NULL
    DROP TABLE #PivotTable;

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
        DATEPART(YEAR, ML.Dates) AS Year,
        ad.DayDef
    FROM dbo.ListOfDaysByEngagementType(''09'', ''2024'') AS ML 
    LEFT JOIN App_AttendanceDetails AS ad ON ML.Dates = AD.Dates 
    WHERE AD.VendorCode = ''17201'' 
    AND DATEPART(MONTH, AD.Dates) = ''09'' 
    AND DATEPART(YEAR, AD.Dates) = ''2024''
)

-- Insert pivoted data into temporary table
SELECT 
    WorkManSLNo, 
    WorkManName, 
    Eng_Type, 
    Month, 
    Year, 
    ' + @DynamicColumns + '
INTO #PivotTable
FROM AttendanceData
PIVOT ( 
    MAX(Present) FOR DayOfMonth IN (' + @DynamicColumns + ') 
) AS PivotTable;

-- Final select with additional columns
SELECT 
    WorkManSLNo, 
    WorkManName, 
    Eng_Type, 
    Month, 
    Year, 
    ' + @DynamicColumns + ', 
    SUM(CASE WHEN Present = ''P'' THEN 1 ELSE 0 END) AS TotalPresent,
    SUM(CASE WHEN DayDef = ''HD'' THEN 1 ELSE 0 END) AS TotalHoliday,
    SUM(CASE WHEN DayDef = ''LV'' THEN 1 ELSE 0 END) AS Leave
FROM #PivotTable
GROUP BY WorkManSLNo, WorkManName, Eng_Type, Month, Year
ORDER BY WorkManSLNo;
';

-- Execute the dynamic SQL query
EXEC sp_executesql @SQLQuery;
