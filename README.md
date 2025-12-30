function validateIntInput(txt, max) {

    // Allow only numbers
    txt.value = txt.value.replace(/[^0-9]/g, '');
    if (txt.value === '') return;

    var val = parseInt(txt.value, 10);

    // Get current row
    var row = txt.closest("tr");

    // Principal employer worked column (BOUND FIELD)
    // Adjust index ONLY if column order changes
    var principalWorkedCell = row.cells[11]; 
    var principalWorked = parseInt(principalWorkedCell.innerText.trim()) || 0;

    // ❌ Contractor cannot exceed principal
    if (val > principalWorked) {
        alert("Contractor establishment worked cannot exceed Establishment of Principal Employer worked.");
        txt.value = principalWorked;
        return;
    }

    // Existing max limit check
    if (val > max) {
        alert("Maximum value allowed is " + max);
        txt.value = max;
    }
}


function applyCanteenAndCrechesRules() {

    var grid = document.querySelector(".styled-grid");
    if (!grid) return;

    var rows = Array.from(grid.querySelectorAll("tr")).slice(1); // skip header

    // Group data by License No (Lic No column)
    var licenseMap = {};

    rows.forEach(function (row) {

        var licCell = row.cells[6];   // LabourLicNo column
        var maleCell = row.cells[12]; // Sex (M)
        var femaleCell = row.cells[13]; // Sex (F)

        if (!licCell) return;

        var licNo = licCell.innerText.trim();
        var male = parseInt(maleCell.innerText.trim()) || 0;
        var female = parseInt(femaleCell.innerText.trim()) || 0;

        if (!licenseMap[licNo]) {
            licenseMap[licNo] = {
                total: 0,
                female: 0,
                rows: []
            };
        }

        licenseMap[licNo].total += (male + female);
        licenseMap[licNo].female += female;
        licenseMap[licNo].rows.push(row);
    });

    // Apply rules PER LICENSE
    Object.keys(licenseMap).forEach(function (licNo) {

        var data = licenseMap[licNo];

        var canteenValue = (data.total >= 100) ? "YES" : "NA";
        var crechesValue = (data.female >= 50) ? "YES" : "NA";

        data.rows.forEach(function (row) {

            var canteenDDL = row.querySelector("select[id*='Welfare_Canteen']");
            var crechesDDL = row.querySelector("select[id*='Welfare_Creches']");

            if (canteenDDL) canteenDDL.value = canteenValue;
            if (crechesDDL) crechesDDL.value = crechesValue;
        });
    });
}


