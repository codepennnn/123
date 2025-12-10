private bool Check()
{
    foreach (GridViewRow row in HalfYearly_Records.Rows)
    {
        int capacity = Convert.ToInt32(row.Cells[7].Text);
        int sexM = Convert.ToInt32(row.Cells[13].Text);
        int sexF = Convert.ToInt32(row.Cells[14].Text);

        if (sexM + sexF > capacity)
        {
            // Show alert
            ScriptManager.RegisterStartupScript(
                this, this.GetType(),
                "alert",
                "alert('Total (M+F) cannot be greater than Capacity');",
                true);

            // ❌ Disable Save Button
            btnsave.Enabled = false;

            return false;
        }
    }

    // ✔ Enable Save Button if everything is OK
    btnsave.Enabled = true;

    return true;
}

protected void btnsave_Click(object sender, EventArgs e)
{
    if (!Check())
        return;   // Stop saving

    // Save code here...
}

