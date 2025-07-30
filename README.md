<script>

                    function checkCheckboxesFromDropdownText() {
        const dropdownText = document.getElementById("DeptDropdown").value;
        const selectedNames = dropdownText.split(",").map(s => s.trim()).filter(s => s);


        document.querySelectorAll(".Dept-checkbox").forEach(cb => cb.checked = false);


        selectedNames.forEach(name => {
            document.querySelectorAll(".Dept-checkbox").forEach(cb => {
                const label = document.querySelector(`label[for="${cb.id}"]`);
                if (label && label.textContent.trim() === name) {
                    cb.checked = true;
                }
            });
        });


        updateHiddenFieldFromCheckboxes();
    }

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

</script>





<script>

    


        $('form').on('submit', function () {
            var selected = [];
            $('.Dept-checkbox:checked').each(function () {
                selected.push($(this).val());
            });
            $('#Dept').val(selected.join(','));
        });





    function setAction(action, event) {
        if (action === 'Delete' && !confirm("Are you sure you want to delete this record?")) {
            event.preventDefault();
            return;
        }
        document.getElementById('actionType').value = action;
    }

    $(document).ready(function () {
        $('#showFormButton2').click(function () {
            $('#formContainer').show();
            $('#Pno, #DeptName').val('');
            $('#Id').val('');
        });

        $('.OpenFilledForm').click(function () {
            const id = $(this).data('id');
            $.ajax({
                url: '@Url.Action("CoordinatorMaster", "Master")',
                data: { id: id },
                success: function (data) {
                    $('#Id').val(data.id);
                    $('#Pno').val(data.pno);
                    $('#DeptName').val(data.dept);
                    $('#CreatedBy').val(data.createdby);
                    $('#CreatedOn').val(data.createdon);
                    $('#formContainer').show();
                },
                error: function () {
                    alert("Error loading data");
                }
            });
        });
    });
</script>

