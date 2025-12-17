// ----------------------------
// FORCE DEFAULT WORK ORDER
// ----------------------------
if (ddlRowWorkOrder != null
    && string.IsNullOrEmpty(ddlRowWorkOrder.SelectedValue)
    && ddlRowWorkOrder.Items.Count > 1)
{
    // Select first valid WorkOrder (index 1, after "-- Select --")
    ddlRowWorkOrder.SelectedIndex = 1;

    var li = ddlRowWorkOrder.SelectedItem;

    // Auto-fill Location from WO
    if (ddlRowLocation != null &&
        li.Attributes["data-loc"] != null &&
        ddlRowLocation.Items.FindByValue(li.Attributes["data-loc"]) != null)
    {
        ddlRowLocation.SelectedValue = li.Attributes["data-loc"];
    }

    // Auto-fill EngagementType from WO
    if (ddlEngType != null && li.Attributes["data-eng"] != null)
    {
        ddlEngType.SelectedValue = li.Attributes["data-eng"];
    }
}


if (ddlEngType != null && string.IsNullOrEmpty(ddlEngType.SelectedValue))
{
    ddlEngType.SelectedValue = "ManPowerSupply";
}

