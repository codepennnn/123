.
[HttpGet]
public async Task<IActionResult> GetEmployeeTagDetails(string pno)
{
    var empPosition = await context.AppEmpPositions.FirstOrDefaultAsync(e => e.Pno == pno);
    var position = empPosition?.Position;

    var worksiteIds = await context.AppPositionWorksites
        .Where(w => w.Position == position)
        .Select(w => w.Worksite)
        .FirstOrDefaultAsync();

    var worksiteList = worksiteIds?.Split(',') ?? new string[0];

    return Json(new
    {
        pno = pno,
        position = position,
        worksiteIds = worksiteList
    });
}


$('.OpenFilledForm').click(function () {
    const pno = $(this).data('pno');

    $.ajax({
        url: '@Url.Action("GetEmployeeTagDetails", "Master")',
        data: { pno: pno },
        success: function (data) {
            $('#formContainer').show();

            $('#Pno').val(data.pno);
            $('#Position').val(data.position);
            $('.worksite-checkbox').prop('checked', false);

            if (data.worksiteIds) {
                data.worksiteIds.forEach(id => {
                    $('#worksite_' + id).prop('checked', true);
                });
            }
        },
        error: function () {
            alert("Error loading data");
        }
    });
});
