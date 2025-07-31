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

            // Clean department string
            if (!string.IsNullOrEmpty(coordinator.DeptName))
            {
                var depts = coordinator.DeptName
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .Distinct();

                coordinator.DeptName = string.Join(",", depts);
            }

            // ✅ Check only by Pno
            var existing = await context.AppCoordinatorMasters
                .FirstOrDefaultAsync(x => x.Pno == coordinator.Pno);

            if (existing != null)
            {
                // Only update department and audit info
                existing.DeptName = coordinator.DeptName;
                existing.CreatedBy = coordinator.CreatedBy;
                existing.CreatedOn = coordinator.CreatedOn;
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
            var existing = await context.AppCoordinatorMasters
                .FirstOrDefaultAsync(x => x.Pno == coordinator.Pno);

            if (existing != null)
                context.AppCoordinatorMasters.Remove(existing);
        }

        await context.SaveChangesAsync();
        TempData["Dltmsg1"] = "Coordinator Deleted Successfully!";
    }

    return RedirectToAction("CoordinatorMaster");
}
