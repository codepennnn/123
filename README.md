WITH AttWO AS (
    SELECT DISTINCT
        att.WorkOrderNo AS wo_no,
        w.LOC_OF_WORK   AS LOC_CODE,
        w.Engagement_Type AS EngagementType
    FROM App_AttendanceDetails att
    LEFT JOIN app_workorder_reg w 
        ON w.WO_NO = att.WorkOrderNo
    WHERE att.Dates >= '2025-03-01'
      AND att.Dates <  '2025-04-01'
      AND att.VendorCode = '17201'
      AND att.WorkOrderNo IS NOT NULL
),
C3WO AS (
    SELECT DISTINCT
        dtl.WO_NO AS wo_no,
        w2.LOC_OF_WORK AS LOC_CODE,
        w2.Engagement_Type AS EngagementType
    FROM App_Vendor_form_C3_Dtl dtl
    LEFT JOIN app_workorder_reg w2 
        ON w2.WO_NO = dtl.WO_NO
    WHERE dtl.C3_CLOSER_DATE >= '2025-03-01'
      AND dtl.WO_NO IS NOT NULL
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
              AND (
                    CAST(n2.TEMPORARY_YEAR AS INT) * 100
                    + ISNULL(TRY_CAST(n2.CLOSER_DATE AS INT), 0)
                  ) <= 202503
      )
)

SELECT
    wo_no,
    LOC_CODE,
    ISNULL(EngagementType, 'Manpower Supply') AS EngagementType
FROM (
    SELECT wo_no, LOC_CODE, EngagementType FROM AttWO
    UNION
    SELECT wo_no, LOC_CODE, EngagementType FROM C3WO
) X;
