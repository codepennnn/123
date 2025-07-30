
 <a href="javascript:void(0);" data-id="@item.Id" class="OpenFilledForm btn gridbtn" 



<!-- Form Container (Hidden by Default) -->
<div id="formContainer" style="display:none;">
    <form asp-action="EmpTaggingMaster" asp-controller="Master" method="post">
        @Html.AntiForgeryToken()
         <input type="hidden" name="ActionType" id="actionType" />
        <input type="hidden" name="EmpTags[0].Id" id="Id" value="@Model.Id" />


        <div class="card mt-3">
            <div class="card-header">Tag Employee Position</div>
            <div class="card-body">

                <div class="row">
                    <!-- PNO Dropdown -->
                    <div class="col-md-4">
                        <label for="Pno">PNO</label>
                        <select class="form-control form-control-sm" name="EmpTags[0].Pno" id="Pno" required>
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
                        <input type="number" name="EmpTags[0].Position" id="Position" class="form-control form-control-sm" required />
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
    <input type="hidden" id="Worksite" name="EmpTags[0].Worksite" />





                </div>

                <div class="text-center mt-3">
                    <button type="submit" class="btn btn-success">Submit</button>
                </div>

            </div>
        </div>
    </form>
</div>

<!-- Scripts -->
@section Scripts {
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>

        <script>
            $(document).ready(function () {

                // Show form and reset
                $('#showFormButton2').click(function () {
                    $('#formContainer').show();
                    $('#Pno').val('');
                    $('#Position').val('');
                    $('.worksite-checkbox').prop('checked', false);
                });

                // Collect checked worksite values before form submit
                $('form').on('submit', function () {
                    var selected = [];
                    $('.worksite-checkbox:checked').each(function () {
                        selected.push($(this).val());
                    });
                    $('#Worksite').val(selected.join(','));
                });

                // Placeholder for edit via AJAX
                $('.OpenFilledForm').click(function () {
                    const id = $(this).data('id');
                    $.ajax({
                        url: '@Url.Action("EmpTaggingMaster", "Master")',
                        data: { id: id },
                        success: function (data) {
                            $('#formContainer').show();
                            $('#Pno').val(data.pno);
                            $('#Position').val(data.position);
                            $('.worksite-checkbox').prop('checked', false);
                            if (data.worksiteIds) {
                                data.worksiteIds.forEach(id => {
                                    $('#worksite_' + id).prop('checked', true);
                                });
                            }
                        },
                        error: function () {
                            alert("Error loading data");
                        }
                    });
                });
            });
        </script>
        
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

      // --- Save worksite in a single record ---
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




when i click pno its hsould be open my pre filled form of that pno data
