    
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


    <script>
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            foreach(GridViewRow row in WorkOder_wise_records.Rows)
            {
        CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                if (chk != null && chk.Checked) {
            string workOrderNo = ((Label)row.FindControl("Slno"))?.Text;
                
                }
            }
        }
    </script>



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
            var totalInput = document.getElementById('MainContent_bocw_summary_Total_Base_Amount_0');
    if (totalInput) {
        // format to two decimals (adjust formatting as you like)
        totalInput.value = sum.toFixed(2);
    }

    // also set Subjective_base_amount if you want same behavior:
            var subj = document.getElementById('MainContent_bocw_summary_Subjective_base_amount_0');
    if (subj) subj.value = sum.toFixed(2);

    // And update cess calculation: 1% of sum into Cess_Amount and Subjective_cess_amount
    var cess = (sum * 1) / 100;
            var cessInput = document.getElementById('MainContent_bocw_summary_Cess_Amount_0');
    if (cessInput) cessInput.value = cess.toFixed(2);
            var subjCess = document.getElementById('<MainContent_bocw_summary_Subjective_cess_amount_0');
  if (subjCess) subjCess.value = cess.toFixed(2);

  // update Subjective_balance, etc. if required by your logic
            var subjBalance = document.getElementById('MainContent_bocw_summary_Subjective_balancet_0');
  if (subjBalance) {
      var prevOutstanding = parseNumberSafe(document.getElementById('MainContent_bocw_summary_Subjective_Previous_outstanding_0' )?.value);
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
   
            // the above may fail if header row not accessible; we already have inline onclick on header chk so it's optional
        }

        // call wire function when DOM ready
        if (document.readyState === 'loading') {
            document.addEventListener('DOMContentLoaded', wireRowCheckboxEvents);
        } else {
            wireRowCheckboxEvents();
        }

        // If you use ASP.NET UpdatePanel partial postbacks, re-wire after async postbacks:
        if (typeof (Sys) !== 'undefined' && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                wireRowCheckboxEvents();
            });
        }
    </script>
