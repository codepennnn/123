  if (PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[0]["Status"].ToString() == "Approved")

  {
      foreach (DataRow r in PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows)

      {

          r["CC_UpdatedOn"] = System.DateTime.Now;
          r["CC_UpdatedBy"] = Session["UserName"].ToString();
          r["Status"] = "Approved";
          r["Remarks"] = r["Remarks"].ToString() + "( CC --" + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " -- " + Remarks_CC + ")|";


      }
  }
  else
  {

      foreach (DataRow r in PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows)

      {
          r["CC_UpdatedOn"] = System.DateTime.Now;
          r["CC_UpdatedBy"] = Session["UserName"].ToString();
          r["Status"] = "Return";
          r["Remarks"] = r["Remarks"].ToString() + "( CC --" + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " -- " + Remarks_CC + ")|";
      }
  }


   if (PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[0]["Status"].ToString() == "Approved")

   {
       foreach (DataRow r in PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows)

       {

           r["CC_UpdatedOn"] = System.DateTime.Now;
           r["CC_UpdatedBy"] = Session["UserName"].ToString();
           r["Status"] = "Approved";
           r["Remarks"] = r["Remarks"].ToString() + "( CC --" + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " -- " + Remarks_CC + ")|";


       }
   }
   else
   {

       foreach (DataRow r in PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows)

       {
           r["CC_UpdatedOn"] = System.DateTime.Now;
           r["CC_UpdatedBy"] = Session["UserName"].ToString();
           r["Status"] = "Return";
           r["Remarks"] = r["Remarks"].ToString() + "( CC --" + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " -- " + Remarks_CC + ")|";
       }
   }
