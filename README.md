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


            <div class="col-md-4">
         <label>Worksite</label>

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


                                            @section Scripts {
        <script>
            $(document).ready(function () {

                // Show form
                $('#showFormButton2').click(function () {
                    $('#formContainer').show();
                    $('#Pno').val('');
                    $('#Position').val('');
                    $('.worksite-checkbox').prop('checked', false);
                });

                // Before form submit, collect all checked worksite values
                $('form').on('submit', function () {
                    var selected = [];
                    $('.worksite-checkbox:checked').each(function () {
                        selected.push($(this).val());
                    });
                    $('#Worksite').val(selected.join(','));
                });
            });
        </script>
}


                        </div>
                    </li>
                                }
        </ul>
    </div>

    <!-- This hidden field will be posted to the controller -->
    <input type="hidden" id="Worksite" name="Worksite" />
    </div>
