   protected void Status_SelectedIndexChanged(object sender, EventArgs e)
   {
       string stat = ((RadioButtonList)summary_Record.Rows[0].FindControl("Status")).SelectedValue;
       if (stat == "Return")
       {

          rfvBOCW_APPLICABLE_AS_PER_CC.ControlToValidate = "sss";



       }
       else 
       {
          

       }
   }

                        <asp:TemplateField HeaderText="BOCW APPLICABLE AS PER CC" HeaderStyle-ForeColor="White" HeaderStyle-Width="15%">
                         <ItemTemplate>
                             <asp:DropDownList ID="BOCW_APPLICABLE_AS_PER_CC" runat="server" CssClass="form-control form-control-sm font-small" >

                                   <asp:ListItem></asp:ListItem>
                                 <asp:ListItem Value="0">0 - Not applicable</asp:ListItem>
                                 <asp:ListItem Value="1">1 - Applicable and pay by Tata Steel UISL</asp:ListItem>
                                 <asp:ListItem Value="2">2 - AApplicable and pay by Vendor</asp:ListItem>

                             </asp:DropDownList>


                             <asp:RequiredFieldValidator
                                 ID="rfvBOCW_APPLICABLE_AS_PER_CC"
                                 runat="server"
                                 ControlToValidate="BOCW_APPLICABLE_AS_PER_CC"
                                 InitialValue=""
                                 ErrorMessage="BOCW APPLICABLE AS PER CC is required."
                                 Display="Dynamic"
                                 ForeColor="Red"
                                 ValidationGroup="Save" />

                         </ItemTemplate>
                     </asp:TemplateField>



                       <div class="form-inline row">
      <div class="form-group col-md-6 mb-1 mt-2">
          <label for="Status" class="m-0 mr-0 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6 justify-content-start">Action<span style="color: #FF0000;">*</span>:</label>
          <asp:RadioButtonList ID="STATUS" runat="server" CssClass="form-check-input col-sm-8 radio" RepeatColumns="2" RepeatDirection="Horizontal" OnSelectedIndexChanged="Status_SelectedIndexChanged" AutoPostBack="true">
              <asp:ListItem Value="Approved">&nbsp&nbsp Approved</asp:ListItem>
              <asp:ListItem Value="Return">&nbsp&nbsp Return</asp:ListItem>
          </asp:RadioButtonList>
          <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="Status" ErrorMessage="Please Select Radiobutton !!" ValidationGroup="Save" ForeColor="Red"></asp:RequiredFieldValidator>
      </div>

  </div>



  i want when select radio button approved then my required validator works otherwise in return it should remove validation
