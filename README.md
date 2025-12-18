if (dt.Rows.Count == 0)
{
    gvAttendance.DataSource = null;
    gvAttendance.DataBind();
    pnlGrid.Visible = false;

    lblRowCount.Text = "0";
    litMessage.Text =
        "<div class='message err'>No attendance dates available for this workman in the selected month (DOJ / DOE restriction).</div>";
    return;
}




protected void ddlAadhar_SelectedIndexChanged(object sender, EventArgs e)
{
    // Clear text fields
    txtName.Text = "";
    txtSlNo.Text = "";
    txtCategory.Text = "";
    txtVname.Text = "";
    txtVcode.Text = "";

    // Clear grid + state
    gvAttendance.DataSource = null;
    gvAttendance.DataBind();
    pnlGrid.Visible = false;

    lblRowCount.Text = "0";
    litMessage.Text = "";

    ViewState["ExistingAttendance"] = null;

    // If nothing selected, stop here
    if (string.IsNullOrEmpty(ddlAadhar.SelectedValue))
        return;

    string aadhar = ddlAadhar.SelectedValue;

    // Reload employee details
    using (var cn = new SqlConnection(_connString))
    using (var cmd = new SqlCommand(
        @"SELECT Name, WorkManSlNo, WorkManCategory, VendorName, VendorCode
          FROM App_EmployeeMaster
          WHERE AadharCard = @Aadhar
            AND VendorCode = @VendorCode", cn))
    {
        cmd.Parameters.AddWithValue("@Aadhar", aadhar);
        cmd.Parameters.AddWithValue("@VendorCode", Session["UserName"].ToString());

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

