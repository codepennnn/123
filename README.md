string debugScript = $"console.log('DEBUG: validation reached for {licNo}'); alert('{safe}');";
ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(), debugScript, true);


// build message safely
string msg = $"Gross Wages for License {licNo} exceed limit.\\n" +
             $"Total Mandays = {totalMandays}\\n" +
             $"Allowed Amount = {allowedAmount}\\n" +
             $"Entered Gross = {grossWages}";

// escape backslashes and single quotes, convert new lines to \n
string safe = msg
    .Replace("\\", "\\\\")
    .Replace("'", "\\'")
    .Replace("\r\n", "\\n")
    .Replace("\n", "\\n");

// unique key so ASP.NET won't ignore duplicates
string script = $"alert('{safe}');";

// If you use a ScriptManager on page (works with and without UpdatePanel)
ScriptManager.RegisterClientScriptBlock(
    this, 
    this.GetType(), 
    Guid.NewGuid().ToString(),   // unique key
    script, 
    true
);

