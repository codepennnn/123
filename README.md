private string ShortFileName(string fileName, int length = 20)
{
    if (string.IsNullOrEmpty(fileName))
        return fileName;

    return fileName.Length > length
        ? fileName.Substring(0, length) + "..."
        : fileName;
}


protected void WorkOrder_Exemption_Record_RowDataBound(object sender, GridViewRowEventArgs e)
{
    if (e.Row.RowType == DataControlRowType.DataRow)
    {
        BulletedList bl = (BulletedList)e.Row.FindControl("Attachment");

        string attachment = DataBinder.Eval(e.Row.DataItem, "Attachment")?.ToString();

        if (!string.IsNullOrEmpty(attachment))
        {
            bl.Items.Clear();

            foreach (string file in attachment.Split(','))
            {
                bl.Items.Add(new ListItem
                {
                    Text = ShortFileName(file, 20), // UI only
                    Value = file                    // full filename
                });
            }
        }
    }
}
