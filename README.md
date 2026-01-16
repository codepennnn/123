private string CleanCellText(string value)
{
    if (string.IsNullOrWhiteSpace(value) || value == "&nbsp;")
        return "0";   // default for numeric fields

    return value.Trim();
}

CleanCellText(g1.Cells[24].Text)

