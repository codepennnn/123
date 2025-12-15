
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ddlLabourLicNo.Visible = false;
                lblLabourLicNo.Visible = false;
            }

        }


        protected void img_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/App/Input/Half_Yearly_Main.aspx");
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            string vcode = Session["UserName"].ToString();
            int year = Convert.ToInt32(Year.SelectedValue);
            string period = SearchPeriod.SelectedValue;

            

            BL_Half_Yearly blobj = new BL_Half_Yearly();
            DataSet dsLic = blobj.Get_LabourLicNo(vcode, period, year);

           
         

            if (dsLic != null && dsLic.Tables[0].Rows.Count > 0)
            {

                ddlLabourLicNo.DataSource = dsLic;
                ddlLabourLicNo.DataTextField = "LabourLicNo";
                ddlLabourLicNo.DataValueField = "LabourLicNo";
                ddlLabourLicNo.DataBind();
                ddlLabourLicNo.Items.Insert(0, new ListItem("-- Select Labour License No --", ""));
                // divLabourLicNo.Style["display"] = "flex";
                ddlLabourLicNo.Visible = true;
                lblLabourLicNo.Visible = true;
            }
            else
            {
                ddlLabourLicNo.Visible = false;
                lblLabourLicNo.Visible = false;
                MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors,
                    "No Labour License No found for the selected period.");
            }
        }









        protected void ddlLabourLicNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLabourLicNo.SelectedValue == "")
            {
                ReportViewer1.Visible = false;
                return;
            }

            LoadReport();
        }






        private void LoadReport()
        {
            string lic = ddlLabourLicNo.SelectedValue;
            string vcode = Session["UserName"].ToString();
            string year = Year.SelectedValue;
            string period = SearchPeriod.SelectedValue;



            string displayPeriod = "";

            if (period == "Jan-June")
            {
                displayPeriod = $"Jan{year.ToString().Substring(2)} - June{year.ToString().Substring(2)}";
            }
            else
            {
                displayPeriod = $"July{year.ToString().Substring(2)} - Dec{year.ToString().Substring(2)}";
            }


            BL_Half_Yearly blobj = new BL_Half_Yearly();
            DataSet ds = blobj.Get_Report_Data( lic,vcode,year,period);

            // fetch vname

            BL_Vendor_RFQ_Block_Unblock blobj2 = new BL_Vendor_RFQ_Block_Unblock();
            DataSet dsV = blobj2.GetVName(vcode);
            string vname = "";
            if (dsV != null && dsV.Tables[0].Rows.Count > 0)
            {
                vname = dsV.Tables[0].Rows[0]["V_NAME"].ToString();
            }



            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                ReportViewer1.Visible = false;
                MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors,
                    "No report data found for the selected RefNo.");
                return;
            }



            ReportParameter[] rp = new ReportParameter[]
            {
                new ReportParameter("PeriodDisplay", displayPeriod),
                new ReportParameter("VendorName", vname)
            };

            ReportViewer1.LocalReport.SetParameters(rp);
            ReportViewer1.LocalReport.Refresh();


            ReportViewer1.LocalReport.ReportPath = "App\\Report\\Half_Yearly_Report.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource rds = new ReportDataSource("DataSet1", ds.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Add(rds);

            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.Refresh();
        }


       <div class="card-body pt-1">
           <fieldset class="" style="border: 1px solid #bfbebe; padding: 5px 20px 5px 20px; border-radius: 6px; overflow: auto">
               <legend style="width: auto; border: 0; font-size: 14px; margin: 0px 6px 0px 6px; padding: 0px 5px 0px 5px; color: darkslategray"><b>Search</b></legend>

                   <div class="row">









                   <div class="form-group col-md-4">
                       <label for="lblYear" class="m-0 mr-0 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6">Year:</label>
                       <asp:DropDownList ID="Year" runat="server" CssClass="form-control form-control-sm col-sm-6">
                           <asp:ListItem Value="2024">2024</asp:ListItem>
                           <asp:ListItem Value="2025">2025</asp:ListItem>
                           <asp:ListItem Value="2026">2026</asp:ListItem>
                           <asp:ListItem Value="2027">2027</asp:ListItem>
                           <asp:ListItem Value="2028">2028</asp:ListItem>
                           <asp:ListItem Value="2029">2029</asp:ListItem>
                           <asp:ListItem Value="2030">2030</asp:ListItem>
                       </asp:DropDownList>
                   </div>

                   <div class="form-group col-md-4">
                       <label for="lblMonth" class="m-0 mr-0 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6">Half Year:</label>
                       <asp:DropDownList ID="SearchPeriod" runat="server"
                           CssClass="form-control form-control-sm col-sm-6">

                           <asp:ListItem Value="Jan-June">Jan-June</asp:ListItem>
                           <asp:ListItem Value="July-Dec">July-Dec</asp:ListItem>

                       </asp:DropDownList>
                   </div>


                   
                   <div class="form-group">
                       <asp:Button ID="Search" runat="server" Text="Search"  CssClass="btn btn-sm btn-primary mt-4" OnClick="Search_Click" />
                   </div>

          



      

                       <div class="form-group col-md-4">
                           <label for="lblLabourLicNo" id="lblLabourLicNo" runat="server" class="m-0 mr-0 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6">Labour License No:</label>
                           <asp:DropDownList ID="ddlLabourLicNo" runat="server"
                               CssClass="form-control form-control-sm col-sm-6" AutoPostBack="true"
                               OnSelectedIndexChanged="ddlLabourLicNo_SelectedIndexChanged">
                               <asp:ListItem Value=""></asp:ListItem>
                           </asp:DropDownList>
                       </div>


               
</div>
                


               




               <div class="form-inline row">

                        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                        Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"  Width ="100%">
                        <LocalReport ReportPath="App\Report\Half_Yearly_Report.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>
                

               </div>




            
           </fieldset>
         
     

       </div>

       i want in aspx page two textbox to show vname and vcode but how to do it i dont understand



    }
