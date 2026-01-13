  protected void btnSave_Click(object sender, EventArgs e)
  {

      WODetails_Record.UnbindData();
      summary_Record.UnbindData();
     

      



      string Remarks_CC = ((TextBox)summary_Record.Rows[0].FindControl("NewRemarks")).Text;



      if (PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["STATUS"].ToString() == "Approved")

      {

          PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["CC_CreatedOn"] = System.DateTime.Now;
          PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["CC_CreatedBy"] = Session["UserName"].ToString();
          PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["STATUS"] = "Approved";
          PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["Remarks"] = PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["Remarks"].ToString() + "( CC --" + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " -- " + Remarks_CC + ")|";


          


      }
      else
      {

          PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["CC_CreatedOn"] = System.DateTime.Now;
          PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["CC_CreatedBy"] = Session["UserName"].ToString();
          PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["STATUS"] = "Return";
          PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["Remarks"] = PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["Remarks"].ToString() + "( CC --" + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + " -- " + Remarks_CC + ")|";
          
          
      }






      bool result = Save();




      if (result)
      {
          PageRecordDataSet.Clear();
          summary_Record.BindData();
          PageRecordsDataSet.Clear();
          summary_Entry_Record.BindData();
          GetRecords(GetFilterCondition(), summary_Records.PageSize, 10, "");
          summary_Records.BindData();
          btnSave.Visible = false;
          div_Remarks.Visible = false;
          WODetails_Record.BindData();
          ContainerDiv.Style["overflow"] = "";
          ContainerDiv.Style["height"] = "";


          // SearchLLBtn.Visible = false;
          MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Success, "Record saved successfully !");
      }
      else
      {
          MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors, "Error While Saving !");
      }
  }

