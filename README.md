protected void Page_Load(object sender, EventArgs e)
{
    if (!IsPostBack)
    {
        CheckBocwApplicability();
    }
}

private void CheckBocwApplicability()
{
    // Example: you fetch bocw_applicable_as_per_matrix from database
    int bocwApplicableMatrix = GetBocwApplicableAsPerMatrix(); // your method
    int bocwApplicableInput = 0;

    // Try to get BOCW_APPLICABLE textbox value
    int.TryParse(BOCW_APPLICABLE.Text, out bocwApplicableInput);

    // Check if both are 2
    if (bocwApplicableMatrix == 2 && bocwApplicableInput == 2)
    {
        EnableAndRequireBocwFields(true);
    }
    else
    {
        EnableAndRequireBocwFields(false);
    }
}

private void EnableAndRequireBocwFields(bool enable)
{
    // Enable/disable relevant controls
    REGIS_NUMBER.Enabled = enable;
    REGIS_DATE.Enabled = enable;
    REGIS_VALID_UPTO.Enabled = enable;
    NO_EMP_BOCW_OBTAINED.Enabled = enable;

    // Optional: set required validators dynamically
    CustomValidator19.Enabled = enable;
}
