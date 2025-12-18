 <asp:TemplateField HeaderText="Contractor establishment had worked" HeaderStyle-ForeColor="White">
         <ItemTemplate>
             <asp:TextBox ID="Contractors_Establishment_Worked" runat="server" CssClass="form-control form-control-sm font-small"  MaxLength="154">
             </asp:TextBox>

                <asp:RequiredFieldValidator 
              ID="Contractors_Establishment_Worked_V"
              runat="server"
              ControlToValidate="Contractors_Establishment_Worked"
              InitialValue=""
              ErrorMessage="This field is required."
              Display="Dynamic"
              ForeColor="Red"
              ValidationGroup="save" />


         </ItemTemplate>
 </asp:TemplateField>
