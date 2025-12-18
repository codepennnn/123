<div class="w-100 border" style="overflow:auto;height:250px;">
          <cc1:DetailsContainer ID="HalfYearly_Records" runat="server" AutoGenerateColumns="False"
              AllowPaging="true" CellPadding="4" GridLines="None" Width="100%" DataMember="App_Half_Yearly_Details"
              DataKeyNames="ID,VCode,Year,Period" DataSource="<%# PageRecordsDataSet %>"
              ForeColor="#333333" ShowHeaderWhenEmpty="True" 
              OnSelectedIndexChanged="HalfYearly_Records_SelectedIndexChanged" PageSize="10" PagerSettings-Visible="True" PagerStyle-HorizontalAlign="Center"
              PagerStyle-Wrap="False" HeaderStyle-Font-Size="Smaller" RowStyle-Font-Size="Smaller">
      <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
      <Columns>
          <asp:TemplateField HeaderText="ID" SortExpression="ID" Visible="False">
              <ItemTemplate>
                  <asp:Label ID="ID" runat="server"></asp:Label>
              </ItemTemplate>
          </asp:TemplateField>

          <asp:TemplateField HeaderText="Sl No." SortExpression="Sl_No"
              HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
              <ItemTemplate><%# Container.DataItemIndex + 1 + "."%> </ItemTemplate>
          </asp:TemplateField>




               <asp:TemplateField HeaderText="Vendor Code" 
                SortExpression="VCode">
                <ItemTemplate>
                <B> <asp:LinkButton ID="LinkButton1"  runat="server"  CommandName="select" ForeColor="#003399" Text='<%# Bind("VCode") %>' ></asp:LinkButton> </B>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>



                 <asp:TemplateField HeaderText="Period" SortExpression="Period">
                     <ItemTemplate>
                         <asp:Label ID="Period" runat="server"></asp:Label>&nbsp
                     </ItemTemplate>
                 </asp:TemplateField>


             <asp:TemplateField HeaderText="Year" SortExpression="Year">
                 <ItemTemplate>
                     <asp:Label ID="Year" runat="server"></asp:Label>&nbsp
                 </ItemTemplate>
             </asp:TemplateField>



          
          <asp:TemplateField HeaderText="Status" SortExpression="Status">
              <ItemTemplate>
                  <asp:Label ID="Status" runat="server"></asp:Label>&nbsp
              </ItemTemplate>
          </asp:TemplateField>


          
              <asp:TemplateField HeaderText="Created On" SortExpression="CreatedOn">
                  <ItemTemplate>
                      <asp:Label 
                          ID="CreatedOn" 
                          runat="server" 
                          Text='<%# Eval("CreatedOn", "{0:dd-MMM-yyyy hh:mm tt}") %>'>
                      </asp:Label>&nbsp;
                     </ItemTemplate>
                  </asp:TemplateField>

              <asp:TemplateField HeaderText="Days Count" SortExpression="dayscountCreatedOn">
              <ItemTemplate>
                  <asp:Label ID="dayscountCreatedOn" runat="server"></asp:Label>&nbsp
              </ItemTemplate>
          </asp:TemplateField>

                
          <asp:TemplateField HeaderText="Resubmitted On" SortExpression="ResubmittedOn">
              <ItemTemplate>
                  <asp:Label ID="ResubmitedOn" runat="server"></asp:Label>&nbsp
              </ItemTemplate>
          </asp:TemplateField>

          <asp:TemplateField HeaderText="Resubmitted Days Count" SortExpression="dayscountResub">
              <ItemTemplate>
                  <asp:Label ID="dayscountResub" runat="server"></asp:Label>&nbsp
              </ItemTemplate>
          </asp:TemplateField>



         


        



      </Columns>
      <EditRowStyle BackColor="#999999" />
      <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
      <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
      <PagerSettings Mode="Numeric" />
      <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Font-Bold="True" CssClass="pager1" />
      <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
      <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
      <SelectedRowStyle BackColor="#E2DED6" Font-Bold="False" ForeColor="#333333" />
      <SortedAscendingCellStyle BackColor="#E9E7E2" />
      <SortedAscendingHeaderStyle BackColor="#506C8C" />
      <SortedDescendingCellStyle BackColor="#FFFDF8" />
      <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
          </cc1:DetailsContainer>

    </div>


     public DataSet GetRecords(System.Collections.Specialized.StringDictionary FilterConditions, int totalPagesize, string sortExpression)
 {
               

     //string strSQL = "select * ,DATEDIFF(day, CreatedOn,getdate()) as dayscountCreatedOn,DATEDIFF(day, ResubmitedOn, getdate()) as dayscountResub from App_Half_Yearly_Details  ";
     string strSQL = "SELECT * FROM ( SELECT *, DATEDIFF(day, CreatedOn, GETDATE()) AS dayscountCreatedOn, DATEDIFF(day, ResubmitedOn, GETDATE()) AS dayscountResub, ROW_NUMBER() OVER (PARTITION BY Year,Period,vcode ORDER BY CreatedOn DESC) AS rn FROM App_Half_Yearly_Details ) A ";
     DataHelper dh = new DataHelper();
     Dictionary<string, object> objParam = null;

     if (FilterConditions != null && FilterConditions.Count > 0)
     {
         strSQL += " where A.rn = 1 and";
         strSQL += " Status='" + FilterConditions["Status"] + "'     ";

         if (FilterConditions["Search_With"] != null)
             strSQL += "and " + FilterConditions["Search_With"] + " like '%" + FilterConditions["Enter_Detail"] + "'   ";

     }
     if (sortExpression != "")
         strSQL += "order by " + sortExpression;
     return dh.GetDataset(strSQL, "App_Half_Yearly_Details", objParam);

 }



 why my createdon and resubmitedon showing only date not with time
