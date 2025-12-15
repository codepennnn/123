public DataSet GetRecords(
    System.Collections.Specialized.StringDictionary FilterConditions,
    int totalPagesize,
    string sortExpression)
{
    string strSQL =
        "SELECT DISTINCT CreatedOn, LabourLicNo, Period, Year, Status " +
        "FROM App_half_yearly_details " +
        "WHERE Status IN ('Pending With CC','Return','Approved')";

    DataHelper dh = new DataHelper();
    Dictionary<string, object> objParam = new Dictionary<string, object>();

    if (FilterConditions != null &&
        FilterConditions.ContainsKey("VendorCode"))
    {
        strSQL += " AND VCode = @VCode";
        objParam.Add("@VCode", FilterConditions["VendorCode"]);
    }

    strSQL += " ORDER BY CreatedOn DESC";

    return dh.GetDataset(strSQL, "App_half_yearly_details", objParam);
}
