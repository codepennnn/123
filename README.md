<div class="field">
    <label class="form-label fw-bold small">Aadhar / Name</label>

    <asp:DropDownList ID="ddlAadhar" runat="server"
        CssClass="form-control form-control-sm"
        AutoPostBack="true"
        OnSelectedIndexChanged="ddlAadhar_SelectedIndexChanged">
    </asp:DropDownList>

    <!-- Search box under dropdown -->
    <input type="text"
           id="txtSearchAadhar"
           class="form-control form-control-sm mt-1"
           placeholder="Search by Aadhar or Name..."
           onkeyup="filterAadharDropdown()" />
</div>

<script>
    function filterAadharDropdown() {
        var input = document.getElementById("txtSearchAadhar");
        var filter = input.value.toUpperCase();

        var ddl = document.getElementById("<%= ddlAadhar.ClientID %>");
        var options = ddl.options;

        for (var i = 0; i < options.length; i++) {
            var txt = options[i].text.toUpperCase();

            if (txt.indexOf(filter) > -1 || options[i].value.toUpperCase().indexOf(filter) > -1) {
                options[i].style.display = "";
            } else {
                options[i].style.display = "none";
            }
        }
    }
</script>
