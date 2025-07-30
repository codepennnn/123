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

    string connectionString = GetRFIDConnectionString();

    List<dynamic> empList = new();
    List<SelectListItem> worksiteList = new();
    List<SelectListItem> pnoDropdown = new(); // 👈 for dropdown

    using (var conn = new SqlConnection(connectionString))
    {
        await conn.OpenAsync();

        // 1. Get logged-in user's department
        string deptSql = "SELECT DepartmentName FROM App_Empl_Master WHERE Pno = @Pno";
        var department = await conn.QueryFirstOrDefaultAsync<string>(deptSql, new { Pno = UserId });

        if (string.IsNullOrEmpty(department))
        {
            TempData["msg"] = "Department not found for logged-in user.";
            return View();
        }

        // 2. Get employee data (Pno, Name, Position, Worksites)
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

        empList = (await conn.QueryAsync(dataSql, new { DeptName = department })).ToList();

        // 3. Prepare Pno dropdown (only Pno from department)
        string pnoSql = @"SELECT DISTINCT Pno FROM App_Empl_Master WHERE DepartmentName = @DeptName ORDER BY Pno";
        var rawPnos = await conn.QueryAsync<string>(pnoSql, new { DeptName = department });

        pnoDropdown = rawPnos.Select(p => new SelectListItem
        {
            Value = p,
            Text = p
        }).ToList();

        // 4. Worksite list
        string wsQuery = "SELECT ID, Work_Site FROM App_LocationMaster";
        var worksites = await conn.QueryAsync(wsQuery);
        worksiteList = worksites.Select(ws => new SelectListItem
        {
            Value = ws.ID.ToString(),
            Text = ws.Work_Site.ToString()
        }).ToList();
    }

    // 5. Search filter
    if (!string.IsNullOrEmpty(searchString))
    {
        empList = empList
            .Where(e => ((string)e.Pno).Contains(searchString, StringComparison.OrdinalIgnoreCase)
                     || (!string.IsNullOrEmpty((string)e.Name) && ((string)e.Name).Contains(searchString, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    // 6. Pagination
    int pageSize = 5;
    var pagedData = empList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

    // 7. Send to View
    ViewBag.pList = pagedData; // Table display
    ViewBag.PnoDropdown = pnoDropdown; // Dropdown Pno
    ViewBag.WorksiteList = worksiteList;

    ViewBag.CurrentPage = page;
    ViewBag.TotalPages = (int)Math.Ceiling(empList.Count / (double)pageSize);
    ViewBag.SearchString = searchString;

    return View();
}
