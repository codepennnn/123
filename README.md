private StringDictionary GetFilterCondition()
{
    StringDictionary d = new StringDictionary();

    if (Session["UserName"] != null)
    {
        d.Add("VendorCode", Session["UserName"].ToString());
    }

    return d;
}

public DataSet GetRecords(
    System.Collections.Specialized.StringDictionary FilterConditions,
    int totalPagesize,
    string sortExpression)
{
    string strSQL =
        "SELECT * FROM App_half_yearly_details " +
        "WHERE Status IN ('Pending With CC','Return','Approved')";

    DataHelper dh = new DataHelper();
    Dictionary<string, object> objParam = new Dictionary<string, object>();

    if (FilterConditions != null && FilterConditions.Count > 0)
    {
        foreach (string key in FilterConditions.Keys)
        {
            if (key == "VendorCode")
            {
                strSQL += " AND VCode = @VCode";
                objParam.Add("@VCode", FilterConditions[key]);
            }
        }
    }

    strSQL += " ORDER BY CreatedOn DESC";

    return dh.GetDataset(strSQL, "App_half_yearly_details", objParam);
}

