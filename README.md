private bool Check()
{
    foreach (GridViewRow row in HalfYearly_Records.Rows)
    {
        string licNo = row.Cells[6].Text.Trim();
        int capacity = Convert.ToInt32(row.Cells[7].Text);
        int sexM = Convert.ToInt32(row.Cells[13].Text);
        int sexF = Convert.ToInt32(row.Cells[14].Text);

        if (sexM + sexF > capacity)
        {
            string msg = $"Maximum No. of Workers (Male + Female + Children) cannot be greater than Capacity of License - {licNo}";

            ScriptManager.RegisterStartupScript(
                this, this.GetType(),
                "alert",
                $"alert('{msg}');",
                true);

            btnSave.Enabled = false;   // disable button
            return false;
        }

        // --- Mandays Validation ---
        double mandaysMale = Convert.ToDouble(row.Cells[15].Text);
        double mandaysFemale = Convert.ToDouble(row.Cells[16].Text);
        double daysWorked = Convert.ToDouble(row.Cells[12].Text);

        double totalMandays = mandaysMale + mandaysFemale;
        double maxAllowedDays = capacity * daysWorked;

        if (totalMandays > maxAllowedDays)
        {
            string msg = $"Mandays (Male + Female) cannot be greater than Total Mandays Allowed for License - {licNo}";

            ScriptManager.RegisterStartupScript(
                this, this.GetType(),
                "alert",
                $"alert('{msg}');",
                true);

            btnSave.Enabled = false;
            return false;
        }
    }

    btnSave.Enabled = true;  // enable button if all good
    return true;
}
