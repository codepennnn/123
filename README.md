function updateDashboard() {
    var rows = document.querySelectorAll("#<%= gvAttendance.ClientID %> tr");

    var total = 0,
        present = 0,
        absent = 0,
        od = 0,
        leave = 0,
        holiday = 0,
        halfday = 0;

    rows.forEach(function (row) {

        var chk = row.querySelector("input[type='checkbox'][id*='chkPresent']");
        var ddlDay = row.querySelector("select[id*='ddlDayDef']");

        if (!chk || !ddlDay) return;

        total++;

        var dayVal = ddlDay.value;

        switch (dayVal) {
            case "WD":
                if (chk.checked) present++;
                else absent++;
                break;

            case "OD":
                od++;
                break;

            case "L":
                leave++;
                break;

            case "NH":
                holiday++;
                break;

            case "HF":
                halfday++;
                break;
        }
    });

    document.getElementById("dashTotal").textContent = total;
    document.getElementById("dashPresent").textContent = present;
    document.getElementById("dashAbsent").textContent = absent;
    document.getElementById("dashOD").textContent = od;
    document.getElementById("dashLeave").textContent = leave;
    document.getElementById("dashHoliday").textContent = holiday;
    document.getElementById("dashHalfDay").textContent = halfday;
}





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

