     protected void Remarks_grid_RowDataBound(object sender, GridViewRowEventArgs e)
     {
         if ((e.Row.Cells[1].Text.StartsWith("( CC --")) || (e.Row.Cells[1].Text.StartsWith("\n( CC --")))
         {
             e.Row.Attributes.CssStyle.Value = "color: Blue;Font-weight:bold";
         }

       

     }

     and my remarks saving like this -   r["Remarks"] =  "( CC --" + System.DateTime.Now.ToString("dd/MM/yyyy") + " -- " + Remarks_CC + ") ||";
output - ( CC --16/12/2025 -- ( CC --16/12/2025 -- try again) || test) ||


                         <div id="div_Remarks" runat="server">
             <fieldset class="" style="border:1px solid #bfbebe;padding:5px 20px 5px 20px;border-radius:6px">
               <legend style="width:auto;border:0;font-size:14px;margin:0px 6px 0px 6px;padding:0px 5px 0px 5px;color:#0000FF"><b>Remarks History</b></legend>
  
                 <asp:GridView ID="Remarks_grid" runat="server" AutoGenerateColumns="false"
                     AllowPaging="false" CellPadding="0" GridLines="Both" Width="100%" 
                               ForeColor="#333333" ShowHeaderWhenEmpty="False" OnRowDataBound="Remarks_grid_RowDataBound"
                               PageSize="10" PagerSettings-Visible="false" PagerStyle-HorizontalAlign="Center" 
                               PagerStyle-Wrap="True" HeaderStyle-Font-Size="Smaller" RowStyle-Font-Size="Small" CssClass="m-auto border border-info" 
                               HeaderStyle-HorizontalAlign="Center"  RowStyle-ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"> 
                               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                         <Columns> 
                               <asp:TemplateField  Visible="False">
                                           <ItemTemplate >
                                           <asp:Label ID="ID" runat="server"></asp:Label>
                                           
                                       </ItemTemplate>
                                   </asp:TemplateField>
                               <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Width="30" />    
                
                         </Columns>
                               <EditRowStyle BackColor="#999999" />
                               <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                               <HeaderStyle BackColor="#328cda" Font-Bold="True" ForeColor="White" />
                               <PagerSettings Mode="Numeric" />
                               <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Font-Bold="True"  CssClass="pager1" />
                               <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                               <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                               <SelectedRowStyle BackColor="#E2DED6" Font-Bold="False" ForeColor="#333333" />
                               <SortedAscendingCellStyle BackColor="#E9E7E2" />
                               <SortedAscendingHeaderStyle BackColor="#506C8C" />
                               <SortedDescendingCellStyle BackColor="#FFFDF8" />
                               <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                  </asp:GridView>
 
               </fieldset>
</div>
