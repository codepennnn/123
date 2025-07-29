@{
    ViewData["Title"] = "Emp Tagging Master";
}

<!-- Page Header with "New" button -->
<div class="card rounded-9 mt-3">
    <div class="row align-items-center form-group">
        <div class="col-md-9">
            <h4>Emp Tagging Master</h4>
        </div>
        <div class="col-md-3 mb-2 text-end">
            <button id="showFormButton2" class="btn btn-primary">New</button>
        </div>
    </div>

    @if (TempData["msg"] != null)
    {
        <div class="alert alert-success">@TempData["msg"]</div>
    }
</div>

<!-- Form Container (Hidden by Default) -->
<div id="formContainer" style="display:none;">
    <form asp-action="EmpTaggingMaster" asp-controller="Master" method="post">
        @Html.AntiForgeryToken()

        <div class="card mt-3">
            <div class="card-header">Tag Employee Position</div>
            <div class="card-body">

                <div class="row">
                    <!-- PNO Dropdown -->
                    <div class="col-md-4">
                        <label for="Pno">PNO</label>
                        <select class="form-control" name="Pno" id="Pno" required>
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
                        <input type="number" name="Position" id="Position" class="form-control" required />
                    </div>

                    <!-- Worksite Checkboxes -->
                    <div class="col-md-4">
                        <label>Worksites</label>
                        <div class="border p-2" style="max-height: 200px; overflow-y: auto;">
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

                <div class="text-center mt-3">
                    <button type="submit" class="btn btn-success">Submit</button>
                </div>

            </div>
        </div>
    </form>
</div>

<!-- Scripts -->
@section Scripts {
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>

    <script>
        $(document).ready(function () {
            // Show form and reset fields when "New" is clicked
            $('#showFormButton2').click(function () {
                $('#formContainer').show();
                $('#Pno').val('');
                $('#Position').val('');
                $('input[type="checkbox"]').prop('checked', false);
            });

            // Placeholder if you later enable editing
            $('.OpenFilledForm').click(function () {
                const id = $(this).data('id');
                $.ajax({
                    url: '@Url.Action("EmpTaggingMaster", "Master")',
                    data: { id: id },
                    success: function (data) {
                        $('#formContainer').show();
                        $('#Pno').val(data.pno);
                        $('#Position').val(data.position);
                        $('input[name="WorksiteIds"]').prop('checked', false);
                        if (data.worksiteIds) {
                            data.worksiteIds.forEach(id => {
                                $('input[name="WorksiteIds"][value="' + id + '"]').prop('checked', true);
                            });
                        }
                    },
                    error: function () {
                        alert("Error loading data");
                    }
                });
            });
        });
    </script>
}
