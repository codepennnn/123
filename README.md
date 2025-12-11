if (totalMandays > maxAllowedMandays)
{
    string msg = $"Total Mandays(Male+Female=Children) for License {licNo} exceeds allowed limit.\n\n" +
                 $"Total Employees: {totalEmp}\n" +
                 $"Days Worked: {item.Value.daysWorked}\n" +
                 $"Max Allowed Mandays: {maxAllowedMandays}\n" +
                 $"Entered Mandays: {totalMandays}";

    ShowAlert(msg);
    //btnSave.Enabled = false;
    return false;
}

if (totalEmp > capacity)
{
    string msg = $"Total Workers (Male+Female=Children) for License {licNo} : {totalEmp} exceeds Capacity : {capacity}";
    ShowAlert(msg);
    //btnSave.Enabled = false;
    return false;
}

private void ShowAlert(string message)
{
    if (string.IsNullOrEmpty(message)) return;

    // make JS string safe
    string safe = message
        .Replace("\\", "\\\\")    // escape backslashes
        .Replace("'", "\\'")      // escape single quotes
        .Replace("\r\n", "\\n")
        .Replace("\n", "\\n");

    string script = $"alert('{safe}');";

    // use unique key so ASP.NET doesn't skip it
    ScriptManager.RegisterClientScriptBlock(
        this,
        this.GetType(),
        Guid.NewGuid().ToString(),
        script,
        true
    );
}

