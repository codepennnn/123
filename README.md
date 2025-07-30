  [HttpGet]
  public async Task<IActionResult> EmpTaggingMaster(Guid? id, string searchString = "", int page = 1)
  {
      var UserId = HttpContext.Request.Cookies["Session"];

      if (string.IsNullOrEmpty(UserId))
          return RedirectToAction("Login", "User");

      var allowedPnos = context.AppPermissionMasters.Select(x => x.Pno).ToList();
      if (!allowedPnos.Contains(UserId))
          return RedirectToAction("Login", "User");


   
      string connectionString = GetRFIDConnectionString();

      List<SelectListItem> pnoList = new();

      using (var conn = new SqlConnection(connectionString))
      {
          await conn.OpenAsync();

          // 3. Find department of logged-in PNO
          string getDeptSql = "SELECT DepartmentName FROM App_Empl_Master WHERE Pno = @Pno";
          var department = await conn.QueryFirstOrDefaultAsync<string>(getDeptSql, new { Pno = UserId });

          // 4. Get all PNOs in same department
          string getPnosSql = @"
      SELECT distinct Pno 
      FROM App_Empl_Master 
      WHERE DepartmentName = @department
      ORDER BY Pno";

          var pnos = await conn.QueryAsync<string>(getPnosSql, new { DeptName = department });

          // 5. Convert to SelectListItem
          pnoList = pnos.Select(p => new SelectListItem
          {
              Value = p,
              Text = p
          }).ToList();
      }







      int pageSize = 5;
      var queryCoordinators = context.AppCoordinatorMasters.AsQueryable();

      if (!string.IsNullOrEmpty(searchString))
          queryCoordinators = queryCoordinators.Where(c => c.Pno.Contains(searchString));

      var data = queryCoordinators.OrderBy(c => c.Pno).ToList();
      var pagedData = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

      ViewBag.pList = pagedData;
      ViewBag.CurrentPage = page;
      ViewBag.TotalPages = (int)Math.Ceiling(data.Count / (double)pageSize);
      ViewBag.SearchString = searchString;













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

