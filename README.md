string deleteQuery = @"
DELETE p
FROM App_UserFormPermission p
JOIN App_ApplicationForms f ON p.FormId = f.Id
JOIN UserLoginDB.dbo.aspnet_Users u ON u.UserId = p.UserId
WHERE 
    f.Description LIKE 'V - %'
    AND LEN(u.UserName) = 5
    AND ISNUMERIC(u.LoweredUserName) = 1
    AND p.AllowRead = 0
    AND p.AllowWrite = 0
    AND p.AllowModify = 0;
";

using (SqlCommand delCmd = new SqlCommand(deleteQuery, con))
{
    delCmd.ExecuteNonQuery();
}
