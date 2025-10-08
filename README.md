var blockFromDiv = (HtmlGenericControl)row.FindControl("blockFromDiv");
    var blockToDiv = (HtmlGenericControl)row.FindControl("blockToDiv");

    // Hide or show based on selection
    if (Type == "RFQ_U")  // Unblock
    {
        blockFromDiv.Visible = false;
        blockToDiv.Visible = false;
    }
    else // Block
    {
        blockFromDiv.Visible = true;
        blockToDiv.Visible = true;
    }
