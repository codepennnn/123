private string JsSafe(string message)
{
    return message
        .Replace("\\", "\\\\")
        .Replace("'", "\\'")
        .Replace("\"", "\\\"")
        .Replace("\r", "")
        .Replace("\n", "\\n");
}

string errorMessage;

if (!ValidateExcelData(out errorMessage))
{
    string safeMsg = JsSafe(errorMessage);

    ScriptManager.RegisterStartupScript(
        this,
        this.GetType(),
        "excelError",
        $"alert('{safeMsg}');",
        true
    );
    return;
}

