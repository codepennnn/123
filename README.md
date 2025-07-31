@if (TempData["msg1"] != null)
{
        <div class="alert alert-success">@TempData["msg1"]</div>
}


@if (TempData["Dltmsg1"] != null)
{
                <div class="alert alert-danger">@TempData["Dltmsg1"]</div>
}

