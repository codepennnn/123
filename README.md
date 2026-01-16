foreach (Row row in rows)
{
    // 🔴 SKIP completely empty rows
    bool isEmptyRow = true;

    foreach (Cell cell in row.Descendants<Cell>())
    {
        if (!string.IsNullOrWhiteSpace(GetCellValue(spreadSheetDocument, cell)))
        {
            isEmptyRow = false;
            break;
        }
    }

    if (isEmptyRow)
        continue;

    DataRow tempRow = dt.NewRow();

    int columnIndex = 0;
    foreach (Cell cell in row.Descendants<Cell>())
    {
        tempRow[columnIndex++] = GetCellValue(spreadSheetDocument, cell);
    }

    dt.Rows.Add(tempRow);
}
