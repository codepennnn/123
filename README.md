  <div runat="server" id="ContainerDiv" class="w-100 border">
      <cc1:DetailsContainer ID="WODetails_Record" runat="server"

          AutoGenerateColumns="False" AllowPaging="false"
          CellPadding="4" GridLines="None" Width="100%" DataMember="App_Bocw_details_workorder"
          DataKeyNames="ID"
          DataSource="<%# PageRecordDataSet %>"
          ForeColor="#333333" ShowHeaderWhenEmpty="True"                              
          PageSize="10" PagerSettings-Mode="Numeric" PagerStyle-Wrap="False" HeaderStyle-Font-Size="small" RowStyle-Font-Size="Small">
        


          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
          <Columns>

              <asp:TemplateField HeaderText="ID" Visible="False" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                  <ItemTemplate>
                      <asp:Label ID="ID" runat="server"></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>


             
               <asp:TemplateField HeaderText="Work Order" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" HeaderStyle-Width="5%"  >
                   <ItemTemplate>
                       <asp:label ID="WO_NO" runat="server"  Width="100%"></asp:label>
                   </ItemTemplate>
               </asp:TemplateField>

              <asp:TemplateField HeaderText="From Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" HeaderStyle-Width="5%">
                  <ItemTemplate>
                      <asp:Label ID="FROM_DATE" runat="server"  Width="100%"></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="To Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" HeaderStyle-Width="5%">
                  <ItemTemplate>
                      <asp:Label ID="TO_DATE" runat="server"  Width="100%"></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>



                   <asp:TemplateField HeaderText="Department Code" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" HeaderStyle-Width="5%">
             <ItemTemplate>
                 <asp:Label ID="DEPT_CODE" runat="server" Width="100%"></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>



              <asp:TemplateField HeaderText="Department Desc" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" HeaderStyle-Width="5%">
                  <ItemTemplate>
                      <asp:Label ID="DepartmentDesc" runat="server" Width="100%"></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>


         


              
              <asp:TemplateField HeaderText="Nature of Work" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" HeaderStyle-Width="5%">
                  <ItemTemplate>
                      <asp:Label ID="NATURE_OF_WORK" runat="server"  Width="100%"></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>


              <asp:TemplateField HeaderText="BOCW Applicablity No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" HeaderStyle-Width="5%">
                  <ItemTemplate>
                      <asp:Label ID="BOCW_NUMBER" runat="server"  Width="100%"></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="BOCW Applicablity Desc." HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White" HeaderStyle-Width="5%">
                  <ItemTemplate>
                      <asp:Label ID="BOCW_DESC" runat="server"  Width="100%"></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="BOCW APPLICABLE AS PER CC" HeaderStyle-ForeColor="White">
                  <ItemTemplate>
                      <asp:DropDownList ID="BOCW_APPLICABLE_AS_PER_CC" runat="server" CssClass="form-control form-control-sm font-small" Width="300px" Enabled="false">

                          <asp:ListItem></asp:ListItem>
                          <asp:ListItem Value="0">0 - Not applicable</asp:ListItem>
                          <asp:ListItem Value="1">1 - Applicable and pay by Tata Steel UISL</asp:ListItem>
                          <asp:ListItem Value="2">2 - AApplicable and pay by Vendor</asp:ListItem>

                      </asp:DropDownList>


                 

                  </ItemTemplate>
              </asp:TemplateField>
    


          </Columns>
          <EditRowStyle BackColor="#999999" />
          <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
          <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />

          <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"
              Font-Bold="True" CssClass="pager1" />
          <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Font-Bold="True" />
          <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
          <SelectedRowStyle BackColor="#E2DED6" Font-Bold="False" ForeColor="#333333" />
          <SortedAscendingCellStyle BackColor="#E9E7E2" />
          <SortedAscendingHeaderStyle BackColor="#506C8C" />
          <SortedDescendingCellStyle BackColor="#FFFDF8" />
          <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
      </cc1:DetailsContainer>
  </div>
