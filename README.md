Repeater rpt = (Repeater)SummaryReport_Record.Rows[0]
                .FindControl("WAGE_REGIS_FILE1");

string file = PageRecordDataSet.Tables["App_Online_Wages"]
              .Rows[0]["WAGE_REGIS_FILE"].ToString();

if (!string.IsNullOrWhiteSpace(file))
{
    DataTable dt = new DataTable();
    dt.Columns.Add("Name");
    dt.Columns.Add("Url");

    // Show only original filename (no GUID)
    string displayName = file.Contains("_")
        ? file.Substring(file.IndexOf('_') + 1)
        : file;

    // 🔥 IMPORTANT: avoid + in URL (NO handler change needed)
    string safeFile = file.Replace(" ", "%20");

    dt.Rows.Add(
        displayName,
        ResolveUrl("~/FileDownloadHandler.ashx?file=" + safeFile)
    );

    rpt.DataSource = dt;
    rpt.DataBind();
}
