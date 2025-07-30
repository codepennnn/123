    <div class="col-md-12">
        <table class="table table-bordered" id="myTable">
            <thead class="table" style="background-color: #d2b1ff;color: #000000;">
                <tr>
                    <th width="50%">PNO</th>
                    <th width="50%"></th>
                </tr>
            </thead>
            <tbody>
                @if (ViewBag.pList != null)
                {
                    foreach (var item in ViewBag.pList)
                    {
                        <tr>
                            <td>
                                <a href="javascript:void(0);" data-id="@item.Id" class="OpenFilledForm btn gridbtn" style="text-decoration:none;background-color:#ffffff;font-weight:bolder;color:darkblue;">
                                    @item.Pno
                                </a>
                            </td>
                            <td>@item.</td>
                        </tr>
                    }
                }
                else
                {
                    <tr>
                        <td colspan="2">No data available</td>
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
                            <a class="page-link" asp-action="CoordinatorMaster" asp-route-page="@(ViewBag.CurrentPage - 1)" asp-route-searchString="@ViewBag.SearchString">Previous</a>
                        </li>
                        @for (int i = 1; i <= ViewBag.TotalPages; i++)
                        {
                            <li class="page-item @(ViewBag.CurrentPage == i ? "active" : "")">
                                <a class="page-link" asp-action="CoordinatorMaster" asp-route-page="@i" asp-route-searchString="@ViewBag.SearchString">@i</a>
                            </li>
                        }
                        <li class="page-item @(ViewBag.CurrentPage == ViewBag.TotalPages ? "disabled" : "")">
                            <a class="page-link" asp-action="CoordinatorMaster" asp-route-page="@(ViewBag.CurrentPage + 1)" asp-route-searchString="@ViewBag.SearchString">Next</a>
                        </li>
                    </ul>
                </nav>
            }
        </div>
    </div>
</div> 

