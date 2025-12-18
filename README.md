<script type="text/javascript">
    function limitTextWithAlert(txt, max) {
        if (txt.value.length > max) {
            txt.value = txt.value.substring(0, max);
            alert("Maximum " + max + " characters allowed.");
        }
    }
</script>

<asp:TextBox 
    ID="Contractors_Establishment_Worked"
    runat="server"
    CssClass="form-control form-control-sm font-small"
    MaxLength="154"
    oninput="limitTextWithAlert(this,154);">
</asp:TextBox>

