var blockFromDiv = container.FindControl("blockFromDiv") as HtmlGenericControl;
    var blockToDiv = container.FindControl("blockToDiv") as HtmlGenericControl;

    if (blockFromDiv != null && blockToDiv != null)
    {
        // Hide when Unblock selected
        if (Type == "RFQ_U")
        {
            blockFromDiv.Visible = false;
            blockToDiv.Visible = false;
        }
        else
        {
            blockFromDiv.Visible = true;
            blockToDiv.Visible = true;
        }
