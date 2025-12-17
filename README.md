protected void btnSave_Click(object sender, EventArgs e)
{
    litMessage.Text = "";

    if (gvAttendance.Rows.Count == 0)
    {
        litMessage.Text = "<div class='message err'>No attendance rows to save.</div>";
        return;
    }

    int month = int.Parse(ddlMonth.SelectedValue);
    int year = int.Parse(ddlYear.SelectedValue);

    DateTime first = new DateTime(year, month, 1);
    int days = DateTime.DaysInMonth(year, month);
    DateTime last = new DateTime(year, month, days);

    string selectedAadhar = ddlAadhar.SelectedValue;

    /* =========================================================
       YOUR SAVE LOGIC
       (UNCHANGED – exactly as you already have)
       ========================================================= */

    // ... all validation code
    // ... all insert/update code
    // ... transaction commit

    /* =========================================================
       AFTER SAVE – RELOAD EXISTING ATTENDANCE
       ========================================================= */

    if (!string.IsNullOrEmpty(selectedAadhar))
        ViewState["ExistingAttendance"] =
            LoadExistingAttendance(first, last, selectedAadhar);
    else
        ViewState["ExistingAttendance"] = null;

    /* =========================================================
       FETCH DOJ & DOE (IMPORTANT)
       ========================================================= */

    DateTime? doj = null;
    DateTime? doe = null;

    using (var cn = new SqlConnection(_connString))
    using (var cmd = new SqlCommand(
        @"SELECT DOJ, DOE
          FROM App_EmployeeMaster
          WHERE AadharCard = @Aadhar
            AND VendorCode = @VendorCode", cn))
    {
        cmd.Parameters.AddWithValue("@Aadhar", selectedAadhar);
        cmd.Parameters.AddWithValue("@VendorCode", Session["UserName"].ToString());

        cn.Open();
        using (var dr = cmd.ExecuteReader())
        {
            if (dr.Read())
            {
                if (dr["DOJ"] != DBNull.Value)
                    doj = Convert.ToDateTime(dr["DOJ"]).Date;

                if (dr["DOE"] != DBNull.Value)
                    doe = Convert.ToDateTime(dr["DOE"]).Date;
            }
        }
    }

    /* =========================================================
       🔴 OLD WRONG CODE (THIS CAUSED FULL MONTH)
       =========================================================
       
    DataTable dt = new DataTable();
    dt.Columns.Add("Date", typeof(DateTime));

    for (int d = 0; d < days; d++)
    {
        DateTime curr = first.AddDays(d);

        if (doj.HasValue && curr < doj.Value)
            continue;

        DataRow r = dt.NewRow();
        r["Date"] = curr;
        dt.Rows.Add(r);
    }
    */

    /* =========================================================
       ✅ NEW CORRECT CODE (DOJ + DOE APPLIED)
       ========================================================= */

    DataTable dt = new DataTable();
    dt.Columns.Add("Date", typeof(DateTime));

    for (int d = 0; d < days; d++)
    {
        DateTime curr = first.AddDays(d);

        // before DOJ → skip
        if (doj.HasValue && curr < doj.Value)
            continue;

        // after DOE → skip (DOE DAY INCLUDED)
        if (doe.HasValue && curr > doe.Value)
            continue;

        DataRow r = dt.NewRow();
        r["Date"] = curr;
        dt.Rows.Add(r);
    }

    gvAttendance.DataSource = dt;
    gvAttendance.DataBind();
    pnlGrid.Visible = true;

    lblRowCount.Text = dt.Rows.Count.ToString();
}
