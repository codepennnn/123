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


        public void LoadLookups_emp_wo()
        {
            
            DataTable dtWorkOrders = new DataTable();
            DataTable dtLocations = new DataTable();

            string vcode = Session["UserName"].ToString();
            string ddl_month = ddlMonth.SelectedValue.Length < 2 ? "0" + ddlMonth.SelectedValue : ddlMonth.SelectedValue ;
            string ddl_year = ddlYear.SelectedValue;
            string ddl_mix = ddl_year + ddl_month;

            using (var cn = new SqlConnection(_connString))
            {
                cn.Open();

                



                var month = int.Parse(ddlMonth.SelectedValue);
                var year = int.Parse(ddlYear.SelectedValue);

                var fromDate = new DateTime(year, month, 1);
                var toDate = fromDate.AddMonths(1); // exclusive end

                string monthStr = month.ToString("00");   // '01'..'12'
                string yearStr = year.ToString("0000");  // '2025'
                int mixInt = int.Parse(yearStr + monthStr);

                string sql = @"
WITH AttWO AS (
    SELECT DISTINCT
        att.WorkOrderNo AS wo_no,
        w.LOC_OF_WORK   AS LOC_CODE,
        w.Engagement_Type AS EngagementType
    FROM App_AttendanceDetails att
    LEFT JOIN app_workorder_reg w ON w.WO_NO = att.WorkOrderNo
    WHERE att.Dates >= @FromDate AND att.Dates < @ToDate
      AND att.VendorCode = @VendorCode and att.WorkOrderNo is not null
),
C3WO AS (
    SELECT DISTINCT
        dtl.WO_NO       AS wo_no,
        w2.LOC_OF_WORK  AS LOC_CODE,
        w2.Engagement_Type AS EngagementType
    FROM App_Vendor_form_C3_Dtl dtl
    LEFT JOIN app_workorder_reg w2 ON w2.WO_NO = dtl.WO_NO
    WHERE dtl.C3_CLOSER_DATE >= @FromDate and dtl.WO_NO is not null
      AND dtl.V_CODE = @VendorCode
      AND NOT EXISTS (
            SELECT 1
            FROM App_Wo_Nil n
            WHERE n.WO_NO = dtl.WO_NO
              AND n.TEMPORARY_MONTH = @MonthStr
              AND n.TEMPORARY_YEAR  = @YearStr
              AND n.NO_WORK = 'Temporary'
      )
      AND NOT EXISTS (
            SELECT 1
            FROM App_Wo_Nil n2
            WHERE n2.WO_NO = dtl.WO_NO
              AND n2.NO_WORK = 'Permanent'
              AND (CAST(n2.TEMPORARY_YEAR AS INT) * 100
                   + CASE WHEN TRY_CAST(n2.CLOSER_DATE AS INT) IS NULL
                          THEN 0 ELSE TRY_CAST(n2.CLOSER_DATE AS INT) END) <= @MixInt
      )
)
SELECT
    wo_no,
    LOC_CODE,
    ISNULL(EngagementType, 'Manpower Supply') AS EngagementType
FROM (
    SELECT wo_no, LOC_CODE, EngagementType FROM AttWO
    UNION
    SELECT wo_no, LOC_CODE, EngagementType FROM C3WO
) X;
";

                using (var da = new SqlDataAdapter(sql, cn))
                {
                    da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate;
                    da.SelectCommand.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = toDate;
                    da.SelectCommand.Parameters.Add("@VendorCode", SqlDbType.VarChar, 50).Value = vcode;
                    da.SelectCommand.Parameters.Add("@MonthStr", SqlDbType.Char, 2).Value = monthStr;
                    da.SelectCommand.Parameters.Add("@YearStr", SqlDbType.Char, 4).Value = yearStr;
                    da.SelectCommand.Parameters.Add("@MixInt", SqlDbType.Int).Value = mixInt;

                    da.SelectCommand.CommandTimeout = 120; // if needed
                    da.Fill(dtWorkOrders);
                }


                using (var da = new SqlDataAdapter("SELECT LocationCode,Location FROM [CLMSDB].[dbo].[App_LocationMaster] ORDER BY Location", cn))
                    da.Fill(dtLocations);
            }

            

           
            ViewState["WorkOrders"] = dtWorkOrders;
            ViewState["Locations"] = dtLocations;
        }

        private DataTable LoadExistingAttendance(DateTime fromDate, DateTime toDate, string aadharFilter)
        {
            var dt = new DataTable();
            using (var cn = new SqlConnection(_connString))
            {
                cn.Open();
                string sql = @"
SELECT MasterID, Dates, AadharNo, WorkManName, WorkOrderNo, LocationCode, OT_Hrs, DayDef, Present,EngagementType,Shifts
FROM App_AttendanceDetails
WHERE Dates >= @FromDate AND Dates <= @ToDate";
                if (!string.IsNullOrEmpty(aadharFilter))
                    sql += " AND AadharNo = @AadharNo";

                using (var cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.Date) { Value = fromDate.Date });
                    cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.Date) { Value = toDate.Date });
                    if (!string.IsNullOrEmpty(aadharFilter))
                        cmd.Parameters.Add(new SqlParameter("@AadharNo", SqlDbType.VarChar, 50) { Value = aadharFilter });

                    using (var da = new SqlDataAdapter(cmd))
                        da.Fill(dt);
                }
            }
            return dt;
        }


        <asp:Panel ID="pnlGrid" runat="server" Visible="false" Style="margin-top:18px">
            <div class="grid-wrap">
                <asp:GridView ID="gvAttendance" runat="server" CssClass="attendance" AutoGenerateColumns="false"
                    OnRowDataBound="gvAttendance_RowDataBound" GridLines="None">
                    <Columns>
                       <%-- <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date","{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <!-- what user sees -->
                                <asp:Label ID="lblDateDisplay" runat="server"
                                    Text='<%# Eval("Date", "{0:dd-MM-yyyy}") %>'></asp:Label>

                                <!-- hidden ISO value used by C# code -->
                                <asp:HiddenField ID="hfDateIso" runat="server"
                                    Value='<%# Eval("Date", "{0:yyyy-MM-dd}") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Aadhar">
                            <ItemTemplate>
                                <asp:Label ID="lblRowAadhar" runat="server" CssClass="grid-label row-aadhar-label"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Workman Name">
                            <ItemTemplate>
                                <asp:Label ID="lblRowName" runat="server" CssClass="grid-label row-name-label"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Work Order">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlRowWorkOrder" runat="server" CssClass="row-select row-wo" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Location">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlRowLocation" runat="server" CssClass="row-select row-loc" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="OT(hrs)">
                            <ItemTemplate>
                                <asp:TextBox ID="txtOT" runat="server" CssClass="otbox row-ot" Text="0"/>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Day Def">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlDayDef" runat="server" CssClass="small row-day">
                                    <asp:ListItem Value="WD">Working Day</asp:ListItem>
                                    <asp:ListItem Value="OD">Off Day</asp:ListItem>
                                    <asp:ListItem Value="L">Leave</asp:ListItem>
                                    <asp:ListItem Value="NH">Holiday</asp:ListItem>
                                    <asp:ListItem Value="HF">Half Day</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Engagement Type">
    <ItemTemplate>
        <asp:DropDownList ID="ddlEngagementType" runat="server" CssClass="row-select row-engtype">
            <asp:ListItem Value="">-- Select --</asp:ListItem>
            <asp:ListItem Value="ManPowerSupply">Manpower Supply</asp:ListItem>
            <asp:ListItem Value="ItemRate">Item Rate</asp:ListItem>
        </asp:DropDownList>
    </ItemTemplate>
</asp:TemplateField>


                        <asp:TemplateField HeaderText="Present" ItemStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkPresent" runat="server" CssClass="row-present" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <div style="margin-top:10px; display:flex; justify-content:space-between; align-items:center;">
                    <div class="muted">Rows: <asp:Label ID="lblRowCount" runat="server" Text="0" /></div>
                    <div style="display:flex; gap:8px;">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-sm btn-success" Text="Save Attendance" OnClick="btnSave_Click" />
                        <asp:Button ID="btnExport" runat="server" CssClass="btn btn-sm btn-warning" Text="Export CSV" OnClientClick="exportCsv(); return false;" />
                    </div>
                </div>
            </div>
        </asp:Panel>
