      public DataSet chkExist(string vc,string year,string Period)
      {
          string strSQL = " select * from App_Half_Yearly_Details where Vcode=@Vcode and Year=@year and Period=@Period ";
          Dictionary<string, object> objParam = new Dictionary<string, object>();
          DataHelper dh = new DataHelper();
          objParam.Add("Vcode", vc);
          objParam.Add("Year", year);
          objParam.Add("Period", Period);
          
          return dh.GetDataset(strSQL, "App_Half_Yearly_Details", objParam);
      }
