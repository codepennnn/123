        protected void btnView_Click(object sender, EventArgs e)
        {
            LoadLookups_emp_wo();

            litMessage.Text = "";
            int month = int.Parse(ddlMonth.SelectedValue);
            int year = int.Parse(ddlYear.SelectedValue);

            DateTime first = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            DateTime last = new DateTime(year, month, days);

            // Reload existing attendance if aadhar selected
            string selectedAadhar = ddlAadhar.SelectedValue;
            if (!string.IsNullOrEmpty(selectedAadhar))
            {
                var existing = LoadExistingAttendance(first, last, selectedAadhar);
                ViewState["ExistingAttendance"] = existing;
                if (existing.Rows.Count > 0)
                    litMessage.Text = $"<div class='message'>Loaded existing attendance for {selectedAadhar} ({existing.Rows.Count} rows).</div>";
                else
                    litMessage.Text = "<div class='message'>No existing attendance found for selected Aadhar; showing blank month.</div>";
            }
            else
            {
                ViewState["ExistingAttendance"] = null;
            }

            // --- Determine DOJ for selected Aadhar (nullable)
            DateTime? doj = null;

            if (!string.IsNullOrEmpty(selectedAadhar))
            {
                var dtEmployees = ViewState["Employees"] as DataTable;
                if (dtEmployees != null && dtEmployees.Columns.Contains("DOJ"))
                {
                    // safe select from the cached lookup table
                    DataRow[] rows = dtEmployees.Select($"AadharCard = '{selectedAadhar.Replace("'", "''")}'");
                    if (rows.Length > 0 && rows[0]["DOJ"] != DBNull.Value)
                    {
                        doj = Convert.ToDateTime(rows[0]["DOJ"]).Date;
                    }
                }

                // fallback: if DOJ not found in ViewState, query DB for this single aadhar
                if (!doj.HasValue)
                {
                    using (var cn = new SqlConnection(_connString))
                    using (var cmd = new SqlCommand("SELECT DOJ FROM App_EmployeeMaster WHERE AadharCard = @Aadhar", cn))
                    {
                        cmd.Parameters.AddWithValue("@Aadhar", selectedAadhar);
                        cn.Open();
                        var obj = cmd.ExecuteScalar();
                        if (obj != null && obj != DBNull.Value)
                        {
                            doj = Convert.ToDateTime(obj).Date;
                        }
                    }
                }
            }

            // Build and bind date rows, skipping dates before DOJ (if DOJ known)
            DataTable dt = new DataTable();
            dt.Columns.Add("Date", typeof(DateTime));
            for (int d = 0; d < days; d++)
            {
                DateTime currentDate = first.AddDays(d);

                // Skip dates BEFORE DOJ (if doj known)
                if (doj.HasValue && currentDate < doj.Value)
                    continue;

                var r = dt.NewRow();
                r["Date"] = currentDate;
                dt.Rows.Add(r);
            }

            gvAttendance.DataSource = dt;
            gvAttendance.DataBind();
            pnlGrid.Visible = true;
        }



Dates should appear based on DOJ and DOE after Aadhaar selection:
Before DOJ, dates should not appear.
After DOE, dates should not appear.

here is my table select DOJ,DOE,apprvstatus from App_EmployeeMaster where aadharcard='917543928910' and vendorcode='17201' 
