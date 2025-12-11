// In BL_Half_Yearly.cs (or your BL class)
public double GetWageRateForWorkOrder(string workOrderNo, string yearWage, string vendorCode)
{
    string strSQL = @"
SELECT TOP 1 MAX(BasicRate) + MAX(DARate) AS Rate
FROM App_WagesDetailsJharkhand
WHERE YearWage = @YearWage
  AND MonthWage = (
        SELECT DISTINCT TOP (1) MonthWage
        FROM App_WagesDetailsJharkhand
        WHERE WorkOrderNo = @WO
          AND VendorCode = @VC
          AND MonthWage IN (1,2,3,4,5,6)   -- keep same months as your business rule
          AND YearWage = @YearWage
        ORDER BY MonthWage DESC
    )
  AND WorkOrderNo = @WO
  AND VendorCode = @VC
GROUP BY WorkManCategory
ORDER BY
    CASE
        WHEN WorkManCategory = 'Unskilled'      THEN 1
        WHEN WorkManCategory = 'Semi Skilled'   THEN 2
        WHEN WorkManCategory = 'Skilled'        THEN 3
        WHEN WorkManCategory = 'Highly Skilled' THEN 4
        ELSE 5
    END";

    Dictionary<string, object> objParam = new Dictionary<string, object>();
    DataHelper dh = new DataHelper();

    objParam.Add("WO", workOrderNo);
    objParam.Add("VC", vendorCode);
    objParam.Add("YearWage", yearWage);

    DataSet ds = dh.GetDataset(strSQL, "App_WagesDetailsJharkhand", objParam);

    if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        return 0.0;

    // safe parse
    double rate = 0.0;
    double.TryParse(ds.Tables[0].Rows[0]["Rate"].ToString(), out rate);
    return rate;
}


