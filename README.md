protected void btnSave_Click(object sender, EventArgs e)
{
    // BEFORE saving – validate capacity
    foreach (GridViewRow row in HalfYearly_Records.Rows)
    {
        string licNo = row.Cells[?].Text.Trim();       // <-- Replace index
        int capacity = Convert.ToInt32(row.Cells[?].Text); // workerno column index
        
        int sexM = Convert.ToInt32(row.Cells[?].Text);
        int sexF = Convert.ToInt32(row.Cells[?].Text);

        int total = sexM + sexF;

        if (sexM > capacity)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"alert('Sex (M) for License {licNo} exceeds capacity {capacity}');", true);
            return;  // stop saving
        }

        if (sexF > capacity)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"alert('Sex (F) for License {licNo} exceeds capacity {capacity}');", true);
            return; 
        }

        if (total > capacity)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                $"alert('Total (M + F) for License {licNo} exceeds capacity {capacity}');", true);
            return;
        }
    }
