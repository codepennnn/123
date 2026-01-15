     <cc1:formcointainer ID="Leavecomplience" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowDataBound="Leavecomplience_RowDataBound"
                            PageSize="1" ShowHeader="False" Width="100%"  DataSource="<%# PageRecordDataSet %>"  
                            DataMember="App_Leave_Comp_Summary" DataKeyNames="ID" OnNewRowCreatingEvent="Leavecomplience_NewRowCreatingEvent"            
                            BindingErrorMessage="aaaaaaaa" GridLines="None">
                  <Columns>
                       <asp:TemplateField SortExpression="ID" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="ID" runat="server"></asp:Label>
                                    </ItemTemplate>
                          </asp:TemplateField>

                        <asp:TemplateField>
                          <ItemTemplate>
                 <div class="form-inline row">                    
                     <div class="form-group col-md-6 mb-2">
                                     <label for="PaymentDate" class="m-0 mr-2 p-0 col-form-label-sm col-sm-5 font-weight-bold fs-6 justify-content-start">PaymentDate:</label>
                                     <asp:TextBox ID="PaymentDate" runat="server" CssClass="form-control form-control-sm col-sm-6"></asp:TextBox>
                                     <ask:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="TopRight" TargetControlID="PaymentDate" TodaysDateFormat="dd/MM/yyyy" DefaultView="Days"></ask:CalendarExtender>
                                     <asp:CustomValidator ID="CustomValidator21" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="PaymentDate" ValidateEmptyText="true"></asp:CustomValidator>
                          </div>                                
                            <div class="form-group col-md-6 mb-2">
                                     <label for="ModeOfPayment" class="m-0 mr-2 p-0 col-form-label-sm col-sm-5 font-weight-bold fs-6 justify-content-start">ModeOfPayment:</label>
                                     <asp:DropDownList ID="ModeOfPayment" runat="server" CssClass="form-control form-control-sm col-sm-6" OnTextChanged="ModeOfPayment_TextChanged" AutoPostBack="true">
                                         <asp:ListItem></asp:ListItem>
                                         <asp:ListItem Value="Bank">Bank</asp:ListItem>
                                         <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                         <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                     </asp:DropDownList>
                                     <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="ModeOfPayment" ValidateEmptyText="true"></asp:CustomValidator>
                          </div> 

                     
                        
                     </div>

                        <div class="form-inline row">   
                             <div class="form-group col-md-6 mb-2">
                                    <asp:Label ID="lbl_PaymentAmount" runat="server" class="m-0 mr-2 p-0 col-form-label-sm col-sm-5 font-weight-bold fs-6 justify-content-start"></asp:Label>
                                    <asp:TextBox ID="PaymentAmount" runat="server" CssClass="form-control form-control-sm col-sm-6"></asp:TextBox>
                                 <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="PaymentAmount" ValidateEmptyText="true"></asp:CustomValidator>
                                 <%--<asp:RegularExpressionValidator runat="server" ID="rev_shifts"  ControlToValidate="PaymentAmount"  ValidationExpression="^[1-9]\d*(\.\d+)?$" ErrorMessage="Invalid amount" ValidationGroup="save" Display="Dynamic" ForeColor="Red" Font-Size="XX-Small" />--%>
                             </div>

                           <div class="form-group col-md-6 mb-2">
                                    <label for="TotalWorker" class="m-0 mr-2 p-0 col-form-label-sm col-sm-5  font-weight-bold fs-6 justify-content-start">Total Workers:</label>
                                    <asp:TextBox ID="TotalWorker" runat="server" CssClass="form-control form-control-sm col-sm-6" Enabled="false"></asp:TextBox>
                                 <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="TotalWorker" ValidateEmptyText="true"></asp:CustomValidator>
                               <%--<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1"  ControlToValidate="TotalWorker"  ValidationExpression="^[0-9]+$" ErrorMessage="Invalid" ValidationGroup="save" Display="Dynamic" ForeColor="Red" Font-Size="XX-Small" />--%>
                           </div>
                            

                           
                            </div>

                       <div class="form-inline row">   
                            <div class="form-group col-md-6 mb-2">
                                     <label for="NoOfWorkers_eligiable" class="m-0 mr-2 p-0 col-form-label-sm col-sm-5  font-weight-bold fs-6 justify-content-start">No.Of Workers Eligible for Leave:</label>
                                    <asp:TextBox ID="NoOfWorkers_eligiable" runat="server" CssClass="form-control form-control-sm col-sm-6" Enabled="false"></asp:TextBox>
                                <asp:CustomValidator ID="CustomValidator4" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="NoOfWorkers_eligiable" ValidateEmptyText="true"></asp:CustomValidator>
                                <%--<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2"  ControlToValidate="NoOfWorkers_eligiable"  ValidationExpression="^[0-9]+$" ErrorMessage="Invalid" ValidationGroup="save" Display="Dynamic" ForeColor="Red" Font-Size="XX-Small" />--%>
                              </div>
                            <div class="form-group col-md-6 mb-2">
                                    <label for="NoOfWorkers_Noteligiable" class="m-0 mr-2 p-0 col-form-label-sm col-sm-5  font-weight-bold fs-6 justify-content-start">No.Of Workers Not Eligible for Leave:</label>
                                     <asp:TextBox ID="NoOfWorkers_Noteligiable" runat="server" CssClass="form-control form-control-sm col-sm-6" Enabled="false" ></asp:TextBox>
                                <asp:CustomValidator ID="CustomValidator5" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="NoOfWorkers_Noteligiable" ValidateEmptyText="true"></asp:CustomValidator>
                                <%--<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator3"  ControlToValidate="NoOfWorkers_Noteligiable"  ValidationExpression="^[0-9]+$" ErrorMessage="Invalid" ValidationGroup="save" Display="Dynamic" ForeColor="Red" Font-Size="XX-Small" />--%>
                                </div>  
                             
                                            
                            
                            </div>

                       <div class="form-inline row"> 
                          <div class="form-group col-md-6 mb-2">
                                    <label for="No_Worker_Paid" class="m-0 mr-2 p-0 col-form-label-sm col-sm-5  font-weight-bold fs-6 justify-content-start">No. of Paid Worker:</label>
                                        <asp:TextBox ID="No_Worker_Paid" runat="server" CssClass="form-control form-control-sm col-sm-6" Enabled="false"></asp:TextBox>
                              <asp:CustomValidator ID="CustomValidator6" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="No_Worker_Paid" ValidateEmptyText="true"></asp:CustomValidator>
                              <%--<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator4"  ControlToValidate="No_Worker_Paid"  ValidationExpression="^[0-9]+$" ErrorMessage="Invalid" ValidationGroup="save" Display="Dynamic" ForeColor="Red" Font-Size="XX-Small" />--%>
                                </div>  
                           <div class="form-group col-md-6 mb-2">
                                    <label for="TotalLeavePayabledays" class="m-0 mr-2 p-0 col-form-label-sm col-sm-5  font-weight-bold fs-6 justify-content-start">Total Leave Payable days:</label>
                                      <asp:TextBox ID="TotalLeavePayabledays" runat="server" CssClass="form-control form-control-sm col-sm-6" Enabled="false"></asp:TextBox>
                               <asp:CustomValidator ID="CustomValidator7" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="TotalLeavePayabledays" ValidateEmptyText="true"></asp:CustomValidator>
                               <%--<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator5"  ControlToValidate="TotalLeavePayabledays"  ValidationExpression="^[0-9]+$" ErrorMessage="Invalid" ValidationGroup="save" Display="Dynamic" ForeColor="Red" Font-Size="XX-Small" />--%>
                                </div> 
                          
                       </div>

                              <div class="form-inline row"> 
                          <div class="form-group col-md-6 mb-2">
                                    <label for="TotalLeavePayableAmount" class="m-0 mr-2 p-0 col-form-label-sm col-sm-5  font-weight-bold fs-6 justify-content-start">Total Leave Payable Amount:</label>
                                     <asp:TextBox ID="TotalLeavePayableAmount" runat="server" CssClass="form-control form-control-sm col-sm-6" Enabled="false"></asp:TextBox>
                              <asp:CustomValidator ID="CustomValidator8" runat="server" ClientValidationFunction="ValidateWithZero" ValidationGroup="save" ControlToValidate="TotalLeavePayableAmount" ValidateEmptyText="true"></asp:CustomValidator>
                                 <%--<asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator6"  ControlToValidate="TotalLeavePayableAmount"  ValidationExpression="^[1-9]\d*(\.\d+)?$" ErrorMessage="Invalid amount" ValidationGroup="save" Display="Dynamic" ForeColor="Red" Font-Size="XX-Small" />--%>
                          </div>  
                          
                       </div>

                               <%--<div class="form-inline row"> 
                          <div class="form-group col-md-12 mb-2">
                                    <label for="Remarks" class="m-0 mr-4 p-0 col-form-label-sm col-sm-2  font-weight-bold fs-6 justify-content-start">Remarks:</label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                    <asp:TextBox ID="Remarks" runat="server" CssClass="form-control form-control-sm col-sm-9" TextMode="MultiLine"></asp:TextBox>
                              <asp:CustomValidator ID="CustomValidator9" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="Remarks" ValidateEmptyText="true"></asp:CustomValidator>
                                </div>  
                          
                          
                       </div>--%>
                    
                              

                         
                        </ItemTemplate>
                  </asp:TemplateField>
                         </Columns>

                   <PagerSettings Visible="False" />
               </cc1:formcointainer>
  
  
  
  <div class="form-inline row">
          <div class="form-group col-md-6 mb-2">
             <label for="Leave_Register_Upload" class="m-0 mr-0 p-0 col-form-label-sm col-sm-5 font-weight-bold fs-6 justify-content-start">Leave Register File(pdf)<span style="color: #FF0000;">*</span>:</label>
              <asp:FileUpload ID="Leave_Register_Upload" runat="server" AllowMultiple="true" CssClass="form-control-sm col-sm-4" onchange="validateFileType()"></asp:FileUpload>
            
              
              
                         <asp:Repeater ID="Leave_Register_Upload1" runat="server">
                         <ItemTemplate>
                             <a href='<%# Eval("Url") %>' target="_blank"
                                style="color:blue;text-decoration:underline">
                                 <%# Eval("Name") %>
                             </a><br /> &nbsp &nbsp
                         </ItemTemplate>
                     </asp:Repeater>
           
              
              
              
              <asp:CustomValidator ID="CustomValidator91" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="Leave_Register_Upload" ValidateEmptyText="true"></asp:CustomValidator>
              <%--<asp:RequiredFieldValidator ID="Leave_Register_Upload_1" runat="server" Display="Dynamic" ErrorMessage="File Upload is Required" ControlToValidate="Leave_Register_Upload" ValidationGroup="save" ForeColor="Red" class="m-0 mr-0 p-0 col-form-label-sm  font-weight-bold"></asp:RequiredFieldValidator>--%>
              <%--<asp:BulletedList runat="server" ID="Leave_Register_Upload1" DisplayMode="HyperLink" Target="_blank"/>--%>
         </div>
         <div class="form-group col-md-6 mb-2">
             <label for="Bank_Statement_Upload" class="m-0 mr-0 p-0 col-form-label-sm col-sm-5 font-weight-bold fs-6 justify-content-start">Bank Statement File(pdf)<span style="color: #FF0000;">*</span>:</label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="*" ControlToValidate="Bank_Statement_Upload" ValidationGroup="save" ForeColor="Red"></asp:RequiredFieldValidator>
             <asp:FileUpload ID="Bank_Statement_Upload" runat="server" AllowMultiple="true" CssClass="form-control-sm col-sm-4" onchange="validateFileType1()"></asp:FileUpload>
            
                 <asp:Repeater ID="Bank_Statement_Upload1" runat="server">
                     <ItemTemplate>
                         <a href='<%# Eval("Url") %>' target="_blank"
                            style="color:blue;text-decoration:underline">
                             <%# Eval("Name") %>
                         </a><br /> &nbsp &nbsp
                     </ItemTemplate>
                 </asp:Repeater>
             
             
             <asp:CustomValidator ID="CustomValidator10" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="Bank_Statement_Upload" ValidateEmptyText="true"></asp:CustomValidator>
              <%--<asp:BulletedList runat="server" ID="Bank_Statement_Upload1" DisplayMode="HyperLink" Target="_blank"/>--%>
         </div>
  </div>


     protected void Leavecomplience_RowDataBound(object sender, GridViewRowEventArgs e)
     {
         if (e.Row.RowType != DataControlRowType.DataRow) return;

         DataRow dr = PageRecordDataSet.Tables["App_Leave_Comp_Summary"].Rows[0];

         BindAttachments(
             dr["Leave_Register_Upload"].ToString(),
             (Repeater)e.Row.FindControl("Leave_Register_Upload1")
         );

         BindAttachments(
             dr["Bank_Statement_Upload"].ToString(),
             (Repeater)e.Row.FindControl("Bank_Statement_Upload1")
         );

      
     }

        private void BindAttachments(string dbValue, Repeater rpt)
   {
       if (string.IsNullOrWhiteSpace(dbValue))
       {
           rpt.DataSource = null;
           rpt.DataBind();
           return;
       }

       DataTable dt = new DataTable();
       dt.Columns.Add("Name");
       dt.Columns.Add("Url");

       string[] files = dbValue.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

       foreach (string file in files)
       {
           string cleanFile = file.Trim();

           string displayName = cleanFile.Contains("_")
               ? cleanFile.Substring(cleanFile.IndexOf('_') + 1)
               : cleanFile;

           dt.Rows.Add(
               displayName,
               ResolveUrl("~/FileDownloadHandler.ashx?file=" + cleanFile)
           );
       }

       rpt.DataSource = dt;
       rpt.DataBind();
   }
  
