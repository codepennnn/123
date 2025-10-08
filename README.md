<div class="row g-2"><!-- Bootstrap gutter spacing -->
    <!-- Vendor Code -->
    <div class="col-md-4 mb-2">
        <div class="row">
            <div class="col-sm-4">
                <asp:Label AssociatedControlID="V_Code" runat="server"
                    CssClass="col-form-label col-form-label-sm fw-bold">Vendor Code:</asp:Label>
            </div>
            <div class="col-sm-8">
                <asp:TextBox ID="V_Code" runat="server" autocomplete="off"
                    CssClass="form-control form-control-sm"
                    OnTextChanged="V_Code_TextChanged" AutoPostBack="true"></asp:TextBox>
            </div>
        </div>
    </div>

    <!-- Vendor Name -->
    <div class="col-md-4 mb-2">
        <div class="row">
            <div class="col-sm-4">
                <asp:Label AssociatedControlID="V_Name" runat="server"
                    CssClass="col-form-label col-form-label-sm fw-bold">Vendor Name:</asp:Label>
            </div>
            <div class="col-sm-8">
                <asp:TextBox ID="V_Name" runat="server"
                    CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
            </div>
        </div>
    </div>

    <!-- Block/Unblock -->
    <div class="col-md-4 mb-2">
        <div class="row">
            <div class="col-sm-4">
                <asp:Label AssociatedControlID="Block_unblock" runat="server"
                    CssClass="col-form-label col-form-label-sm fw-bold">Block/Unblock:</asp:Label>
            </div>
            <div class="col-sm-8">
                <asp:DropDownList ID="Block_unblock" runat="server"
                    CssClass="form-control form-control-sm"
                    OnSelectedIndexChanged="Block_unblock_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Text="--Select--" Value="M"></asp:ListItem>
                    <asp:ListItem Text="Block" Value="RFQ_B"></asp:ListItem>
                    <asp:ListItem Text="Unblock" Value="RFQ_U"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
</div>
