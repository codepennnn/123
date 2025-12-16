private void PopulateMonthYear()
{
    ddlMonth.Items.Clear();
    for (int m = 1; m <= 12; m++)
        ddlMonth.Items.Add(
            new ListItem(
                CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m),
                m.ToString()
            )
        );

    ddlYear.Items.Clear();
    int cur = DateTime.Now.Year;
    for (int y = cur - 2; y <= cur + 2; y++)
        ddlYear.Items.Add(new ListItem(y.ToString(), y.ToString()));

    // --------- Added logic (previous month) ----------
    DateTime prev = DateTime.Now.AddMonths(-1);
    ddlMonth.SelectedValue = prev.Month.ToString();
    ddlYear.SelectedValue = prev.Year.ToString();
    // -----------------------------------------------
}
