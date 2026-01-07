insertCount += insertcmd.ExecuteNonQuery();

using (SqlCommand delCmd = new SqlCommand(deleteQuery, con))
{
    deleteCount = delCmd.ExecuteNonQuery();
}



int deleteCount = 0;
int insertCount = 0;


string status = "User permissions updated successfully on vendor side. " +
                "Inserted: " + insertCount + 
                ", Deleted: " + deleteCount;
