DataRow[] rows = workOrderDs.Tables[0]
    .Select("WO_NO = '" + workOrderNo + "'");

if (rows.Length > 0)
{
    rows[0]["BOCW_APPLICABLE_AS_PER_CC"] = bocwValue;
}
