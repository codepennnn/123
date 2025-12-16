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

            using (var da = new SqlDataAdapter("SELECT id, AadharCard, Name, DOJ, WorkManSlNo, WorkManCategory,Shift FROM App_EmployeeMaster where vendorcode='"+ vcode + "'  and ApprvStatus = 'Approve' and WorkManSlNo != '' and Shift != ''  ORDER BY Name", cn)
                
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
            string display = $"{aadhar} - {name} - {sl}";

            ddlAadhar.Items.Add(new ListItem(display, aadhar));


        }

           <div class="field">
       <%--<label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Work Order Number :<span class="text-danger">*</span></label>--%>
       <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" for="ddlAadhar">Aadhar</label>
       <asp:DropDownList ID="ddlAadhar" runat="server" CssClass="form-control form-control-sm col-sm-12" AutoPostBack="true"
           OnSelectedIndexChanged="ddlAadhar_SelectedIndexChanged" />
   </div>


        ViewState["Employees"] = dtEmployees;
       
    }
