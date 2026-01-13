    public void get_wo()
    {
        //PageRecordsDataSet.Tables["App_BOCW_SAP_to_Local"].Clear();
        //PageRecordsDataSet.Tables["App_BOCW_Details"].Clear();



        string period = ((DropDownList)summary_Record.Rows[0].FindControl("PERIOD")).SelectedValue;
        string year = ((DropDownList)summary_Record.Rows[0].FindControl("YEAR")).SelectedValue;


        DataSet ds_dup_chk = new DataSet();
        ds_dup_chk = blobj.get_dup_chk(year,period, Session["UserName"].ToString());
        if (ds_dup_chk.Tables[0].Rows.Count > 0)
        {
            MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors, "Data with selected Period & Year already exists, can not proceed with selected items again !!!");


            BtnSave.Visible = false;
        }
        else
        {
            BtnSave.Visible = true;
            string from = null, to = null;
            if (period == "April to June")
            {
                from = year + "-03-01 00:00:00.000";
                to = year + "-06-30 00:00:00.000";
            }
            else if (period == "July to September")
            {
                from = year + "-07-01 00:00:00.000";
                to = year + "-09-30 00:00:00.000";
            }
            else if (period == "October to December")
            {
                from = year + "-10-01 00:00:00.000";
                to = year + "-12-31 00:00:00.000";
            }
            else if (period == "January to March")
            {
                from = year + "-01-01 00:00:00.000";
                to = year + "-03-31 00:00:00.000";
            }



            DataSet ds = new DataSet();

            ds = blobj.Get_tab1_detail(from, to, Session["UserName"].ToString(), PageRecordDataSet.Tables["App_BOCW_Summary_wo"].Rows[0]["ID"].ToString());
            if (ds != null && ds.Tables["App_Bocw_details_workorder"].Rows.Count > 0)
            {
                PageRecordDataSet.Tables["App_Bocw_details_workorder"].Rows.Clear();
                PageRecordDataSet.Merge(ds);

            }


        }

    public DataSet Get_tab1_detail(string from, string to, string v_code,string masterid)
    {
       string strSQL = " select newid() as ID,'"+ masterid + "' as Master_ID,tab.WO_NO,tab.FROM_DATE ,tab.TO_DATE, tab.DEPT_CODE as DepartmentDesc ,tab.DepartmentCode as DEPT_CODE,tab.BOCW_NUMBER,tab.BOCW_DESC,tab.NATURE_OF_WORK from ( "
                       + " select distinct BS.KONNR as WO_NO,W.FROM_DATE,W.TO_DATE,W.BOCW_APPLICABLE as BOCW_NUMBER,'' as BOCW_DESC,W.DEPT_CODE,D.DepartmentCode,W.NATURE_OF_WORK from App_BOCW_SAP_to_Local BS left join App_WorkOrder_Reg W  on BS.KONNR=W.WO_NO   left join App_DepartmentMaster D on W.DEPT_CODE =D.DepartmentName  where BS.BUDAT between @BUDAT and '" + to+"' and BS.LIFNR=@LIFNR "
                       + " )tab order by tab.WO_NO ";
       Dictionary<string, object> objParam = new Dictionary<string, object>();
       objParam.Add("BUDAT", from);
      // objParam.Add("BUDAT", to);
       objParam.Add("LIFNR", "00000"+v_code);
       DataHelper dh = new DataHelper();
       return dh.GetDataset(strSQL, "App_Bocw_details_workorder", objParam);
    }


    }


      protected void PERIOD_SelectedIndexChanged(object sender, EventArgs e)
  {
      if (((DropDownList)summary_Record.Rows[0].FindControl("PERIOD")).SelectedValue == "" || ((DropDownList)summary_Record.Rows[0].FindControl("YEAR")).SelectedValue == "")
      {
          MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Warning, "Please select Period & Year.");
      }
      else
      {
          PageRecordDataSet.Tables["App_Bocw_details_workorder"].Clear();
          get_wo();
          WODetails_Record.BindData();
      }
  }

  protected void YEAR_SelectedIndexChanged(object sender, EventArgs e)
  {
      if (((DropDownList)summary_Record.Rows[0].FindControl("PERIOD")).SelectedValue == "" || ((DropDownList)summary_Record.Rows[0].FindControl("YEAR")).SelectedValue == "")
      {
          MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Warning, "Please select Period & Year.");
      }
      else
      {
          PageRecordDataSet.Tables["App_Bocw_details_workorder"].Clear();
          get_wo();
          WODetails_Record.BindData();
      }
  }


