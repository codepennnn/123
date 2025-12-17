[System.Web.Services.WebMethod]
public static List<string> GetPresentAadhars(int month, int year)
{
    var result = new List<string>();
    string connStr = ConfigurationManager.ConnectionStrings["YourConnName"].ConnectionString;

    DateTime fromDate = new DateTime(year, month, 1);
    DateTime toDate   = fromDate.AddMonths(1);

    using (var cn = new SqlConnection(connStr))
    using (var cmd = new SqlCommand(@"
        SELECT DISTINCT AadharNo
        FROM App_AttendanceDetails
        WHERE Dates >= @FromDate
          AND Dates <  @ToDate
          AND Present = 1", cn))
    {
        cmd.Parameters.AddWithValue("@FromDate", fromDate);
        cmd.Parameters.AddWithValue("@ToDate", toDate);

        cn.Open();
        using (var dr = cmd.ExecuteReader())
        {
            while (dr.Read())
                result.Add(dr.GetString(0));
        }
    }

    return result;
}


function colorAadharDropdown() {

    var month = document.getElementById("<%= ddlMonth.ClientID %>").value;
    var year  = document.getElementById("<%= ddlYear.ClientID %>").value;

    fetch("Attendance.aspx/GetPresentAadhars", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ month: parseInt(month), year: parseInt(year) })
    })
    .then(res => res.json())
    .then(data => {
        var presentList = data.d || [];

        var ddl = document.getElementById("<%= ddlAadhar.ClientID %>");
        if (!ddl) return;

        for (var i = 0; i < ddl.options.length; i++) {
            var opt = ddl.options[i];

            if (presentList.includes(opt.value)) {
                opt.style.backgroundColor = "#dcfce7";
                opt.style.color = "#166534";
                opt.style.fontWeight = "600";
            }
        }

        // If using custom dropdown UI, repaint it
        repaintCustomAadharUI(presentList);
    });
}


function repaintCustomAadharUI(presentList) {

    document.querySelectorAll(".searchList .search-item").forEach(function (item) {
        if (presentList.includes(item.dataset.value)) {
            item.style.backgroundColor = "#dcfce7";
            item.style.color = "#166534";
            item.style.fontWeight = "600";
        }
    });
}

document.addEventListener("change", function (e) {
    if (e.target.id.includes("ddlMonth") || e.target.id.includes("ddlYear")) {
        setTimeout(colorAadharDropdown, 300);
    }
});


