var chk = document.getElementById('<%= chkAcknowledgement.ClientID %>');
var err = document.getElementById("ackError");

if (!chk.checked) {
    err.style.display = "block";

    alert("Please confirm the acknowledgement before submitting the request.");

    chk.focus();
    return false;
}

err.style.display = "none";
