protected void ReturnAttachment_Click(object sender, BulletedListEventArgs e)
{
    string fullFileName = e.Text;

    string displayName = fullFileName.Length > 20
        ? fullFileName.Substring(0, 20) + "..."
        : fullFileName;

    // Use displayName for UI only
}
