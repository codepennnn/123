SELECT 
    ID,
    Remarks,
    Attachment,
    CreatedOn,
    CreatedBy,
    CASE 
        WHEN CreatedBy LIKE '[0-9][0-9][0-9][0-9][0-9]' 
            THEN 'Response by Vendor'
        ELSE 'Action by TSUISL'
    END AS Communicator
FROM App_WorkOrder_Exemption_Remarks
WHERE MASTER_ID = '35608F1E-8F35-4CC9-AD1B-3382707B9616'
ORDER BY CreatedOn DESC;
