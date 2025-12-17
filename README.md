 private void LoadLookups()
 {
     DataTable dtEmployees = new DataTable();
    

     string vcode = Session["UserName"].ToString();
     string ddl_month = ddlMonth.SelectedValue;
     string ddl_year = ddlYear.SelectedValue;
     string ddl_mix =ddl_year+ddl_month;

     using (var cn = new SqlConnection(_connString))
     {
         cn.Open();

         using (var da = new SqlDataAdapter("SELECT id, AadharCard, Name, DOJ, WorkManSlNo, WorkManCategory,Shift FROM App_EmployeeMaster where vendorcode='"+ vcode + "'  and ApprvStatus = 'Approve' and WorkManSlNo != '' and Shift != ''  ORDER BY Name ASC, AadharCard ASC", cn)
             
             )
             da.Fill(dtEmployees);

     }

     ddlAadhar.Items.Clear();
     ddlAadhar.Items.Add(new ListItem("-- Select --", ""));


     foreach (DataRow r in dtEmployees.Rows)
     {
         string aadhar = Convert.ToString(r["AadharCard"]);
         string name = Convert.ToString(r["Name"]);
         //ddlAadhar.Items.Add(new ListItem(aadhar, aadhar));
         string sl = Convert.ToString(r["WorkManSlNo"]);
         string display = $"{sl} - {name} -  {aadhar}";

         ddlAadhar.Items.Add(new ListItem(display, aadhar));


     }

     ViewState["Employees"] = dtEmployees;
    
 }

 public void LoadLookups_emp_wo()
 {
     
     DataTable dtWorkOrders = new DataTable();
     DataTable dtLocations = new DataTable();

     DataTable dtAttended = new DataTable();

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
                                ISNULL(
                                 CASE 
                                     WHEN EngagementType = 'Manpower Supply' THEN 'ManPowerSupply'
                                     WHEN EngagementType = 'Item Rate' THEN 'ItemRate'
                                     ELSE EngagementType
                                 END,
                                 'ManPowerSupply'
                             ) AS EngagementType
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



         string attSql = @"SELECT DISTINCT AadharNo FROM App_AttendanceDetails WHERE Dates >= @FromDate AND Dates <  @ToDate and Present=1";

         using (var da = new SqlDataAdapter(attSql, cn))
         {
             da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.Date).Value = fromDate;
             da.SelectCommand.Parameters.Add("@ToDate", SqlDbType.Date).Value = toDate;
             da.Fill(dtAttended);
         }
     }

     




    
     ViewState["WorkOrders"] = dtWorkOrders;
     ViewState["Locations"] = dtLocations;
     ViewState["AttendedAadhars"] = dtAttended;
 }



 private void BindAadharDropdown()
 {
     var dtEmployees = ViewState["Employees"] as DataTable;
     var dtPresent = ViewState["PresentAadhars"] as DataTable;

     ddlAadhar.Items.Clear();
     ddlAadhar.Items.Add(new ListItem("-- Select --", ""));

     foreach (DataRow r in dtEmployees.Rows)
     {
         string aadhar = r["AadharCard"].ToString();
         string name = r["Name"].ToString();

         var li = new ListItem($"{name} ({aadhar})", aadhar);

         // ✅ COLOR GREEN IF PRESENT=1 EXISTS
         if (dtPresent != null &&
             dtPresent.Select($"AadharNo = '{aadhar}'").Length > 0)
         {
             li.Attributes["class"] = "att-present";
         }

         ddlAadhar.Items.Add(li);
     }
 }


select option.att-done {
    background-color: #dcfce7;
    color: #166534;
    font-weight: 600;
}


  <div class="SearchDropDown" style="position: relative; width: 100%;">


      <button type="button"
          class="btn btn-sm btn-light w-100 text-start"
          onclick="toggleAadharDD(this)"
          style="border: 1px solid #ced2d5;background:#fff">
          <span class="selected-text">-- Select Workman --</span>
          <span class="caret float-end"></span>
      </button>

      <div class="floatDiv"
          style="position: absolute; display: none; z-index: 1050; width: 100%; background: #fff; border: 1px solid #ced2d5; box-shadow: 0 6px 12px rgba(0,0,0,.18); padding: 6px;">


          <input type="text"
              class="form-control form-control-sm mb-1"
              placeholder="Search..."
              onkeyup="filterAadhar(this)" />


          <div class="searchList" style="max-height: 220px; overflow-y: auto;">
          </div>
      </div>


      <asp:DropDownList ID="ddlAadhar" runat="server"
          AutoPostBack="true"
          OnSelectedIndexChanged="ddlAadhar_SelectedIndexChanged"
          Style="display: none;">
      </asp:DropDownList>

  </div>

   protected void Page_Load(object sender, EventArgs e)
 {
     if (!IsPostBack)
     {
         PopulateMonthYear();
         LoadLookups();
         BindAadharDropdown();
     }
 }
