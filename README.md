<asp:TextBox 
    ID="DOJ"
    runat="server"
    CssClass="form-control form-control-sm textboxstyle"
    placeholder="DD / MM / YYYY"
    MaxLength="14"
    inputmode="numeric"
    oninput="formatDOB(this)"
    onblur="validateDOB(this)" />


    <script>
function formatDOB(input) {
    let value = input.value.replace(/\D/g, '');

    if (value.length > 8) value = value.substring(0, 8);

    let formatted = '';

    if (value.length >= 1)
        formatted = value.substring(0, 2);
    if (value.length >= 3)
        formatted = value.substring(0, 2) + ' / ' + value.substring(2, 4);
    if (value.length >= 5)
        formatted = value.substring(0, 2) + ' / ' + value.substring(2, 4) + ' / ' + value.substring(4, 8);

    input.value = formatted;
}

function validateDOB(input) {
    const regex = /^(0[1-9]|[12][0-9]|3[01]) \/ (0[1-9]|1[0-2]) \/ \d{4}$/;
    if (input.value !== "" && !regex.test(input.value)) {
        alert("Please enter a valid date in DD / MM / YYYY format");
        input.value = "";
        input.focus();
    }
}
</script>
