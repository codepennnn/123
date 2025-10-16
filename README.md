<asp:TemplateField HeaderStyle-Width="3%" HeaderText="">
    <HeaderTemplate>
        <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" ToolTip="Select All" />
    </HeaderTemplate>
    <ItemTemplate>
        <asp:CheckBox ID="chkSelect" runat="server" />
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>


<script type="text/javascript">
    function checkAll(source) {
        var grid = document.getElementById('<%= WorkOder_wise_records.ClientID %>');
        var checkBoxes = grid.getElementsByTagName("input");
        for (var i = 0; i < checkBoxes.length; i++) {
            if (checkBoxes[i].type == "checkbox" && checkBoxes[i].id.indexOf("chkSelect") > -1) {
                checkBoxes[i].checked = source.checked;
            }
        }
    }
</script>



protected void btnProcess_Click(object sender, EventArgs e)
{
    foreach (GridViewRow row in WorkOder_wise_records.Rows)
    {
        CheckBox chk = (CheckBox)row.FindControl("chkSelect");
        if (chk != null && chk.Checked)
        {
            string workOrderNo = ((Label)row.FindControl("Slno"))?.Text;
            // Do your logic here, e.g., save or process selected work orders
        }
    }
}
