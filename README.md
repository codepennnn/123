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
    List<SelectListItem> pnoList = new();

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

        // 2. Get employee PNOs + Name + Position + Worksites in the same department
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

        // 3. PNO dropdown (all PNOs from same department)
        pnoList = empList
            .Select(e => new SelectListItem
            {
                Value = e.Pno,
                Text = e.Pno
            })
            .DistinctBy(x => x.Value) // Avoid duplicate PNOs if any
            .ToList();

        // 4. Worksite dropdown
        string wsQuery = "SELECT ID, Work_Site FROM App_LocationMaster";
        var worksites = await conn.QueryAsync(wsQuery);
        worksiteList = worksites.Select(ws => new SelectListItem
        {
            Value = ws.ID.ToString(),
            Text = ws.Work_Site.ToString()
        }).ToList();
    }

    // 5. Filter by search string (optional)
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

    // Pass to view
    ViewBag.pList = pagedData;
    ViewBag.CurrentPage = page;
    ViewBag.TotalPages = (int)Math.Ceiling(empList.Count / (double)pageSize);
    ViewBag.SearchString = searchString;
    ViewBag.PnoList = pnoList;
    ViewBag.WorksiteList = worksiteList;

    return View();
}
