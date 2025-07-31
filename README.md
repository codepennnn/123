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

            // Clean and deduplicate department names
            if (!string.IsNullOrEmpty(coordinator.DeptName))
            {
                var depts = coordinator.DeptName
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .Distinct();

                coordinator.DeptName = string.Join(",", depts);
            }

            AppCoordinatorMaster existing = null;

            // Try to find by Guid Id if it's valid
            if (!string.IsNullOrEmpty(coordinator.Id) && Guid.TryParse(coordinator.Id, out Guid guidId))
            {
                existing = await context.AppCoordinatorMasters.FindAsync(guidId);
            }

            // If not found by Id, try to find by Pno
            if (existing == null && !string.IsNullOrEmpty(coordinator.Pno))
            {
                existing = await context.AppCoordinatorMasters
                    .FirstOrDefaultAsync(x => x.Pno == coordinator.Pno);
            }

            // Update or Insert
            if (existing != null)
            {
                // Overwrite only relevant fields, or all
                context.Entry(existing).CurrentValues.SetValues(coordinator);
            }
            else
            {
                // Assign new GUID if needed
                if (string.IsNullOrEmpty(coordinator.Id))
                    coordinator.Id = Guid.NewGuid().ToString();

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
            if (!string.IsNullOrEmpty(coordinator.Id) && Guid.TryParse(coordinator.Id, out Guid guidId))
            {
                var existing = await context.AppCoordinatorMasters.FindAsync(guidId);
                if (existing != null)
                    context.AppCoordinatorMasters.Remove(existing);
            }
        }

        await context.SaveChangesAsync();
        TempData["Dltmsg1"] = "Coordinator Deleted Successfully!";
    }

    return RedirectToAction("CoordinatorMaster");
}
