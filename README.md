<script type="text/javascript">
function onAcknowledgementChange() {
    var chk = document.getElementById("chkAcknowledgement");
    var err = document.getElementById("ackError");
    var btn = document.getElementById('<%= btnSubmit.ClientID %>');

    if (chk.checked) {
        err.style.display = "none";   // ✅ hide message immediately
        btn.disabled = false;         // ✅ enable submit
    } else {
        btn.disabled = true;          // disable again if unchecked
    }
}
</script>
