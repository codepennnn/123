<div id="dashboard" class="dashboard-container">
    <div class="dashboard-card">
        <div class="dashboard-title"><strong>Total Days</strong></div>
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

    <div class="dashboard-card od">
        <div class="dashboard-title"><strong>Off Day</strong></div>
        <div class="dashboard-value" id="dashOD">0</div>
    </div>

    <div class="dashboard-card leave">
        <div class="dashboard-title"><strong>Leave</strong></div>
        <div class="dashboard-value" id="dashLeave">0</div>
    </div>

    <div class="dashboard-card holiday">
        <div class="dashboard-title"><strong>Holiday</strong></div>
        <div class="dashboard-value" id="dashHoliday">0</div>
    </div>

    <div class="dashboard-card halfday">
        <div class="dashboard-title"><strong>Half Day</strong></div>
        <div class="dashboard-value" id="dashHalfDay">0</div>
    </div>
</div>


document.addEventListener("DOMContentLoaded", updateDashboard);

document.addEventListener("change", function (e) {
    if (
        e.target.matches("input[type='checkbox'][id*='chkPresent']") ||
        e.target.matches("select[id*='ddlDayDef']")
    ) {
        updateDashboard();
    }
});

