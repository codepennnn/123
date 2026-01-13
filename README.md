protected void Status_SelectedIndexChanged(object sender, EventArgs e)
{
    string stat = ((RadioButtonList)summary_Record.Rows[0]
                   .FindControl("STATUS")).SelectedValue;

    foreach (GridViewRow row in WODetails_Record.Rows)
    {
        RequiredFieldValidator rfv =
            (RequiredFieldValidator)row
            .FindControl("rfvBOCW_APPLICABLE_AS_PER_CC");

        if (rfv != null)
        {
            rfv.Enabled = (stat == "Approved");
        }
    }
}
