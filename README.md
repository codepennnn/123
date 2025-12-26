protected void Search_Click(object sender, EventArgs e)
{
    int selectedYear = Convert.ToInt32(Year.SelectedValue);
    string selectedPeriod = SearchPeriod.SelectedValue;

    // Current YYYYMM
    int currentYYYYMM = DateTime.Today.Year * 100 + DateTime.Today.Month;

    // Selected half-year YYYYMM (June or December)
    int selectedYYYYMM;

    if (selectedPeriod == "Jan-June")
    {
        selectedYYYYMM = selectedYear * 100 + 6;   // YYYY06
    }
    else // July-Dec
    {
        selectedYYYYMM = selectedYear * 100 + 12;  // YYYY12
    }

    // ❌ FUTURE PERIOD BLOCK
    if (currentYYYYMM < selectedYYYYMM)
    {
        MyMsgBox.show(
            CLMS.Control.MyMsgBox.MessageType.Errors,
            "You cannot generate Half-Yearly data for a future period."
        );

        HalfYearly_Records.Visible = false;
        btnSave.Visible = false;
        return;
    }

    // ✅ CONTINUE EXISTING LOGIC BELOW
