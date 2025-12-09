  protected void btnSave_Click(object sender, EventArgs e)
  {
      HalfYearly_Records.UnbindData();

      PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[0]["VCode"] = Session["Username"].ToString();
      PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[0]["Year"] = Year.SelectedValue;
      PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[0]["Period"] = SearchPeriod.SelectedValue;
     

      string vc = PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[0]["VCode"].ToString();
      string year = PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[0]["Year"].ToString();
      string Period = PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[0]["Period"].ToString();



      BL_Half_Yearly blobj = new BL_Half_Yearly();
      DataSet dsExist = blobj.chkExist(vc, year, Period);
      bool isExist = dsExist != null && dsExist.Tables[0].Rows.Count > 0;

      if (isExist)
      {


          DataSet ds = new DataSet();
          ds = blobj.GetDelete(vc, year, Period);

          if (PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[0].RowState.ToString() == "Modified")
          {

              string oldRef = dsExist.Tables[0].Rows[0]["RefNo"].ToString();
              for (int i = 0; i < PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows.Count; i++)
              {
                  PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i]["VCode"] = Session["Username"].ToString();
                  PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i]["Year"] = Year.SelectedValue;
                  PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i]["Period"] = SearchPeriod.SelectedValue;
                  PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i]["RefNo"] = oldRef;
                  PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i].AcceptChanges();
                  PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i].SetAdded();
                  PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i]["CreatedBy"] = Session["UserName"].ToString();
                  PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i]["CreatedOn"] = System.DateTime.Now;

              }


          }
      }


      else
      {
          DataSet ds = new DataSet();
          DataSet ds1 = new DataSet();
          ds = blobj.GetDelete(vc, year, Period);

      
         // string refNo = blobj.Generate_Global_RefNo("HALFYEARLY");

          if (PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[0].RowState == DataRowState.Modified)
          {

              for (int i = 0; i < PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows.Count; i++)
              {
                  PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i]["VCode"] = Session["Username"].ToString();
                  PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i]["Year"] = Year.SelectedValue;
                  PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i]["Period"] = SearchPeriod.SelectedValue;
              //    PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i]["RefNo"] = refNo;
                  PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i].AcceptChanges();
                  PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i].SetAdded();
                  PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i]["CreatedBy"] = Session["UserName"].ToString();
                  PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[i]["CreatedOn"] = System.DateTime.Now;

              }


          }
      }







      bool result = Save();

      if (result)
      {
          string Ref_No = blobj.Get_Ref_No(PageRecordDataSet.Tables["App_Half_Yearly_Details"].Rows[0]["ID"].ToString());




         System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Your Half Yearly Ref No. is: " + Ref_No + "');", true);

         PageRecordDataSet.Clear();
         HalfYearly_Records.BindData();
         btnSave.Visible = false;
         MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Success, "Record saved successfully !");



      }
      else
      {
          MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors, "Error While Saving !");
      }
  }
