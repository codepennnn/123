<asp:TextBox 
    ID="Contractors_Establishment_Worked"
    runat="server"
    CssClass="form-control form-control-sm font-small"
    MaxLength="154">
</asp:TextBox>

<asp:RequiredFieldValidator 
    ID="rfvCEW"
    runat="server"
    ControlToValidate="Contractors_Establishment_Worked"
    ErrorMessage="This field is required."
    ValidationGroup="save"
    Display="Dynamic"
    ForeColor="Red" />

<asp:CustomValidator 
    ID="cvCEWMax"
    runat="server"
    ControlToValidate="Contractors_Establishment_Worked"
    ErrorMessage="Maximum 154 characters allowed."
    ValidationGroup="save"
    Display="Dynamic"
    ForeColor="Red"
    OnServerValidate="cvCEWMax_ServerValidate" />

protected void cvCEWMax_ServerValidate(object source, ServerValidateEventArgs args)
{
    args.IsValid = args.Value.Length <= 154;
}

    
