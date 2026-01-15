Repeater rpt = (Repeater)SummaryReport_Record.Rows[0].FindControl("WAGE_REGIS_FILE1");

string fileValue = PageRecordDataSet.Tables["App_Online_Wages"]
                   .Rows[0]["WAGE_REGIS_FILE"].ToString();

if (!string.IsNullOrWhiteSpace(fileValue))
{
    DataTable dt = new DataTable();
    dt.Columns.Add("Name");
    dt.Columns.Add("Url");

    string[] files = fileValue.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

    foreach (string file in files)
    {
        string cleanFile = file.Trim();

        string displayName = cleanFile.Contains("_")
            ? cleanFile.Substring(cleanFile.IndexOf('_') + 1)
            : cleanFile;

        dt.Rows.Add(
            displayName,
            ResolveUrl("~/FileDownloadHandler.ashx?file=" + cleanFile)
        );
    }

    rpt.DataSource = dt;
    rpt.DataBind();
}
