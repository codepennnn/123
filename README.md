<script>
function applyCanteenAndCrechesRules() {

    // Get all grid rows (skip header row)
    var grid = document.querySelector(".styled-grid");
    if (!grid) return;

    var rows = grid.querySelectorAll("tr");

    rows.forEach(function (row, index) {

        // Skip header
        if (index === 0) return;

        // Get Sex(M) and Sex(F) cells (adjust index if column order changes)
        var maleCell = row.cells[14];   // Sex (M)
        var femaleCell = row.cells[15]; // Sex (F)

        if (!maleCell || !femaleCell) return;

        var male = parseInt(maleCell.innerText.trim()) || 0;
        var female = parseInt(femaleCell.innerText.trim()) || 0;

        var totalManpower = male + female;

        // Get dropdowns inside row
        var canteenDDL = row.querySelector("select[id*='Welfare_Canteen']");
        var crechesDDL = row.querySelector("select[id*='Welfare_Creches']");

        // Apply Canteen rule
        if (canteenDDL) {
            canteenDDL.value = (totalManpower >= 100) ? "YES" : "NO";
        }

        // Apply Creches rule
        if (crechesDDL) {
            crechesDDL.value = (female >= 50) ? "YES" : "NO";
        }
    });
}

// Call on page load
document.addEventListener("DOMContentLoaded", applyCanteenAndCrechesRules);
</script>
