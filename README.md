   DataSet ds = new DataSet();
   BL_Vendorwodetails blobj = new BL_Vendorwodetails();


   string Loc_code = ((TextBox)vendor_wo_Record.Rows[0].FindControl("LOC_CODE")).Text;
   string Service_Code = ((TextBox)vendor_wo_Record.Rows[0].FindControl("SERVICE_CODE")).Text;


   ds = blobj.GetBocwApplicableAsPerMatrix(Loc_code, Service_Code);

   bocw_chk = ((TextBox)vendor_wo_Record.Rows[0].FindControl("BOCW_APPLICABLE")).Text;

   PageRecordDataSet.Tables["App_WorkOrder_Reg"].Rows[0]["BOCW_APPLICABLE_AS_PER"];

