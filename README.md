



      select WorkManSl,WorkManName,EngagementType,DayDef,Present,

        SUM(TotalPresent present  that is DayDef 'WD' and present 1) As Present ,

        SUM(TotalLeave that is DayDef 'LV' ) As Leave,

        SUM(totalHoliday that is DayDef 'HD' ) As Holiday,
      
      from App_AttendanceDetails where 
      
      VendorCode='17201' and datepart(month,dates)='10' and datepart(year,dates)='2024' and AadharNo='275225445020'


----
    Select '10' as month,'2024' as year,convert(varchar,datepart(d, ML.Dates) )as Dates,
   ML.EngagementType,
   ad.WorkManSl as WorkManSLNo,
   ad.WorkManName as WorkManName, 
   ad.DayDef as DayDef, 
   case when( ML.EngagementType = AD.EngagementType and Present = 'True')
   then Convert(varchar,1) else Convert(varchar,0) end as Present ,
   ad.EngagementType as Eng_Type from dbo.ListOfDaysByEngagementType('10','2024')
   as ML
   left join 
   
   App_AttendanceDetails as ad on ML.Dates = AD.Dates
   where AD.VendorCode = '17201'  and DATEPART(month, AD.Dates)= '10' and DATEPART(year, AD.Dates)= '2024'  and AadharNo='275225445020' 
   group by ML.Dates,ML.EngagementType,AD.EngagementType,AD.WorkManSl,ad.WorkManName,AD.Present,AD.WorkOrderNo,ad.DayDef order by ML.Dates,AD.WorkManSl  

 
 -----------------
 
 string str_exit = ds.Tables[0].Rows[j]["Date_of_Exit"].ToString();
                    string str_notice = ds.Tables[0].Rows[j]["Date_of_Notice"].ToString();


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
