SELECT *
FROM App_FNF_Compliance
WHERE wo_no IN (
    SELECT wo_no
    FROM App_FNF_Compliance
    GROUP BY wo_no
    HAVING COUNT(*) > 1
)
ORDER BY wo_no;
