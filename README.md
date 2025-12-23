The specified path, file name, or both are too long. The fully qualified file name must be less than 260 characters, and the directory name must be less than 248 characters.'      string getFileName2 = "";
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


  and here    <asp:TemplateField HeaderText="Attachment" SortExpression="Attachment" >
             <ItemTemplate>
                 <asp:BulletedList runat="server" ID="Attachment" CssClass="attachment-list" DisplayMode="HyperLink"  OnClick="ReturnAttachment_Click" />
             </ItemTemplate>
         </asp:TemplateField>  i want Length/ size of File name should be freezed (Say - 20 words only) 
