// --- Save worksite in a single record ---
var existingWorksite = await context.AppPositionWorksites
    .FirstOrDefaultAsync(w => w.Position == Position);

if (existingWorksite != null)
{
    // Update existing
    existingWorksite.Worksite = string.Join(",", worksiteIds);
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
        Worksite = string.Join(",", worksiteIds),
        CreatedBy = UserId,
        CreatedOn = DateTime.Now
    };
    await context.AppPositionWorksites.AddAsync(ws);




    // Get logged-in employee from AppEmployeeMaster
var loggedInEmp = await context.AppEmployeeMasters.FirstOrDefaultAsync(e => e.Pno == UserId);
if (loggedInEmp == null)
    return Unauthorized();

string userDept = loggedInEmp.DeptName;

// Filter PNOs from CoordinatorMaster where DeptName = user's dept
ViewBag.PnoList = context.AppCoordinatorMasters
    .Where(e => e.DeptName == userDept)
    .Select(e => new SelectListItem
    {
        Value = e.Pno,
        Text = e.Pno
    }).ToList();
}
