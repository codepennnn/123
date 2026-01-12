<asp:TemplateField HeaderText="From Date"
    HeaderStyle-HorizontalAlign="Center"
    HeaderStyle-ForeColor="White"
    HeaderStyle-Width="7%">

    <ItemStyle HorizontalAlign="Center" />

    <ItemTemplate>
        <asp:Label ID="FROM_DATE" runat="server"></asp:Label>
    </ItemTemplate>
</asp:TemplateField>

/* Hover effect */
.custom-grid tr:hover td {
    background-color: #eef5ff;
    transition: background-color 0.2s ease-in-out;
}

/* Sticky header */
.custom-grid th {
    position: sticky;
    top: 0;
    z-index: 10;
}
