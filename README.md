
<button type="submit" class="btn btn-danger" onclick="setAction('Delete', event)">Delete</button>



[HttpPost]
public async Task<IActionResult> EmpTaggingMaster(string Pno, int Position, string Worksite, string ActionType)
{
    var UserId = HttpContext.Request.Cookies["Session"];
    if (string.IsNullOrEmpty(UserId))
        return RedirectToAction("Login", "User");

    if (ActionType == "Delete")
    {
        var empPosition = await context.AppEmpPositions.FirstOrDefaultAsync(e => e.Pno == Pno);
        if (empPosition != null)
            context.AppEmpPositions.Remove(empPosition);

        var positionWorksite = await context.AppPositionWorksites.FirstOrDefaultAsync(w => w.Position == Position);
        if (positionWorksite != null)
            context.AppPositionWorksites.Remove(positionWorksite);

        await context.SaveChangesAsync();
        TempData["msg"] = "Deleted Successfully!";
        return RedirectToAction("EmpTaggingMaster");
    }

    // Save or update position
    var existingEmp = await context.AppEmpPositions.FirstOrDefaultAsync(e => e.Pno == Pno);
    if (existingEmp != null)
    {
        existingEmp.Position = Position;
        context.AppEmpPositions.Update(existingEmp);
    }
    else
    {
        var empPosition = new AppEmpPosition
        {
            Id = Guid.NewGuid(),
            Pno = Pno,
            Position = Position
        };
        await context.AppEmpPositions.AddAsync(empPosition);
    }

    // Save or update worksite
    var existingWorksite = await context.AppPositionWorksites.FirstOrDefaultAsync(w => w.Position == Position);
    if (existingWorksite != null)
    {
        existingWorksite.Worksite = Worksite; // No need for string.Join
        existingWorksite.CreatedBy = UserId;
        existingWorksite.CreatedOn = DateTime.Now;
        context.AppPositionWorksites.Update(existingWorksite);
    }
    else
    {
        var ws = new AppPositionWorksite
        {
            Id = Guid.NewGuid(),
            Position = Position,
            Worksite = Worksite,
            CreatedBy = UserId,
            CreatedOn = DateTime.Now
        };
        await context.AppPositionWorksites.AddAsync(ws);
    }

    await context.SaveChangesAsync();
    TempData["msg"] = "Tagged Successfully!";
    return RedirectToAction("EmpTaggingMaster");
}



<script>
    var refNoLinks = document.querySelectorAll(".refNoLink");
    var FormContainer = document.getElementById("formContainer");

    refNoLinks.forEach(link => {
        link.addEventListener("click", function (event) {
            event.preventDefault();
            FormContainer.style.display = "block";

            document.getElementById("Pno").value = this.getAttribute("data-Pno");
            document.getElementById("Position").value = this.getAttribute("data-Position");

            // Reset checkboxes first
            document.querySelectorAll(".worksite-checkbox").forEach(cb => cb.checked = false);

            const worksites = this.getAttribute("data-Worksites");
            if (worksites) {
                worksites.split(',').forEach(id => {
                    const checkbox = document.getElementById("worksite_" + id.trim());
                    if (checkbox) checkbox.checked = true;
                });
            }

            document.getElementById("actionType").value = ""; // default
        });
    });

    // Capture form submit to collect worksites
    document.querySelector("form").addEventListener("submit", function () {
        const selected = [];
        document.querySelectorAll(".worksite-checkbox:checked").forEach(cb => {
            selected.push(cb.value);
        });
        document.getElementById("Worksite").value = selected.join(",");
    });

    function setAction(action, event) {
        event.preventDefault();
        document.getElementById("actionType").value = action;
        document.querySelector("form").submit();
    }
</script>
