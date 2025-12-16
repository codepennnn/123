DateTime prev = DateTime.Now.AddMonths(-1);

ddlMonth.SelectedValue = prev.Month.ToString();
ddlYear.SelectedValue = prev.Year.ToString();
