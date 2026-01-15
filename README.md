/* Label */
.attachment-label {
    display: block;
    font-weight: 600;
    font-size: 13px;
    margin-bottom: 6px;
}

/* Upload + list wrapper */
.upload-box {
    display: flex;
    flex-direction: column;
    gap: 6px;
}

/* File upload */
.upload-input {
    width: 70%;
}

/* Attachment list */
.attachment-container {
    display: block;
    padding-left: 2px;
}

/* Each file */
.attachment-link {
    display: block;
    color: #0b5ed7;
    text-decoration: underline;
    font-size: 12px;
    margin-bottom: 4px;
    word-break: break-word;
}


<div class="form-group col-md-6 mb-3">
    <label class="attachment-label">
        Bonus Register File (Pdf & Excel both)
        <span class="text-danger">*</span>
    </label>

    <div class="upload-box">
        <asp:FileUpload ID="Bonus_Register_Upload"
            runat="server"
            AllowMultiple="true"
            CssClass="form-control-sm upload-input"
            onchange="validateFileType()" />

        <div class="attachment-container">
            <asp:Repeater ID="Bonus_Register_Upload1" runat="server">
                <ItemTemplate>
                    <a href='<%# Eval("Url") %>' target="_blank" class="attachment-link">
                        <%# Eval("Name") %>
                    </a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>



<div class="form-group col-md-6 mb-3">
    <label class="attachment-label">
        Bank Statement File (PDF)
        <span class="text-danger">*</span>
    </label>

    <div class="upload-box">
        <asp:FileUpload ID="Bank_Statement_Upload"
            runat="server"
            AllowMultiple="true"
            CssClass="form-control-sm upload-input"
            onchange="validateFileType1()" />

        <div class="attachment-container">
            <asp:Repeater ID="Bank_Statement_Upload1" runat="server">
                <ItemTemplate>
                    <a href='<%# Eval("Url") %>' target="_blank" class="attachment-link">
                        <%# Eval("Name") %>
                    </a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>

