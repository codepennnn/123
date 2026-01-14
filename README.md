Repeater rpt = (Repeater)SummaryReport_Record.Rows[0]
                .FindControl("WAGE_REGIS_FILE1");

string file = PageRecordDataSet.Tables["App_Online_Wages"]
              .Rows[0]["WAGE_REGIS_FILE"].ToString();

if (!string.IsNullOrWhiteSpace(file))
{
    DataTable dt = new DataTable();
    dt.Columns.Add("Name");
    dt.Columns.Add("Url");

    // Show clean filename (remove GUID)
    string displayName = file.Contains("_")
        ? file.Substring(file.IndexOf('_') + 1)
        : file;

    // 🚫 NO UrlEncode
    dt.Rows.Add(
        displayName,
        ResolveUrl("~/FileDownloadHandler.ashx?file=" + file)
    );

    rpt.DataSource = dt;
    rpt.DataBind();
}
