
<div id="formContainer" style="display:none;">
   

    <form asp-action="EmpTaggingMaster" asp-controller="Master" method="post">
        @Html.AntiForgeryToken()

        <div class="row">
            <!-- PNO Dropdown -->
            <div class="col-md-4">
                <label for="Pno">PNO</label>
                <select class="form-control" name="Pno" required>
                    <option value="">-- Select PNO --</option>
                    @foreach (var pno in ViewBag.PnoList as List<SelectListItem>)
                    {
                        <option value="@pno.Value">@pno.Text</option>
                    }
                </select>
            </div>

            <!-- Position TextBox -->
            <div class="col-md-4">
                <label for="Position">Position</label>
                <input type="number" name="Position" class="form-control" required />
            </div>

            <!-- Worksite Checkboxes -->
            <div class="col-md-4">
                <label>Worksites</label>
                <div class="form-control" style="height:auto;">
                    @foreach (var ws in ViewBag.WorksiteList as List<SelectListItem>)
                    {
                        <div class="form-check">
                            <input type="checkbox" class="form-check-input" name="WorksiteIds" value="@ws.Value" />
                            <label class="form-check-label">@ws.Text</label>
                        </div>
                    }
                </div>
            </div>
        </div>

        <div class="mt-3 text-center">
            <button type="submit" class="btn btn-success">Submit</button>
            <button type="submit" class="btn btn-danger" onclick="setAction('Delete', event)">Delete</button>
        </div>
    </form>



</div>




<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>


<script>
    function setAction(action, event) {
        if (action === 'Delete' && !confirm("Are you sure you want to delete this record?")) {
            event.preventDefault();
            return;
        }
        document.getElementById('actionType').value = action;
    }

    $(document).ready(function () {
        $('#showFormButton2').click(function () {
            $('#formContainer').show();
            $('#Pno, #DeptName').val('');
            $('#Id').val('');
        });

        $('.OpenFilledForm').click(function () {
            const id = $(this).data('id');
            $.ajax({
                url: '@Url.Action("CoordinatorMaster", "Master")',
                data: { id: id },
                success: function (data) {
                    $('#Id').val(data.id);
                    $('#Pno').val(data.pno);
                    $('#DeptName').val(data.dept);
                    $('#CreatedBy').val(data.createdby);
                    $('#CreatedOn').val(data.createdon);
                    $('#formContainer').show();
                },
                error: function () {
                    alert("Error loading data");
                }
            });
        });
    });
</script>

and 


  [HttpGet]
  public async Task<IActionResult> EmpTaggingMaster(Guid? id)
  {
      var UserId = HttpContext.Request.Cookies["Session"];
      if (string.IsNullOrEmpty(UserId))
          return RedirectToAction("Login", "User");

      ViewBag.CreatedBy = UserId;

      // Get logged-in user's department
      var loggedInEmp = await context.AppCoordinatorMasters.FirstOrDefaultAsync(e => e.Pno == UserId);
      if (loggedInEmp == null)
          return Unauthorized();

      string userDept = loggedInEmp.DeptName;

      // Filter PNOs by department
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
          string query = "SELECT Id, LocationName FROM App_LocationMaster";
          using (SqlCommand cmd = new SqlCommand(query, conn))
          using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
          {
              while (await reader.ReadAsync())
              {
                  worksiteList.Add(new SelectListItem
                  {
                      Value = reader["Id"].ToString(),
                      Text = reader["LocationName"].ToString()
                  });
              }
          }
      }

      ViewBag.WorksiteList = worksiteList;

      return View();
  }



  [HttpPost]
  public async Task<IActionResult> EmpTaggingMaster(string Pno, int Position, List<string> WorksiteIds)
  {
      var UserId = HttpContext.Request.Cookies["Session"];
      if (string.IsNullOrEmpty(UserId))
          return RedirectToAction("Login", "User");

      // Save in AppEmpPosition
      var empPosition = new AppEmpPosition
      {
          Id = Guid.NewGuid(),
          Pno = Pno,
          Position = Position
      };
      await context.AppEmpPositions.AddAsync(empPosition);

      // Save in AppPositionWorksite
      foreach (var wsId in WorksiteIds)
      {
          var ws = new AppPositionWorksite
          {
              Id = Guid.NewGuid(),
              Position = Position,
              Worksite = wsId, // Save LocationMaster Id
              CreatedBy = UserId,
              CreatedOn = DateTime.Now
          };
          await context.AppPositionWorksites.AddAsync(ws);
      }

      await context.SaveChangesAsync();
      TempData["msg"] = "Tagged Successfully!";
      return RedirectToAction("EmpTaggingMaster");
  }
