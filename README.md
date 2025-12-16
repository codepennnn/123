<asp:Panel ID="pnlFilters" runat="server">
    <div class="container-fluid">
        <div class="row g-2 align-items-end">

            <div class="col-lg-3 col-md-4 col-sm-6">
                <label class="form-label fw-bold small">Year</label>
                <asp:DropDownList ID="ddlYear" runat="server"
                    CssClass="form-select form-select-sm" />
            </div>

            <div class="col-lg-3 col-md-4 col-sm-6">
                <label class="form-label fw-bold small">Month</label>
                <asp:DropDownList ID="ddlMonth" runat="server"
                    CssClass="form-select form-select-sm" />
            </div>

            <div class="col-lg-3 col-md-4 col-sm-6">
                <label class="form-label fw-bold small">Aadhar</label>
                <asp:DropDownList ID="ddlAadhar" runat="server"
                    CssClass="form-select form-select-sm"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlAadhar_SelectedIndexChanged" />
            </div>

            <div class="col-lg-3 col-md-4 col-sm-6">
                <label class="form-label fw-bold small">Name</label>
                <asp:TextBox ID="txtName" runat="server"
                    ReadOnly="true"
                    CssClass="form-control form-control-sm" />
            </div>

            <div class="col-lg-3 col-md-4 col-sm-6">
                <label class="form-label fw-bold small">Workman Sl No</label>
                <asp:TextBox ID="txtSlNo" runat="server"
                    ReadOnly="true"
                    CssClass="form-control form-control-sm" />
            </div>

            <div class="col-lg-3 col-md-4 col-sm-6">
                <label class="form-label fw-bold small">Workman Category</label>
                <asp:TextBox ID="txtCategory" runat="server"
                    ReadOnly="true"
                    CssClass="form-control form-control-sm" />
            </div>

            <div class="col-lg-3 col-md-4 col-sm-6">
                <label class="form-label fw-bold small">Vendor Name</label>
                <asp:TextBox ID="txtVname" runat="server"
                    ReadOnly="true"
                    CssClass="form-control form-control-sm" />
            </div>

            <div class="col-lg-3 col-md-4 col-sm-6">
                <label class="form-label fw-bold small">Vendor Code</label>
                <asp:TextBox ID="txtVcode" runat="server"
                    ReadOnly="true"
                    CssClass="form-control form-control-sm" />
            </div>

            <!-- Buttons -->
            <div class="col-lg-3 col-md-4 col-sm-6">
                <label class="form-label invisible">Action</label>
                <div class="d-flex gap-2">
                    <asp:Button ID="btnView" runat="server"
                        CssClass="btn btn-sm btn-primary"
                        Text="View" OnClick="btnView_Click" />

                    <asp:Button ID="btnClear" runat="server"
                        CssClass="btn btn-sm btn-warning"
                        Text="Clear" OnClick="btnClear_Click" />
                </div>
            </div>

        </div>
    </div>
</asp:Panel>
