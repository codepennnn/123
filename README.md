<asp:CheckBox ID="chkAcknowledgement"
              runat="server"
              CssClass="ack-checkbox"
              onchange="onAcknowledgementChange()" />

function onAcknowledgementChange() {
    var chk = document.getElementById('<%= chkAcknowledgement.ClientID %>');
    var err = document.getElementById("ackError");
    var btn = document.getElementById("MainContent_btnSave");

    if (chk.checked) {
        err.style.display = "none";
    } else {
        err.style.display = "block";
    }
}
