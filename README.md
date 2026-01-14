BulletedList bl =
    (BulletedList)SummaryReport_Record.Rows[0]
    .FindControl("WAGE_REGIS_FILE1");

bl.Items.Clear();

string dbFile =
    PageRecordDataSet.Tables["App_Online_Wages"]
    .Rows[0]["WAGE_REGIS_FILE"].ToString();

if (!string.IsNullOrEmpty(dbFile))
{
    ListItem li = new ListItem();

    // ✔ Display only original file name (after GUID)
    li.Text = dbFile.Contains("_")
        ? dbFile.Substring(dbFile.IndexOf('_') + 1)
        : dbFile;

    // 🔥 MOST IMPORTANT LINE — PASS DB VALUE AS-IS
    li.Value = "~/FileDownloadHandler.ashx?file=" + dbFile;

    bl.Items.Add(li);
}