private bool Check()
{
    // --------------- utility local funcs ----------------
    double SafeDouble(string s)
    {
        double v;
        if (double.TryParse(s, out v)) return v;
        return 0.0;
    }

    int SafeInt(string s)
    {
        int v;
        if (int.TryParse(s, out v)) return v;
        return 0;
    }
    // ----------------------------------------------------

    // Use dictionary per License -> then inside per WorkOrder accumulation
    // licenseTotals: sums across rows (for capacity and mandays checks)
    var licenseTotals = new Dictionary<string, (int totalM, int totalF, double mandaysM, double mandaysF, int capacity, double daysWorked)>();

    // workorderTotals[licNo][workOrder] = (mandaysSum, grossSum)
    var workorderTotals = new Dictionary<string, Dictionary<string, (double mandays, double gross)>>();

    // get vendor/year info for BL call
    string vendorCode = Session["Username"]?.ToString() ?? "";
    string yearWage = Year.SelectedValue; // or get from PageRecordDataSet if you prefer

    BL_Half_Yearly bl = new BL_Half_Yearly();

    foreach (GridViewRow row in HalfYearly_Records.Rows)
    {
        // decode licence text in case of HTML encoded quotes
        string raw = WebUtility.HtmlDecode(row.Cells[6].Text ?? "");
        string licNo = raw.Trim();

        // read values safely (change indexes if your columns differ)
        int capacity = SafeInt(row.Cells[7].Text);
        int sexM = SafeInt(row.Cells[13].Text);
        int sexF = SafeInt(row.Cells[14].Text);

        double mandaysMale = SafeDouble(row.Cells[15].Text);
        double mandaysFemale = SafeDouble(row.Cells[16].Text);

        // DaysWorked (Establishment_Of_Principal_Emp_Worked) - same per license (assumption)
        double daysWorked = SafeDouble(row.Cells[12].Text);

        // Gross wages (Male + Female) - exclude children as requested
        double grossMale = SafeDouble(row.Cells[/* put actual Gross(M) column index */ 20].Text); // <-- replace 20 with actual index
        double grossFemale = SafeDouble(row.Cells[/* put actual Gross(F) column index */ 21].Text); // <-- replace 21 with actual index
        double grossRow = grossMale + grossFemale;

        // WorkOrderNo column
        string workOrderNo = WebUtility.HtmlDecode(row.Cells[11].Text ?? "").Trim(); // cell 11 used earlier

        // ---------- accumulate licenseTotals ----------
        if (!licenseTotals.ContainsKey(licNo))
            licenseTotals[licNo] = (0, 0, 0.0, 0.0, capacity, daysWorked);

        var existing = licenseTotals[licNo];
        existing.totalM += sexM;
        existing.totalF += sexF;
        existing.mandaysM += mandaysMale;
        existing.mandaysF += mandaysFemale;
        existing.capacity = capacity;       // keep last seen capacity (should be same)
        existing.daysWorked = daysWorked;   // keep last seen daysWorked
        licenseTotals[licNo] = existing;

        // ---------- accumulate workorderTotals ----------
        if (!workorderTotals.ContainsKey(licNo))
            workorderTotals[licNo] = new Dictionary<string, (double mandays, double gross)>();

        if (!workorderTotals[licNo].ContainsKey(workOrderNo))
            workorderTotals[licNo][workOrderNo] = (0.0, 0.0);

        var wexist = workorderTotals[licNo][workOrderNo];
        wexist.mandays += (mandaysMale + mandaysFemale);
        wexist.gross += grossRow;
        workorderTotals[licNo][workOrderNo] = wexist;
    }

    // ----------- 1) VALIDATIONS per license (capacity & mandays) -----------
    foreach (var kv in licenseTotals)
    {
        string licNo = kv.Key;
        int totalEmp = kv.Value.totalM + kv.Value.totalF;
        int capacity = kv.Value.capacity;
        double totalMandays = kv.Value.mandaysM + kv.Value.mandaysF;
        double maxAllowedMandays = capacity * kv.Value.daysWorked;

        // Employee count > capacity
        if (totalEmp > capacity)
        {
            string msg = $"Employee count exceeded for License: {licNo}\\n" +
                         $"Capacity Allowed: {capacity}\\n" +
                         $"Entered Employees (M+F): {totalEmp}";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"alert('{msg}');", true);

            btnSave.Enabled = false;
            return false;
        }

        // Mandays validation
        if (totalMandays > maxAllowedMandays)
        {
            string msg = $"Mandays limit exceeded for License: {licNo}\\n" +
                         $"Total Employees (M+F): {totalEmp}\\n" +
                         $"Days Worked: {kv.Value.daysWorked}\\n" +
                         $"Max Allowed Mandays (Capacity × DaysWorked): {maxAllowedMandays}\\n" +
                         $"Entered Mandays (M+F): {totalMandays}";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"alert('{msg}');", true);

            btnSave.Enabled = false;
            return false;
        }
    }

    // ----------- 2) NEW Wage-Rate Validation per license (sum across workorders) -----------
    foreach (var licEntry in workorderTotals)
    {
        string licNo = licEntry.Key;
        double totalExpectedForLicense = 0.0; // sum of (rate * mandays) across all workorders for this license
        double totalGrossForLicense = 0.0;   // sum of gross (M+F) across all workorders for this license

        foreach (var woEntry in licEntry.Value)
        {
            string workOrder = woEntry.Key;
            double mandaysForWO = woEntry.Value.mandays;
            double grossForWO = woEntry.Value.gross;

            // get rate from BL (your GetDataset style)
            double rate = bl.GetWageRateForWorkOrder(workOrder, yearWage, vendorCode);

            // expected for this workorder
            double expectedForWO = rate * mandaysForWO;

            totalExpectedForLicense += expectedForWO;
            totalGrossForLicense += grossForWO;
        }

        // compare totals for the license
        if (totalGrossForLicense > totalExpectedForLicense)
        {
            string msg = $"Gross wages exceed expected limit for License: {licNo}\\n\\n" +
                         $"Total Gross (M+F) for license (all workorders): {totalGrossForLicense:F2}\\n" +
                         $"Total Allowed (sum of Rate × Mandays for each workorder): {totalExpectedForLicense:F2}\\n" +
                         $"Please check rates / mandays for the workorders under this license.";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"alert('{msg}');", true);

            btnSave.Enabled = false;
            return false;
        }
    }

    // all checks passed
    btnSave.Enabled = true;
    return true;
}
