    protected void ddlAadhar_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtName.Text = "";
        txtSlNo.Text = "";
        txtCategory.Text = "";
        txtVname.Text = "";
        txtVcode.Text = "";

        if (string.IsNullOrEmpty(ddlAadhar.SelectedValue))
            return;

        string aadhar = ddlAadhar.SelectedValue;

        using (var cn = new SqlConnection(_connString))
        using (var cmd = new SqlCommand(
            @"SELECT Name, WorkManSlNo, WorkManCategory,VendorName,VendorCode 
      FROM App_EmployeeMaster 
      WHERE AadharCard = @Aadhar", cn))
        {
            cmd.Parameters.AddWithValue("@Aadhar", aadhar);
            cn.Open();
            using (var dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    txtName.Text = dr["Name"].ToString();
                    txtSlNo.Text = dr["WorkManSlNo"].ToString();
                    txtCategory.Text = dr["WorkManCategory"].ToString();
                    txtVname.Text = dr["VendorName"].ToString();
                    txtVcode.Text = dr["VendorCode"].ToString();
                }
            }
        }
    }
