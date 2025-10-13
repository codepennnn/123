protected void Page_Load(object sender, EventArgs e)
{
    if (!IsPostBack)
    {
        CheckBocwApplicable();
    }
}

private void CheckBocwApplicable()
{
    // Database se value uthaiye
    int bocwApplicableAsPerMatrix = GetBocwApplicableAsPerMatrix(); // <-- apna method yahaan call karein

    // Textbox ka value uthaiye
    int bocwApplicableTextbox = 0;
    int.TryParse(BOCW_APPLICABLE.Text, out bocwApplicableTextbox);

    // Check condition
    if (bocwApplicableAsPerMatrix == 2 && bocwApplicableTextbox == 2)
    {
        // Enable fields
        REGIS_NUMBER.Enabled = true;
        REGIS_DATE.Enabled = true;
        REGIS_VALID_UPTO.Enabled = true;
        NO_EMP_BOCW_OBTAINED.Enabled = true;

        // Required validators enable kar do (agar lagaye hain)
        CustomValidator19.Enabled = true;
    }
    else
    {
        // Disable fields
        REGIS_NUMBER.Enabled = false;
        REGIS_DATE.Enabled = false;
        REGIS_VALID_UPTO.Enabled = false;
        NO_EMP_BOCW_OBTAINED.Enabled = false;

        CustomValidator19.Enabled = false;
    }
}
