<script type="text/javascript">
function toggleBlockDates(dropdown) {
    var fromDiv = document.getElementById("blockFromDiv");
    var toDiv = document.getElementById("blockToDiv");

    if (!fromDiv || !toDiv) return;

    if (dropdown.value === "RFQ_U") { // Unblock selected
        fromDiv.style.display = "none";
        toDiv.style.display = "none";
    } else { // Block or default
        fromDiv.style.display = "block";
        toDiv.style.display = "block";
    }
}
</script>
