<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <uc1:MyMsgBox ID="MyMsgBox" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>

protected void ShowError(string message)
{
    lblMessage.Text = message;
    alertBox.Visible = true;
    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "showAlert();", true);
}


<script>
function showAlert() {
    const alert = document.getElementById('<%= MyMsgBox.ClientID %>_alertBox');
    if (alert) {
        alert.classList.add('show');
        setTimeout(() => alert.classList.remove('show'), 5000); // auto-hide after 5s
    }
}
</script>

.alert-box {
    opacity: 0;
    transform: translateY(-20px);
    transition: all 0.4s ease;
    position: fixed;
    top: 20px;
    right: 20px;
    background-color: #f44336;
    color: white;
    padding: 15px 20px;
    border-radius: 8px;
    box-shadow: 0 4px 10px rgba(0,0,0,0.2);
    z-index: 9999;
}
.alert-box.show {
    opacity: 1;
    transform: translateY(0);
}
