


                                            <a href="javascript:void(0);"  data-id="@item.Id" data-Pno="@item.Pno" data-Position="@item.Position" data-Worksites="@item.Worksites"
                                @item.Subject class="OpenFilledForm btn gridbtn refNoLink" 
                                       style="text-decoration:none;background-color:#ffffff;font-weight:bolder;color:darkblue;">
                                    @item.Pno
                                    </a>

<div id="formContainer" style="display:none;">
    <form asp-action="EmpTaggingMaster" asp-controller="Master" method="post">
        @Html.AntiForgeryToken()
            <input type="hidden" name="ActionType" id="actionType" />



        <div class="card mt-3">
            <div class="card-header">Tag Employee Position</div>
            <div class="card-body">

                <div class="row">
                   
                    <div class="col-md-4">
                        <label for="Pno">PNO</label>
                        <select class="form-control form-control-sm" name="Pno" id="Pno" required>
                            <option value="">-- Select PNO --</option>
                            @foreach (var pno in ViewBag.pnoDropdown as List<SelectListItem>)
                            {
                                <option value="@pno.Value">@pno.Text</option>
                            }
                        </select>
                    </div>

                    <!-- Position TextBox -->
                    <div class="col-md-4">
                        <label for="Position">Position</label>
                        <input type="number" name="Position" id="Position" class="form-control form-control-sm" required />
                            <button type="submit" class="btn btn-danger" onclick="setAction('Delete', event)">Delete</button>
                    </div>

                    <!-- Worksite Checkboxes -->
           
                                      

                    <div class="col-md-4">
    <label>Worksite</label>

    <div class="dropdown">
        <input class="form-control form-control-sm" placeholder="Select Worksites"
               type="button" id="worksiteDropdown" data-bs-toggle="dropdown" aria-expanded="false" />

        <ul class="dropdown-menu w-100" aria-labelledby="worksiteDropdown" id="locationList" style="max-height: 200px; overflow-y: auto;">
                                @foreach (var item in ViewBag.WorksiteList as List<SelectListItem>)
                                {
                    <li style="margin-left:5%;">
                        <div class="form-check">
                            <input type="checkbox" class="form-check-input worksite-checkbox"
                                   value="@item.Value" id="worksite_@item.Value" />
                            <label class="form-check-label" for="worksite_@item.Value">@item.Text</label>
                        </div>
                    </li>
                                }
        </ul>
    </div>

    <!-- This hidden field stores selected values -->
    <input type="hidden" id="Worksite" name="Worksite" />





                </div>

                <div class="text-center mt-3">
                    <button type="submit" class="btn btn-success">Submit</button>
                </div>

            </div>
        </div>
    </form>
        </div>

   

                            <script>
                                var NewButton = document.getElementById("newButton");
                                var FormContainer = document.getElementById("formContainer");
        var refNoLinks = document.querySelectorAll(".refNoLink");
        

        if (NewButton) {
            NewButton.addEventListener("click", function () {
                FormContainer.style.display = "block";
                document.getElementById("Pno").value = "";
                document.getElementById("Position").value = "";
                document.getElementById("worksiteDropdown").value = "";
                //deleteButton.style.display = "none";
            });
        }


                                refNoLinks.forEach(link => {
            link.addEventListener("click", function (event) {
                event.preventDefault();
                FormContainer.style.display = "block";

                document.getElementById("Pno").value = this.getAttribute("data-Pno");
                document.getElementById("Position").value = this.getAttribute("data-Position");
                document.getElementById("worksiteDropdown").value = this.getAttribute("data-Worksites");
                //document.getElementById("SubjectId").value = this.getAttribute("data-id");

                if (deleteButton) {
                    deleteButton.style.display = "inline-block";
                }
            });
        });
                            </script>


                            and 

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

    
      var existingWorksite = await context.AppPositionWorksites
          .FirstOrDefaultAsync(w => w.Position == Position);

      if (existingWorksite != null)
      {
          // Update existing
          existingWorksite.Worksite = string.Join(",", Worksite);
          existingWorksite.CreatedBy = UserId;
          existingWorksite.CreatedOn = DateTime.Now;
          context.AppPositionWorksites.Update(existingWorksite);
      }
      else
      {
          // Create new
          var ws = new AppPositionWorksite
          {
              Id = Guid.NewGuid(),
              Position = Position,
              Worksite = string.Join(",", Worksite),
              CreatedBy = UserId,
              CreatedOn = DateTime.Now
          };
          await context.AppPositionWorksites.AddAsync(ws);
      }

      await context.SaveChangesAsync();
      TempData["msg"] = "Tagged Successfully!";
      return RedirectToAction("EmpTaggingMaster");
  }

when i submit its saving position correcly but not saving worksite 
and also add delete logic for existing record
and one more problem in worksite that when prefilled form open worksite showing but not checked solve this also 
