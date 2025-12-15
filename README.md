   private StringDictionary GetFilterCondition()
   {
       StringDictionary d = null;
       d = new StringDictionary();
       d.Add("VendorCode", Session["UserName"].ToString());
       return d;
   }
  
  public DataSet GetRecords(System.Collections.Specialized.StringDictionary FilterConditions, int totalPagesize, string sortExpression)
  {

      string strSQL = " select * from App_half_yearly_details where Status in ('Pending With CC','Return','Approved') ";
      DataHelper dh = new DataHelper();
      Dictionary<string, object> objParam = null;
      if (FilterConditions != null && FilterConditions.Count > 0)
      {
          strSQL += " and ";
          objParam = new Dictionary<string, object>();
          int cnt = FilterConditions.Count;
          foreach (string dickey in FilterConditions.Keys)
          {
              objParam.Add(dickey, "%" + FilterConditions[dickey] + '%');
              if (cnt > 0 && cnt != FilterConditions.Count)
                  strSQL += " and " + dickey + " like @" + dickey;
              else
                  strSQL += dickey + " like @" + dickey;
              cnt--;
          }
      }
      strSQL += " order by CreatedOn desc";
      return dh.GetDataset(strSQL, "App_half_yearly_details", objParam);

  }
 
 
 select * from App_half_yearly_details where Status in ('Pending With CC','Return','Approved')  and vendorcode like @vendorcode order by CreatedOn desc
 but i want   select * from App_half_yearly_details where Status in ('Pending With CC','Return','Approved')  and vcode = @vcode order by CreatedOn desc
