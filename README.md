        protected void btnSave_Click(object sender, EventArgs e)
        {
            litMessage.Text = "";
            if (gvAttendance.Rows.Count == 0)
            {
                litMessage.Text = "<div class='message err'>No attendance rows to save. Click View first.</div>";
                return;
            }

            // ----------------------------------------------------
            // Build submission DataTable (used for validations)
            // ----------------------------------------------------
            DataTable submission = new DataTable();
            submission.Columns.Add("Date", typeof(DateTime));
            submission.Columns.Add("Aadhar", typeof(string));
            submission.Columns.Add("IsPresent", typeof(bool));

            DateTime minDate = DateTime.MaxValue;
            DateTime maxDate = DateTime.MinValue;

            foreach (GridViewRow row in gvAttendance.Rows)
            {
                //var lblDate = row.FindControl("lblDate") as Label;
                var hfDate = row.FindControl("hfDateIso") as HiddenField;

                var lblAadhar = row.FindControl("lblRowAadhar") as Label;
                var chkPresent = row.FindControl("chkPresent") as CheckBox;

                //if (lblDate == null) continue;

                //DateTime date;
                //if (!DateTime.TryParse(lblDate.Text, out date)) continue;

                if (hfDate == null) continue;

                DateTime date;
                if (!DateTime.TryParseExact(hfDate.Value, "yyyy-MM-dd",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                    continue;




                string aadhar = lblAadhar?.Text?.Trim() ?? "";
                bool isPresent = chkPresent?.Checked ?? false;

                if (string.IsNullOrEmpty(aadhar)) continue;

                var r = submission.NewRow();
                r["Date"] = date.Date;
                r["Aadhar"] = aadhar;
                r["IsPresent"] = isPresent;
                submission.Rows.Add(r);

                if (date < minDate) minDate = date;
                if (date > maxDate) maxDate = date;
            }

            // ----------------------------------------------------
            // DOJ validation
            // ----------------------------------------------------
            string dojError = ValidateDojRestrictions(submission);
            if (!string.IsNullOrEmpty(dojError))
            {
                string jsMsg = System.Web.HttpUtility.JavaScriptStringEncode(dojError);
                string script = $"showValidationModal('{jsMsg}');";

                if (ScriptManager.GetCurrent(this.Page) != null)
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showDojModal", script, true);
                else
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "showDojModal", script, true);

                litMessage.Text = "<div class='message err'>DOJ validation failed — see details in dialog.</div>";
                return;
            }

            // ----------------------------------------------------
            // 9-day consecutive present rule
            // ----------------------------------------------------
            string validationError = ValidateConsecutivePresentLimit(submission);
            if (!string.IsNullOrEmpty(validationError))
            {
                string jsMsg = System.Web.HttpUtility.JavaScriptStringEncode(validationError);
                string script = $"showValidationModal('{jsMsg}');";

                if (ScriptManager.GetCurrent(this.Page) != null)
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showValModal", script, true);
                else
                    ClientScript.RegisterStartupScript(this.Page.GetType(), "showValModal", script, true);

                litMessage.Text = "<div class='message err'>Validation failed — see details in the dialog.</div>";
                return;
            }

            // ----------------------------------------------------
            // SAVE (UPSERT)
            // ----------------------------------------------------

            var shiftParts = new Dictionary<string, (int InHr, int InMin, int OutHr, int OutMin)>(StringComparer.OrdinalIgnoreCase);
            using (var cn = new SqlConnection(_connString))
            {  cn.Open(); 
                // --- Prefetch shift hour/min parts (one DB read, no UI changes) ---
                

            // If Intime/Outtime are VARCHAR with a dot (e.g., '8.50'), use this:
            string shiftPartsSql = @"
SELECT
    Shift,
    CAST(LEFT(Intime, CHARINDEX('.', Intime) - 1) AS INT) AS InTimeHr,
    CAST(RIGHT('00' + RIGHT(Intime, LEN(Intime) - CHARINDEX('.', Intime)), 2) AS INT) AS InTimeMin,
    CAST(LEFT(Outtime, CHARINDEX('.', Outtime) - 1) AS INT) AS OutTimeHr,
    CAST(RIGHT('00' + RIGHT(Outtime, LEN(Outtime) - CHARINDEX('.', Outtime)), 2) AS INT) AS OutTimeMin
FROM App_ShiftMasterTable";

            using (var cmdShiftParts = new SqlCommand(shiftPartsSql, cn))
            using (var rdr = cmdShiftParts.ExecuteReader())
            {
                while (rdr.Read())
                {
                    string sh = Convert.ToString(rdr["Shift"]);
                    int inHr = rdr["InTimeHr"] != DBNull.Value ? Convert.ToInt32(rdr["InTimeHr"]) : 0;
                    int inMin = rdr["InTimeMin"] != DBNull.Value ? Convert.ToInt32(rdr["InTimeMin"]) : 0;
                    int outHr = rdr["OutTimeHr"] != DBNull.Value ? Convert.ToInt32(rdr["OutTimeHr"]) : 0;
                    int outMin = rdr["OutTimeMin"] != DBNull.Value ? Convert.ToInt32(rdr["OutTimeMin"]) : 0;

                    if (!string.IsNullOrEmpty(sh))
                        shiftParts[sh] = (inHr, inMin, outHr, outMin);
                }
            }


                cn.Close();
            }










            int month = int.Parse(ddlMonth.SelectedValue);
            int year = int.Parse(ddlYear.SelectedValue);
            DateTime first = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            DateTime last = new DateTime(year, month, days);

            string selectedAadhar = ddlAadhar.SelectedValue;

            using (var cn = new SqlConnection(_connString))
            {
                cn.Open();
                using (var tran = cn.BeginTransaction())
                {
                    try
                    {
                        int inHr = 0, inMin = 0, outHr = 0, outMin = 0;
                        string date_mm = null;
                        string date_dd = null;
                        string date_yyyy = null;
                        
                        foreach (GridViewRow row in gvAttendance.Rows)
                        {
                            //var lblDate = row.FindControl("lblDate") as Label;
                            var hfDate = row.FindControl("hfDateIso") as HiddenField;

                           
                            var lblAadhar = row.FindControl("lblRowAadhar") as Label;
                            var lblName = row.FindControl("lblRowName") as Label;
                            var ddlRowWorkOrder = row.FindControl("ddlRowWorkOrder") as DropDownList;
                            var ddlRowLocation = row.FindControl("ddlRowLocation") as DropDownList;
                            var txtOT = row.FindControl("txtOT") as TextBox;
                            var ddlDayDef = row.FindControl("ddlDayDef") as DropDownList;
                            var chkPresent = row.FindControl("chkPresent") as CheckBox;

                           
                            var ddlEngType = row.FindControl("ddlEngagementType") as DropDownList;
                            string engType = ddlEngType?.SelectedValue ?? "";



                            //if (lblDate == null || lblAadhar == null) continue;
                            //if (!DateTime.TryParse(lblDate.Text, out DateTime date)) continue;
                            if (!DateTime.TryParseExact(hfDate.Value, "yyyy-MM-dd",
                            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                                continue;



                            string aadhar = lblAadhar.Text?.Trim() ?? "";
                            string name = lblName?.Text?.Trim() ?? "";
                            if (string.IsNullOrEmpty(aadhar)) continue;

                            string workOrderNo = ddlRowWorkOrder?.SelectedValue ?? "";
                            string locCode = ddlRowLocation?.SelectedValue ?? "";

                            decimal ot = 0;
                            if (!string.IsNullOrWhiteSpace(txtOT?.Text))
                                decimal.TryParse(txtOT.Text, out ot);

                            string dayDef = ddlDayDef?.SelectedValue ?? "WD";
                            bool isPresent = chkPresent?.Checked ?? false;


                            string shift = null,masteid=null;
                            var _dtEmployees= ViewState["Employees"] as DataTable;
                            if (_dtEmployees != null && _dtEmployees.Columns.Contains("Shift") && !string.IsNullOrEmpty(aadhar))
                            {

                                var key = (aadhar ?? string.Empty)
                                    .Replace("\\", "\\\\")
                                    .Replace("'", "''");

                                var rows = _dtEmployees.Select($"AadharCard = '{key}'");


                                //var rows = dtEmployees.Select($"AadharCard = '{aadhar.Replace(\"'\", \"''\")}'");
                                    if(rows.Length>0)
                                    {
                                        shift = Convert.ToString(rows[0]["Shift"]);
                                        masteid= Convert.ToString(rows[0]["id"]);
                                    }
                            }


                            //intime outtime

                            inHr = 0; inMin = 0; outHr = 0; outMin = 0;
                            bool hasShiftParts = false;

                            if (!string.IsNullOrEmpty(shift) && shiftParts.TryGetValue(shift, out var parts))
                            {
                                inHr = parts.InHr;
                                inMin = parts.InMin;
                                outHr = parts.OutHr;
                                outMin = parts.OutMin;
                                hasShiftParts = true;
                            }




                            // UPDATE   --- UpdatedOn = GETDATE(),
                            string updateSql = @"
UPDATE App_attendanceDetails
SET WorkManName = @Name,
    WorkOrderNo = @WorkOrderNo,
    LocationCode = @LocationCode,
    OT_Hrs = @OT,
    DayDef = @DayDef,
    Present = @IsPresent,    
    WorkManSl = @SlNo,
    WorkManCategory = @Category,
EngagementType = @EngagementType,
Shifts = @Shift,
InTimeHr        = @InTimeHr,
    InTimeMin       = @InTimeMin,
    OutTimeHr       = @OutTimeHr,
    OutTimeMin      = @OutTimeMin


WHERE Dates = @Dates AND AadharNo = @AadharCard AND MasterID=@MasterID";

                            using (var cmd = new SqlCommand(updateSql, cn, tran))
                            {
                                cmd.Parameters.AddWithValue("@MasterID", masteid);
                                cmd.Parameters.AddWithValue("@Name", (object)name ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@WorkOrderNo", string.IsNullOrEmpty(workOrderNo) ? (object)DBNull.Value : workOrderNo);
                                cmd.Parameters.AddWithValue("@LocationCode", string.IsNullOrEmpty(locCode) ? (object)DBNull.Value : locCode);
                                cmd.Parameters.AddWithValue("@OT", ot);
                                cmd.Parameters.AddWithValue("@DayDef", (object)dayDef ?? "WD");
                                cmd.Parameters.AddWithValue("@IsPresent", isPresent);
                                
                                cmd.Parameters.AddWithValue("@Dates", date.Date);
                                cmd.Parameters.AddWithValue("@AadharCard", aadhar);
                                cmd.Parameters.AddWithValue("@SlNo", txtSlNo.Text);
                                cmd.Parameters.AddWithValue("@Category", txtCategory.Text);    
                                cmd.Parameters.Add(new SqlParameter("@EngagementType", SqlDbType.VarChar, 50)
                                {
                                    Value = string.IsNullOrEmpty(engType) ? (object)DBNull.Value : engType
                                });                               

                                cmd.Parameters.AddWithValue("@Shift", string.IsNullOrEmpty(shift) ? (object)DBNull.Value : shift);
                                cmd.Parameters.AddWithValue("@InTimeHr", hasShiftParts ? (object)inHr : DBNull.Value);
                                cmd.Parameters.AddWithValue("@InTimeMin", hasShiftParts ? (object)inMin : DBNull.Value);
                                cmd.Parameters.AddWithValue("@OutTimeHr", hasShiftParts ? (object)outHr : DBNull.Value);
                                cmd.Parameters.AddWithValue("@OutTimeMin", hasShiftParts ? (object)outMin : DBNull.Value);


                                int updated = cmd.ExecuteNonQuery();
                                if (updated == 0)
                                {
                                    // INSERT
                                    string insertSql = @"
INSERT INTO App_attendanceDetails
(MasterID, Dates,DayWeek, AadharNo, WorkManName, VendorCode,WorkManSl, WorkManCategory,
 WorkOrderNo, LocationCode, OT_Hrs, DayDef,Status, Present, EngagementType,Shifts,InTimeHr, InTimeMin, OutTimeHr, OutTimeMin)
VALUES (@MasterID, @Date,@DayWeek, @AadharCard, @Name, @VendorCode,@SlNo, @Category,
        @WorkOrderNo, @LocationCode, @OT, @DayDef,@Status ,@IsPresent, @EngagementType,@Shift,@InTimeHr, @InTimeMin, @OutTimeHr, @OutTimeMin)";

                                    using (var ins = new SqlCommand(insertSql, cn, tran))
                                    {
                                        ins.Parameters.AddWithValue("@MasterID", masteid);
                                        
                                        ins.Parameters.AddWithValue("@Date", date.Date);
                                        ins.Parameters.AddWithValue("@DayWeek", date.Date.DayOfWeek.ToString());
                                        ins.Parameters.AddWithValue("@AadharCard", aadhar);
                                        ins.Parameters.AddWithValue("@Name", (object)name ?? DBNull.Value);
                                        ins.Parameters.AddWithValue("@VendorCode", Session["UserName"].ToString());
                                        ins.Parameters.AddWithValue("@SlNo", txtSlNo.Text);
                                        ins.Parameters.AddWithValue("@Category", txtCategory.Text);
                                        ins.Parameters.AddWithValue("@WorkOrderNo", string.IsNullOrEmpty(workOrderNo) ? (object)DBNull.Value : workOrderNo);
                                        ins.Parameters.AddWithValue("@LocationCode", string.IsNullOrEmpty(locCode) ? (object)DBNull.Value : locCode);
                                        ins.Parameters.AddWithValue("@OT", ot);
                                        ins.Parameters.AddWithValue("@DayDef", (object)dayDef ?? "WD");
                                        ins.Parameters.AddWithValue("@Status", "Pending for approval"); 

                                        ins.Parameters.AddWithValue("@IsPresent", isPresent);
                                        ins.Parameters.Add(new SqlParameter("@EngagementType", SqlDbType.VarChar, 50)
                                        {
                                            Value = string.IsNullOrEmpty(engType) ? (object)DBNull.Value : engType
                                        });
                                        
                                        ins.Parameters.AddWithValue("@Shift", string.IsNullOrEmpty(shift) ? (object)DBNull.Value : shift);

                                        ins.Parameters.AddWithValue("@InTimeHr", hasShiftParts ? (object)inHr : DBNull.Value);
                                        ins.Parameters.AddWithValue("@InTimeMin", hasShiftParts ? (object)inMin : DBNull.Value);
                                        ins.Parameters.AddWithValue("@OutTimeHr", hasShiftParts ? (object)outHr : DBNull.Value);
                                        ins.Parameters.AddWithValue("@OutTimeMin", hasShiftParts ? (object)outMin : DBNull.Value);

                                        int int_ins =  ins.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                        tran.Commit();
                        litMessage.Text = "<div class='message ok'>Attendance saved successfully.</div>";
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        litMessage.Text = "<div class='message err'>Error: " + ex.Message + "</div>";
                        return;
                    }
                }
            }

            // ----------------------------------------------------------------------
            // RELOAD grid using DOJ trimming (same logic as View)
            // ----------------------------------------------------------------------
            if (!string.IsNullOrEmpty(selectedAadhar))
                ViewState["ExistingAttendance"] = LoadExistingAttendance(first, last, selectedAadhar);
            else
                ViewState["ExistingAttendance"] = null;

            // --- Get DOJ ---
            DateTime? doj = null;
            var dtEmployees = ViewState["Employees"] as DataTable;
            if (dtEmployees != null && dtEmployees.Columns.Contains("DOJ"))
            {
                var rows = dtEmployees.Select($"AadharCard = '{selectedAadhar.Replace("'", "''")}'");
                if (rows.Length > 0 && rows[0]["DOJ"] != DBNull.Value)
                    doj = Convert.ToDateTime(rows[0]["DOJ"]).Date;
            }

            if (!doj.HasValue)
            {
                using (var cn2 = new SqlConnection(_connString))
                using (var cm2 = new SqlCommand("SELECT DOJ FROM App_EmployeeMaster WHERE AadharCard = @Aadhar", cn2))
                {
                    cm2.Parameters.AddWithValue("@Aadhar", selectedAadhar);
                    cn2.Open();
                    var obj = cm2.ExecuteScalar();
                    if (obj != null && obj != DBNull.Value)
                        doj = Convert.ToDateTime(obj).Date;
                }
            }

            // --- Build trimmed date list ---
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

            gvAttendance.DataSource = dt;
            gvAttendance.DataBind();
            pnlGrid.Visible = true;

            lblRowCount.Text = dt.Rows.Count.ToString();
        }
