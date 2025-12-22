  protected void btnSave_Click(object sender, EventArgs e)
  {


      PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Wage"] = 0;
      PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["PfEsi"] = 0;
      PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Leave"] = 0;
      PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Bonus"] = 0;
      PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["LL"] = 0;
      PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Grievance"] = 0;
      PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Notice"] = 0;


      if (((CheckBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Wage")).Checked)
          PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Wage"] = 1;

      if (((CheckBox)WorkOrder_Exemption_Record.Rows[0].FindControl("PfEsi")).Checked)
          PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["PfEsi"] = 1;

      if (((CheckBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Leave")).Checked)
          PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Leave"] = 1;

      if (((CheckBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Bonus")).Checked)
          PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Bonus"] = 1;

      if (((CheckBox)WorkOrder_Exemption_Record.Rows[0].FindControl("LL")).Checked)
          PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["LL"] = 1;

      if (((CheckBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Grievance")).Checked)
          PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Grievance"] = 1;

      if (((CheckBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Notice")).Checked)
          PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Notice"] = 1;






      string selectedWorkorder = string.Join(",", ((CheckBoxList)WorkOrder_Exemption_Record.Rows[0].FindControl("WorkOrderNo")).Items.
      Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value));

      //PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["WorkOrderNo"] = selectedWorkorder;


      //string strRemarks = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Remarks"].ToString();
      
      
      WorkOrder_Exemption_Record.UnbindData();
      BL_WorkOrder_Exemption blobj = new BL_WorkOrder_Exemption();

      string Workordercomma = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["WorkOrderNo"].ToString();
      string WorkOrderTrim = Workordercomma.TrimEnd(',');
      PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["WorkOrderNo"] = WorkOrderTrim;



      string getFileName2 = "";
      List<string> FileList2 = new List<string>();
      if (((FileUpload)WorkOrder_Exemption_Record.Rows[0].FindControl("DbstsAttachment")).HasFile)
      {
          foreach (HttpPostedFile htfiles in ((FileUpload)WorkOrder_Exemption_Record.Rows[0].FindControl("DbstsAttachment")).PostedFiles)
          {
              getFileName2 = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["ID"].ToString() + "_" + Path.GetFileName(htfiles.FileName);
              getFileName2 = Regex.Replace(getFileName2, @"[,+*/?|><&=\#%:;@[^$?:'()!~}{`]", "");
              htfiles.SaveAs((@"D:/Cybersoft_Doc/CLMS/Attachments/" + getFileName2));
              FileList2.Add(getFileName2);
          }
          PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["DbstsAttachment"] = string.Join(",", FileList2);
      }



      string getFileName3 = "";
      List<string> FileList3 = new List<string>();
      if (((FileUpload)WorkOrder_Exemption_Record.Rows[0].FindControl("ComplianceAttachment")).HasFile)
      {
          foreach (HttpPostedFile htfiles in ((FileUpload)WorkOrder_Exemption_Record.Rows[0].FindControl("ComplianceAttachment")).PostedFiles)
          {
              getFileName3 = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["ID"].ToString() + "_" + Path.GetFileName(htfiles.FileName);
              getFileName3 = Regex.Replace(getFileName3, @"[,+*/?|><&=\#%:;@[^$?:'()!~}{`]", "");
              htfiles.SaveAs((@"D:/Cybersoft_Doc/CLMS/Attachments/" + getFileName3));
              FileList3.Add(getFileName3);
          }
          PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["ComplianceAttachment"] = string.Join(",", FileList3);
      }







      DataTable remarksTable = PageRecordDataSet.Tables["App_WorkOrder_exemption_remarks"];
      Guid masterID = Guid.Parse(PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["ID"].ToString());

      if (remarksTable.Rows.Count == 0)
      {
          DataRow newRow = remarksTable.NewRow();

          newRow["ID"] = Guid.NewGuid();



          string getFileName = "";
          List<string> FileList = new List<string>();
          if (((FileUpload)Attachment).HasFile)
          {
              foreach (HttpPostedFile htfiles in ((FileUpload)Attachment).PostedFiles)
              {
                  getFileName = newRow["ID"].ToString() + "_" + Path.GetFileName(htfiles.FileName);
                  getFileName = Regex.Replace(getFileName, @"[,+*/?|><&=\#%:;@[^$?:'()!~}{`]", "");
                  htfiles.SaveAs((@"D:/Cybersoft_Doc/CLMS/Attachments/" + getFileName));
                  FileList.Add(getFileName);
              }
              newRow["Attachment"] = string.Join(",", FileList);
          }






          newRow["MASTER_ID"] = masterID;
          newRow["Remarks"] = Remarks.Text;
          newRow["CreatedOn"] = DateTime.Now;
          newRow["CreatedBy"] = Session["UserName"].ToString();



          remarksTable.Rows.Add(newRow);
      }




      PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Status"] = "Pending With CC";

      if (PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0].RowState.ToString() == "Modified")
      {
          PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["ResubmittedOn"] = System.DateTime.Now;
          PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["ResubmittedBy"] = Session["UserName"].ToString();
      }











      //if (PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Remarks"].ToString() == "")
      //{
      //    PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Remarks"] = strRemarks + "( VENDOR --" + System.DateTime.Now.ToString("dd/MM/yyyy")
      //        + " -- " + ((TextBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Remarks")).Text + ")|";
      //}
      //else
      //{
      //    PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Remarks"] = strRemarks + "( VENDOR --" + System.DateTime.Now.ToString("dd/MM/yyyy")
      //        + " -- " + ((TextBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Remarks")).Text + ")|";
      //}


      //validation for checkboxlist
      int count = 0;
      foreach (ListItem lstitem in ((CheckBoxList)WorkOrder_Exemption_Record.Rows[0].FindControl("WorkOrderNo")).Items)
      {
          if (lstitem.Selected == true)
              count++;
      }
      if (count == 0)
      {

          MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors, "Please Select atleast one workorder no. !!");
          return;
      }


      //validation - Approved work orders should not be repeated within the exempted period defined by the CC team

      string[] workOrders = PageRecordDataSet.Tables["App_WorkOrder_Exemption"]
                                 .Rows[0]["WorkOrderNo"]
                                 .ToString()
                                 .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

      foreach (string wo in workOrders)
      {
          List<string> conflicts = blobj.GetWorkOrdersInExemptionPeriod(wo.Trim());

          if (conflicts.Any())
          {
              string joined = string.Join(", ", conflicts);
              MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors,
                  $"The following work orders are already approved and still within the exemption period: {joined}. Duplicate not allowed!");
              return;
          }
      }




    




      bool result = Save();

      if (result)
      {

          string v_code = Session["UserName"].ToString();
          string v_name = "";
          string wo_no = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["WorkOrderNo"].ToString();
          string subject = "";
          string msg = "";
          string cc = "";
          DataSet ds = new DataSet();
          DataSet ds1 = new DataSet();
          BL_Send_Mail blobj1 = new BL_Send_Mail();
          ds = blobj1.Get_Vendor_mail(Session["UserName"].ToString());
          if (ds != null && ds.Tables[0].Rows.Count > 0)
          {
              cc = ds.Tables["aspnet_Membership"].Rows[0]["Email"].ToString();
          }

          ds1 = blobj1.Get_Vendor_name(v_code);
          if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
          {
              v_name = ds1.Tables["App_vendor_reg"].Rows[0]["v_name"].ToString();
          }



          //subject = "Application for WorkOrder Exemption has been submitted or updated by M/S  " + v_name + " (Vendor Code – " + v_code + " ) ";
          //msg = "<html><body><P><B>"
          //              + "To<BR /> "
          //              + "Contractor Cell <BR/>"
          //              + "<BR />Dear Sir/Madam"
          //              + "<BR /> "

          //              + "M/s " + v_name + " (" + v_code + ") has submitted Work Order Registration application against Work Order " + wo_no + ", request you to please do the needful as soon as possible.<BR/>"

          //              + "<BR />Link :- https://services.tsuisl.co.in/CLMS "
          //              + " <BR /> " + " <BR /> Thanks & Regards " + " <BR /> "
          //              + " <BR /> TATA STEEL UISL Contractor's Cell </B></P></html></body>"
          //              + " <BR /> "
          //              + " <BR /> Note: Please do not reply as it is a system generated mail. ";


          //blobj1.sendmail_request("", cc, subject, msg, "");



          subject = "Submission of Application for Interim Bill Submission – M/s " + v_name + " (Vendor Code: " + v_code + ") – Work Order No(s): " + wo_no;


          msg = "<html><body><p>"
                        + "<b>To: </b><br/>Contractors’ Cell<br/><br/>"
                             + "<b>Cc:</b> M/s " + v_name + " (Vendor Code: " + v_code + ")<br/><br/>"
                        + "Dear Sir/Madam,<br/><br/>"
                        + "M/s " + v_name + " (Vendor Code: " + v_code + ") has submitted an application through the "
                        + "“WorkOrder Exemption (Billing)” module in CLMS, requesting additional time to complete compliance updates "
                        + "and seeking permission to submit bills in the interim against Work Order No(s): " + wo_no + ".<br/><br/>"
                        + "Kindly review the application and take necessary action at the earliest.<br/><br/>"
                        + "You may access the application via the following link: "
                        + "<a href='https://services.tsuisl.co.in/CLMS'>https://services.tsuisl.co.in/CLMS</a><br/><br/>"
                        + "Thanks & Regards,<br/>"
                        + "<b>TATA Steel UISL</b><br/>"
                        + "<b>Contractors’ Cell</b><br/><br/>"
                        + "<i>Note: This is a system-generated email. Please do not reply.</i>"
                        + "</p></body></html>";







          PageRecordDataSet.Clear();
          remarksTable.Clear();
          Remarks.Text = "";
          WorkOrder_Exemption_Record.BindData();
          GetRecords(GetFilterCondition(), WorkOrder_Exemption_Records.PageSize, 10, "");
          WorkOrder_Exemption_Records.BindData();

          MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Success, "Record saved successfully !");
          btnSave.Visible = false;
          DivInputFields.Visible = false;

      }
      else
      {
          MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors, "Error While Saving !");
      }
  }
