SELECT wo_no,
       LOC_CODE,
       ISNULL(EngagementType, 'Manpower Supply') AS EngagementType
FROM (
    SELECT wo_no, LOC_CODE, EngagementType FROM AttWO
    UNION
    SELECT wo_no, LOC_CODE, EngagementType FROM C3WO
) x;
