                  <div class="form-inline row">


                      <div class="form-group col-md-4 col-margin mb-1">
                          <asp:Label for="V_Code" runat="server" CssClass="m-0 mr-2 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6">Vendor Code:</asp:Label>
                          <asp:TextBox ID="V_Code" runat="server" autocomplete="off"  CssClass="form-control form-control-sm col-sm-8" OnTextChanged="V_Code_TextChanged" AutoPostBack="true" ></asp:TextBox>
                      </div>


                      <div class="form-group col-md-4 col-margin mb-1">
                          <asp:Label for="V_Name" runat="server" CssClass="m-0 mr-2 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6">Vendor Name:</asp:Label>
                          <asp:TextBox ID="V_Name" runat="server" CssClass="form-control form-control-sm col-sm-8" Enabled="false"></asp:TextBox>
                      </div>

                      <div class="form-group col-md-4 col-margin mb-1">
                          <asp:Label for="Block_unblock" runat="server" CssClass="m-0 mr-2 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6">Block/unblock:</asp:Label>
                          <asp:DropDownList ID="Block_unblock" runat="server"
                              CssClass="form-control form-control-sm col-sm-8"
                              OnSelectedIndexChanged="Block_unblock_SelectedIndexChanged"
                              AutoPostBack="true">

                              <asp:ListItem Text="--Select--" Value="M"></asp:ListItem>
                              <asp:ListItem Text="Block" Value="RFQ_B"></asp:ListItem>
                              <asp:ListItem Text="Unblock" Value="RFQ_U"></asp:ListItem>
                          </asp:DropDownList>
                      </div>



                      <div class="form-group col-md-4 col-margin mb-1">
                          <asp:Label for="Email" runat="server" CssClass="m-0 mr-2 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6">Vendor Email:</asp:Label>
                          <asp:TextBox ID="Email" runat="server" CssClass="form-control form-control-sm col-sm-8" Enabled="false"></asp:TextBox>
                      </div>


<%--                      <div class="form-group col-md-4 col-margin mb-1">
                           <asp:Label for="CONTACT_NO" runat="server" CssClass="m-0 mr-2 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6">Vendor CONTACT_NO:</asp:Label>
                         <asp:TextBox ID="CONTACT_NO" runat="server" CssClass="form-control form-control-sm col-sm-8" Enabled="false"></asp:TextBox>
                    </div>--%>



                      <div class="form-group col-md-4 col-margin mb-1">
                          <asp:Label for="Reason" runat="server" CssClass="m-0 mr-2 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6">Reason:</asp:Label>
                          <asp:DropDownList ID="Reason" runat="server"
                               DataMember="RFQ_Reason" 

                              DataTextField="Reason"
                              DataValueField="Reason" 

                              AutoPostBack="true"
                              DataSource="<%# PageDDLDataset %>" 
                              CssClass="form-control form-control-sm col-sm-8"
                              OnSelectedIndexChanged="Reason_SelectedIndexChanged"
                             ></asp:DropDownList>
                          <asp:TextBox ID="Status" runat="server" CssClass="form-control form-control-sm col-sm-8" Visible="false"></asp:TextBox>
                      </div>


                      <div class="form-group col-md-4 col-margin mb-1">
                          <asp:Label for="Internal_Remarks" runat="server" CssClass="m-0 mr-2 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6">Remarks:</asp:Label>
                          <asp:TextBox ID="Internal_Remarks" runat="server" autocomplete="off"  CssClass="form-control form-control-sm col-sm-8" Rows="2" TextMode="MultiLine"></asp:TextBox>
                      </div>






                      <div class="form-group col-md-4 col-margin mb-1">
                          <asp:Label for="Block_From" runat="server" CssClass="m-0 mr-2 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6"> From Date :</asp:Label>
                          <asp:TextBox ID="Block_From" runat="server" autocomplete="off" CssClass="form-control form-control-sm col-sm-8"></asp:TextBox>
                          <ask:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="TopRight" TargetControlID="Block_From" TodaysDateFormat="dd/MM/yyyy"></ask:CalendarExtender>
                      </div>


                      <div class="form-group col-md-4 col-margin mb-1">
                          <asp:Label for="Block_To" runat="server" CssClass="m-0 mr-2 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6">To Date:</asp:Label>
                          <asp:TextBox ID="Block_To" runat="server"  autocomplete="off"  CssClass="form-control form-control-sm col-sm-8"></asp:TextBox>
                          <ask:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="TopRight" TargetControlID="Block_To" TodaysDateFormat="dd/MM/yyyy"></ask:CalendarExtender>
                      </div>




                      <div class="form-group col-md-4 col-margin mb-1">
                          <asp:Label for="Createdon" runat="server" CssClass="m-0 mr-2 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6">Action Date:</asp:Label>
                          <asp:TextBox ID="Createdon" runat="server" CssClass="form-control form-control-sm col-sm-8" Enabled="false"></asp:TextBox>
                      </div>


                  </div>
