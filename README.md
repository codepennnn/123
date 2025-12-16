<div class="SearchDropDown" style="position:relative; width:100%;">

    <!-- Fake dropdown button -->
    <button type="button"
            class="btn btn-sm btn-light w-100 text-start"
            onclick="toggleAadharDD(this)"
            style="border:1px solid #ced2d5;">
        <span class="selected-text">-- Select Workman --</span>
        <span class="caret float-end"></span>
    </button>

    <!-- Floating panel -->
    <div class="floatDiv"
         style="position:absolute; display:none; z-index:1050;
                width:100%; background:#fff;
                border:1px solid #ced2d5;
                box-shadow:0 6px 12px rgba(0,0,0,.18);
                padding:6px;">

        <!-- Search box -->
        <input type="text"
               class="form-control form-control-sm mb-1"
               placeholder="Search..."
               onkeyup="filterAadhar(this)" />

        <!-- Items -->
        <div class="searchList" style="max-height:220px; overflow-y:auto;">
            <!-- Items generated from ddlAadhar by JS -->
        </div>
    </div>

    <!-- Your original dropdown (hidden) -->
    <asp:DropDownList ID="ddlAadhar" runat="server"
        AutoPostBack="true"
        OnSelectedIndexChanged="ddlAadhar_SelectedIndexChanged"
        style="display:none;">
    </asp:DropDownList>

</div>

<script>
    document.addEventListener("DOMContentLoaded", function () {
        var ddl = document.getElementById("<%= ddlAadhar.ClientID %>");
        var container = ddl.closest(".SearchDropDown");
        var list = container.querySelector(".searchList");
        var selectedText = container.querySelector(".selected-text");

        // Build list from dropdown
        for (var i = 0; i < ddl.options.length; i++) {
            if (ddl.options[i].value === "") continue;

            var div = document.createElement("div");
            div.className = "dropdown-item";
            div.style.cursor = "pointer";
            div.innerText = ddl.options[i].text;

            div.dataset.value = ddl.options[i].value;

            div.onclick = function () {
                ddl.value = this.dataset.value;
                selectedText.innerText = this.innerText;
                container.querySelector(".floatDiv").style.display = "none";

                // trigger postback
                ddl.dispatchEvent(new Event('change'));
            };

            list.appendChild(div);
        }
    });

    function toggleAadharDD(btn) {
        var panel = btn.nextElementSibling;
        panel.style.display = panel.style.display === "block" ? "none" : "block";
    }

    function filterAadhar(input) {
        var filter = input.value.toUpperCase();
        var items = input.parentElement.querySelectorAll(".dropdown-item");

        items.forEach(function (item) {
            item.style.display = item.innerText.toUpperCase().indexOf(filter) > -1 ? "" : "none";
        });
    }

    // close when clicking outside
    document.addEventListener("click", function (e) {
        document.querySelectorAll(".SearchDropDown").forEach(function (dd) {
            if (!dd.contains(e.target)) {
                dd.querySelector(".floatDiv").style.display = "none";
            }
        });
    });
</script>
