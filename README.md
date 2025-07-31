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

            if (!string.IsNullOrEmpty(coordinator.DeptName))
            {
                var depts = coordinator.DeptName
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .Distinct();

                coordinator.DeptName = string.Join(",", depts);
            }

            AppCoordinatorMaster existing = null;

            // 1. Try finding by ID first
            if (coordinator.Id > 0)
                existing = await context.AppCoordinatorMasters.FindAsync(coordinator.Id);

            // 2. If not found by ID, try by PNO
            if (existing == null && !string.IsNullOrEmpty(coordinator.Pno))
                existing = await context.AppCoordinatorMasters
                    .FirstOrDefaultAsync(x => x.Pno == coordinator.Pno);

            if (existing != null)
            {
                // Only update DeptName and tracking fields, not PNO
                existing.DeptName = coordinator.DeptName;
                existing.CreatedBy = coordinator.CreatedBy;
                existing.CreatedOn = coordinator.CreatedOn;
                context.Update(existing);
            }
            else
            {
                await context.AppCoordinatorMasters.AddAsync(coordinator);
            }
        }

        await context.SaveChangesAsync();
        TempData["msg1"] = "Coordinator Saved Successfully!";
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
        TempData["Dltmsg1"] = "Coordinator Deleted Successfully!";
    }

    return RedirectToAction("CoordinatorMaster");
}
