  protected void BtnSave_Click(object sender, EventArgs e)
  {
      //DataSet ds = new DataSet();
      //DataSet ds_Loc_Name = new DataSet();
      //ds_Loc_Name = blobj.Get_Location(Month_Year.Text.Substring(5, 2), Month_Year.Text.Substring(0, 4), Session["UserName"].ToString());
      //ds =blobj.check_exist_data(Session["UserName"].ToString(), Month_Year.Text.Substring(0, 4), Month_Year.Text.Substring(5, 2), Location.SelectedValue);
      //GridViewRow g1 = BankStatement_Grid();

      //if ()
      //{
      //    //MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Warning, "your Record is already exist of "+Month_Year.Text+"Month/Year and "+ ds_Loc_Name.Tables[0].Rows[0]["LocationNM"].ToString() +"Location ");
      //}
      //else
      //{
          SqlConnection con;
          SqlCommand cmd;
          SqlTransaction tran;
          string strcon = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
          con = new SqlConnection(strcon);
          cmd = new SqlCommand();
          cmd.Connection = con;
          con.Open();
          tran = con.BeginTransaction();
          cmd.Transaction = tran;
      GridViewRow fr = BankStatement_Grid.Rows[0];
      //string yrwage = fr.Cells[4].Text;
      //string monthwage = fr.Cells[5].Text;
      //string vcode = fr.Cells[7].Text;
      //string loc = fr.Cells[10].Text;
     
      //int returnvalue= Convert.ToInt32(ds_check.Tables[0].Rows[0]["returndata"].ToString());
      DataSet ds_check = new DataSet();
      ds_check = blobj.check_exist_data(fr.Cells[7].Text, fr.Cells[4].Text, fr.Cells[5].Text, fr.Cells[10].Text);
      if (Convert.ToInt32(ds_check.Tables[0].Rows[0]["returndata"])>0)
      {
          //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "Validate_stat('Record already exist !! Your Month/year is: " + fr.Cells[5].Text + "/"+ fr.Cells[4].Text + " and Location "+ fr.Cells[10].Text + "');", true);
          ////this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('warning !!!', 'Please Insert Department Details !!!, 'warning');", true);
          ////this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('Sorry ' , 'Vendor Registration not applied or Pending for approval !!!', 'error');", true);
          string query = string.Empty;
          
         query = "delete from App_Att_Cum_Wage_Gen_Excel_Upload  where VendorCode='"+ fr.Cells[7].Text + "' and YearWage='"+ fr.Cells[4].Text + "' and MonthWage='"+ fr.Cells[5].Text + "' and LocationCode='"+ fr.Cells[10].Text + "'";
          cmd.CommandText = query;
          cmd.ExecuteNonQuery();
          tran.Commit();
          con.Close();
          insert_statement();
      }
      else
      {
          insert_statement();
      }


  }
  public void insert_statement()
  {
      SqlConnection con;
      SqlCommand cmd;
      SqlTransaction tran;
      string strcon = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
      con = new SqlConnection(strcon);
      cmd = new SqlCommand();
      cmd.Connection = con;
      con.Open();
      tran = con.BeginTransaction();
      cmd.Transaction = tran;
          try
          {

              foreach (GridViewRow g1 in BankStatement_Grid.Rows)
              {
                  string query = string.Empty;
                  query = "insert into App_Att_Cum_Wage_Gen_Excel_Upload(" +
                       "ID,MasterID,EMP_PF_EXEMPTED,EMP_ESI_EXEMPTED,OT_hrs,YearWage,MonthWage,AadharNo,VendorCode,VendorName," +
                       "StateName,LocationCode,LocationNM,WorkOrderNo,WorkManSl,WorkManName,WorkManCategory,PFNo,ESINo,UANNo," +
                       "TotDaysInMonth,TotSunDays,TotHoliDays,TotWorkingDays,NoOfDaysWorkedMP,NoOfDaysWorkedRate,NoOfDaysAbsMP,BasicRate," +
                       "DARate,OtherAllow,HRA,holiday,OtherDeduAmt,CashPaymentAmt,WeeklyAllowance,CreatedOn,CreatedBy) values (newid(),'" + g1.Cells[0].Text + "','" + g1.Cells[1].Text + "','" + g1.Cells[2].Text + "','" + g1.Cells[3].Text + "','" + g1.Cells[4].Text + "','" + g1.Cells[5].Text + "','"
                       + g1.Cells[6].Text + "','" + g1.Cells[7].Text + "','" + g1.Cells[8].Text + "','" + g1.Cells[9].Text + "','" + g1.Cells[10].Text + "','"
                       + g1.Cells[11].Text + "','" + g1.Cells[12].Text + "','" + g1.Cells[13].Text + "','" + g1.Cells[14].Text + "','" + g1.Cells[15].Text + "','"
                       + g1.Cells[16].Text + "','" + g1.Cells[17].Text + "','" + g1.Cells[18].Text + "','" + g1.Cells[19].Text + "','" + g1.Cells[20].Text + "','"
                       + g1.Cells[21].Text + "','" + g1.Cells[22].Text + "','" + g1.Cells[23].Text + "','" + g1.Cells[24].Text + "','" + g1.Cells[25].Text + "','"
                       + g1.Cells[26].Text + "','" + g1.Cells[27].Text + "','" + g1.Cells[28].Text + "','" + g1.Cells[29].Text + "','" + g1.Cells[30].Text + "','"
                       + g1.Cells[31].Text + "','" + g1.Cells[32].Text + "','" + g1.Cells[33].Text + "','" + DateTime.Now.ToString() + "','"
                       + Session["UserName"].ToString() + " ')";
                 
                  cmd.CommandText = query;
                  cmd.ExecuteNonQuery();
              }
              tran.Commit();
              MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Success, "Record saved successfully !");

          }
          catch (Exception ex)
          {

              tran.Rollback();
              ex.Message.ToString();
              MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors, "Error while saving !");
          }
          finally
          {
              con.Close();
          }
      
  }
  public void excel_upload()
  {

      OleDbConnection ExcelConection = null;
      OleDbCommand ExcelCommand = null;
      OleDbDataReader ExcelReader = null;
      OleDbConnectionStringBuilder OleStringBuilder = null;
      DataTable dt = new DataTable();
      try
      {

          DataSet ds = new DataSet();


          string path = MapPath(@"~\Upload");
          string filename = path + "\\" + File_Attachment.FileName;
          //string fs= File_Attachment.FileName;
          File_Attachment.SaveAs(filename);
          using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(@filename, false))
          {

              WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
              IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
              string relationshipId = sheets.First().Id.Value;
              WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
              Worksheet workSheet = worksheetPart.Worksheet;
              SheetData sheetData = workSheet.GetFirstChild<SheetData>();
              IEnumerable<Row> rows = sheetData.Descendants<Row>();

              foreach (Cell cell in rows.ElementAt(0))
              {
                  dt.Columns.Add(GetCellValue(spreadSheetDocument, cell));
              }

              foreach (Row row in rows) //this will also include your header row...
              {
                  DataRow tempRow = dt.NewRow();

                  for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                  {
                      tempRow[i] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i));

                  }

                  dt.Rows.Add(tempRow);
              }

          }
          dt.Rows.RemoveAt(0); //...so i'm taking it out here.



          //dt.Fill(ds);
          ds.Tables.Add(dt);

          BankStatement_Grid.DataSource = ds;

          BankStatement_Grid.DataBind();
          GridViewRow fr = BankStatement_Grid.Rows[0];
          //string yrwage = fr.Cells[4].Text;
          //string monthwage = fr.Cells[5].Text;
          //string vcode = fr.Cells[7].Text;
          //string loc = fr.Cells[10].Text;

          
          DataSet ds_check = new DataSet();
          ds_check = blobj.check_exist_data(fr.Cells[7].Text, fr.Cells[4].Text, fr.Cells[5].Text, fr.Cells[10].Text);
          if (Convert.ToInt32(ds_check.Tables[0].Rows[0]["returndata"]) > 0)
          {
              ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "Validate_stat('Record already exist !! Your Month/year is: " + fr.Cells[5].Text + "/" + fr.Cells[4].Text + " and Location " + fr.Cells[11].Text + " if yoy submit than your record will be updated');", true);
              //this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('warning !!!', 'Please Insert Department Details !!!, 'warning');", true);
              ////this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('Sorry ' , 'Vendor Registration not applied or Pending for approval !!!', 'error');", true);
             
          }

      }
      catch (Exception Args)
      {
          MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors, Args.Message);
      }
      finally
      {
          if (ExcelCommand != null)
              ExcelCommand.Dispose();
          if (ExcelReader != null)
              ExcelReader.Dispose();
          if (ExcelConection != null)
              ExcelConection.Dispose();
      }
  }



Conversion failed when converting the varchar value '&nbsp;' to data type int.
