BulletedList bl = (BulletedList)SummaryReport_Record.Rows[0]
                    .FindControl("WAGE_REGIS_FILE1");

bl.Items.Clear();

string files = PageRecordDataSet.Tables["App_Online_Wages"]
                .Rows[0]["WAGE_REGIS_FILE"].ToString();

if (!string.IsNullOrEmpty(files))
{
    string[] attachments = files.Split(',');

    foreach (string fullName in attachments)
    {
        if (string.IsNullOrWhiteSpace(fullName)) continue;

        string fileName = Path.GetFileName(fullName); // ✅ SAFE

        ListItem li = new ListItem();
        li.Text = fileName;
        li.Value = ResolveUrl("~/FileDownloadHandler.ashx?file=" + Server.UrlEncode(fileName));

        bl.Items.Add(li);
    }
}
