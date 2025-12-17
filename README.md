    if (dtWorkOrders != null && string.IsNullOrEmpty(dtWorkOrders.SelectedValue)
        && dtWorkOrders.Items.Count > 1)
    {
        dtWorkOrders.SelectedIndex = 1; 

        var li = dtWorkOrders.SelectedItem;
        if (ddlRowLocation.Items.FindByValue(li.Attributes["data-loc"]) != null)
            ddlRowLocation.SelectedValue = li.Attributes["data-loc"];

        ddlEngType.SelectedValue = li.Attributes["data-eng"];
    }

    
    if (ddlEngType != null && string.IsNullOrEmpty(ddlEngType.SelectedValue))
        ddlEngType.SelectedValue = "ManPowerSupply";

        error SelectedValue Items SelectedIndex

        Compiler Error CS1061
'type' does not contain a definition for 'name' and no accessible extension method 'name' accepting a first argument of type 'type' could be found (are you missing a using directive or an assembly reference?).
