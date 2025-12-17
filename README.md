<script>
/* ------------------------------
   OPEN / CLOSE DROPDOWN
--------------------------------*/
function toggleAadharDD(btn) {
    var container = btn.closest(".SearchDropDown");
    var dd = container.querySelector(".floatDiv");
    dd.style.display = (dd.style.display === "block") ? "none" : "block";
}

/* Close on outside click */
document.addEventListener("click", function (e) {
    document.querySelectorAll(".SearchDropDown .floatDiv").forEach(function (dd) {
        if (!dd.closest(".SearchDropDown").contains(e.target)) {
            dd.style.display = "none";
        }
    });
});

/* ------------------------------
   FILTER SEARCH
--------------------------------*/
function filterAadhar(input) {
    var filter = input.value.toLowerCase();
    input.closest(".floatDiv").querySelectorAll(".search-item")
        .forEach(function (item) {
            item.style.display =
                item.textContent.toLowerCase().includes(filter)
                    ? "block" : "none";
        });
}

/* ------------------------------
   BUILD CUSTOM LIST FROM ASP.NET DDL
--------------------------------*/
function buildAadharList() {
    var ddl = document.getElementById("<%= ddlAadhar.ClientID %>");
    var list = document.querySelector(".searchList");

    if (!ddl || !list) return;

    list.innerHTML = "";

    for (var i = 1; i < ddl.options.length; i++) {
        var opt = ddl.options[i];

        var div = document.createElement("div");
        div.className = "search-item";
        div.textContent = opt.text;
        div.dataset.value = opt.value;

        div.onclick = function () {
            ddl.value = this.dataset.value;
            ddl.dispatchEvent(new Event("change"));
            document.querySelector(".selected-text").textContent = this.textContent;
            document.querySelector(".floatDiv").style.display = "none";
        };

        list.appendChild(div);
    }
}

/* ------------------------------
   COLOR GREEN USING DB (AJAX)
--------------------------------*/
function colorAadharGreen() {

    var month = document.getElementById("<%= ddlMonth.ClientID %>").value;
    var year  = document.getElementById("<%= ddlYear.ClientID %>").value;

    fetch("Vendor_attendance.aspx/GetPresentAadhars", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ month: parseInt(month), year: parseInt(year) })
    })
    .then(res => res.json())
    .then(data => {
        var present = data.d || [];

        document.querySelectorAll(".search-item").forEach(function (item) {
            if (present.includes(item.dataset.value)) {
                item.classList.add("att-done");
            }
        });
    });
}

/* ------------------------------
   INIT
--------------------------------*/
document.addEventListener("DOMContentLoaded", function () {
    buildAadharList();
    colorAadharGreen();
});
</script>
