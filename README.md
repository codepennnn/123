<asp:TemplateField HeaderStyle-Width="3%" HeaderText="" HeaderStyle-HorizontalAlign="Left">
    <HeaderTemplate>
        <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" ToolTip="Select All" />
    </HeaderTemplate>
    <ItemTemplate>
        <asp:CheckBox ID="chkSelect" runat="server" onclick="updateTotals();" />
        <asp:HiddenField ID="hfAmount" runat="server" Value='<%# Eval("RMWWR") %>' />
        <asp:HiddenField ID="hfKONNR" runat="server" Value='<%# Eval("KONNR") %>' />
    </ItemTemplate>
</asp:TemplateField>



<script type="text/javascript">
function parseNum(v) {
    v = (v || "").toString().replace(/,/g, "");
    var n = parseFloat(v);
    return isNaN(n) ? 0 : n;
}

// Sum checked rows and update totals
function updateTotals() {
    var total = 0.0;

    // find all row checkboxes
    var checkboxes = document.querySelectorAll('input[id*="chkSelect"]');
    checkboxes.forEach(function (cb) {
        if (cb.checked) {
            // find the hidden field in the same row
            var row = cb.closest("tr");
            if (row) {
                var hfAmt = row.querySelector('input[id*="hfAmount"]');
                if (hfAmt) total += parseNum(hfAmt.value);
            }
        }
    });

    // compute 1% cess
    var cess = (total * 1 / 100);

    // update the summary fields
    var totalBase = document.getElementById("MainContent_bocw_summary_Total_Base_Amount_0");
    var cessAmt   = document.getElementById("MainContent_bocw_summary_Cess_Amount_0");
    var subjBase  = document.getElementById("MainContent_bocw_summary_Subjective_base_amount_0");
    var subjCess  = document.getElementById("MainContent_bocw_summary_Subjective_cess_amount_0");
    var subjBal   = document.getElementById("MainContent_bocw_summary_Subjective_balance_0");

    if (totalBase) totalBase.value = total.toFixed(2);
    if (cessAmt)   cessAmt.value   = cess.toFixed(2);
    if (subjBase)  subjBase.value  = total.toFixed(2);
    if (subjCess)  subjCess.value  = cess.toFixed(2);
    if (subjBal)   subjBal.value   = (total - cess).toFixed(2);
}

// handle select-all checkbox
function checkAll(master) {
    var checkboxes = document.querySelectorAll('input[id*="chkSelect"]');
    checkboxes.forEach(function (cb) {
        cb.checked = master.checked;
    });
    updateTotals(); // recalc totals when all toggled
}
</script>
