public DataSet Get_BasicRate_By_Lic(string vcode, string licNo, string year, string wageMonthList)
{
    string sql = @"
        SELECT 
            WorkManCategory,
            MAX(BasicRate) + MAX(DARate) AS Rate
        FROM App_WagesDetailsJharkhand
        WHERE YearWage = @year
          AND MonthWage = (
                SELECT TOP 1 MonthWage
                FROM App_WagesDetailsJharkhand
                WHERE VendorCode = @vcode
                  AND WorkOrderNo IN (
                        SELECT WorkOrderNo 
                        FROM App_LabourLicenseSubmission 
                        WHERE LicNo = @licNo
                  )
                  AND MonthWage IN (" + wageMonthList + @")
                  AND YearWage = @year
                ORDER BY MonthWage DESC
          )
          AND VendorCode = @vcode
          AND WorkOrderNo IN (
               SELECT WorkOrderNo 
               FROM App_LabourLicenseSubmission 
               WHERE LicNo = @licNo
          )
        GROUP BY WorkManCategory
        ORDER BY 
            CASE 
                WHEN WorkManCategory='Unskilled' THEN 1
                WHEN WorkManCategory='Semi Skilled' THEN 2
                WHEN WorkManCategory='Skilled' THEN 3
                WHEN WorkManCategory='Highly Skilled' THEN 4
                ELSE 5
            END";

    Dictionary<string, object> param = new Dictionary<string, object>();
    param.Add("vcode", vcode);
    param.Add("licNo", licNo);
    param.Add("year", year);

    DataHelper dh = new DataHelper();
    return dh.GetDataset(sql, "BasicRateData", param);
}


// NEW VALIDATION: Compare Gross Wage with Allowed Amount based on BasicRate × Total Mandays
BL_Half_Yearly blobj = new BL_Half_Yearly();
string vcode = Session["UserName"].ToString();
string year = Year.SelectedValue;
string period = SearchPeriod.SelectedValue;

string wageMonth = (period == "Jan-June") ? "1,2,3,4,5,6" : "7,8,9,10,11,12";

foreach (var item in licenseTotals)
{
    string licNo = item.Key;

    double totalMandays = item.Value.mandaysM + item.Value.mandaysF + item.Value.mandaysF;
    double grossWages = 0;

    // Get sum of Gross wages from grid row
    foreach (GridViewRow row in HalfYearly_Records.Rows)
    {
        string lic = row.Cells[6].Text.Trim();
        if (lic == licNo)
        {
            double m = Convert.ToDouble(row.Cells[18].Text);
            double f = Convert.ToDouble(row.Cells[19].Text);
            double c = Convert.ToDouble(row.Cells[20].Text);

            grossWages += (m + f + c);
        }
    }

    // Fetch basic rate from BL
    DataSet dsRate = blobj.Get_BasicRate_By_Lic(vcode, licNo, year, wageMonth);

    if (dsRate == null || dsRate.Tables[0].Rows.Count == 0)
        continue;

    double allowedRate = 0;
    foreach (DataRow r in dsRate.Tables[0].Rows)
    {
        allowedRate += Convert.ToDouble(r["Rate"]);
    }

    double allowedAmount = allowedRate * totalMandays;

    if (grossWages > allowedAmount)
    {
        string msg = $"Gross Wages for License {licNo} exceed limit.\n" +
                     $"Total Mandays = {totalMandays}\n" +
                     $"Allowed Amount = {allowedAmount}\n" +
                     $"Entered Gross = {grossWages}";

        ScriptManager.RegisterStartupScript(
            this, this.GetType(),
            "alertExceed",
            $"alert('{msg}');",
            true
        );

        return false;
    }
}

