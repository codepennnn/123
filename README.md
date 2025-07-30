const worksites = this.getAttribute("data-Worksites");
if (worksites) {
    document.querySelectorAll(".worksite-checkbox").forEach(cb => cb.checked = false); // Reset all

    worksites.split(',').forEach(id => {
        const trimmedId = id.trim();
        const checkbox = document.querySelector(`#worksite_${trimmedId}`);
        if (checkbox) {
            checkbox.checked = true;
        }
    });
}


document.getElementById("Worksite").value = Array.from(document.querySelectorAll(".worksite-checkbox:checked"))
    .map(cb => cb.value)
    .join(",");



    // 1. Remove existing worksites for this position
var existingWorksites = await context.AppPositionWorksites
    .Where(w => w.Position == Position)
    .ToListAsync();

context.AppPositionWorksites.RemoveRange(existingWorksites);

// 2. Add new worksites (splitting comma-separated input)
foreach (var site in Worksite.Split(",", StringSplitOptions.RemoveEmptyEntries))
{
    var ws = new AppPositionWorksite
    {
        Id = Guid.NewGuid(),
        Position = Position,
        Worksite = site.Trim(), // Assuming site is an ID
        CreatedBy = UserId,
        CreatedOn = DateTime.Now
    };

    await context.AppPositionWorksites.AddAsync(ws);
}

await context.SaveChangesAsync();
