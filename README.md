ISNULL(
    CASE 
        WHEN EngagementType = 'Manpower Supply' THEN 'ManPowerSupply'
        WHEN EngagementType = 'Item Rate' THEN 'ItemRate'
        ELSE EngagementType
    END,
    'ManPowerSupply'
) AS EngagementType
