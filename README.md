..
using System.Data.SqlClient;
using Dapper;

[HttpGet]
public async Task<IActionResult> EmpTaggingMaster()
{
    var UserId = HttpContext.Request.Cookies["Session"];
    if (string.IsNullOrEmpty(UserId))
        return RedirectToAction("Login", "User");

    string connectionString = GetRFIDConnectionString();

    using (var conn = new SqlConnection(connectionString))
    {
        await conn.OpenAsync();

        // 1. Get logged-in user's department
        string deptSql = "SELECT DepartmentName FROM App_Empl_Master WHERE Pno = @Pno";
        string department = await conn.QueryFirstOrDefaultAsync<string>(deptSql, new { Pno = UserId });

        // 2. Get employee PNOs + Name + Position + Worksites
        string dataSql = @"
            SELECT 
                e.Pno,
                e.Name,
                ep.Position,
                STRING_AGG(l.Work_Site, ', ') AS Worksites
            FROM App_Empl_Master e
            LEFT JOIN App_Emp_Position ep ON e.Pno = ep.Pno
            LEFT JOIN App_Position_Worksite pw ON ep.Position = pw.Position
            LEFT JOIN App_LocationMaster l ON pw.Worksite = l.ID
            WHERE e.DepartmentName = @DeptName
            GROUP BY e.Pno, e.Name, ep.Position
            ORDER BY e.Pno";

        var data = await conn.QueryAsync(dataSql, new { DeptName = department });

        ViewBag.pList = data;
    }

    return View();
}
