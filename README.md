<div class="mb-3">
    <label for="Pno" class="form-label">PNO</label>
    <select name="Coordinators[0].Pno" id="Pno" class="form-control">
        <option value="">-- Select PNO --</option>
        @foreach (var item in ViewBag.PnoList as List<SelectListItem>)
        {
            <option value="@item.Value">@item.Text</option>
        }
    </select>
</div>



<!-- Select2 CSS -->
<link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />





<script>
    $(document).ready(function () {
        $('#Pno').select2({
            placeholder: "-- Select PNO --",
            allowClear: true,
            width: '100%'
        });
    });
</script>

<!-- jQuery (required for Select2) -->
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<!-- Select2 JS -->
<script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

