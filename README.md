 public void UserPermission()
 {
     string start = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    // if (System.DateTime.Now.ToString("dddd") == "Monday"  || System.DateTime.Now.ToString("dddd") == "Friday")     //Monday or Friday
    // {
         

         
             List<string> formIds = new List<string>();
             DataSet Ds_USerId = new DataSet();

             using (SqlConnection con = new SqlConnection("Data Source=10.0.168.50;Initial Catalog=CLMSDB;User ID=fs;Password=p@ssW0Rd321"))
            // using (SqlConnection con = new SqlConnection("Data Source=10.0.168.30;Initial Catalog=CLMSDB;User ID=fs;Password=jusco@123"))
             {
                 con.Open();

             //--------Asif

             string deleteQuery = @"Delete p from App_UserFormPermission p
                                 join App_ApplicationForms f on p.FormId = f.Id
                                 join UserLoginDB.dbo.aspnet_Users u on u.UserId = p.UserId
                                 where 
                                     f.Description like 'V - %'
                                     and len(u.UserName) = 5
                                     and isnumeric(u.LoweredUserName) = 1
                                     and ( p.AllowRead = 0 or p.AllowWrite = 0 or p.AllowModify = 0 );";

             using (SqlCommand delCmd = new SqlCommand(deleteQuery, con))
             {
                 delCmd.ExecuteNonQuery();
             }





             //-------------jyoti

             string FormIdQuery = @"select Id from App_ApplicationForms where Description like 'V - %' order by Id ";
                 using (SqlCommand cmdFormId = new SqlCommand(FormIdQuery, con))
                 {
                     using (SqlDataReader dr = cmdFormId.ExecuteReader())
                     {
                         while (dr.Read())
                         {
                             formIds.Add(dr["Id"].ToString());
                         }

                     }
                 }

                 string formId = "";
                 string query = "";
                 string userId = "";
                 string insertQuery = "";

                 for (int i = 0; i < formIds.Count; i++) //formIds.Count
                 {
                     formId = formIds[i];
                     query = @"select  * from UserLoginDB.dbo.aspnet_Users where UserId not in (select distinct UserId from App_UserFormPermission where FormId=@FormId) and len(UserName) =5 and ISNUMERIC(LoweredUserName)=1";
                   

                     using (SqlCommand cmd = new SqlCommand(query, con))
                     {
                         cmd.Parameters.AddWithValue("@FormId", formId);
                         SqlDataAdapter da = new SqlDataAdapter(cmd);
                         Ds_USerId.Clear();
                         da.Fill(Ds_USerId);
                     }


                     if (Ds_USerId.Tables.Count > 0 && Ds_USerId.Tables[0].Rows.Count > 0)
                     {
                         for (int j = 0; j < Ds_USerId.Tables[0].Rows.Count; j++)
                         {
                             userId = Ds_USerId.Tables[0].Rows[j]["UserId"].ToString();
                             insertQuery = @"insert into App_UserFormPermission (userID,FormId,AllowRead,AllowWrite,AllowDelete,AllowAll,AllowModify) values(@UserId,@FormId,1,1,0,0,1)";

                             using (SqlCommand insertcmd = new SqlCommand(insertQuery, con))
                             {
                                 insertcmd.Parameters.AddWithValue("@FormId", formId);
                                 insertcmd.Parameters.AddWithValue("@UserId", userId);
                                 SqlDataAdapter da = new SqlDataAdapter(insertcmd);
                                 insertcmd.ExecuteNonQuery();
                             }

                         }
                     }

                     Ds_USerId.Clear();
                 }
                 con.Close();
             }
         

       
        
    // }
     string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
     string status = "User permissions updated successfully on vendor side.";    //vendor user permissions have been succefully updated.
     string Task = "Vendor User Permission";
     Tracelog(start, EndTime, Task, status);
 }


 protected override void OnStop()
 {

 }

 public void Tracelog(string start, string EndTime, string Task, string status)
 {
     string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    SqlConnection con3 = new SqlConnection("Data Source=10.0.168.50;Initial Catalog=CLMSDB;User ID=fs;Password=p@ssW0Rd321");
     //SqlConnection con3 = new SqlConnection("Data Source=10.0.168.30;Initial Catalog=CLMSDB;User ID=fs;Password=jusco@123");
     con3.Open();
     String s1 = "Insert into App_JUSCOTASKDETAILS (UPDATEDAT,UPDATEDBY,STARTTIME,ENDTIME,TASKNAME,STATUS) " +
                 "values('" + now + "',000001,'" + start + "','" + EndTime + "','" + Task + "','" + status + "')";

     SqlCommand cmd = new SqlCommand(s1, con3);
     cmd.ExecuteNonQuery();
     con3.Close();
 }
