..
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

            // Clean up DeptName (optional: remove duplicate or empty entries)
            if (!string.IsNullOrEmpty(coordinator.DeptName))
            {
                var depts = coordinator.DeptName
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .Distinct();

                coordinator.DeptName = string.Join(",", depts); // Final cleaned, comma-separated string
            }

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
