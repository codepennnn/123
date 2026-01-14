BulletedList bl = (BulletedList)SummaryReport_Record.Rows[0]
                    .FindControl("WAGE_REGIS_FILE1");

bl.Items.Clear();

string file = PageRecordDataSet.Tables["App_Online_Wages"]
               .Rows[0]["WAGE_REGIS_FILE"].ToString();

if (!string.IsNullOrWhiteSpace(file))
{
    string fileName = Path.GetFileName(file); // ✅ SAFE

    ListItem li = new ListItem();
    li.Text = fileName;
    li.Value = ResolveUrl("~/FileDownloadHandler.ashx?file=" +
                          Server.UrlEncode(fileName));

    bl.Items.Add(li);
}
