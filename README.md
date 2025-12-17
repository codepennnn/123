private void BindAadharDropdown()
{
    var dtEmployees = ViewState["Employees"] as DataTable;
    var dtPresent   = ViewState["AttendedAadhars"] as DataTable; // ✅ FIXED

    ddlAadhar.Items.Clear();
    ddlAadhar.Items.Add(new ListItem("-- Select --", ""));

    foreach (DataRow r in dtEmployees.Rows)
    {
        string aadhar = r["AadharCard"].ToString();
        string name   = r["Name"].ToString();
        string sl     = r["WorkManSlNo"].ToString();

        var li = new ListItem($"{sl} - {name} - {aadhar}", aadhar);

        // GREEN if Present=1 exists
        if (dtPresent != null &&
            dtPresent.Select($"AadharNo = '{aadhar}'").Length > 0)
        {
            li.Attributes["class"] = "att-done"; // matches CSS
        }

        ddlAadhar.Items.Add(li);
    }
}


select option.att-done {
    background-color: #dcfce7;
    color: #166534;
    font-weight: 600;
}



function buildAadharSearchList() {
    var ddl = document.getElementById("<%= ddlAadhar.ClientID %>");
    var list = document.querySelector(".searchList");

    list.innerHTML = "";

    for (var i = 1; i < ddl.options.length; i++) {
        var opt = ddl.options[i];

        var div = document.createElement("div");
        div.textContent = opt.text;
        div.dataset.value = opt.value;
        div.className = "search-item";

        // ✅ COPY GREEN CLASS
        if (opt.classList.contains("att-done")) {
            div.classList.add("att-done");
        }

        div.onclick = function () {
            ddl.value = this.dataset.value;
            ddl.dispatchEvent(new Event("change"));
            document.querySelector(".selected-text").textContent = this.textContent;
            document.querySelector(".floatDiv").style.display = "none";
        };

        list.appendChild(div);
    }
}

BindAadharDropdown();
ScriptManager.RegisterStartupScript(
    this, GetType(), "buildAadhar", "buildAadharSearchList();", true);

.search-item {
    padding: 6px 8px;
    cursor: pointer;
}

.search-item:hover {
    background: #f3f4f6;
}

.search-item.att-done {
    background-color: #dcfce7;
    color: #166534;
    font-weight: 600;
}
