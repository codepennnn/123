  protected void btnSave_Click(object sender, EventArgs e)
  {
      string vcode = Session["UserName"].ToString();
      string period = SearchPeriod.SelectedValue;
      string year = Year.SelectedValue;
      BL_Half_Yearly blobj = new BL_Half_Yearly();

      foreach (GridViewRow row in gvRefUpload.Rows)
      {
          string lic = row.Cells[0].Text.Trim();

          //string raw = WebUtility.HtmlDecode(row.Cells[0].Text);
          //string lic = Regex.Replace(raw, "[^A-Za-z0-9]", "");

          FileUpload fu = (FileUpload)row.FindControl("Final_Attachment");

          if (fu == null || !fu.HasFile)
              continue;  

        
          DataSet ds = blobj.Get_Data_By_Lic(lic, vcode, period, year);
          if (ds == null || ds.Tables[0].Rows.Count == 0)
              continue;

          List<string> fileList = new List<string>();

          foreach (HttpPostedFile file in fu.PostedFiles)
          {
              string fileName =
                  ds.Tables[0].Rows[0]["ID"].ToString() + "_" +
                  Path.GetFileName(file.FileName);

              fileName = Regex.Replace(fileName,
                  @"[,+*/?|><&=\#%:;@^$?:'()!~}{`]", "");

              file.SaveAs(@"D:/Cybersoft_Doc/CLMS/Attachments/" + fileName);
              fileList.Add(fileName);
          }

          string attachments = string.Join(",", fileList);

          foreach (DataRow dr in ds.Tables[0].Rows)
          {
              dr["Final_Attachment"] = attachments;
              dr["Status"] = "Pending With CC";
          }

          blobj.SaveRecord(ref ds);
      }

      MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Success,
          "Record saved successfully !");

      gvRefUpload.Visible = false;
      btnSave.Visible = false;
  }






string lic = row.Cells[0].Text.Trim();   some parameter comes like this &quot;NA&quot;15

so that this licno does not match with my table column labourLicNo that is "NA"15

SELECT * FROM App_Half_Yearly_Details WHERE labourLicNo = @lic AND vcode = @vcode AND period = @period AND year=@year 

SELECT * FROM App_Half_Yearly_Details WHERE labourLicNo = '&quot;NA&quot;15' AND vcode = '10517' AND period = 'Jan-June' AND year='2024'


please what is the solution for this so that it exact match in my query 

