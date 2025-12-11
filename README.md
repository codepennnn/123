// Compare Gross Wage > BaiscRate * TotalMandays
BL_Half_Yearly blobj = new BL_Half_Yearly();
string vcode = Session["UserName"].ToString();
string year = Year.SelectedValue;
string period = SearchPeriod.SelectedValue;
string wageMonth = (period == "Jan-June") ? "1,2,3,4,5,6" : "7,8,9,10,11,12";

foreach (var item in licenseTotals)
{
    string licNo = item.Key;
    double totalMandays = item.Value.mandaysM + item.Value.mandaysF;

    // 🔹 Step 1: Collect WORKORDER LIST for this license
    List<string> wos = new List<string>();

    foreach (GridViewRow row in HalfYearly_Records.Rows)
    {
        string lic = row.Cells[6].Text.Trim();
        if (lic == licNo)
        {
            string wo = row.Cells[11].Text.Trim(); // WorkOrderNo column index verify once
            if (!wos.Contains(wo))
                wos.Add(wo);
        }
    }

    if (wos.Count == 0)
        continue;

    string woList = string.Join(",", wos.Select(x => $"'{x}'"));

    // 🔹 Step 2: Compute gross wages
    double grossWages = 0;
    foreach (GridViewRow row in HalfYearly_Records.Rows)
    {
        string lic = row.Cells[6].Text.Trim();
        if (lic == licNo)
        {
            double gm = Convert.ToDouble(row.Cells[21].Text);
            double gf = Convert.ToDouble(row.Cells[22].Text);
            grossWages += (gm + gf);
        }
    }

    // 🔹 Step 3: Fetch BASIC RATE using WORKORDER (NOT LICENSE)
    DataSet dsRate = blobj.Get_BasicRate_By_WorkOrder(vcode, woList, year, wageMonth);

    if (dsRate == null || dsRate.Tables[0].Rows.Count == 0)
        continue;

    double allowedRate = dsRate.Tables[0].AsEnumerable().Sum(r => Convert.ToDouble(r["Rate"]));

    double allowedAmount = allowedRate * totalMandays;

    // 🔹 Step 4: Validate
    if (grossWages > allowedAmount)
    {
        string msg = $"Gross Wages for License {licNo} exceed allowed limit.\n" +
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

public DataSet Get_BasicRate_By_WorkOrder(string vcode, string woList, string year, string wageMonthList)
{
    string strSQL = @"
        SELECT WorkManCategory,
               MAX(BasicRate) + MAX(DARate) AS Rate
        FROM App_WagesDetailsJharkhand
        WHERE YearWage = @year
          AND MonthWage = (
                SELECT TOP 1 MonthWage
                FROM App_WagesDetailsJharkhand
                WHERE VendorCode = @vcode
                  AND WorkOrderNo IN (" + woList + @")
                  AND MonthWage IN (" + wageMonthList + @")
                  AND YearWage = @year
                ORDER BY MonthWage DESC
          )
          AND VendorCode = @vcode
          AND WorkOrderNo IN (" + woList + @")
        GROUP BY WorkManCategory
        ORDER BY 
            CASE 
                WHEN WorkManCategory='Unskilled' THEN 1
                WHEN WorkManCategory='Semi Skilled' THEN 2
                WHEN WorkManCategory='Skilled' THEN 3
                WHEN WorkManCategory='Highly Skilled' THEN 4
                ELSE 5
            END";

    Dictionary<string, object> objParam = new Dictionary<string, object>();
    objParam.Add("vcode", vcode);
    objParam.Add("year", year);

    DataHelper dh = new DataHelper();
    return dh.GetDataset(strSQL, "App_Half_Yearly_Details", objParam);
}

