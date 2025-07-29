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

    <!-- This hidden field will be posted to the controller -->
    <input type="hidden" id="Worksite" name="Worksite" />
</div>


