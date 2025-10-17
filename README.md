<!-- replace existing RMWWR BoundField with this -->
<asp:TemplateField HeaderText="Total Base Amount" SortExpression="RMWWR" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
  <ItemTemplate>
    <span class="rmwwr"><%# Eval("RMWWR") %></span>
  </ItemTemplate>
  <HeaderStyle HorizontalAlign="Left" />
  <ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>

<script type="text/javascript">

// utility: parse number safely (handles commas, empty, etc.)
function parseNumberSafe(s) {
  if (!s) return 0;
  // remove commas and any non-numeric except dot and minus
  s = s.toString().replace(/,/g, '').replace(/[^\d\.\-]/g, '');
  var v = parseFloat(s);
  return isNaN(v) ? 0 : v;
}

// recalc sum of checked rows and update Total_Base_Amount textbox
function recalcSelectedTotal() {
  // find the details grid by ID - ClientID will be same as server ID rendered
  var grid = document.getElementById('<%= WorkOder_wise_records.ClientID %>');
  if (!grid) return;

  var sum = 0;
  // find all row checkboxes (any chkSelect inside grid)
  var checks = grid.querySelectorAll('input[id$="chkSelect"][type="checkbox"]');

  for (var i = 0; i < checks.length; i++) {
    var chk = checks[i];
    if (chk.checked) {
      // walk to the row (closest TR)
      var tr = chk.closest('tr');
      if (!tr) continue;

      // find the rmwwr span in this row
      var rmSpan = tr.querySelector('.rmwwr');
      if (rmSpan) {
        var raw = rmSpan.textContent || rmSpan.innerText || '';
        sum += parseNumberSafe(raw);
      } else {
        // fallback: if no span found, try to get from a specific cell index
        // (optional - depends on rendered markup)
        // var cell = tr.cells[2]; // adjust index if necessary
        // sum += parseNumberSafe(cell ? cell.textContent : 0);
      }
    }
  }

  // set the summary textbox value (Total_Base_Amount)
  var totalInput = document.getElementById('<%= Total_Base_Amount.ClientID %>');
  if (totalInput) {
    // format to two decimals (adjust formatting as you like)
    totalInput.value = sum.toFixed(2);
  }

  // also set Subjective_base_amount if you want same behavior:
  var subj = document.getElementById('<%= Subjective_base_amount.ClientID %>');
  if (subj) subj.value = sum.toFixed(2);

  // And update cess calculation: 1% of sum into Cess_Amount and Subjective_cess_amount
  var cess = (sum * 1) / 100;
  var cessInput = document.getElementById('<%= Cess_Amount.ClientID %>');
  if (cessInput) cessInput.value = cess.toFixed(2);
  var subjCess = document.getElementById('<%= Subjective_cess_amount.ClientID %>');
  if (subjCess) subjCess.value = cess.toFixed(2);

  // update Subjective_balance, etc. if required by your logic
  var subjBalance = document.getElementById('<%= Subjective_balance.ClientID %>');
  if (subjBalance) {
    var prevOutstanding = parseNumberSafe(document.getElementById('<%= Subjective_Previous_outstanding.ClientID %>')?.value);
    subjBalance.value = (sum - prevOutstanding).toFixed(2);
  }
}

// when header checkbox clicked: check/uncheck all row checkboxes and recalc
function checkAll(headerChk) {
  var grid = document.getElementById('<%= WorkOder_wise_records.ClientID %>');
  if (!grid) return;
  var checks = grid.querySelectorAll('input[id$="chkSelect"][type="checkbox"]');
  for (var i = 0; i < checks.length; i++) {
    checks[i].checked = headerChk.checked;
  }
  recalcSelectedTotal();
}

// wire up event handlers for row checkboxes on page load (and after partial postbacks)
function wireRowCheckboxEvents() {
  var grid = document.getElementById('<%= WorkOder_wise_records.ClientID %>');
  if (!grid) return;
  var checks = grid.querySelectorAll('input[id$="chkSelect"][type="checkbox"]');
  for (var i = 0; i < checks.length; i++) {
    checks[i].addEventListener('change', recalcSelectedTotal);
  }
  // also wire header checkbox if not using onclick inline
  var chkAll = document.getElementById('<%= ((CheckBox)WorkOder_wise_records.HeaderRow?.FindControl("chkAll"))?.ClientID ?? "chkAll" %>');
  // the above may fail if header row not accessible; we already have inline onclick on header chk so it's optional
}

// call wire function when DOM ready
if (document.readyState === 'loading') {
  document.addEventListener('DOMContentLoaded', wireRowCheckboxEvents);
} else {
  wireRowCheckboxEvents();
}

// If you use ASP.NET UpdatePanel partial postbacks, re-wire after async postbacks:
if (typeof(Sys) !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
    wireRowCheckboxEvents();
  });
}
</script>
