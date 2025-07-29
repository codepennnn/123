..
..
@model GFAS.Models.EmpTagViewModel

<form asp-action="EmpTaggingMaster" asp-controller="Master" method="post">
    @Html.AntiForgeryToken()

    <div class="row">
        <!-- PNO Dropdown -->
        <div class="col-md-4">
            <label for="Pno">PNO</label>
            <select class="form-control" name="Pno" required>
                <option value="">-- Select PNO --</option>
                @foreach (var pno in ViewBag.PnoList as List<SelectListItem>)
                {
                    <option value="@pno.Value">@pno.Text</option>
                }
            </select>
        </div>

        <!-- Position TextBox -->
        <div class="col-md-4">
            <label for="Position">Position</label>
            <input type="number" name="Position" class="form-control" required />
        </div>

        <!-- Worksite Checkboxes -->
        <div class="col-md-4">
            <label>Worksites</label>
            <div class="form-control" style="height:auto;">
                @foreach (var ws in ViewBag.WorksiteList as List<SelectListItem>)
                {
                    <div class="form-check">
                        <input type="checkbox" class="form-check-input" name="WorksiteIds" value="@ws.Value" />
                        <label class="form-check-label">@ws.Text</label>
                    </div>
                }
            </div>
        </div>
    </div>

    <div class="mt-3 text-center">
        <button type="submit" class="btn btn-success">Submit</button>
    </div>
</form>
