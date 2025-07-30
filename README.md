.
<div class="col-md-12">
    <table class="table table-bordered" id="myTable">
        <thead class="table" style="background-color: #d2b1ff;color: #000000;">
            <tr>
                <th width="25%">PNO</th>
                <th width="25%">Name</th>
                <th width="25%">Position</th>
                <th width="25%">Worksites</th>
            </tr>
        </thead>
        <tbody>
            @if (ViewBag.pList != null)
            {
                foreach (var item in ViewBag.pList)
                {
                    <tr>
                        <td>
                            <a href="javascript:void(0);" data-id="@item.Pno" class="OpenFilledForm btn gridbtn" 
                               style="text-decoration:none;background-color:#ffffff;font-weight:bolder;color:darkblue;">
                                @item.Pno
                            </a>
                        </td>
                        <td>@item.Name</td>
                        <td>@item.Position</td>
                        <td>@item.Worksites</td>
                    </tr>
                }
            }
            else
            {
                <tr>
                    <td colspan="4">No data available</td>
                </tr>
            }
        </tbody>
    </table>

    <div class="text-center">
        @if (ViewBag.TotalPages > 1)
        {
            <nav aria-label="Page navigation" class="d-flex justify-content-center">
                <ul class="pagination">
                    <li class="page-item @(ViewBag.CurrentPage == 1 ? "disabled" : "")">
                        <a class="page-link" asp-action="EmpTaggingMaster" asp-route-page="@(ViewBag.CurrentPage - 1)" asp-route-searchString="@ViewBag.SearchString">Previous</a>
                    </li>
                    @for (int i = 1; i <= ViewBag.TotalPages; i++)
                    {
                        <li class="page-item @(ViewBag.CurrentPage == i ? "active" : "")">
                            <a class="page-link" asp-action="EmpTaggingMaster" asp-route-page="@i" asp-route-searchString="@ViewBag.SearchString">@i</a>
                        </li>
                    }
                    <li class="page-item @(ViewBag.CurrentPage == ViewBag.TotalPages ? "disabled" : "")">
                        <a class="page-link" asp-action="EmpTaggingMaster" asp-route-page="@(ViewBag.CurrentPage + 1)" asp-route-searchString="@ViewBag.SearchString">Next</a>
                    </li>
                </ul>
            </nav>
        }
    </div>
</div>
