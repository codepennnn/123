[HttpGet]
public async Task<IActionResult> EmpTaggingMaster(Guid? id)
{
    var UserId = HttpContext.Request.Cookies["Session"];
    if (string.IsNullOrEmpty(UserId))
        return RedirectToAction("Login", "User");

    ViewBag.CreatedBy = UserId;

  
    var loggedInEmp = await context.AppCoordinatorMasters.FirstOrDefaultAsync(e => e.Pno == UserId);
    if (loggedInEmp == null)
        return Unauthorized();

    string userDept = loggedInEmp.DeptName;

 
    ViewBag.PnoList = context.AppCoordinatorMasters
        .Where(e => e.DeptName == userDept)
        .Select(e => new SelectListItem
        {
            Value = e.Pno,
            Text = e.Pno
        }).ToList();





    // Get Worksite list via SQL
    List<SelectListItem> worksiteList = new();
    string connectionString = GetRFIDConnectionString();
    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        await conn.OpenAsync();
        string query = "SELECT ID, Work_Site FROM App_LocationMaster";
        using (SqlCommand cmd = new SqlCommand(query, conn))
        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                worksiteList.Add(new SelectListItem
                {
                    Value = reader["ID"].ToString(),
                    Text = reader["Work_Site"].ToString()
                });
            }
        }
    }

    ViewBag.WorksiteList = worksiteList;

    return View();
}





[HttpPost]
public async Task<IActionResult> EmpTaggingMaster(string Pno, int Position, string Worksite)
{
    var UserId = HttpContext.Request.Cookies["Session"];
    if (string.IsNullOrEmpty(UserId))
        return RedirectToAction("Login", "User");

    // --- Update or Insert AppEmpPosition ---
    var empPosition = await context.AppEmpPositions.FirstOrDefaultAsync(e => e.Pno == Pno);
    if (empPosition != null)
    {
        empPosition.Position = Position;
        context.AppEmpPositions.Update(empPosition);
    }
    else
    {
        empPosition = new AppEmpPosition
        {
            Id = Guid.NewGuid(),
            Pno = Pno,
            Position = Position
        };
        await context.AppEmpPositions.AddAsync(empPosition);
    }

    // --- Replace worksite entries ---
    var oldWorksites = context.AppPositionWorksites.Where(w => w.Position == Position).ToList();
    context.AppPositionWorksites.RemoveRange(oldWorksites);

    // Convert hidden input string to list
    var worksiteIds = Worksite?.Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToList() ?? new();

    foreach (var wsId in worksiteIds)
    {
        var ws = new AppPositionWorksite
        {
            Id = Guid.NewGuid(),
            Position = Position,
            Worksite = wsId,
            CreatedBy = UserId,
            CreatedOn = DateTime.Now
        };
        await context.AppPositionWorksites.AddAsync(ws);
    }

    await context.SaveChangesAsync();
    TempData["msg"] = "Tagged Successfully!";
    return RedirectToAction("EmpTaggingMaster");
}


