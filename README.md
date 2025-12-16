   private void PopulateMonthYear()
   {
       ddlMonth.Items.Clear();
       for (int m = 1; m <= 12; m++)
           ddlMonth.Items.Add(new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m), m.ToString()));
       ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

       ddlYear.Items.Clear();
       int cur = DateTime.Now.Year;
       for (int y = cur - 2; y <= cur + 2; y++)
           ddlYear.Items.Add(new ListItem(y.ToString(), y.ToString()));
       ddlYear.SelectedValue = cur.ToString();
   }

   in my month dropdown showing current month that is december but i want one previous month to be show for ex november
