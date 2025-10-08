
                if (e.Row.Cells[9].Text.Trim() != "" && e.Row.Cells[9].Text.Trim() != null && e.Row.Cells[9].Text.Trim() != "&nbsp;")
                {
                    e.Row.Cells[9].Text = (Convert.ToDateTime(e.Row.Cells[9].Text)).ToString("dd-MM-yyyy");
                }

                if (e.Row.Cells[10].Text.Trim() != "" && e.Row.Cells[10].Text.Trim() != null && e.Row.Cells[9].Text.Trim() != "&nbsp;")
                {
                    e.Row.Cells[10].Text = (Convert.ToDateTime(e.Row.Cells[10].Text)).ToString("dd-MM-yyyy");
                }



                if (e.Row.Cells[12].Text.Trim() != "" && e.Row.Cells[12].Text.Trim() != null && e.Row.Cells[12].Text.Trim() != "&nbsp;")
                {
                    e.Row.Cells[12].Text = (Convert.ToDateTime(e.Row.Cells[12].Text)).ToString("dd-MM-yyyy");
                }


 <cc1:DetailsContainer ID="Vendor_Block_Unblock_RFQ_recordsGrid" runat="server" AutoGenerateColumns="False"
     OnRowDataBound="Vendor_Block_Unblock_RFQ_recordsGrid_RowDataBound"
     AllowPaging="false" CellPadding="4" GridLines="None" Width="120%" DataMember="App_RFQ_Block_Unblock"
     DataKeyNames="ID" DataSource="<%# PageRecordsDataSet %>"
     ForeColor="#333333" ShowHeaderWhenEmpty="True"
     PageSize="8" PagerSettings-Visible="True" PagerStyle-HorizontalAlign="Center"
     PagerStyle-Wrap="false" HeaderStyle-Font-Size="Smaller" RowStyle-Font-Size="Smaller">
     <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
     <Columns>
         <asp:TemplateField HeaderText="ID" SortExpression="ID" Visible="false">
             <ItemTemplate>
                 <asp:Label ID="ID" runat="server" Visible="False"></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>

         <asp:TemplateField HeaderText="Sl No." SortExpression="Sl_No"
             HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
             <ItemTemplate><%# Container.DataItemIndex + 1 + "."%></ItemTemplate>
         </asp:TemplateField>


            <asp:BoundField DataField="Createdon" HeaderText="Action Date" ItemStyle-Width="150px"
            SortExpression="Createdon" HeaderStyle-HorizontalAlign="Left"
            ItemStyle-HorizontalAlign="Left">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:BoundField>





         <asp:BoundField DataField="V_Code" HeaderText="Vendor Code"
             SortExpression="V_Code" HeaderStyle-HorizontalAlign="Left"
             ItemStyle-HorizontalAlign="Left">
             <HeaderStyle HorizontalAlign="Left" />
             <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>

         <asp:BoundField DataField="V_NAME" HeaderText="Vendor Name"
             SortExpression="V_Name" HeaderStyle-HorizontalAlign="Left"
             ItemStyle-HorizontalAlign="Left">
             <HeaderStyle HorizontalAlign="Left" />
             <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
         <asp:BoundField DataField="Email" HeaderText="Vendor Email"
             SortExpression="Email" HeaderStyle-HorizontalAlign="Left"
             ItemStyle-HorizontalAlign="Left">
             <HeaderStyle HorizontalAlign="Left" />
             <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
         <asp:BoundField DataField="CONTACT_NO" HeaderText="Contact No."
             SortExpression="CONTACT_NO" HeaderStyle-HorizontalAlign="Left"
             ItemStyle-HorizontalAlign="Left">
             <HeaderStyle HorizontalAlign="Left" />
             <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
         <asp:BoundField DataField="Reason" HeaderText="Reason"
             SortExpression="Reason" HeaderStyle-HorizontalAlign="Left"
             ItemStyle-HorizontalAlign="Left">
             <HeaderStyle HorizontalAlign="Left" />
             <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>
         <asp:BoundField DataField="Status" HeaderText="Status"
             SortExpression="Status" HeaderStyle-HorizontalAlign="Left"
             ItemStyle-HorizontalAlign="Left">
             <HeaderStyle HorizontalAlign="Left" />
             <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>

         <asp:BoundField DataField="Internal_Remarks" HeaderText="Remarks"
             SortExpression="Internal_Remarks" HeaderStyle-HorizontalAlign="Left"
             ItemStyle-HorizontalAlign="Left">
             <HeaderStyle HorizontalAlign="Left" />
             <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>


         <asp:BoundField DataField="Block_From" HeaderText="From Date" ItemStyle-Width="200px"
             SortExpression="Block_From" HeaderStyle-HorizontalAlign="Left"
             ItemStyle-HorizontalAlign="Left">
             <HeaderStyle HorizontalAlign="Left" />
             <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>



         <asp:BoundField DataField="Block_To" HeaderText="To Date" ItemStyle-Width="200px"
             SortExpression="Block_To" HeaderStyle-HorizontalAlign="Left"
             ItemStyle-HorizontalAlign="Left">
             <HeaderStyle HorizontalAlign="Left" />
             <ItemStyle HorizontalAlign="Left" />
         </asp:BoundField>





            <asp:TemplateField HeaderText="For Department Purpose" SortExpression="Attachment_Dept" >
                                 <ItemTemplate>
                                     <asp:BulletedList runat="server" ID="Attachment_Dept" CssClass="attachment-list" DisplayMode="HyperLink" OnClick="Attachment_Dept_Click" />
                                 </ItemTemplate>
                             </asp:TemplateField> 


                           <%--   <asp:TemplateField HeaderText="For Vendor Purpose" SortExpression="Attachment_Vendor" >
                                 <ItemTemplate>
                                     <asp:BulletedList runat="server" CssClass="attachment-list" ID="Attachment_VendorLink" DisplayMode="HyperLink"/>
                                 </ItemTemplate>
                             </asp:TemplateField> --%>


      



     </Columns>
     <EditRowStyle BackColor="#999999" />
     <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
     <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
     <PagerSettings Mode="Numeric" />
     <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Font-Bold="True" CssClass="pager1" />
     <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
     <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
     <SelectedRowStyle BackColor="#E2DED6" Font-Bold="true" ForeColor="#333333" />
     <SortedAscendingCellStyle BackColor="#E9E7E2" />
     <SortedAscendingHeaderStyle BackColor="#506C8C" />
     <SortedDescendingCellStyle BackColor="#FFFDF8" />
     <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
 </cc1:DetailsContainer>
