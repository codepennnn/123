/* Common attachment layout */
.attach-group {
    display: flex;
    flex-direction: column;
    gap: 6px;
}

.attach-label {
    font-weight: 600;
    font-size: 13px;
    margin-bottom: 4px;
}

.attach-upload {
    width: 70%;
}

.attach-list {
    padding-left: 2px;
}

.attach-link {
    display: block;
    color: #0b5ed7;
    text-decoration: underline;
    font-size: 12px;
    margin-bottom: 4px;
    word-break: break-word;
}









<div class="row">
    <div class="col-md-6 mb-3">
        <div class="attach-group">
            <label class="attach-label">
                Leave Register File (PDF)
                <span class="text-danger">*</span>
            </label>

            <asp:FileUpload ID="Leave_Register_Upload"
                runat="server"
                AllowMultiple="true"
                CssClass="form-control-sm attach-upload"
                onchange="validateFileType()" />

            <div class="attach-list">
                <asp:Repeater ID="Leave_Register_Upload1" runat="server">
                    <ItemTemplate>
                        <a href='<%# Eval("Url") %>' target="_blank" class="attach-link">
                            <%# Eval("Name") %>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <div class="col-md-6 mb-3">
        <div class="attach-group">
            <label class="attach-label">
                Bank Statement File (PDF)
                <span class="text-danger">*</span>
            </label>

            <asp:FileUpload ID="Bank_Statement_Upload"
                runat="server"
                AllowMultiple="true"
                CssClass="form-control-sm attach-upload"
                onchange="validateFileType1()" />

            <div class="attach-list">
                <asp:Repeater ID="Bank_Statement_Upload1" runat="server">
                    <ItemTemplate>
                        <a href='<%# Eval("Url") %>' target="_blank" class="attach-link">
                            <%# Eval("Name") %>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</div>




<div class="row">
    <div class="col-md-6 mb-3">
        <div class="attach-group">
            <label class="attach-label">
                PF / ESI File
                <span class="text-danger">*</span>
            </label>

            <asp:FileUpload ID="PF_ESIAttach"
                runat="server"
                AllowMultiple="true"
                CssClass="form-control-sm attach-upload"
                onchange="return ValidateFileExtension(this)" />

            <div class="attach-list">
                <asp:Repeater ID="PF_ESIAttach1" runat="server">
                    <ItemTemplate>
                        <a href='<%# Eval("Url") %>' target="_blank" class="attach-link">
                            <%# Eval("Name") %>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</div>



<div class="row">
    <div class="col-md-4 mb-3">
        <div class="attach-group">
            <label class="attach-label">
                Wage Register File
                <span class="text-danger">*</span>
            </label>

            <asp:FileUpload ID="WAGE_REGIS_FILE"
                runat="server"
                AllowMultiple="true"
                CssClass="form-control-sm attach-upload"
                onchange="return ValidateFileExtension(this)" />

            <div class="attach-list">
                <asp:Repeater ID="WAGE_REGIS_FILE1" runat="server">
                    <ItemTemplate>
                        <a href='<%# Eval("Url") %>' target="_blank" class="attach-link">
                            <%# Eval("Name") %>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <div class="col-md-4 mb-3">
        <div class="attach-group">
            <label class="attach-label">
                Bank Pay File
                <span class="text-danger">*</span>
            </label>

            <asp:FileUpload ID="BANK_PAY_FILE"
                runat="server"
                AllowMultiple="true"
                CssClass="form-control-sm attach-upload"
                onchange="return ValidateFileExtension(this)" />

            <div class="attach-list">
                <asp:Repeater ID="BANK_PAY_FILE1" runat="server">
                    <ItemTemplate>
                        <a href='<%# Eval("Url") %>' target="_blank" class="attach-link">
                            <%# Eval("Name") %>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <div class="col-md-4 mb-3">
        <div class="attach-group">
            <label class="attach-label">
                GP Decl File
                <span class="text-danger">*</span>
            </label>

            <asp:FileUpload ID="GP_DECL_FILE"
                runat="server"
                AllowMultiple="true"
                CssClass="form-control-sm attach-upload"
                onchange="return ValidateFileExtension(this)" />

            <div class="attach-list">
                <asp:Repeater ID="GP_DECL_FILE1" runat="server">
                    <ItemTemplate>
                        <a href='<%# Eval("Url") %>' target="_blank" class="attach-link">
                            <%# Eval("Name") %>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</div>



