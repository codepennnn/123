<asp:Repeater ID="WAGE_REGIS_FILE1" runat="server">
    <ItemTemplate>
        <a href='<%# Eval("Url") %>' target="_blank"
           class="attachment-link">
            <%# Eval("Name") %>
        </a>
    </ItemTemplate>
</asp:Repeater>

.attachment-link {
    display: block;
    color: blue;
    text-decoration: underline;
    margin-bottom: 4px;
}
