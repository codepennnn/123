select * from App_UserFormPermission where UserID in (select distinct UserId from userlogindb.dbo.aspnet_Users where len(username)=5)
and FormId='800D2C60-BD48-4094-AB40-9D6431303360' AND AllowRead=0 
