 protected void SummaryReport_Record_PreRender(object sender, EventArgs e)
 {
     if (SummaryReport_Record.Rows.Count > 0)
    {



         ((BulletedList)SummaryReport_Record.Rows[0].FindControl("WAGE_REGIS_FILE1")).Items.Clear();
         string[] attachments;
         if (!string.IsNullOrEmpty(PageRecordDataSet.Tables["App_Online_Wages"].Rows[0]["WAGE_REGIS_FILE"].ToString()))
         {
             attachments = PageRecordDataSet.Tables["App_Online_Wages"].Rows[0]["WAGE_REGIS_FILE"].ToString().Split(',');

             foreach (string image in attachments)
             {
                 ListItem link = new ListItem();
                 //link.Value = "~/Attachments/" + image;
                 link.Value = "~/FileDownloadHandler.ashx?file=" + Server.UrlEncode(image);
                 link.Text = image;
                 link.Attributes.CssStyle.Add("text-decoration", "underline");
                 link.Attributes.CssStyle.Add("color", "blue");
                 link.Text = image.Substring(37);
                 ((BulletedList)SummaryReport_Record.Rows[0].FindControl("WAGE_REGIS_FILE1")).Items.Add(link);
             }
         }



           ((BulletedList)SummaryReport_Record.Rows[0].FindControl("BANK_PAY_FILE1")).Items.Clear();
         string[] attachments2;
         if (!string.IsNullOrEmpty(PageRecordDataSet.Tables["App_Online_Wages"].Rows[0]["BANK_PAY_FILE"].ToString()))
         {
             attachments2 = PageRecordDataSet.Tables["App_Online_Wages"].Rows[0]["BANK_PAY_FILE"].ToString().Split(',');

             foreach (string image in attachments2)
             {
                 ListItem link = new ListItem();
                 //link.Value = "~/Attachments/" + image;
                 link.Value = "~/FileDownloadHandler.ashx?file=" + Server.UrlEncode(image);
                 link.Text = image;
                 link.Attributes.CssStyle.Add("text-decoration", "underline");
                 link.Attributes.CssStyle.Add("color", "blue");
                 link.Text = image.Substring(37);
                 ((BulletedList)SummaryReport_Record.Rows[0].FindControl("BANK_PAY_FILE1")).Items.Add(link);
             }
         }


             ((BulletedList)SummaryReport_Record.Rows[0].FindControl("GP_DECL_FILE1")).Items.Clear();
         string[] attachments3;
         if (!string.IsNullOrEmpty(PageRecordDataSet.Tables["App_Online_Wages"].Rows[0]["GP_DECL_FILE"].ToString()))
         {
             attachments3 = PageRecordDataSet.Tables["App_Online_Wages"].Rows[0]["GP_DECL_FILE"].ToString().Split(',');

             foreach (string image in attachments3)
             {
                 ListItem link = new ListItem();
                 //link.Value = "~/Attachments/" + image;
                 link.Value = "~/FileDownloadHandler.ashx?file=" + Server.UrlEncode(image);
                 link.Text = image;
                 link.Attributes.CssStyle.Add("text-decoration", "underline");
                 link.Attributes.CssStyle.Add("color", "blue");
                 link.Text = image.Substring(37);
                 ((BulletedList)SummaryReport_Record.Rows[0].FindControl("GP_DECL_FILE1")).Items.Add(link);
             }
         }


     }
 }

               <cc1:FormCointainer ID="SummaryReport_Record" runat="server" AllowPaging="True" AutoGenerateColumns="False"
               PageSize="1" ShowHeader="False" Width="100%" DataSource="<%# PageRecordDataSet %>" 
               DataMember="App_Online_Wages" DataKeyNames="Id,MonthWage,YearWage,V_CODE"
                   OnNewRowCreatingEvent="SummaryReport_Record_NewRowCreatingEvent"
                   OnPreRender="SummaryReport_Record_PreRender"
               BindingErrorMessage="aaaaaaaa" GridLines="None" BorderStyle="Solid" BorderWidth="1px" BorderColor="#bfbebe">
               <Columns>
                   <asp:TemplateField SortExpression="Id" Visible="False">
                       <ItemTemplate>
                          <asp:Label ID="Id" runat="server"></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField SortExpression="MonthWage" Visible="False">
                       <ItemTemplate>
                          <asp:Label ID="MonthWage" runat="server"></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField SortExpression="YearWage" Visible="False">
                       <ItemTemplate>
                          <asp:Label ID="YearWage" runat="server"></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField SortExpression="V_CODE" Visible="False">
                       <ItemTemplate>
                          <asp:Label ID="V_CODE" runat="server"></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField>
                       <ItemTemplate>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                          <ContentTemplate>
                           <div class="form-inline row">
                                   <div class="form-group col-md-12 mb-3" style="background-color: #65646e; height: 28px;color:white;">
                                       <h6 class="m-0">Total Summary Report</h6>
                                   </div>
                               </div>
                           <div class="form-inline row">
                               <div class="form-group col-md-3 mb-2">
                                   <label for="PAYMENT" class="m-0 mr-2 p-0 col-form-label-sm col-sm-8 font-weight-bold fs-6 justify-content-start">Payment Date(dd/MM/yyyy)<span style="color: #FF0000;">*</span>:</label>
                                 
                                   <asp:TextBox ID="PAYMENT_DATE" runat="server" placeholder="dd/MM/yyyy"   CssClass="form-control form-control-sm col-sm-10"  AutoComplete="off"  Required  ></asp:TextBox>
                                   <asp:ImageButton ID="ImageButton2" runat="server" Height="25px" ImageUrl="~/img/istockphoto-181866332-612x612.jpg" Width="30px" />
                                   <ass:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="ImageButton2" PopupPosition="BottomRight" TargetControlID="PAYMENT_DATE"></ass:CalendarExtender>

                                   <asp:CustomValidator ID="CustomValidator51" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="PAYMENT_DATE" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>
                                       <div class="form-group col-md-3 mb-2">
                                           <label for="Mode_of_Payment" class="m-0 mr-2 p-0 col-form-label-sm col-sm-8 font-weight-bold fs-6 justify-content-start">Mode Of Payment<span style="color: #FF0000;">*</span>:</label><span>
                                               <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationGroup="save" ControlToValidate="BANK" ErrorMessage="Enter only 0-9 numbers." ValidationExpression="^[0-9]*$" Font-Size="X-Small" ForeColor="Red" Font-Bold="true" /></span>--%>
                                           <%--<asp:TextBox ID="BANK" runat="server" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>--%>

                                           <asp:DropDownList ID="Mode_of_Payment" runat="server" onchange="text_body()"  CssClass="form-control form-control-sm col-sm-12" Width="292px" >
                                           
                                                   <asp:ListItem Text="BANK" Value="BANK" />
                                                   <asp:ListItem Text="CHEQUE" Value="CHEQUE" />
                                                   <asp:ListItem Text="CASH" Value="CASH" />
                                                   
                                           </asp:DropDownList>

                                           <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="Mode_of_Payment" ValidateEmptyText="true"></asp:CustomValidator>
                                       </div>
                                <div class="form-group col-md-3 mb-2">
                                   <%--<label for="Pay_Amount" class="m-0 mr-2 p-0 col-form-label-sm col-sm-5 font-weight-bold fs-6 justify-content-start">Enter Amount<span style="color: #FF0000;">*</span>:</label><span>--%>
                                    <label for="Pay_Amount" class="m-0 mr-2 p-0 col-form-label-sm col-sm-5 font-weight-bold fs-6 justify-content-start"><asp:Label runat="server" ID="Bankstat"></asp:Label>&nbsp Amount<span style="color: #FF0000;">*</span>:</label><span>
                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ValidationGroup="save" ControlToValidate="Pay_Amount" ErrorMessage="Enter only 0-9 numbers." ValidationExpression="^[0-9]*$" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"/></span>
                                   <asp:TextBox ID="Pay_Amount" runat="server" CssClass="form-control form-control-sm col-sm-12" onchange="chk_pay();"     ></asp:TextBox>
                                    <asp:CustomValidator ID="CustomValidator8" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="Pay_Amount" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>
                               <%-- <div class="form-group col-md-3 mb-2">
                                   <label for="CASH" class="m-0 mr-2 p-0 col-form-label-sm col-sm-4 font-weight-bold fs-6 justify-content-start">Cash<span style="color: #FF0000;">*</span>:</label><span><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="save" ControlToValidate="CASH" ErrorMessage="Enter only 0-9 numbers." ValidationExpression="^[0-9]*$" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"/></span>
                                   <asp:TextBox ID="CASH" runat="server" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
                                    <asp:CustomValidator ID="CustomValidator9" runat="server" ClientValidationFunction="ValidatewithZero" ValidationGroup="save" ControlToValidate="CASH" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>--%>
                           </div>  
                           <div class="form-inline row">
                               <div class="form-group col-md-3 mb-2">
                                   <label for="TOTAL_GROSS" class="m-0 mr-2 p-0 col-form-label-sm col-sm-4 font-weight-bold fs-6 justify-content-start">Total Gross<span style="color: #FF0000;">*</span>:</label>
                                   <asp:TextBox ID="TOTAL_GROSS" runat="server" Enabled="false" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
                                   <asp:CustomValidator ID="CustomValidator10" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="TOTAL_GROSS" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>
                               <div class="form-group col-md-3 mb-2">
                                   <label for="PF_DEDUCTION" class="m-0 mr-2 p-0 col-form-label-sm col-sm-4 font-weight-bold fs-6 justify-content-start">PF Deduction (Employee Contribution)<span style="color: #FF0000;">*</span>:</label>
                                   <asp:TextBox ID="PF_DEDUCTION" runat="server" Enabled="false" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
                                   <asp:CustomValidator ID="CustomValidator11" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="PF_DEDUCTION" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>
                               <div class="form-group col-md-3 mb-2">
                                   <label for="ESI_DEDUCTION" class="m-0 mr-2 p-0 col-form-label-sm col-sm-4 font-weight-bold fs-6 justify-content-start">ESI Deduction (Employee Contribution)<span style="color: #FF0000;">*</span>:</label>
                                   <asp:TextBox ID="ESI_DEDUCTION" runat="server" Enabled="false" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
                                   <asp:CustomValidator ID="CustomValidator12" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="ESI_DEDUCTION" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>
                               <div class="form-group col-md-3 mb-2">
                                   <label for="OTHER_DEDUCTION" class="m-0 mr-0 p-0 col-form-label-sm col-sm-6 font-weight-bold fs-6 justify-content-start">Other Deduction in wage register<span style="color: #FF0000;">*</span>:</label>
                                   <asp:TextBox ID="OTHER_DEDUCTION" runat="server" Enabled="false" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
                                   <asp:CustomValidator ID="CustomValidator13" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="OTHER_DEDUCTION" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>
                                    
                           </div>              
                           <div class="form-inline row">
                                <div class="form-group col-md-3 mb-2">
                                   <label for="NET_WAGES" class="m-0 mr-2 p-0 col-form-label-sm col-sm-4 font-weight-bold fs-6 justify-content-start">Net Wages<span style="color: #FF0000;">*</span>:</label>
                                   <asp:TextBox ID="NET_WAGES" runat="server" Enabled="false" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
                                    <asp:CustomValidator ID="CustomValidator14" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="NET_WAGES" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>
                                <div class="form-group col-md-3 mb-2">
                                   <label for="TOTAL_WORKER" class="m-0 mr-2 p-0 col-form-label-sm col-sm-4 font-weight-bold fs-6 justify-content-start">Total Worker<span style="color: #FF0000;">*</span>:</label><span><asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ValidationGroup="save" ControlToValidate="TOTAL_WORKER" ErrorMessage="Enter only 0-9 numbers." ValidationExpression="^[0-9]*$" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator></span>
                                   <asp:TextBox ID="TOTAL_WORKER" runat="server" Enabled="false" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
                                    <asp:CustomValidator ID="CustomValidator15" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="TOTAL_WORKER" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>
                               <div class="form-group col-md-3 mb-2">
                                   <label for="TOTAL_MANDAYS" class="m-0 mr-0 p-0 col-form-label-sm col-sm-6 font-weight-bold fs-6 justify-content-start">Total Mandays<span style="color: #FF0000;">*</span>:</label>
                                   <asp:TextBox ID="TOTAL_MANDAYS" runat="server" Enabled="false" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
                                   <asp:CustomValidator ID="CustomValidator16" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="TOTAL_MANDAYS" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>
                               <div class="form-group col-md-3 mb-2">
                                   <label for="TOTAL_VALID_GP" class="m-0 mr-2 p-0 col-form-label-sm col-sm-6 font-weight-bold fs-6 justify-content-start">Total Valid GP<span style="color: #FF0000;">*</span>:</label>
                                   <asp:TextBox ID="TOTAL_VALID_GP" runat="server" Enabled="false" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
                                   <asp:CustomValidator ID="CustomValidator17" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="TOTAL_VALID_GP" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>
                           </div>    
                            <div class="form-inline row">
                                 <div class="form-group col-md-3 mb-2">
                                   <label for="PAID_WORKER" class="m-0 mr-0 p-0 col-form-label-sm col-sm-6 font-weight-bold fs-6 justify-content-start">No. of Workers Paid<span style="color: #FF0000;">*</span>:</label><span><asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="PAID_WORKER" ErrorMessage="Enter only 0-9 numbers." ValidationGroup="save" ValidationExpression="^[0-9]*$" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator></span>
                                   <asp:TextBox ID="PAID_WORKER" runat="server" Enabled="false" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
                                    <asp:CustomValidator ID="CustomValidator21" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="PAID_WORKER" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>
                                 <div class="form-group col-md-3 mb-2">
                                   <label for="UNPAID_WORKER" class="m-0 mr-0 p-0 col-form-label-sm col-sm-6 font-weight-bold fs-6 justify-content-start">No. of Workers Unpaid<span style="color: #FF0000;">*</span>:</label><span><asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="UNPAID_WORKER" ErrorMessage="Enter only 0-9 numbers." ValidationGroup="save" ValidationExpression="^[0-9]*$" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator></span>
                                   <asp:TextBox ID="UNPAID_WORKER" runat="server" Enabled="false" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
                                    <asp:CustomValidator ID="CustomValidator22" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="UNPAID_WORKER" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>
                                <div class="form-group col-md-3 mb-2">
                                   <label for="NET_AMOUNT_PAID" class="m-0 mr-0 p-0 col-form-label-sm col-sm-6 font-weight-bold fs-6 justify-content-start">Net Amount (Paid)<span style="color: #FF0000;">*</span>:</label><span><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="NET_AMOUNT_PAID" ErrorMessage="Enter only 0-9 numbers." ValidationGroup="save" ValidationExpression="^[0-9]*$" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator></span>
                                   <asp:TextBox ID="NET_AMOUNT_PAID" runat="server" Enabled="false" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
                                    <asp:CustomValidator ID="CustomValidator19" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="NET_AMOUNT_PAID" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>
                                <div class="form-group col-md-3 mb-2">
                                   <label for="NET_AMOUNT_UNPAID" class="m-0 mr-0 p-0 col-form-label-sm col-sm-6 font-weight-bold fs-6 justify-content-start">Net Amount (Unpaid)<span style="color: #FF0000;">*</span>:</label><span><asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="NET_AMOUNT_UNPAID" ErrorMessage="Enter only 0-9 numbers." ValidationGroup="save" ValidationExpression="^[0-9]*$" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"></asp:RegularExpressionValidator></span>
                                   <asp:TextBox ID="NET_AMOUNT_UNPAID" runat="server" Enabled="false" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
                                    <asp:CustomValidator ID="CustomValidator20" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="NET_AMOUNT_UNPAID" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>
                           </div> 




                            <div class="form-inline row">


                                 <div class="form-group col-md-3 mb-2">
                                   <label for="Total_Absent" class="m-0 mr-2 p-0 col-form-label-sm col-sm-5 font-weight-bold fs-6 justify-content-start">Total Absent<span style="color: #FF0000;">*</span>:</label><span>
                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="save" ControlToValidate="Total_Absent" ErrorMessage="Enter only 0-9 numbers." ValidationExpression="^[0-9]*$" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"/></span>
                                   <asp:TextBox ID="Total_Absent" runat="server" CssClass="form-control form-control-sm col-sm-12" Enabled="false"></asp:TextBox>
                                    <asp:CustomValidator ID="CustomValidator9" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="Total_Absent" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>

                                
                                 <div class="form-group col-md-3 mb-2">
                                   <label for="No_of_Workers_Payable" class="m-0 mr-0 p-0 col-form-label-sm col-sm-6 font-weight-bold fs-6 justify-content-start">No of Workers Payable<span style="color: #FF0000;">*</span>:</label><span>
                                       <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="save" ControlToValidate="No_of_Workers_Payable" ErrorMessage="Enter only 0-9 numbers." ValidationExpression="^[0-9]*$" Font-Size="X-Small" ForeColor="Red" Font-Bold="true"/></span>
                                   <asp:TextBox ID="No_of_Workers_Payable" runat="server" CssClass="form-control form-control-sm col-sm-12" Enabled="false"></asp:TextBox>
                                    <asp:CustomValidator ID="CustomValidator6" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="No_of_Workers_Payable" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>


                                 <div class="form-group col-md-6 mb-2" style="display:none">
                                   <label for="REMARKS" class="m-0 mr-0 p-0 col-form-label-sm col-sm-2 font-weight-bold fs-6 justify-content-start">Remarks<span style="color: #FF0000;">*</span>:</label>
                                   <asp:TextBox ID="REMARKS" runat="server" CssClass="form-control form-control-sm col-sm-10" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                   <asp:CustomValidator ID="CustomValidator18" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="REMARKS" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>
                               </div>

                              <div class="form-inline row">
                                    <div class="form-group col-md-6 mb-2">
                                   <label for="LEVEL_1_REASON" class="m-0 mr-0 p-0 col-form-label-sm col-sm-2 font-weight-bold fs-6 justify-content-start">L1 Remarks<span style="color: #FF0000;">*</span>:</label>
                                   <asp:TextBox ID="LEVEL_1_REASON" runat="server" CssClass="form-control form-control-sm col-sm-10" TextMode="MultiLine" Rows="2" Enabled="false"></asp:TextBox>
                                   <asp:CustomValidator ID="CustomValidator7" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="LEVEL_1_REASON" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>

                                   <div class="form-group col-md-6 mb-2">
                                   <label for="LEVEL_2_REASON" class="m-0 mr-0 p-0 col-form-label-sm col-sm-2 font-weight-bold fs-6 justify-content-start">L2 Remarks<span style="color: #FF0000;">*</span>:</label>
                                   <asp:TextBox ID="LEVEL_2_REASON" runat="server" CssClass="form-control form-control-sm col-sm-10" TextMode="MultiLine" Rows="2" Enabled="false"></asp:TextBox>
                                   <asp:CustomValidator ID="CustomValidator23" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="LEVEL_2_REASON" ValidateEmptyText="true"></asp:CustomValidator>
                               </div>


                              </div>

                               </ContentTemplate>
                          </asp:UpdatePanel>
                           <hr />
                           <div class="form-inline row">
                                   <div class="form-group col-md-12 mb-3 mt-2">
                                       <label for="Label" class="m-0 mr-2 p-0 col-form-label-sm col-sm-8 font-weight-bold fs-6 justify-content-start" style="color:blue;font-size: smaller;">Attachments (File type should be .pdf format) :-</label>
                                   </div>
                               </div>
                            <div class="form-inline row">
                                <div class="form-group col-md-4 mb-2">
                                   <label for="WAGE_REGIS_FILE" class="m-0 mr-0 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6 justify-content-start">Wage Register File<span style="color: #FF0000;">*</span>:</label>
                                   <asp:FileUpload ID="WAGE_REGIS_FILE" runat="server" AllowMultiple="true" CssClass="form-control-sm col-sm-9" onchange="return ValidateFileExtension(this)"></asp:FileUpload>
                                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator66" Display="Dynamic" ControlToValidate="WAGE_REGIS_FILE" ErrorMessage="*" ValidationGroup="save" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                    <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="WAGE_REGIS_FILE" ValidateEmptyText="true"></asp:CustomValidator>
                                   
                                    <asp:BulletedList  ID="WAGE_REGIS_FILE1" runat="server" DisplayMode="HyperLink" Target="_blank" />
                                     
                               
                                </div>
                                 <div class="form-group col-md-4 mb-2">
                                   <label for="BANK_PAY_FILE" class="m-0 mr-0 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6 justify-content-start">Bank Pay File<span style="color: #FF0000;">*</span>:</label>
                                   <asp:FileUpload ID="BANK_PAY_FILE" runat="server" AllowMultiple="true" CssClass="form-control-sm col-sm-9" onchange="return ValidateFileExtension(this)"></asp:FileUpload>
                                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="BANK_PAY_FILE" ErrorMessage="*" ValidationGroup="save" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                     <asp:CustomValidator ID="CustomValidator4" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="BANK_PAY_FILE" ValidateEmptyText="true"></asp:CustomValidator>
                                   
                                     <asp:BulletedList  ID="BANK_PAY_FILE1"  runat="server" DisplayMode="HyperLink"   />
                               </div>
                                <div class="form-group col-md-4 mb-2">
                                   <label for="GP_DECL_FILE" class="m-0 mr-0 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6 justify-content-start">GP DECL File<span style="color: #FF0000;">*</span>:</label>
                                   <asp:FileUpload ID="GP_DECL_FILE" runat="server" AllowMultiple="true" CssClass="form-control-sm col-sm-9" onchange="return ValidateFileExtension(this)"></asp:FileUpload>
                                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" Display="Dynamic" ControlToValidate="GP_DECL_FILE" ErrorMessage="*" ValidationGroup="save" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                    <asp:CustomValidator ID="CustomValidator5" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="GP_DECL_FILE" ValidateEmptyText="true"></asp:CustomValidator>
                                   
                                    <asp:BulletedList  ID="GP_DECL_FILE1"  runat="server" DisplayMode="HyperLink"  />
                               </div>
                                </div>



                            <div class="form-inline row">
      <div class="form-group col-md-4 mb-2">
         <asp:Label ID="advance_register" runat="server" CssClass=""></asp:Label>
             
     </div>
       <div class="form-group col-md-4 mb-2">
         <asp:Label ID="fine_register" runat="server" CssClass=""></asp:Label>
             
     </div>
      <div class="form-group col-md-4 mb-2">
         <asp:Label ID="deduction_register" runat="server" CssClass=""></asp:Label>
             
     </div>
      <div class="form-group col-md-4 mb-2">
          <asp:Label ID="overtime_register" runat="server" CssClass=""></asp:Label>
   
      </div>

</div>





                          
                       </ItemTemplate>
                   </asp:TemplateField>
               </Columns>
               
           </cc1:FormCointainer>


           <ul id="MainContent_SummaryReport_Record_WAGE_REGIS_FILE1_0">
				<li style="text-decoration:underline;color:blue;"><a>WAGES REGISTER FEB-25.pdf</a></li>
			</ul>
