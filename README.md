 <asp:TemplateField HeaderText="Attachment" SortExpression="Attachment">
     <ItemTemplate>
         <asp:BulletedList runat="server" ID="Attachment" CssClass="attachment-list" DisplayMode="HyperLink">
        <asp:ListItem Text='<%# Eval("Attachment").ToString().Length > 20 ? Eval("Attachment").ToString().Substring(0,20) + "..." : Eval("Attachment").ToString() %>' Value='<%# Eval("Attachment") %>' />
         </asp:BulletedList>
     </ItemTemplate>

     </asp:TemplateField>


     Parser Error
Description: An error occurred during the parsing of a resource required to service this request. Please review the following specific parse error details and modify your source file appropriately.

Parser Error Message: Databinding expressions are only supported on objects that have a DataBinding event. System.Web.UI.WebControls.ListItem does not have a DataBinding event.

Source Error:


Line 565:                                <ItemTemplate>
Line 566:                                    <asp:BulletedList runat="server" ID="Attachment" CssClass="attachment-list" DisplayMode="HyperLink">
Line 567:                                   <asp:ListItem Text='<%# Eval("Attachment").ToString().Length > 20 ? Eval("Attachment").ToString().Substring(0,20) + "..." : Eval("Attachment").ToString() %>' Value='<%# Eval("Attachment") %>' />
Line 568:                                    </asp:BulletedList>
Line 569:                                </ItemTemplate>
