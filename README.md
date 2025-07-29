public async Task<IActionResult> EmpTaggingMaster(int page = 1, int pageSize = 10)
{
    var UserId = HttpContext.Request.Cookies["Session"];
    if (string.IsNullOrEmpty(UserId))
        return RedirectToAction("Login", "User");

    // Get logged-in employee's department
    var loggedInEmp = await context.AppEmployeeMasters
        .FirstOrDefaultAsync(e => e.Pno == UserId);

    if (loggedInEmp == null)
        return Unauthorized();

    string userDept = loggedInEmp.DeptName;
    int offset = (page - 1) * pageSize;

    List<dynamic> result = new();

    string sql = @"
        SELECT 
            emp.Pno,
            emp.Name,
            pos.Position,
            STUFF((
                SELECT ', ' + loc.Work_Site
                FROM App_Position_Worksite ws
                INNER JOIN App_LocationMaster loc ON loc.ID = ws.Worksite
                WHERE ws.Position = pos.Position
                FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS Worksites
        FROM App_Employee_Master emp
        INNER JOIN App_Emp_Position pos ON emp.Pno = pos.Pno
        WHERE emp.DeptName = @DeptName
        ORDER BY emp.Name
        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
    ";

    string connectionString = GetRFIDConnectionString();
    using (SqlConnection conn = new SqlConnection(connectionString))
    using (SqlCommand cmd = new SqlCommand(sql, conn))
    {
        cmd.Parameters.AddWithValue("@DeptName", userDept);
        cmd.Parameters.AddWithValue("@Offset", offset);
        cmd.Parameters.AddWithValue("@PageSize", pageSize);

        await conn.OpenAsync();
        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                result.Add(new
                {
                    Pno = reader["Pno"].ToString(),
                    Name = reader["Name"].ToString(),
                    Position = Convert.ToInt32(reader["Position"]),
                    Worksites = reader["Worksites"].ToString()
                });
            }
        }
    }

    ViewBag.EmpList = result;
    ViewBag.Page = page;
    ViewBag.PageSize = pageSize;

    return View();
}



@if (ViewBag.EmpList != null)
{
    <table class="table table-bordered">
        <thead>
            <tr>
                <th>PNO</th>
                <th>Name</th>
                <th>Position</th>
                <th>Worksites</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var emp in ViewBag.EmpList)
            {
                <tr>
                    <td>@emp.Pno</td>
                    <td>@emp.Name</td>
                    <td>@emp.Position</td>
                    <td>@emp.Worksites</td>
                </tr>
            }
        </tbody>
    </table>
}
