
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









                           <div class="form-inline row">
          <div class="form-group col-md-12 mb-3 mt-2">
              <label for="Label" class="m-0 mr-2 p-0 col-form-label-sm col-sm-8 font-weight-bold fs-6 justify-content-start" style="color:blue;font-size: smaller;">Attachments (File type should be .pdf format) :-</label>
          </div>
      </div>
   <div class="form-inline row">
       <div class="form-group col-md-6 mb-2">
          <label for="PF_ESIAttach" class="m-0 mr-0 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6 justify-content-start">PF/ESI File<span style="color: #FF0000;">*</span>:</label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="*" ControlToValidate="PF_ESIAttach" ValidationGroup="save" ForeColor="Red"></asp:RequiredFieldValidator>
          <asp:FileUpload ID="PF_ESIAttach" runat="server" AllowMultiple="true" CssClass="form-control-sm col-sm-9" onchange="return ValidateFileExtension(this)"></asp:FileUpload>
  	<%--<asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="PF_ESIAttach" ValidateEmptyText="true"></asp:CustomValidator>--%>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="File Upload is Required" ControlToValidate="PF_ESIAttach" ValidationGroup="save" ForeColor="Red" class="m-0 mr-0 p-0 col-form-label-sm  font-weight-bold"></asp:RequiredFieldValidator>
         

                <asp:Repeater ID="PF_ESIAttach1" runat="server">
                  <ItemTemplate>
                      <a href='<%# Eval("Url") %>' target="_blank"
                         style="color:blue;text-decoration:underline;display:block;">
                          <%# Eval("Name") %>
                      </a><br /> &nbsp &nbsp
                  </ItemTemplate>
              </asp:Repeater>
     
           
       </div>
       </div>


       <div class="form-inline row">
    <div class="form-group col-md-4 mb-2">
       <label for="WAGE_REGIS_FILE" class="m-0 mr-0 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6 justify-content-start">Wage Register File<span style="color: #FF0000;">*</span>:</label>
       <asp:FileUpload ID="WAGE_REGIS_FILE" runat="server" AllowMultiple="true" CssClass="form-control-sm col-sm-9" onchange="return ValidateFileExtension(this)"></asp:FileUpload>
        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator66" Display="Dynamic" ControlToValidate="WAGE_REGIS_FILE" ErrorMessage="*" ValidationGroup="save" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
        <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="WAGE_REGIS_FILE" ValidateEmptyText="true"></asp:CustomValidator>
       
                 <asp:Repeater ID="WAGE_REGIS_FILE1" runat="server">
                     <ItemTemplate>
                         <a href='<%# Eval("Url") %>' target="_blank"
                            style="color:blue;text-decoration:underline">
                             <%# Eval("Name") %>
                         </a><br /> &nbsp &nbsp
                     </ItemTemplate>
                 </asp:Repeater>
         
   
    </div>
     <div class="form-group col-md-4 mb-2">
       <label for="BANK_PAY_FILE" class="m-0 mr-0 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6 justify-content-start">Bank Pay File<span style="color: #FF0000;">*</span>:</label>
       <asp:FileUpload ID="BANK_PAY_FILE" runat="server" AllowMultiple="true" CssClass="form-control-sm col-sm-9" onchange="return ValidateFileExtension(this)"></asp:FileUpload>
        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="BANK_PAY_FILE" ErrorMessage="*" ValidationGroup="save" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
         <asp:CustomValidator ID="CustomValidator4" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="BANK_PAY_FILE" ValidateEmptyText="true"></asp:CustomValidator>
       
     <asp:Repeater ID="BANK_PAY_FILE1" runat="server">
           <ItemTemplate>
               <a href='<%# Eval("Url") %>' target="_blank"
                  style="color:blue;text-decoration:underline">
                   <%# Eval("Name") %>
               </a><br />
           </ItemTemplate>
       </asp:Repeater>

   </div>
    <div class="form-group col-md-4 mb-2">
       <label for="GP_DECL_FILE" class="m-0 mr-0 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6 justify-content-start">GP DECL File<span style="color: #FF0000;">*</span>:</label>
       <asp:FileUpload ID="GP_DECL_FILE" runat="server" AllowMultiple="true" CssClass="form-control-sm col-sm-9" onchange="return ValidateFileExtension(this)"></asp:FileUpload>
        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" Display="Dynamic" ControlToValidate="GP_DECL_FILE" ErrorMessage="*" ValidationGroup="save" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
        <asp:CustomValidator ID="CustomValidator5" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="GP_DECL_FILE" ValidateEmptyText="true"></asp:CustomValidator>
       
     <asp:Repeater ID="GP_DECL_FILE1" runat="server">
           <ItemTemplate>
               <a href='<%# Eval("Url") %>' target="_blank"
                  style="color:blue;text-decoration:underline">
                   <%# Eval("Name") %>
               </a><br />
           </ItemTemplate>
       </asp:Repeater>

   </div>
    </div>
