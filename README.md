    [HttpPost]
    public async Task<IActionResult> CoordinatorMaster(CoordinatorWrapper model)
    {
        var UserId = HttpContext.Request.Cookies["Session"];

        if (model == null || model.Coordinators == null || !model.Coordinators.Any())
            return BadRequest("No coordinator data received.");

        if (string.IsNullOrEmpty(model.ActionType))
            return BadRequest("No action specified.");




        if (model.ActionType == "Submit")
        {
            foreach (var coordinator in model.Coordinators)
            {
                coordinator.CreatedBy = UserId;
                coordinator.CreatedOn = DateTime.Now;

                var existing = await context.AppCoordinatorMasters.FindAsync(coordinator.Id);
                if (existing != null)
                    context.Entry(existing).CurrentValues.SetValues(coordinator);
                else
                    await context.AppCoordinatorMasters.AddAsync(coordinator);
            }

            await context.SaveChangesAsync();
            TempData["msg"] = "Coordinator Saved Successfully!";
        }
        else if (model.ActionType == "Delete")
        {
            foreach (var coordinator in model.Coordinators)
            {
                var existing = await context.AppCoordinatorMasters.FindAsync(coordinator.Id);
                if (existing != null)
                    context.AppCoordinatorMasters.Remove(existing);
            }

            await context.SaveChangesAsync();
            TempData["Dltmsg"] = "Coordinator Deleted Successfully!";
        }

        return RedirectToAction("CoordinatorMaster");
    }

 <form asp-action="CoordinatorMaster" asp-controller="Master" method="post">
     @Html.AntiForgeryToken()
     <input type="hidden" name="ActionType" id="actionType" />
     <input type="hidden" name="Coordinators[0].Id" id="Id" value="@Model.Id" />
     <input type="hidden" name="Coordinators[0].CreatedBy" id="CreatedBy" value="@ViewBag.CreatedBy" />
     <input type="hidden" name="Coordinators[0].CreatedOn" id="CreatedOn" value="@Model.CreatedOn" />

     <div class="card mt-3">
         <div class="card-header">Coordinator Master Entry</div>
         <div class="card-body">


             <div class="row">

                 <div class="form-group row">

                     <div class="col-sm-3">
                         <label for="pnoSearch" class="form-label">PNO</label>
                         <input type="text" id="pnoSearch" class="form-control" placeholder="Search PNO..." autocomplete="off">
                         <ul id="pnoList" class="list-group" style="max-height: 150px; overflow-y: auto; position: absolute; z-index: 10;"></ul>

                         <!-- Hidden select to preserve form structure -->
                         <select name="Coordinators[0].Pno" id="Pno" style="display: none;">
                             <option value="">-- Select PNO --</option>
                             @foreach (var item in ViewBag.PnoList as List<SelectListItem>)
                             {
                                 <option value="@item.Value">@item.Text</option>
                             }
                         </select>
                     </div>




                     <div class="col-sm-3">
                         <label class="form-label">Deptartment</label>

                         <div class="dropdown">
                             <input class="form-control " placeholder="Select Depts"
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



             </div>
             </div>

             <div class="">
                 <button type="submit" class="btn btn-success" onclick="setAction('Submit', event)">Submit</button>
                 <button type="submit" class="btn btn-danger" onclick="setAction('Delete', event)">Delete</button>
             </div>
         </div>
     </div>
 </form>
