private bool Check()
{
    // Dictionary to accumulate totals by License No
    var licenseTotals = new Dictionary<string, (int totalM, int totalF, double mandaysM, double mandaysF, int capacity, double daysWorked)>();

    foreach (GridViewRow row in HalfYearly_Records.Rows)
    {
        string licNo = row.Cells[6].Text.Trim();

        int capacity = Convert.ToInt32(row.Cells[7].Text);
        int sexM = Convert.ToInt32(row.Cells[13].Text);
        int sexF = Convert.ToInt32(row.Cells[14].Text);

        double mandaysMale = Convert.ToDouble(row.Cells[15].Text);
        double mandaysFemale = Convert.ToDouble(row.Cells[16].Text);
        double daysWorked = Convert.ToDouble(row.Cells[12].Text); // Same for each license

        // Add to dictionary
        if (!licenseTotals.ContainsKey(licNo))
        {
            licenseTotals[licNo] = (0, 0, 0, 0, capacity, daysWorked);
        }

        var existing = licenseTotals[licNo];
        existing.totalM += sexM;
        existing.totalF += sexF;
        existing.mandaysM += mandaysMale;
        existing.mandaysF += mandaysFemale;
        existing.capacity = capacity;      // same for all entries
        existing.daysWorked = daysWorked;  // same for all entries

        licenseTotals[licNo] = existing;
    }

    // NOW validate totals PER LICENSE
    foreach (var item in licenseTotals)
    {
        string licNo = item.Key;
        int totalEmp = item.Value.totalM + item.Value.totalF;
        int capacity = item.Value.capacity;

        double totalMandays = item.Value.mandaysM + item.Value.mandaysF;
        double maxAllowedMandays = totalEmp * item.Value.daysWorked;

        // ---- VALIDATION 1: Employees > Capacity ----
        if (totalEmp > capacity)
        {
            string msg = $"Total Workers (M+F) for License {licNo} = {totalEmp} exceeds Capacity = {capacity}";

            ScriptManager.RegisterStartupScript(
                this, this.GetType(),
                "alert",
                $"alert('{msg}');",
                true);

            btnSave.Enabled = false;
            return false;
        }

        // ---- VALIDATION 2: Mandays > Allowed ----
        if (totalMandays > maxAllowedMandays)
        {
            string msg = $"Total Mandays for License {licNo} exceeds allowed limit.\n\n" +
                         $"Total Employees: {totalEmp}\n" +
                         $"Days Worked: {item.Value.daysWorked}\n" +
                         $"Max Allowed Mandays: {maxAllowedMandays}\n" +
                         $"Entered Mandays: {totalMandays}";

            ScriptManager.RegisterStartupScript(
                this, this.GetType(),
                "alert",
                $"alert('{msg}');",
                true);

            btnSave.Enabled = false;
            return false;
        }
    }

    btnSave.Enabled = true;
    return true;
}
