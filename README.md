WITH AttWO AS (
    SELECT DISTINCT
        att.WorkOrderNo AS wo_no,
        w.LOC_OF_WORK   AS LOC_CODE,
        w.Engagement_Type AS EngagementType
    FROM App_AttendanceDetails att
    LEFT JOIN app_workorder_reg w ON w.WO_NO = att.WorkOrderNo
    WHERE att.Dates >= '3/1/2025 12:00:00' AND att.Dates < '4/1/2025 12:00:00'
      AND att.VendorCode = '17201' and att.WorkOrderNo is not null
),
C3WO AS (
    SELECT DISTINCT
        dtl.WO_NO       AS wo_no,
        w2.LOC_OF_WORK  AS LOC_CODE,
        w2.Engagement_Type AS EngagementType
    FROM App_Vendor_form_C3_Dtl dtl
    LEFT JOIN app_workorder_reg w2 ON w2.WO_NO = dtl.WO_NO
    WHERE dtl.C3_CLOSER_DATE >= '3/1/2025 12:00:00' and dtl.WO_NO is not null
      AND dtl.V_CODE = '17201'
      AND NOT EXISTS (
            SELECT 1
            FROM App_Wo_Nil n
            WHERE n.WO_NO = dtl.WO_NO
              AND n.TEMPORARY_MONTH = '03'
              AND n.TEMPORARY_YEAR  = '2025'
              AND n.NO_WORK = 'Temporary'
      )
      AND NOT EXISTS (
            SELECT 1
            FROM App_Wo_Nil n2
            WHERE n2.WO_NO = dtl.WO_NO
              AND n2.NO_WORK = 'Permanent'
              AND (CAST(n2.TEMPORARY_YEAR AS INT) * 100
                   + CASE WHEN TRY_CAST(n2.CLOSER_DATE AS INT) IS NULL
                          THEN 0 ELSE TRY_CAST(n2.CLOSER_DATE AS INT) END) <= '202503'
      )
)
SELECT wo_no, LOC_CODE, EngagementType FROM AttWO
UNION
SELECT wo_no, LOC_CODE, EngagementType FROM C3WO;

some engagment_type is coming null because of no data so i want if null then default show - Manpower Supply
