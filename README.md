   protected virtual void GetRecords(StringDictionary FilterCriteria, Int32 displayPageSize, Int32 displayPageNo, string shortExpression)
   {
       int totalPageSize = 0;
       totalPageSize = (displayPageSize * (displayPageNo + 1)) + 1;
       _PageRecordsDataSet.Clear();
       _PageRecordsDataSet.Merge(_BLObject.GetRecords(FilterCriteria, totalPageSize, shortExpression));
   }
      protected virtual void GetRecords(StringDictionary FilterCriteria, Int32 displayPageSize, Int32 displayPageNo, string shortExpression)
   {
       int totalPageSize = 0;
       totalPageSize = (displayPageSize * (displayPageNo + 1)) + 1;
       _PageRecordsDataSet.Clear();
       _PageRecordsDataSet.Merge(_BLObject.GetRecords(FilterCriteria, totalPageSize, shortExpression));
   }

      protected void Page_Load(object sender, EventArgs e)
   {
       HalfYearly_Records.DataSource = PageRecordsDataSet;

       if (!IsPostBack)
       {

           GetRecords(GetFilterCondition(), HalfYearly_Records.PageSize, 10, "");
           HalfYearly_Records.DataBind();

       }
   }

   protected override void SetBaseControls()
   {
       base.SetBaseControls();
       PageRecordsDataSet = dsRecords;
       BLObject = new BL_Half_Yearly();
   }

   private StringDictionary GetFilterCondition()
   {
       StringDictionary d = null;
       d = new StringDictionary();
       d.Add("V_Code", Session["UserName"].ToString());
       return d;
   }

    public DataSet GetRecords(System.Collections.Specialized.StringDictionary FilterConditions, int totalPagesize, string sortExpression)
 {

     string strSQL = "select distinct Createdon,LabourLicNo,Period,Year,Status from App_half_yearly_details where ";

     DataHelper dh = new DataHelper();
     Dictionary<string, object> objParam = null;

     if (FilterConditions != null && FilterConditions.Count > 0)
     {
         objParam = new Dictionary<string, object>();
         int cnt = FilterConditions.Count;
         if (FilterConditions["V_Code"] != null)
             strSQL += " VCode = '" + FilterConditions["V_Code"] + "'";


     }




     return dh.GetDataset(strSQL, "App_Half_Yearly_Details", objParam);


 }
