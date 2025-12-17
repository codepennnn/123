// --- Rebuild grid AFTER SAVE using DOJ + DOE (same as View) ---
DataTable dt = new DataTable();
dt.Columns.Add("Date", typeof(DateTime));

for (int d = 0; d < days; d++)
{
    DateTime curr = first.AddDays(d);

    // before DOJ
    if (doj.HasValue && curr < doj.Value)
        continue;

    // after DOE (DOE day INCLUDED)
    if (doe.HasValue && curr > doe.Value)
        continue;

    DataRow r = dt.NewRow();
    r["Date"] = curr;
    dt.Rows.Add(r);
}

gvAttendance.DataSource = dt;
gvAttendance.DataBind();
pnlGrid.Visible = true;
lblRowCount.Text = dt.Rows.Count.ToString();
