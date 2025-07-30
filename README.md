using Dapper;
using System.Data.SqlClient;

public async Task<IActionResult> EmpTaggingMaster()
{
    // 1. Get logged-in PNO
    var UserId = HttpContext.Request.Cookies["Session"];
    if (string.IsNullOrEmpty(UserId))
        return RedirectToAction("Login", "User");

    // 2. Connection string (use your actual method)
    string connectionString = GetRFIDConnectionString();

    List<SelectListItem> pnoList = new();

    using (var conn = new SqlConnection(connectionString))
    {
        await conn.OpenAsync();

        // 3. Find department of logged-in PNO
        string getDeptSql = "SELECT DeptName FROM App_Empl_Master WHERE Pno = @Pno";
        var department = await conn.QueryFirstOrDefaultAsync<string>(getDeptSql, new { Pno = UserId });

        if (string.IsNullOrEmpty(department))
            return Unauthorized();

        // 4. Get all PNOs in same department
        string getPnosSql = @"
            SELECT Pno 
            FROM App_Empl_Master 
            WHERE DeptName = @DeptName
            ORDER BY Pno";

        var pnos = await conn.QueryAsync<string>(getPnosSql, new { DeptName = department });

        // 5. Convert to SelectListItem
        pnoList = pnos.Select(p => new SelectListItem
        {
            Value = p,
            Text = p
        }).ToList();
    }

    // 6. Pass to view
    ViewBag.PnoList = pnoList;
    return View();
}
