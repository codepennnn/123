 <a href="javascript:void(0);" data-id="@item.Id" class="OpenFilledForm btn gridbtn" style="text-decoration:none;background-color:#ffffff;font-weight:bolder;color:darkblue;">
     @item.Pno
 </a>



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

                    <div class="form-group row">

                        <div class="col-sm-3">
                            <label for="pnoSearch" class="form-label">PNO</label>
                            <input type="text" id="pnoSearch" class="form-control" placeholder="Search PNO..." autocomplete="off">
                            <ul id="pnoList" class="list-group" style="max-height: 150px; overflow-y: auto; position: absolute; z-index: 10;"></ul>

                            <!-- Hidden select to preserve form structure -->
                            <select name="Coordinators[0].Pno" id="Pno" style="display: none;">
                                <option value="">-- Select PNO --</option>
                                @foreach (var item in ViewBag.PnoList as List<SelectListItem>)
                                {
                                    <option value="@item.Value">@item.Text</option>
                                }
                            </select>
                        </div>




                        <div class="col-sm-3">
                            <label class="form-label">Deptartment</label>

                            <div class="dropdown">
                                <input class="form-control " placeholder="Select Depts"
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

                            <!-- This hidden field stores selected values -->
                            <input type="hidden" id="Dept" name="Dept" />





                        </div>



                </div>
                </div>

                <div class="">
                    <button type="submit" class="btn btn-success" onclick="setAction('Submit', event)">Submit</button>
                    <button type="submit" class="btn btn-danger" onclick="setAction('Delete', event)">Delete</button>
                </div>
            </div>
        </div>
    </form>
</div>




<script>


        $('#DeptDropdown').val(data.dept);  
    $('#Dept').val(data.dept);         
    checkCheckboxesFromDropdownText();  




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



<script>
    document.addEventListener("DOMContentLoaded", function () {
      const input = document.getElementById("pnoSearch");
      const select = document.getElementById("Pno");
      const ul = document.getElementById("pnoList");

      // Load options from the select element
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
