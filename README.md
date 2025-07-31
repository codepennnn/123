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


 <script>
    // Collect and update Dept values before form submission
    $('form').on('submit', function () {
        const selected = $('.Dept-checkbox:checked').map(function () {
            return $(this).val();
        }).get();
        $('#Dept').val(selected.join(','));
    });


      setTimeout(function () {
        $('.alert').fadeOut('slow');
    }, 5000); // 5000 ms = 5 seconds



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
            $('#Pno,#pnoSearch, #DeptDropdown, #Dept').val('');
            $('#Id').val('');
            $('.Dept-checkbox').prop('checked', false);


           $("#deleteButton").hide();
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
             $("#deleteButton").show();
        });
    });
