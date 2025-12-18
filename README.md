function updateDashboard() {

    var rows = document.querySelectorAll(
        "#<%= gvAttendance.ClientID %> tr");

    var total = 0;
    var present = 0;
    var absent = 0;
    var offDay = 0;
    var leave = 0;
    var holiday = 0;
    var halfDay = 0;

    rows.forEach(function (row) {

        var chk = row.querySelector(
            "input[type='checkbox'][id*='chkPresent']");
        var dayDef = row.querySelector(
            "select[id*='ddlDayDef']");

        if (!chk || !dayDef) return;

        total++;

        var def = dayDef.value;

        // ✅ COUNT ONLY WHEN CHECKED
        if (chk.checked) {
            switch (def) {
                case "WD":
                    present++;
                    break;
                case "OD":
                    offDay++;
                    break;
                case "L":
                    leave++;
                    break;
                case "NH":
                    holiday++;
                    break;
                case "HF":
                    halfDay++;
                    break;
            }
        }
        // ❌ UNCHECKED → only WD becomes Absent
        else {
            if (def === "WD") {
                absent++;
            }
        }
    });

    document.getElementById("dashTotal").textContent = total;
    document.getElementById("dashPresent").textContent = present;
    document.getElementById("dashAbsent").textContent = absent;
    document.getElementById("dashOffDay").textContent = offDay;
    document.getElementById("dashLeave").textContent = leave;
    document.getElementById("dashHoliday").textContent = holiday;
    document.getElementById("dashHalfDay").textContent = halfDay;
}
