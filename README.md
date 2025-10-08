protected void Block_unblock_SelectedIndexChanged(object sender, EventArgs e)
{
    // The dropdown that triggered the event
    DropDownList ddl = (DropDownList)sender;

    // Get the template container of this dropdown
    Control container = ddl.NamingContainer;

    // Get the selected value
    string Type = ddl.SelectedValue;

    // Refresh Reason dropdown
    Dictionary<string, object> ddlParams = new Dictionary<string, object>();
    ddlParams.Add("type", Type);
    DropDownList ddlReason = (DropDownList)container.FindControl("Reason");
    if (ddlReason != null)
    {
        GetDropdowns("RFQ_Reason", ddlParams);
        ddlReason.DataBind();
    }

    // Find the divs inside the same container
    System.Web.UI.HtmlControls.HtmlGenericControl blockFromDiv =
        container.FindControl("blockFromDiv") as System.Web.UI.HtmlControls.HtmlGenericControl;

    System.Web.UI.HtmlControls.HtmlGenericControl blockToDiv =
        container.FindControl("blockToDiv") as System.Web.UI.HtmlControls.HtmlGenericControl;

    // Set visibility
    if (blockFromDiv != null && blockToDiv != null)
    {
        if (Type == "RFQ_U")  // Unblock
        {
            blockFromDiv.Visible = false;
            blockToDiv.Visible = false;
        }
        else
        {
            blockFromDiv.Visible = true;
            blockToDiv.Visible = true;
        }
    }
}
