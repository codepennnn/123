my js working well calculation so now i submit so its calculated value will be or i should change in my below code please suggest and do
  
  
  
  protected void btnSave_Click(object sender, EventArgs e)
  {
      bocw_summary.BindData();
      bocw_tab_records.UnbindData();
      bocw_summary.UnbindData();

      
      PageRecordDataSet.Tables["App_BOCW_SAP_to_Local"].AcceptChanges();
     

      for (int i = 0; i < PageRecordDataSet.Tables["App_BOCW_Details"].Rows.Count; i++)
      {
          PageRecordDataSet.Tables["App_BOCW_Details"].Rows[i]["CreatedOn"] = System.DateTime.Now;
          PageRecordDataSet.Tables["App_BOCW_Details"].Rows[i]["CreatedBy"] = Session["UserName"].ToString();
          PageRecordDataSet.Tables["App_BOCW_Details"].Rows[i]["Master_Id"] = PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Id"].ToString();
          PageRecordDataSet.Tables["App_BOCW_Details"].Rows[i].AcceptChanges();
          PageRecordDataSet.Tables["App_BOCW_Details"].Rows[i].SetAdded();
      }
      
      PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Status"] = "Pending with CC";
      
      PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Cess_Amount_Balance"] = Convert.ToDouble(PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Cess_Amount"])-Convert.ToDouble(PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Cess_Amount_Paid"]);
      string a_d = ((DropDownList)bocw_summary.Rows[0].FindControl("Agree_Disagree")).SelectedValue;
      if (a_d == "NO")
      {
          PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Subjective_balance"] = Convert.ToDouble(PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Subjective_cess_amount"]) - Convert.ToDouble(PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Cess_Amount_Paid"]);
      }
      else
      {
          PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Subjective_balance"] = 0.000;
          PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Subjective_cess_amount"] = 0.000;
          PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Subjective_base_amount"] = 0.000;
      }
      PageRecordDataSet.Tables["App_BOCW_SAP_to_Local"].AcceptChanges();




      if (Payment_Attach_Name.HasFile)
      {
          string getFileName = "";
          List<string> FileList = new List<string>();
          foreach (HttpPostedFile htfiles in Payment_Attach_Name.PostedFiles)
          {
              getFileName = PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["ID"].ToString() + "_" + Path.GetFileName(htfiles.FileName);
              getFileName = Regex.Replace(getFileName, @"[,+*/?|><&=\#%:;@[^$?:'()!~}{`]", "");
              htfiles.SaveAs((@"D:/Cybersoft_Doc/CLMS/Attachments/" + getFileName));

              FileList.Add(getFileName);
              getFileName = "";
          }
          PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Payment_Attach_Name"] = string.Join(",", FileList);
      }


      if (Other_attach_name.HasFile)
      {
          string getFileName = "";
          List<string> FileList = new List<string>();
          foreach (HttpPostedFile htfiles in Other_attach_name.PostedFiles)
          {
              getFileName = PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["ID"].ToString() + "_" + Path.GetFileName(htfiles.FileName);
              getFileName = Regex.Replace(getFileName, @"[,+*/?|><&=\#%:;@[^$?:'()!~}{`]", "");
              htfiles.SaveAs((@"D:/Cybersoft_Doc/CLMS/Attachments/" + getFileName));

              FileList.Add(getFileName);
              getFileName = "";
          }
          PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Other_attach_name"] = string.Join(",", FileList);
      }


      bool result = Save();
          if (result)
          {
              //MyMsgBox.show(MRPS.Control.MyMsgBox.MessageType.Success, "Data Save Successfully !");
              //GetRecords(GetFilterCondition(), vendor_reg_Records.PageSize, 0, "");
              //vendor_reg_Records.BindData();
              PageRecordDataSet.Clear();
              bocw_tab_records.BindData();
              WorkOder_wise_records.BindData();
              //Test_Form_Record.BindData();

              btnSave.Visible = false;

              MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Success, "Record saved successfully !");
          }
          else
          {
              MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors, "Error while saving !");

          }

  }
