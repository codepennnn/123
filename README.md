     if (PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["STATUS"].ToString() == "Approved")

     {

         PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["CC_CreatedOn"] = System.DateTime.Now;
         PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["CC_CreatedBy"] = Session["UserName"].ToString();
         PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["STATUS"] = "Approved";
         PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["Remarks"] = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Remarks"].ToString() + "( CC --" + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " -- " + Remarks_CC + ")|";


      

         
     }
     else
     {

         PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["CC_CreatedOn"] = System.DateTime.Now;
         PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["CC_CreatedBy"] = Session["UserName"].ToString();
         PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["STATUS"] = "Return";
         PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["Remarks"] = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Remarks"].ToString() + "( CC --" + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " -- " + Remarks_CC + ")|";
         
         
     }
