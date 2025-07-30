..
using Dapper;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

[HttpGet]
public async Task<IActionResult> EmpTaggingMaster(Guid? id, string searchString = "", int page = 1)
{
    var UserId = HttpContext.Request.Cookies["Session"];
    if (string.IsNullOrEmpty(UserId))
        return RedirectToAction("Login", "User");

    // Check permission
    var allowedPnos = context.AppPermissionMasters.Select(x => x.Pno).ToList();
    if (!allowedPnos.Contains(UserId))
        return RedirectToAction("Login", "User");

    // Use your connection string
    string connectionString = GetRFIDConnectionString();

    // Final list for dropdown
    List<SelectListItem> pnoList = new();
    List<SelectListItem> worksiteList = new();

    // Final list for table
    List<EmpDetailsViewModel> empList = new();

    using (var conn = new SqlConnection(connectionString))
    {
        await conn.OpenAsync();

        // Get department of logged-in user
        string getDeptSql = "SELECT DepartmentName FROM App_Empl_Master WHERE Pno = @Pno";
        var department = await conn.QueryFirstOrDefaultAsync<string>(getDeptSql, new { Pno = UserId });

        if (string.IsNullOrEmpty(department))
            return Unauthorized();

        // Get all employees in same department with position and worksite
        string getEmpSql = @"
            SELECT 
                E.Pno,
                E.Name,
                EP.Position,
                ISNULL(
                    STUFF((
                        SELECT ', ' + L.Work_Site
                        FROM App_Position_Worksite PW
                        JOIN App_LocationMaster L ON PW.Worksite = L.ID
                        WHERE PW.Position = EP.Position
                        FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, ''
                ), '') AS Worksites
            FROM App_Empl_Master E
            LEFT JOIN App_Emp_Position EP ON E.Pno = EP.Pno
            WHERE E.DepartmentName = @Department
            ORDER BY E.Pno";

        var empResults = await conn.QueryAsync<EmpDetailsViewModel>(getEmpSql, new { Department = department });
        empList = empResults.ToList();

        // Filter by search string
        if (!string.IsNullOrEmpty(searchString))
        {
            empList = empList
                .Where(e => e.Pno.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                         || (e.Name != null && e.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        // Pagination
        int pageSize = 5;
        var pagedData = empList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        ViewBag.pList = pagedData;
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(empList.Count / (double)pageSize);
        ViewBag.SearchString = searchString;

        // Dropdown PNOs
        pnoList = empList.Select(e => new SelectListItem
        {
            Value = e.Pno,
            Text = e.Pno
        }).ToList();

        ViewBag.PnoList = pnoList;

        // Worksite list
        string wsQuery = "SELECT ID, Work_Site FROM App_LocationMaster";
        var worksites = await conn.QueryAsync(wsQuery);
        foreach (var ws in worksites)
        {
            worksiteList.Add(new SelectListItem
            {
                Value = ws.ID.ToString(),
                Text = ws.Work_Site.ToString()
            });
        }

        ViewBag.WorksiteList = worksiteList;
    }

    return View();
}
