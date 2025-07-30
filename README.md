. 


<div id="formContainer" style="display:none;">
    <form asp-action="CoordinatorMaster" asp-controller="Master" method="post">
        @Html.AntiForgeryToken()

        <input type="hidden" name="ActionType" id="actionType" />
        <input type="hidden" name="Coordinators[0].Id" id="Id" value="@Model.Id" />
        <input type="hidden" name="Coordinators[0].CreatedBy" id="CreatedBy" value="@ViewBag.CreatedBy" />
        <input type="hidden" name="Coordinators[0].CreatedOn" id="CreatedOn" value="@Model.CreatedOn" />

        <div class="card mt-3">
            <div class="card-header">Coordinator Master Entry</div>
            <div class="card-body">
                <div class="row">

                    <!-- PNO Search + Select -->
                    <div class="col-sm-3">
                        <label for="pnoSearch" class="form-label">PNO</label>
                        <input type="text" id="pnoSearch" class="form-control" placeholder="Search PNO..." autocomplete="off">
                        <ul id="pnoList" class="list-group" style="max-height: 150px; overflow-y: auto; position: absolute; z-index: 10;"></ul>

                        <!-- Hidden real select -->
                        <select name="Coordinators[0].Pno" id="Pno" style="display: none;">
                            <option value="">-- Select PNO --</option>
                            @foreach (var item in ViewBag.PnoList as List<SelectListItem>)
                            {
                                <option value="@item.Value">@item.Text</option>
                            }
                        </select>
                    </div>

                    <!-- Department with checkboxes -->
                    <div class="col-sm-5">
                        <label class="form-label">Department</label>

                        <div class="dropdown">
                            <input class="form-control" placeholder="Select Depts"
                                   type="button" id="DeptDropdown" data-bs-toggle="dropdown" aria-expanded="false" />

                            <ul class="dropdown-menu w-100" aria-labelledby="DeptDropdown" id="locationList" style="max-height: 200px; overflow-y: auto;">
                                @foreach (var item in ViewBag.DeptList as List<SelectListItem>)
                                {
                                    <li style="margin-left:5%;">
                                        <div class="form-check">
                                            <input type="checkbox" class="form-check-input Dept-checkbox"
                                                   value="@item.Value" id="Dept_@item.Value" />
                                            <label class="form-check-label" for="Dept_@item.Value">@item.Text</label>
                                        </div>
                                    </li>
                                }
                            </ul>
                        </div>

                        <!-- Hidden dept field for form -->
                        <input type="hidden" id="Dept" name="Coordinators[0].DeptName" />
                    </div>

                </div>

                <div class="mt-3">
                    <button type="submit" class="btn btn-success" onclick="setAction('Submit', event)">Submit</button>
                    <button type="submit" class="btn btn-danger" onclick="setAction('Delete', event)">Delete</button>
                </div>
            </div>
        </div>
    </form>
</div>


<script>
    // Collect and update Dept values before form submission
    $('form').on('submit', function () {
        const selected = $('.Dept-checkbox:checked').map(function () {
            return $(this).val();
        }).get();
        $('#Dept').val(selected.join(','));
    });

    // Set ActionType and optionally confirm deletion
    function setAction(action, event) {
        if (action === 'Delete' && !confirm("Are you sure you want to delete this record?")) {
            event.preventDefault();
            return;
        }
        document.getElementById('actionType').value = action;
    }

    // Used when manually selecting department checkboxes from dropdown label text
    function checkCheckboxesFromDropdownText() {
        const dropdownText = document.getElementById("DeptDropdown").value;
        const selectedNames = dropdownText.split(",").map(s => s.trim()).filter(s => s);

        // Clear all checkboxes first
        document.querySelectorAll(".Dept-checkbox").forEach(cb => cb.checked = false);

        // Recheck matching boxes based on label text
        selectedNames.forEach(name => {
            document.querySelectorAll(".Dept-checkbox").forEach(cb => {
                const label = document.querySelector(`label[for="${cb.id}"]`);
                if (label && label.textContent.trim() === name) {
                    cb.checked = true;
                }
            });
        });

        updateHiddenFieldFromCheckboxes(); // Update hidden input and label display
    }

    // Update hidden Dept value + update visible text in input
    function updateHiddenFieldFromCheckboxes() {
        const selectedValues = [];
        const selectedLabels = [];

        document.querySelectorAll(".Dept-checkbox:checked").forEach(cb => {
            selectedValues.push(cb.value);
            const label = document.querySelector(`label[for="${cb.id}"]`);
            if (label) selectedLabels.push(label.textContent.trim());
        });

        document.getElementById("Dept").value = selectedValues.join(",");
        document.getElementById("DeptDropdown").value = selectedLabels.join(", ");
    }

    // Handle showing form manually
    $(document).ready(function () {
        $('#showFormButton2').click(function () {
            $('#formContainer').show();
            $('#Pno, #DeptDropdown, #Dept').val('');
            $('#Id').val('');
            $('.Dept-checkbox').prop('checked', false);
        });

        // Load form data on clicking a PNO
        $('.OpenFilledForm').click(function () {
            const id = $(this).data('id');
            $.ajax({
                url: '@Url.Action("CoordinatorMaster", "Master")',
                data: { id: id },
                success: function (data) {
                    $('#Id').val(data.id);
                    $('#Pno').val(data.pno);
                    $('#pnoSearch').val(data.pno); // for display in the search box
                    $('#CreatedBy').val(data.createdby);
                    $('#CreatedOn').val(data.createdon);

                    // Department: fill hidden, display field, and check boxes
                    $('#Dept').val(data.dept);
                    $('#DeptDropdown').val(data.dept);
                    checkCheckboxesFromDropdownText();

                    $('#formContainer').show();
                },
                error: function () {
                    alert("Error loading data");
                }
            });
        });
    });

    // PNO dropdown search functionality (no external libraries)
    document.addEventListener("DOMContentLoaded", function () {
        const input = document.getElementById("pnoSearch");
        const select = document.getElementById("Pno");
        const ul = document.getElementById("pnoList");

        const options = Array.from(select.options)
            .filter(opt => opt.value !== "")
            .map(opt => ({ value: opt.value, text: opt.text }));

        input.addEventListener("input", function () {
            const search = input.value.toLowerCase();
            ul.innerHTML = "";

            if (search === "") {
                ul.style.display = "none";
                return;
            }

            const filtered = options.filter(opt => opt.text.toLowerCase().includes(search));
            if (filtered.length === 0) {
                ul.style.display = "none";
                return;
            }

            filtered.forEach(opt => {
                const li = document.createElement("li");
                li.className = "list-group-item";
                li.textContent = opt.text;
                li.dataset.value = opt.value;

                li.addEventListener("click", () => {
                    input.value = opt.text;
                    select.value = opt.value;
                    ul.style.display = "none";
                });

                ul.appendChild(li);
            });

            ul.style.display = "block";
        });

        document.addEventListener("click", function (e) {
            if (!ul.contains(e.target) && e.target !== input) {
                ul.style.display = "none";
            }
        });
    });
</script>
