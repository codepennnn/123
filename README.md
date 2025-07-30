
      
      
      
      
      var existingWorksites = await context.AppPositionWorksites
.Where(w => w.Position == Position)
.ToListAsync();

      Console.WriteLine("Found worksites: " + existingWorksites.Count);
      foreach (var site in existingWorksites)
      {
          Console.WriteLine("Worksite ID: " + site.Id + ", Worksite: " + site.Worksite);
      }

      var existingWorksite = existingWorksites.FirstOrDefault();

      if (existingWorksite != null)
      {
          existingWorksite.Worksite = Worksite;
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
and 


								refNoLinks.forEach(link => {
			link.addEventListener("click", function (event) {
				event.preventDefault();
				FormContainer.style.display = "block";

				document.getElementById("Pno").value = this.getAttribute("data-Pno");
				document.getElementById("Position").value = this.getAttribute("data-Position");
				document.getElementById("worksiteDropdown").value = this.getAttribute("data-Worksites");
					  const worksites = this.getAttribute("data-Worksites");
if (worksites) {
	document.querySelectorAll(".worksite-checkbox").forEach(cb => cb.checked = false); // Reset all

	worksites.split(',').forEach(id => {
		const checkbox = document.getElementById("worksite_" + id.trim());
		if (checkbox) checkbox.checked = true; // Check matching
	});
}


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

<!-- This hidden field stores selected values -->
<input type="hidden" id="Worksite" name="Worksite" />



still not null showing in existingWorksite

and still not checked worksite showing in edit
