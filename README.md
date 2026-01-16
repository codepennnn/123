private bool IsBlankCell(string value)
{
    return string.IsNullOrWhiteSpace(value) || value == "&nbsp;";
}

private bool ValidateExcelData(out string errorMessage)
{
    errorMessage = "";

    // Column names as per Excel / GridView order
    string[] columnNames =
    {
        "MasterID","EMP_PF_EXEMPTED","EMP_ESI_EXEMPTED","OT_hrs",
        "YearWage","MonthWage","AadharNo","VendorCode","VendorName",
        "StateName","LocationCode","LocationNM","WorkOrderNo",
        "WorkManSl","WorkManName","WorkManCategory","PFNo","ESINo",
        "UANNo","TotDaysInMonth","TotSunDays","TotHoliDays",
        "TotWorkingDays","NoOfDaysWorkedMP","NoOfDaysWorkedRate",
        "NoOfDaysAbsMP","BasicRate","DARate","OtherAllow","HRA",
        "holiday","OtherDeduAmt","CashPaymentAmt","WeeklyAllowance"
    };

    for (int rowIndex = 0; rowIndex < BankStatement_Grid.Rows.Count; rowIndex++)
    {
        GridViewRow row = BankStatement_Grid.Rows[rowIndex];

        for (int colIndex = 0; colIndex < columnNames.Length; colIndex++)
        {
            string cellValue = row.Cells[colIndex].Text;

            if (IsBlankCell(cellValue))
            {
                errorMessage =
                    $"Excel validation failed!\n\n" +
                    $"Row : {rowIndex + 2}\n" +        // +2 because header + 1-based
                    $"Column : {columnNames[colIndex]}\n\n" +
                    $"Blank cells are not allowed.";

                return false;
            }
        }
    }

    return true;
}

protected void BtnSave_Click(object sender, EventArgs e)
{
    string errorMessage;

    if (!ValidateExcelData(out errorMessage))
    {
        ScriptManager.RegisterStartupScript(
            this,
            GetType(),
            "excelError",
            $"alert('{errorMessage.Replace("'", "\\'")}');",
            true
        );
        return; // ⛔ STOP SAVE
    }

    // existing code continues...
}



