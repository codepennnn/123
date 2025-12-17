<div id="dashboard" class="dashboard-container">
    <div class="dashboard-card">
        <div class="dashboard-title"><strong>Total Rows</strong></div>
        <div class="dashboard-value" id="dashTotal">0</div>
    </div>
    <div class="dashboard-card present">
        <div class="dashboard-title"><strong>Present</strong></div>
        <div class="dashboard-value" id="dashPresent">0</div>
    </div>
    <div class="dashboard-card absent">
        <div class="dashboard-title"><strong>Absent</strong></div>
        <div class="dashboard-value" id="dashAbsent">0</div>
    </div>
</div>


            function updateDashboard() {
    var rows = document.querySelectorAll("#<%= gvAttendance.ClientID %> tr");
    var total = 0, present = 0;

    rows.forEach(function(row) {
        var checkbox = row.querySelector("input[type='checkbox'][id*='chkPresent']");
    if (checkbox) {
        total++;
    if (checkbox.checked) present++;
        }
    });

    document.getElementById("dashTotal").textContent = total;
    document.getElementById("dashPresent").textContent = present;
    document.getElementById("dashAbsent").textContent = total - present;
}

    // Call after page load
    document.addEventListener("DOMContentLoaded", updateDashboard);

    // Also call whenever a checkbox changes
    document.addEventListener("change", function(e) {
    if (e.target && e.target.matches("input[type='checkbox'][id*='chkPresent']")) {
        updateDashboard();
    }
});
  
