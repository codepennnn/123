protected void Search_Click(object sender, EventArgs e)
{
    int year = Convert.ToInt32(Year.SelectedValue);
    string period = SearchPeriod.SelectedValue;

    DateTime today = DateTime.Today;
    DateTime periodStartDate;

    if (period == "Jan-June")
    {
        periodStartDate = new DateTime(year, 1, 1);
    }
    else // July-Dec
    {
        periodStartDate = new DateTime(year, 7, 1);
    }

    // ❌ FUTURE PERIOD VALIDATION
    if (periodStartDate > today)
    {
        MyMsgBox.show(
            CLMS.Control.MyMsgBox.MessageType.Errors,
            "You cannot generate Half-Yearly data for a future period."
        );

        HalfYearly_Records.Visible = false;
        btnSave.Visible = false;
        return;
    }
