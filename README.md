private bool Check()
{
    // First remove any old highlighting
    foreach (GridViewRow r in HalfYearly_Records.Rows)
        r.BackColor = System.Drawing.Color.Transparent;

    foreach (GridViewRow row in HalfYearly_Records.Rows)
    {
        string licNo = row.Cells[6].Text.Trim();

        int capacity        = Convert.ToInt32(row.Cells[7].Text);
        int empMale         = Convert.ToInt32(row.Cells[13].Text);
        int empFemale       = Convert.ToInt32(row.Cells[14].Text);

        int mandaysMale     = Convert.ToInt32(row.Cells[15].Text);
        int mandaysFemale   = Convert.ToInt32(row.Cells[16].Text);

        int daysWorked      = Convert.ToInt32(row.Cells[17].Text); 

        int totalEmployees  = empMale + empFemale;
        int totalMandays    = mandaysMale + mandaysFemale;
        int maxAllowedDays  = totalEmployees * daysWorked;

        // -------------------------
        // ❌ VALIDATION 1: Employees > Capacity
        // -------------------------
        if (totalEmployees > capacity)
        {
            // Highlight row
            row.BackColor = System.Drawing.Color.LightCoral;

            string msg = $"❌ Employee count exceeded!\n\n" +
                         $"License No: {licNo}\n" +
                         $"Capacity Allowed: {capacity}\n" +
                         $"Entered Employees (M+F): {totalEmployees}";

            ScriptManager.RegisterStartupScript(
                this, this.GetType(), "alert",
                $"alert('{msg}');" +
                $"document.getElementById('{row.ClientID}').scrollIntoView();", true);

            btnSave.Enabled = false;
            return false;
        }

        // -------------------------
        // ❌ VALIDATION 2: Mandays > TotalEmployees × DaysWorked
        // -------------------------
        if (totalMandays > maxAllowedDays)
        {
            // Highlight row
            row.BackColor = System.Drawing.Color.LightCoral;

            string msg = $"❌ Mandays limit exceeded!\n\n" +
                         $"License No: {licNo}\n" +
                         $"Total Employees (M+F): {totalEmployees}\n" +
                         $"Days Worked: {daysWorked}\n" +
                         $"Maximum Allowed Mandays: {maxAllowedDays}\n" +
                         $"Entered Mandays (M+F): {totalMandays}";

            ScriptManager.RegisterStartupScript(
                this, this.GetType(), "alert",
                $"alert('{msg}');" +
                $"document.getElementById('{row.ClientID}').scrollIntoView();", true);

            btnSave.Enabled = false;
            return false;
        }
    }

    // ✔ Everything OK
    btnSave.Enabled = true;
    return true;
}
