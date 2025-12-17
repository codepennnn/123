protected void btnView_Click(object sender, EventArgs e)
{
    // Load lookup data (work orders, locations, etc.)
    LoadLookups_emp_wo();

    litMessage.Text = "";

    int month = int.Parse(ddlMonth.SelectedValue);
    int year = int.Parse(ddlYear.SelectedValue);

    DateTime first = new DateTime(year, month, 1);
    int days = DateTime.DaysInMonth(year, month);
    DateTime last = new DateTime(year, month, days);

    string selectedAadhar = ddlAadhar.SelectedValue;

    // -------------------------------------------------
    // Load existing attendance (if Aadhaar selected)
    // -------------------------------------------------
    if (!string.IsNullOrEmpty(selectedAadhar))
    {
        var existing = LoadExistingAttendance(first, last, selectedAadhar);
        ViewState["ExistingAttendance"] = existing;

        if (existing.Rows.Count > 0)
            litMessage.Text = $"<div class='message'>Loaded existing attendance for {selectedAadhar} ({existing.Rows.Count} rows).</div>";
        else
            litMessage.Text = "<div class='message'>No existing attendance found; showing blank month.</div>";
    }
    else
    {
        ViewState["ExistingAttendance"] = null;
    }

    // -------------------------------------------------
    // Determine DOJ and DOE
    // -------------------------------------------------
    DateTime? doj = null;
    DateTime? doe = null;

    if (!string.IsNullOrEmpty(selectedAadhar))
    {
        // First try from ViewState Employees
        var dtEmployees = ViewState["Employees"] as DataTable;

        if (dtEmployees != null)
        {
            DataRow[] rows = dtEmployees.Select(
                $"AadharCard = '{selectedAadhar.Replace("'", "''")}'");

            if (rows.Length > 0)
            {
                if (rows[0]["DOJ"] != DBNull.Value)
                    doj = Convert.ToDateTime(rows[0]["DOJ"]).Date;

                if (dtEmployees.Columns.Contains("DOE") &&
                    rows[0]["DOE"] != DBNull.Value)
                    doe = Convert.ToDateTime(rows[0]["DOE"]).Date;
            }
        }

        // Fallback: fetch from DB if missing
        if (!doj.HasValue || !doe.HasValue)
        {
            using (var cn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(
                @"SELECT DOJ, DOE 
                  FROM App_EmployeeMaster 
                  WHERE AadharCard = @Aadhar", cn))
            {
                cmd.Parameters.AddWithValue("@Aadhar", selectedAadhar);
                cn.Open();

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        if (!doj.HasValue && dr["DOJ"] != DBNull.Value)
                            doj = Convert.ToDateTime(dr["DOJ"]).Date;

                        if (!doe.HasValue && dr["DOE"] != DBNull.Value)
                            doe = Convert.ToDateTime(dr["DOE"]).Date;
                    }
                }
            }
        }
    }

    // -------------------------------------------------
    // Build date rows respecting DOJ and DOE
    // -------------------------------------------------
    DataTable dt = new DataTable();
    dt.Columns.Add("Date", typeof(DateTime));

    for (int d = 0; d < days; d++)
    {
        DateTime currentDate = first.AddDays(d);

        // Skip before DOJ
        if (doj.HasValue && currentDate < doj.Value)
            continue;

        // Skip after DOE
        if (doe.HasValue && currentDate > doe.Value)
            continue;

        DataRow r = dt.NewRow();
        r["Date"] = currentDate;
        dt.Rows.Add(r);
    }

    // -------------------------------------------------
    // Bind grid
    // -------------------------------------------------
    gvAttendance.DataSource = dt;
    gvAttendance.DataBind();
    pnlGrid.Visible = true;
}
