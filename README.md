NullReferenceException: Object reference not set to an instance of an object.
AspNetCoreGeneratedDocument.Views_Master_CoordinatorMaster.<ExecuteAsync>b__17_3() in CoordinatorMaster.cshtml
+
                        @foreach (var item in ViewBag.PnoList as List<SelectListItem>)






   <div class="col-sm-3">
   <div class="mb-3">
       <label for="Pno" class="form-label">PNO</label>
       <select name="Coordinators[0].Pno" id="Pno" class="form-control">
           <option value="">-- Select PNO --</option>
           @foreach (var item in ViewBag.PnoList as List<SelectListItem>)
           {
               <option value="@item.Value">@item.Text</option>
           }
       </select>
   </div>


   </div>



           <div class="col-md-4">
               <label>Deptartment</label>

               <div class="dropdown">
                   <input class="form-control form-control-sm" placeholder="Select Depts"
                          type="button" id="DeptDropdown" data-bs-toggle="dropdown" aria-expanded="false" />

                   <ul class="dropdown-menu w-100" aria-labelledby="DeptDropdown" id="locationList" style="max-height: 200px; overflow-y: auto;">
                       @foreach (var item in ViewBag.DeptList as List<SelectListItem>)
                       {
                           <li style="margin-left:5%;">
                               <div class="form-check">
                                   <input type="checkbox" class="form-check-input Dept-checkbox"
                                          value="@item.Value" id="Dept_@item.Value" />
                                   <label class="form-check-label" for="Dept_@item.Value">@item.Text</label>
                               </div>
                           </li>
                       }
                   </ul>
               </div>

               <!-- This hidden field stores selected values -->
               <input type="hidden" id="Dept" name="Dept" />





           </div>



and 


        [HttpGet]
        public async Task<IActionResult> CoordinatorMaster(Guid? id, string searchString = "", int page = 1)
        {
            var UserId = HttpContext.Request.Cookies["Session"];

            if (string.IsNullOrEmpty(UserId))
                return RedirectToAction("Login", "User");

            //var allowedPnos = context.AppPermissionMasters.Select(x => x.Pno).ToList();
            //if (!allowedPnos.Contains(UserId))
            //    return RedirectToAction("Login", "User");

            ViewBag.CreatedBy = UserId;



          



            string connectionString = GetRFIDConnectionString();

            var deptList = new List<SelectListItem>();
            var PnoList = new List<SelectListItem>();
           

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();


                string query1 = "select ema_perno from SAPHRDB.dbo.T_Empl_All";
                using (SqlCommand cmd = new SqlCommand(query1, conn))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            PnoList.Add(new SelectListItem
                            {
                                Value = reader["ema_perno"].ToString(),
                                Text = reader["ema_perno"].ToString()
                            });
                        }
                    }
                }





             
                string query = "select distinct DepartmentName from App_Empl_Master order by DepartmentName";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            deptList.Add(new SelectListItem
                            {
                                Value = reader["DepartmentName"].ToString(),
                                Text = reader["DepartmentName"].ToString()
                            });
                        }
                    }
                }
            }

            ViewBag.DeptList = deptList;


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


            if (id.HasValue)
            {
                var model = await context.AppCoordinatorMasters.FindAsync(id.Value);
                if (model == null)
                    return NotFound();

                return Json(new
                {
                    id = model.Id,
                    pno = model.Pno,
                    dept = model.DeptName,
                    createdby = model.CreatedBy,
                    createdon = model.CreatedOn
                });
            }

            return View(new AppCoordinatorMaster());
        }

