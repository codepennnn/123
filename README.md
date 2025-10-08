SELECT *
FROM App_UserFormPermission
WHERE UserID IN (
    SELECT DISTINCT UserId
    FROM userlogindb.dbo.aspnet_Users
    WHERE LEN(UserName) = 5
      AND UserName NOT LIKE '%[^0-9]%'  -- ensures only digits
)
AND FormId = '800D2C60-BD48-4094-AB40-9D6431303360'
AND AllowRead = 0;
