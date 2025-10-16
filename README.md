SELECT LPAD(
         MAX(TO_NUMBER(REGEXP_SUBSTR(RECEIPT_NO, '\d+$'))),
         6,
         '0'
       ) AS MAX_RECEIPT_NO
FROM CIDRDB.T_BOOKING_DETAILS
WHERE REGEXP_LIKE(RECEIPT_NO, '\d+$');




   <fieldset class="" style="border:1px solid #bfbebe;padding:5px 20px 5px 20px;border-radius:6px;overflow:auto" >
  <legend style="width:auto;border:0;font-size:14px;margin:0px 6px 0px 6px;padding:0px 5px 0px 5px;color:#0000FF"><b>WorkOrder wise summary</b></legend>
    <div class="w-100 border" style="overflow:scroll;height:130px">
        
        <cc1:detailscontainer ID="WorkOder_wise_records" runat="server" AutoGenerateColumns="False" 
              AllowPaging="true" CellPadding="4" GridLines="None" Width="100%" DataMember="App_BOCW_SAP_to_Local"
               DataSource="<%# PageRecordDataSet %>" 
              ForeColor="#333333" ShowHeaderWhenEmpty="True" OnPageIndexChanging="WorkOder_wise_records_PageIndexChanging"
              
               PageSize="10" PagerSettings-Visible="True" PagerStyle-HorizontalAlign="Center" 
        PagerStyle-Wrap="False" HeaderStyle-Font-Size="Smaller" RowStyle-Font-Size="Smaller" >
              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
              <Columns>
                 <%-- <asp:TemplateField HeaderText="ID" SortExpression="ID" Visible="False">
                      <ItemTemplate>
                          <asp:Label ID="ID" runat="server"></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="WORK ORDER" 
                      SortExpression="MANDT">
                      <ItemTemplate>
                        <B> <asp:LinkButton  ID="LinkButton1" runat="server" CommandName="select" Text='<%# Bind("V_CODE") %>'></asp:LinkButton> </B>
                      </ItemTemplate>
                      <HeaderStyle HorizontalAlign="Left" />
                  </asp:TemplateField>--%>



                      <asp:TemplateField  HeaderText="Sl No" SortExpression="SlNo" HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate >
                      <asp:Label ID="Slno" runat="server" Text='<%#Container.DataItemIndex+1 %>' CssClass="form-label"></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>


                   <asp:BoundField DataField="KONNR" HeaderText="WorkOrder No." 
                          SortExpression="KONNR" HeaderStyle-HorizontalAlign="Left" 
                          ItemStyle-HorizontalAlign="Left">
                      <HeaderStyle HorizontalAlign="Left" />
                      <ItemStyle HorizontalAlign="Left" />
                  </asp:BoundField>

                  <asp:BoundField DataField="RMWWR" HeaderText="Total Base Amount" 
                          SortExpression="RMWWR" HeaderStyle-HorizontalAlign="Left" 
                          ItemStyle-HorizontalAlign="Left">
                      <HeaderStyle HorizontalAlign="Left" />
                      <ItemStyle HorizontalAlign="Left" />
                  </asp:BoundField>

                  <asp:BoundField DataField="WMWST1" HeaderText="Total Tax (CGST+SGST)" 
                          SortExpression="WMWST1" HeaderStyle-HorizontalAlign="Left" 
                          ItemStyle-HorizontalAlign="Left">
                      <HeaderStyle HorizontalAlign="Left" />
                      <ItemStyle HorizontalAlign="Left" />
                  </asp:BoundField>
                 
                <%--  <asp:BoundField DataField="SL_NO" HeaderText="Total" 
                          SortExpression="SL_NO" HeaderStyle-HorizontalAlign="Left" 
                          ItemStyle-HorizontalAlign="Left">
                      <HeaderStyle HorizontalAlign="Left" />
                      <ItemStyle HorizontalAlign="Left" />
                  </asp:BoundField>--%>

                     

               

              </Columns>    
              <EditRowStyle BackColor="#999999" />
              <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
              <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
              <PagerSettings Mode="Numeric" />
              <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Font-Bold="True"  CssClass="pager1" />
              <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
              <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
              <SelectedRowStyle BackColor="#E2DED6" Font-Bold="False" ForeColor="#333333" />
              <SortedAscendingCellStyle BackColor="#E9E7E2" />
              <SortedAscendingHeaderStyle BackColor="#506C8C" />
              <SortedDescendingCellStyle BackColor="#FFFDF8" />
              <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
          </cc1:detailscontainer>
        
    </div>
</fieldset>
