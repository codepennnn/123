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






     using (var conn = new SqlConnection(connectionString))
     {
         await conn.OpenAsync();

         // 1. Get logged-in user's department
         string deptSql = "SELECT DepartmentName FROM App_Empl_Master WHERE Pno = @Pno";
         string department = await conn.QueryFirstOrDefaultAsync<string>(deptSql, new { Pno = UserId });

         // 2. Get employee PNOs + Name + Position + Worksites
         string dataSql = @"
     SELECT distinct Pno from App_Empl_Master Where DepartmentName=@Department";

         var data = await conn.QueryAsync(dataSql, new { DeptName = department });
         // Pno Dropdown Only match depatment of logged PNOs
         pnoList = empList.Select(e => new SelectListItem
         {
             Value = e.Pno,
             Text = e.Pno
         }).ToList();

         ViewBag.PnoList = pnoList;
     }


















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

