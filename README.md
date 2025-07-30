var existingWorksites = await context.AppPositionWorksites
    .Where(w => w.Position == Position)
    .ToListAsync();

Console.WriteLine("Found worksites: " + existingWorksites.Count);
foreach (var site in existingWorksites)
{
    Console.WriteLine("Worksite ID: " + site.Id + ", Worksite: " + site.Worksite);
}



// Reset and check worksite checkboxes based on data-Worksites
const worksites = this.getAttribute("data-Worksites");
if (worksites) {
    document.querySelectorAll(".worksite-checkbox").forEach(cb => cb.checked = false); // Reset all

    worksites.split(',').forEach(id => {
        const checkbox = document.getElementById("worksite_" + id.trim());
        if (checkbox) checkbox.checked = true; // Check matching
    });
}
