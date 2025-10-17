<script type="text/javascript">
    // Utility: safely parse numeric string
    function parseNumberSafe(s) {
        if (!s) return 0;
        s = s.toString().replace(/,/g, '').replace(/[^\d.\-]/g, '');
        var v = parseFloat(s);
        return isNaN(v) ? 0 : v;
    }

    // Recalculate total of checked rows
    function recalcSelectedTotal() {
        var grid = document.getElementById('<%= WorkOder_wise_records.ClientID %>');
        if (!grid) return;

        var sum = 0;
        var checks = grid.querySelectorAll('input[id$="chkSelect"][type="checkbox"]');

        for (var i = 0; i < checks.length; i++) {
            var chk = checks[i];
            if (chk.checked) {
                var tr = chk.closest('tr');
                if (!tr) continue;

                // Adjust class name according to your Base Amount cell span
                var rmSpan = tr.querySelector('.rmwwr');
                if (rmSpan) {
                    var raw = rmSpan.textContent || rmSpan.innerText || '';
                    sum += parseNumberSafe(raw);
                }
            }
        }

        // Update Total Base Amount
        var totalInput = document.getElementById('MainContent_bocw_summary_Total_Base_Amount_0');
        if (totalInput) totalInput.value = sum.toFixed(2);

        // Update Subjective base amount
        var subj = document.getElementById('MainContent_bocw_summary_Subjective_base_amount_0');
        if (subj) subj.value = sum.toFixed(2);

        // Update Cess amounts (1%)
        var cess = (sum * 1) / 100;
        var cessInput = document.getElementById('MainContent_bocw_summary_Cess_Amount_0');
        if (cessInput) cessInput.value = cess.toFixed(2);
        var subjCess = document.getElementById('MainContent_bocw_summary_Subjective_cess_amount_0');
        if (subjCess) subjCess.value = cess.toFixed(2);

        // Update Subjective Balance
        var subjBalance = document.getElementById('MainContent_bocw_summary_Subjective_balance_0');
        var prevOutstanding = document.getElementById('MainContent_bocw_summary_Subjective_Previous_outstanding_0');
        if (subjBalance && prevOutstanding) {
            subjBalance.value = (sum - parseNumberSafe(prevOutstanding.value)).toFixed(2);
        }
    }

    // Handle header checkbox (Select All)
    function checkAll(headerChk) {
        var grid = document.getElementById('<%= WorkOder_wise_records.ClientID %>');
        if (!grid) return;
        var checks = grid.querySelectorAll('input[id$="chkSelect"][type="checkbox"]');
        for (var i = 0; i < checks.length; i++) {
            checks[i].checked = headerChk.checked;
        }
        recalcSelectedTotal();
    }

    // Attach change event to each row checkbox
    function wireRowCheckboxEvents() {
        var grid = document.getElementById('<%= WorkOder_wise_records.ClientID %>');
        if (!grid) return;
        var checks = grid.querySelectorAll('input[id$="chkSelect"][type="checkbox"]');
        for (var i = 0; i < checks.length; i++) {
            checks[i].addEventListener('change', recalcSelectedTotal);
        }
    }

    // Initialize on page load
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', wireRowCheckboxEvents);
    } else {
        wireRowCheckboxEvents();
    }

    // Re-attach events after partial postback (UpdatePanel)
    if (typeof (Sys) !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            wireRowCheckboxEvents();
        });
    }
</script>

<asp:TemplateField HeaderText="Select">
    <HeaderTemplate>
        <input type="checkbox" onclick="checkAll(this)" />
    </HeaderTemplate>
    <ItemTemplate>
        <asp:CheckBox ID="chkSelect" runat="server" />
    </ItemTemplate>
</asp:TemplateField>


<asp:TemplateField HeaderText="Base Amount">
    <ItemTemplate>
        <span class="rmwwr"><%# Eval("BaseAmount") %></span>
    </ItemTemplate>
</asp:TemplateField>
