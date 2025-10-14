 <asp:FileUpload ID="WO_ATTACH" runat="server" AllowMultiple="false" onchange="return ValidateFileExtension(this)" />
  <asp:CustomValidator ID="CustomValidator005" ErrorMessage="Required" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="WO_ATTACH" ValidateEmptyText="true" BackColor="Wheat" ForeColor="Red"></asp:CustomValidator>
