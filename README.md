  public List<Dictionary<string, object>> RR_Alert_latest(string WorkOrderNo, string VendorCode)
  {

     
      SqlConnection con = new SqlConnection("Data Source=10.0.168.50;Initial Catalog=CLMSDB;User ID=fs;Password=p@ssW0Rd321;TrustServerCertificate=True");
      con.Open();
      Guid Id = Guid.NewGuid();



      List<Dictionary<string, object>> result = null;


      try
      {
        

          SqlDataAdapter da = new SqlDataAdapter("select c.V_CODE, c.WO_NO, c.START_DATE, c.END_DATE, (select V_NAME from App_Vendor_Reg v where v.V_CODE = c.V_CODE) as V_Name from App_Vendorwodetails c where V_CODE =  '" + VendorCode + "' and WO_NO = '" + WorkOrderNo + "'", con);

      DataSet ds = new DataSet();
      da.SelectCommand.CommandTimeout = 300;
      da.Fill(ds);

    
      int k = 0;


      DataSet ds_final = new DataSet();
      DataTable dt = new DataTable("select c.* ,(select V_NAME from App_Vendor_Reg v where  v.V_CODE  = c.V_CODE) as V_Name from COMPLIANCE_DBTS c ");

      dt.Columns.Add(new DataColumn("CURR_MONTH", typeof(string)));
      dt.Columns.Add(new DataColumn("PROC_MONTH", typeof(string)));
      dt.Columns.Add(new DataColumn("WO_NO", typeof(string)));
      dt.Columns.Add(new DataColumn("V_CODE", typeof(string)));

      dt.Columns.Add(new DataColumn("V_Name", typeof(string)));

      dt.Columns.Add(new DataColumn("WAGES", typeof(string)));
      dt.Columns.Add(new DataColumn("PF", typeof(string)));
      dt.Columns.Add(new DataColumn("ESI", typeof(string)));
      dt.Columns.Add(new DataColumn("LL_DETAIL", typeof(string)));
      dt.Columns.Add(new DataColumn("WO_FROM_DATE", typeof(string)));
      dt.Columns.Add(new DataColumn("WO_TO_DATE", typeof(string)));
      dt.Columns.Add(new DataColumn("Grievance", typeof(string)));
      dt.Columns.Add(new DataColumn("Gov_Notice", typeof(string)));
      dt.Columns.Add(new DataColumn("Grievance_Ref_No", typeof(string)));
      dt.Columns.Add(new DataColumn("Gov_Notice_Ref_No", typeof(string)));

      dt.Columns.Add(new DataColumn("Grievance_CreatedOn", typeof(string)));
      dt.Columns.Add(new DataColumn("Gov_Notice_CreatedOn", typeof(string)));


      for (k = 0; k < ds.Tables[0].Rows.Count; k++)
      {

          string vcode = ds.Tables[0].Rows[k]["V_CODE"].ToString();

          string V_name = ds.Tables[0].Rows[k]["V_Name"].ToString();

          string wo_no = ds.Tables[0].Rows[k]["WO_NO"].ToString();
          string str_startdate = ds.Tables[0].Rows[k]["START_DATE"].ToString();
          string str_enddate = ds.Tables[0].Rows[k]["END_DATE"].ToString();

          string start_date = str_startdate;
          string end_date = str_enddate;

          string start_mm1 = Convert.ToDateTime(start_date).ToString("MM");
          string start_yy1 = Convert.ToDateTime(start_date).ToString("yyyy");
          int start_mm = Convert.ToInt32(start_mm1);
          int start_yyyy = Convert.ToInt32(start_yy1);

          string end_mm1 = Convert.ToDateTime(end_date).ToString("MM");
          string end_yy1 = Convert.ToDateTime(end_date).ToString("yyyy");
          int end_mm = Convert.ToInt32(end_mm1);
          int end_yyyy = Convert.ToInt32(end_yy1);
          string procmonth = "";
         

          for (int i = start_yyyy; i <= end_yyyy; i++)
          {
              for (int j = 0; j <= 12; j++)
              {
                  DataRow dr = dt.NewRow();

                  string strprocmonth = "", strwage = "", strpf = "", stresi = "", strll = "", strGriv = "", strGovNo = "", str_Griv_refNo = "", str_GovNo_refNo = "", str_Griv_CreatedOn="", str_GovNo_CreatedOn="";

                  string temp = i.ToString() + start_mm.ToString().PadLeft(2, '0');
                  procmonth = procmonth + "," + temp;

                  SqlDataAdapter da2_gri = new SqlDataAdapter("  select * from App_Vendor_Grievance where  STATUS='OPEN' and ClosedOn is null and V_CODE ='" + vcode + "'  ", con);
                  DataSet ds2_gri = new DataSet();
                  da2_gri.SelectCommand.CommandTimeout = 300;
                  da2_gri.Fill(ds2_gri);
                  if (ds2_gri.Tables[0].Rows.Count > 0)
                  {
                      strGriv = "N";

                          //  SqlDataAdapter da2_gri_ref = new SqlDataAdapter("   (select top 1 ref_no = STUFF((SELECT DISTINCT ', ' + ref_no FROM App_Vendor_Grievance as b WHERE b.STATUS='OPEN' and b.ClosedOn is null and b.V_CODE ='" + vcode + "'     FOR XML PATH('')), 1, 2, '') FROM App_Vendor_Grievance as a)   ", con);
                        
                          SqlDataAdapter da2_gri_ref = new SqlDataAdapter("SELECT TOP 1 ref_no = STUFF((SELECT DISTINCT ', ' + ref_no FROM App_Vendor_Grievance AS b WHERE b.STATUS='OPEN' AND b.ClosedOn IS NULL AND b.V_CODE='" + vcode + "' FOR XML PATH('')),1,2,''), CreatedOn = (SELECT TOP 1 FORMAT(b.CreatedOn ,'dd-MM-yyyy') as CreatedOn  FROM App_Vendor_Grievance b WHERE b.STATUS='OPEN' AND b.ClosedOn IS NULL AND b.V_CODE='" + vcode + "' ORDER BY b.CreatedOn DESC) FROM App_Vendor_Grievance AS a;", con);

                          DataSet ds2_gri_ref = new DataSet();
                      da2_gri_ref.SelectCommand.CommandTimeout = 300;
                      da2_gri_ref.Fill(ds2_gri_ref);
                      if (ds2_gri_ref.Tables[0].Rows.Count > 0)
                      {
                          str_Griv_refNo = ds2_gri_ref.Tables[0].Rows[0]["ref_no"].ToString();
                          str_Griv_CreatedOn = ds2_gri_ref.Tables[0].Rows[0]["CreatedOn"].ToString();
                          }



                  }
                  else
                  {
                      strGriv = "Y";
                  }


                  SqlDataAdapter da2_gov = new SqlDataAdapter("  select * from App_Gov_Notice where  STATUS='OPEN' and ClosedOn is null and V_CODE ='" + vcode + "'  ", con);
                  DataSet ds2_gov = new DataSet();
                  da2_gov.SelectCommand.CommandTimeout = 300;
                  da2_gov.Fill(ds2_gov);
                  if (ds2_gov.Tables[0].Rows.Count > 0)
                  {
                      strGovNo = "N";

                          //SqlDataAdapter da2_gov_ref = new SqlDataAdapter("   (select top 1 ref_no = STUFF((SELECT DISTINCT ', ' + ref_no FROM App_Gov_Notice as b WHERE b.STATUS='OPEN' and b.ClosedOn is null and b.V_CODE ='" + vcode + "'     FOR XML PATH('')), 1, 2, '') FROM App_Gov_Notice as a)    ", con);

                         
                          SqlDataAdapter da2_gov_ref = new SqlDataAdapter("SELECT TOP 1 ref_no = STUFF((SELECT DISTINCT ', ' + ref_no FROM App_Gov_Notice AS b WHERE b.STATUS='OPEN' AND b.ClosedOn IS NULL AND b.V_CODE='" + vcode + "' FOR XML PATH('')),1,2,''), CreatedOn = (SELECT TOP 1  FORMAT(b.CreatedOn ,'dd-MM-yyyy') as CreatedOn  FROM App_Gov_Notice b WHERE b.STATUS='OPEN' AND b.ClosedOn IS NULL AND b.V_CODE='" + vcode + "' ORDER BY b.CreatedOn DESC) FROM App_Gov_Notice AS a;", con);
                          DataSet ds2_gov_ref = new DataSet();
                      da2_gov_ref.SelectCommand.CommandTimeout = 300;
                      da2_gov_ref.Fill(ds2_gov_ref);
                      if (ds2_gov_ref.Tables[0].Rows.Count > 0)
                      {
                          str_GovNo_refNo = ds2_gov_ref.Tables[0].Rows[0]["ref_no"].ToString();
                           str_GovNo_CreatedOn = ds2_gov_ref.Tables[0].Rows[0]["CreatedOn"].ToString();
                      }




                  }
                  else
                  {
                      strGovNo = "Y";
                  }



                  SqlDataAdapter da1 = new SqlDataAdapter("  select case when count(*)>0 then 'Y' else 'N' end as REC from APP_RECOGNIZED_WO where WO_NO='" + wo_no + "'  ", con);
                  DataSet ds1 = new DataSet();
                  da1.SelectCommand.CommandTimeout = 300;
                  da1.Fill(ds1);


                  if (ds1.Tables[0].Rows[0]["REC"].ToString() == "Y")
                  {
                      strprocmonth = temp;
                      strwage = "Y";
                      strpf = "Y";
                      stresi = "Y";
                      strll = "Y";

                

                      dr["CURR_MONTH"] = System.DateTime.Now;
                      dr["PROC_MONTH"] = temp;
                      dr["WO_NO"] = wo_no;
                      dr["V_CODE"] = vcode;

                      dr["V_Name"] = V_name;

                      dr["WAGES"] = strwage;
                      dr["PF"] = strpf;
                      dr["ESI"] = stresi;
                      dr["LL_DETAIL"] = strll;
                      dr["WO_FROM_DATE"] = str_startdate;
                      dr["WO_TO_DATE"] = str_enddate;
                      dr["Grievance"] = strGriv;
                      dr["Gov_Notice"] = strGovNo;
                      dr["Grievance_Ref_No"] = str_Griv_refNo;
                      dr["Gov_Notice_Ref_No"] = str_GovNo_refNo;

                      dr["Grievance_CreatedOn"] = str_Griv_CreatedOn;
                      dr["Gov_Notice_CreatedOn"] = str_GovNo_CreatedOn;

                      dt.Rows.Add(dr);
                     
                  }
                  else if (Convert.ToInt32(temp) >= Convert.ToInt32("202308"))
                  {
                     
                      SqlDataAdapter da2 = new SqlDataAdapter("  select '" + temp + "', WO_NO,( select top 1 case when count(*) >0 then 'Y' else  (      select top 1 case when count(*) >0 then 'Y' else 'N' end   from App_Online_Wages_Details_Supplement Supp_a1   inner join App_Online_WagesSupplement Supp_a2 on Supp_a2.V_CODE=Supp_a1.VendorCode and Supp_a2.PROC_MONTH =Supp_a1.PROC_MONTH and  Supp_a2.STATUS = 'Request Closed'   where Supp_a1.VendorCode='" + vcode + "' AND Supp_a1.WorkOrderNo='" + wo_no + "' and Supp_a1.PROC_MONTH ='" + temp + "'   )   end   from App_Online_Wages_Details a1   inner join App_Online_Wages a2 on a2.V_CODE=a1.VendorCode and a2.PROC_MONTH =a1.PROC_MONTH and  a2.STATUS = 'Request Closed'   where a1.VendorCode='" + vcode + "' AND a1.WorkOrderNo='" + wo_no + "' and a1.PROC_MONTH ='" + temp + "'  ) as wages,(select top 1 case when count(*) >0 then 'Y' else  ( select top 1 case when count(*) >0 then 'Y' else 'N' end  from App_PF_ESI_Details_Supplement supp_a1  inner join App_PF_ESI_Summary_Supplement supp_a2 on supp_a2.VendorCode=supp_a1.VendorCode and supp_a2.PROC_MONTH =supp_a1.PROC_MONTH and supp_a2.STATUS = 'Request Closed'  where supp_a1.VendorCode='" + vcode + "' AND supp_a1.WorkOrderNo='" + wo_no + "' and supp_a1.PROC_MONTH ='" + temp + "'  )         end   from App_PF_ESI_Details a1   inner join App_PF_ESI_Summary a2 on a2.VendorCode=a1.VendorCode and a2.PROC_MONTH =a1.PROC_MONTH and a2.STATUS = 'Request Closed'   where a1.VendorCode='" + vcode + "' AND a1.WorkOrderNo='" + wo_no + "' and a1.PROC_MONTH ='" + temp + "'  ) as PF, (select top 1 case when count(*) >0 then 'Y' else ( select top 1 case when count(*) >0 then 'Y' else 'N' end  from App_PF_ESI_Details_Supplement supp_a1  inner join App_PF_ESI_Summary_Supplement supp_a2 on supp_a2.VendorCode=supp_a1.VendorCode and supp_a2.PROC_MONTH =supp_a1.PROC_MONTH and supp_a2.STATUS = 'Request Closed'  where supp_a1.VendorCode='" + vcode + "' AND supp_a1.WorkOrderNo='" + wo_no + "' and supp_a1.PROC_MONTH ='" + temp + "'  )         end   from App_PF_ESI_Details a1   inner join App_PF_ESI_Summary a2 on a2.VendorCode=a1.VendorCode and a2.PROC_MONTH =a1.PROC_MONTH and a2.STATUS = 'Request Closed'   where a1.VendorCode='" + vcode + "' AND a1.WorkOrderNo='" + wo_no + "' and a1.PROC_MONTH ='" + temp + "'  ) as ESI,(select top 1 case when count(*) >0 then 'Y' else 'N' end from App_LabourLicenseSubmission where Vcode='" + vcode + "' and CONVERT(int, convert(varchar, DATEPART(YEAR,FromDate))+Convert(varchar,FORMAT(DATEPART(month,FromDate), '00')) ) <=" + temp + " and CONVERT(int, convert(varchar, DATEPART(YEAR,ToDate))+Convert(varchar,FORMAT(DATEPART(month,ToDate), '00')) )>=" + temp + " ) as LL from App_Vendorwodetails where WO_NO='" + wo_no + "'  ", con);
                      DataSet ds2 = new DataSet();
                      da2.SelectCommand.CommandTimeout = 300;
                      da2.Fill(ds2);
                      if (ds2.Tables[0].Rows.Count > 0)
                      {
                          strprocmonth = temp;
                          strwage = ds2.Tables[0].Rows[0]["wages"].ToString();
                          strpf = ds2.Tables[0].Rows[0]["PF"].ToString();
                          stresi = ds2.Tables[0].Rows[0]["ESI"].ToString();
                          strll = ds2.Tables[0].Rows[0]["LL"].ToString();

                          //new suraj 10-09-2025=========================
                          if (strll == "N")
                          {
                              SqlDataAdapter da_loc_other = new SqlDataAdapter("  select  case when count(*) >0 then 'Y' else 'N' end as LL from App_WorkOrder_Reg where wo_no ='" + wo_no+"' and LL_LOC='Others'  ", con);
                              DataSet ds_loc_other = new DataSet();
                              da_loc_other.Fill(ds_loc_other);
                              if (ds_loc_other.Tables[0].Rows.Count > 0)
                              {
                                  strll = ds_loc_other.Tables[0].Rows[0]["LL"].ToString();
                              }

                          }
                          //new suraj 10-09-2025



                          dr["CURR_MONTH"] = System.DateTime.Now;
                          dr["PROC_MONTH"] = temp;
                          dr["WO_NO"] = wo_no;
                          dr["V_CODE"] = vcode;
                          dr["V_Name"] = V_name;
                          dr["WAGES"] = strwage;
                          dr["PF"] = strpf;
                          dr["ESI"] = stresi;
                          dr["LL_DETAIL"] = strll;
                          dr["WO_FROM_DATE"] = str_startdate;
                          dr["WO_TO_DATE"] = str_enddate;
                          dr["Grievance"] = strGriv;
                          dr["Gov_Notice"] = strGovNo;
                          dr["Grievance_Ref_No"] = str_Griv_refNo;
                          dr["Gov_Notice_Ref_No"] = str_GovNo_refNo;

                              dr["Grievance_CreatedOn"] = str_Griv_CreatedOn;
                              dr["Gov_Notice_CreatedOn"] = str_GovNo_CreatedOn;

                              dt.Rows.Add(dr);


                      }

                  }
                  else if (Convert.ToInt32(temp) >= Convert.ToInt32("202101") && Convert.ToInt32(temp) < Convert.ToInt32("202308"))
                  {
                      SqlDataAdapter da3 = new SqlDataAdapter("  select '" + temp + "', WO_NO,(select case when count(wo_no)>0 then 'Y' else 'N' end from JCMS_ONLINE_TEMP_C_ENTRY_DETAILS where RIGHT(V_CODE, 5)='" + vcode + "' and WO_NO='" + wo_no + "' and PROC_MONTH='" + temp + "' and STATUS = 'Approved') as wages,(select case when count(wo_no)>0 then 'Y' else 'N' end from JCMS_TEMP_ONLINE_PF_ESI where RIGHT(V_CODE, 5)='" + vcode + "' and WO_NO='" + wo_no + "' and PROC_MONTH='" + temp + "' and STATUS = 'Approved') as pf,(select case when count(wo_no)>0 then 'Y' else 'N' end from JCMS_TEMP_ONLINE_PF_ESI where RIGHT(V_CODE, 5)='" + vcode + "' and WO_NO='" + wo_no + "' and PROC_MONTH='" + temp + "' and STATUS = 'Approved') as esi,(select top 1  case when count(*) >0 then 'Y' else 'N' end from JCMS_LL_DETAILS where RIGHT(V_CODE, 5)='" + vcode + "' and CONVERT(int, convert(varchar, DATEPART(YEAR,FROM_DATE))+Convert(varchar,FORMAT(DATEPART(month,FROM_DATE), '00')) ) <=" + temp + " and CONVERT(int, convert(varchar, DATEPART(YEAR,TO_DATE))+Convert(varchar,FORMAT(DATEPART(month,TO_DATE), '00')) )>=" + temp + " ) as LL from App_Vendorwodetails where WO_NO ='" + wo_no + "'  ", con);
                      DataSet ds3 = new DataSet();
                      da3.SelectCommand.CommandTimeout = 300;
                      da3.Fill(ds3);

                      if (ds3.Tables[0].Rows.Count > 0)
                      {
                          strprocmonth = temp;
                          strwage = ds3.Tables[0].Rows[0]["wages"].ToString();
                          strpf = ds3.Tables[0].Rows[0]["pf"].ToString();
                          stresi = ds3.Tables[0].Rows[0]["esi"].ToString();
                          strll = ds3.Tables[0].Rows[0]["LL"].ToString();
                          if (strll == "N")
                          {
                              SqlDataAdapter da4 = new SqlDataAdapter("  select LL= case when count(*) >0 then 'Y' else 'N' end from App_LabourLicenseSubmission where  Vcode='" + vcode + "'   and CONVERT(int, convert(varchar,DATEPART(YEAR,FROMDATE))+Convert(varchar,FORMAT(DATEPART(month,FROMDATE), '00')) ) <=" + temp + " and CONVERT(int, convert(varchar, DATEPART(YEAR,TODATE))+Convert(varchar,FORMAT(DATEPART(month,TODATE), '00')) )>=" + temp + "  ", con);
                              DataSet ds4 = new DataSet();
                              da4.Fill(ds4);
                              if (ds4.Tables[0].Rows.Count > 0)
                              {
                                  strll = ds4.Tables[0].Rows[0]["LL"].ToString();
                              }


                              if (strll == "N")
                              {
                                  SqlDataAdapter da_loc_before = new SqlDataAdapter("  select  case when count(*) >0 then 'Y' else 'N' end as LL from App_WorkOrder_Reg where wo_no ='" + wo_no + "' and LL_LOC='Others'  ", con);
                                  DataSet ds_loc_before = new DataSet();
                                  da_loc_before.Fill(ds_loc_before);
                                  if (ds_loc_before.Tables[0].Rows.Count > 0)
                                  {
                                      strll = ds_loc_before.Tables[0].Rows[0]["LL"].ToString();
                                  }
                              }

                          }

                          if (strwage == "N")
                          {
                              SqlDataAdapter da5 = new SqlDataAdapter("  select wages =  case when count(*)>0 then 'Y' else 'N' end from JCMS_C_ENTRY_DETAILS where WO_NO='" + wo_no + "' and PROC_MONTH='" + temp + "' and C_NO='4'  ", con);
                              DataSet ds5 = new DataSet();
                              da5.Fill(ds5);
                              if (ds5.Tables[0].Rows.Count > 0)
                              {
                                  strwage = ds5.Tables[0].Rows[0]["wages"].ToString();
                              }

                          }

                          if (strpf == "N")
                          {
                              SqlDataAdapter da6 = new SqlDataAdapter("  select pf =  case when count(*)>0 then 'Y' else 'N' end from JCMS_C_ENTRY_DETAILS where RIGHT(V_CODE, 5)='" + vcode + "' and PROC_MONTH='" + temp + "' and C_NO='5'   ", con);
                              DataSet ds6 = new DataSet();
                              da6.Fill(ds6);
                              if (ds6.Tables[0].Rows.Count > 0)
                              {
                                  strpf = ds6.Tables[0].Rows[0]["pf"].ToString();
                              }

                          }

                          if (stresi == "N")
                          {
                              SqlDataAdapter da7 = new SqlDataAdapter("  select esi =  case when count(*)>0 then 'Y' else 'N' end from JCMS_C_ENTRY_DETAILS where RIGHT(V_CODE, 5)='" + vcode + "' and PROC_MONTH='" + temp + "' and C_NO='6'   ", con);
                              DataSet ds7 = new DataSet();
                              da7.Fill(ds7);
                              if (ds7.Tables[0].Rows.Count > 0)
                              {
                                  stresi = ds7.Tables[0].Rows[0]["esi"].ToString();
                              }

                          }



                          dr["CURR_MONTH"] = System.DateTime.Now;
                          dr["PROC_MONTH"] = temp;
                          dr["WO_NO"] = wo_no;
                          dr["V_CODE"] = vcode;
                          dr["V_Name"] = V_name;
                          dr["WAGES"] = strwage;
                          dr["PF"] = strpf;
                          dr["ESI"] = stresi;
                          dr["LL_DETAIL"] = strll;
                          dr["WO_FROM_DATE"] = str_startdate;
                          dr["WO_TO_DATE"] = str_enddate;
                          dr["Grievance"] = strGriv;
                          dr["Gov_Notice"] = strGovNo;
                          dr["Grievance_Ref_No"] = str_Griv_refNo;
                          dr["Gov_Notice_Ref_No"] = str_GovNo_refNo;

                              dr["Grievance_CreatedOn"] = str_Griv_CreatedOn;
                              dr["Gov_Notice_CreatedOn"] = str_GovNo_CreatedOn;

                              dt.Rows.Add(dr);

                      }


                  }








                  if (temp == end_yy1 + end_mm1)
                  {
                      break;
                  }

                  if (start_mm >= 12)
                  {
                      start_mm = 1;
                      break;
                  }
                  else
                  {
                      start_mm++;
                  }


              }


          }

         


      }


          ds_final.Tables.Add(dt);



          result = ConvertDataTableToDictionaryList(ds_final.Tables[0]);

          //SqlCommand cmd_2 = new SqlCommand("insert into App_Api_Log (ID,RequestedOn,Requested_VendorCode,Requested_WorkOrder,Status,Remarks) values('" + Id + "','" + DateTime.Now + "','" + VendorCode + "','" + WorkOrderNo + "','Done','" + ds_final.Tables[0].Rows.Count.ToString() + " - successfully entered')   ", con);
          //cmd_2.CommandTimeout = 300;
          //cmd_2.ExecuteNonQuery();

      }
      catch (Exception ex)
      {
          // Update into App_Api_Log for status Error

          var comma = ex.Message.Replace("'", "_");



          SqlCommand cmd_2 = new SqlCommand("insert into App_Api_Log (ID,RequestedOn,Requested_VendorCode,Requested_WorkOrder,Status,Remarks) values('" + Id + "','" + DateTime.Now + "','" + VendorCode + "','" + WorkOrderNo + "','Error','" + comma.ToString() + "')   ", con);
          cmd_2.CommandTimeout = 300;
          cmd_2.ExecuteNonQuery();
      }
      finally
      {
          con.Close();
      }

      return result;

  }

