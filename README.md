    <div class="col-lg-4 mb-1 form-row">
        <div class="col-lg-5">
            <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6">Bocw Applicable Number :<span class="text-danger"></span></label>
        </div>
        <div class="col-lg-7">
            <asp:TextBox ID="BOCW_APPLICABLE" Enabled="false" runat="server" CssClass="form-control form-control-sm textboxstyle" MaxLength="3" />
            <asp:CustomValidator ID="CustomValidator19" ErrorMessage="Required" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="BOCW_APPLICABLE" ValidateEmptyText="true" BackColor="Wheat" ForeColor="Red"></asp:CustomValidator>
        </div>
    </div>


    <div class="col-lg-4 mb-1 form-row">
        <div class="col-lg-5">
            <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6">Bocw Registration Number :<span class="text-danger"></span></label>
        </div>
        <div class="col-lg-7">
         
            <asp:DropDownList ID="REGIS_NUMBER" runat="server" CssClass="form-control form-control-sm"
                DataSource="<%# PageDDLDataset %>" DataMember="BOCW_Reg_No"
                DataTextField="BOCW_RegNo" DataValueField="BOCW_RegNo"
                OnSelectedIndexChanged="REGIS_NUMBER_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:DropDownList>
           
        </div>
    </div>

    <div class="col-lg-4 mb-1 form-row">
        <div class="col-lg-5">
            <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6">Bocw Registration Date :<span class="text-danger"></span></label>
        </div>
        <div class="col-lg-7">
            <asp:TextBox ID="REGIS_DATE" runat="server" CssClass="form-control form-control-sm textboxstyle" Enabled="false" AutoComplete="off" />
           
        </div>
    </div>

    <div class="col-lg-4 mb-1 form-row">
        <div class="col-lg-5">
            <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6">Bocw Registration Valid Upto :<span class="text-danger"></span></label>
        </div>
        <div class="col-lg-7">
            <asp:TextBox ID="REGIS_VALID_UPTO" runat="server" CssClass="form-control form-control-sm textboxstyle" Enabled="false" AutoComplete="off" />
           
        </div>
    </div>

    <div class="col-lg-4 mb-1 form-row">
        <div class="col-lg-5">
            <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6">Number of Employee for which Bocw obtained :<span class="text-danger"></span></label>
        </div>
        <div class="col-lg-7">
            <asp:TextBox ID="NO_EMP_BOCW_OBTAINED" runat="server" CssClass="form-control form-control-sm textboxstyle" Enabled="false" TextMode="Number" placeholder="only Numbers" />
          
        </div>
    </div>
