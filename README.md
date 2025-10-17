<script type="text/javascript">
// helper to parse number safely
function parseNum(v) {
    if (v === null || v === undefined || v === '') return 0;
    // remove commas if used for formatting
    v = v.toString().replace(/,/g, '');
    var n = parseFloat(v);
    return isNaN(n) ? 0 : n;
}

// Called when any row checkbox is clicked
function updateTotals(chk) {
    // find the container of the grid (adapt selector to your table structure)
    // We'll search for all checkboxes with class 'wo-checkbox' within the container
    var container = document.getElementById('<%= WorkOder_wise_records.ClientID %>');
    if (!container) {
        // fallback: search document
        container = document;
    }

    var checks = container.querySelectorAll('.wo-checkbox');
    var total = 0.0;
    for (var i = 0; i < checks.length; i++) {
        var c = checks[i];
        if (c.checked) {
            // find sibling hidden field hfAmount inside the same naming container (row)
            // walk up the DOM to the row container (common ancestor)
            var row = c.closest('[data-container]') || c.closest('tr') || c.parentElement;
            // try to find hidden input inside that row
            var hf = row ? row.querySelector('input[id$="hfAmount"]') : null;
            if (!hf) {
                // try global search by nearby elements
                hf = c.parentElement.querySelector('input[id$="hfAmount"]') || document.querySelector('input[id$="hfAmount"]');
            }
            var amt = hf ? hf.value : 0;
            total += parseNum(amt);
        }
    }

    // Update Total_Base_Amount and Cess_Amount textboxes (server controls)
    // Use ClientID to address controls rendered by ASP.NET
    var totalBox = document.getElementById('<%= Total_Base_Amount.ClientID %>');
    var cessBox  = document.getElementById('<%= Cess_Amount.ClientID %>');
    if (totalBox) totalBox.value = total.toFixed(2);
    if (cessBox)  cessBox.value  = (total * 1/100).toFixed(2);

    // If you want to update other fields like Subjective_base_amount etc:
    var subjBase = document.getElementById('<%= Subjective_base_amount.ClientID %>');
    var subjCess = document.getElementById('<%= Subjective_cess_amount.ClientID %>');
    var subjBal  = document.getElementById('<%= Subjective_balance.ClientID %>');
    if (subjBase) subjBase.value = total.toFixed(2);
    if (subjCess) subjCess.value = (total * 1/100).toFixed(2);
    if (subjBal)  subjBal.value  = (parseNum(subjBase ? subjBase.value : 0) - parseNum(subjCess ? subjCess.value : 0)).toFixed(2);
}
</script>



<!-- inside your <ItemTemplate> for each row -->
<asp:HiddenField ID="hfAmount" runat="server" Value='<%# Eval("RMWWR") %>' />
<asp:HiddenField ID="hfKONNR" runat="server" Value='<%# Eval("KONNR") %>' />

<asp:CheckBox ID="chkSelect" runat="server"
    CssClass="wo-checkbox"
    onclick="updateTotals(this);" />
    
