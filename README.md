<style>
    .attachment-container {
        display: block;
        margin-top: 4px;
    }

    .attachment-link {
        display: block;        /* forces new line */
        color: blue;
        text-decoration: underline;
        margin-bottom: 4px;
        word-break: break-word;
    }
</style>


 <div id="upload_file" runat="server" class="form-inline row">
         <div class="form-group col-md-6 mb-2">
            <label for="Leave_Register_Upload" class="m-0 mr-0 p-0 col-form-label-sm col-sm-5 font-weight-bold fs-6 justify-content-start">Bonus Register File(Pdf & Excel both)<span style="color: #FF0000;">*</span>:</label>
             <asp:FileUpload ID="Bonus_Register_Upload" runat="server" AllowMultiple="true" CssClass="form-control-sm col-sm-4" onchange="validateFileType()"></asp:FileUpload>
            
             <div class="attachment-container">
                 <asp:Repeater ID="Bonus_Register_Upload1" runat="server">
                        <ItemTemplate>
                            <a href='<%# Eval("Url") %>' target="_blank" class="attachment-link">
                                <%# Eval("Name") %>
                            </a><br /> &nbsp &nbsp
                        </ItemTemplate>
                    </asp:Repeater>
          </div>
            
             
             
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
