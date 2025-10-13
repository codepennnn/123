  protected void vendor_wo_Records_SelectedIndexChanged(object sender, EventArgs e)
  {
      GetRecord(vendor_wo_Records.SelectedDataKey.Value.ToString());
      //Dictionary<string, object> ddlParams = new Dictionary<string, object>();
      GetDropdowns("Locations");

      WO_ATTACH.Visible = true;
      //BOCW_ATTACH.Visible = true;
      //MV_ATTACH.Visible = true;
      btnSave.Visible = true;
      //CustomValidator19.Enabled = false;
      vendor_wo_Record.BindData();
      formvis.Visible = true;




      //Asif

      string bocw_chk = ((TextBox)vendor_wo_Record.Rows[0].FindControl("BOCW_APPLICABLE")).Text;


      string bocwAsPer = "";
      if (PageRecordDataSet.Tables["App_WorkOrder_Reg"].Rows.Count > 0)
      {
          bocwAsPer = Convert.ToString(PageRecordDataSet.Tables["App_WorkOrder_Reg"].Rows[0]["BOCW_APPLICABLE_AS_PER_CC"]);
      }



      int valTextbox = 0, valAsPer = 0;
      int.TryParse(bocw_chk, out valTextbox);
      int.TryParse(bocwAsPer, out valAsPer);



      if (valTextbox == 2 || valAsPer == 2)
      {
          ((TextBox)vendor_wo_Record.Rows[0].FindControl("REGIS_NUMBER")).Enabled = true;
          ((TextBox)vendor_wo_Record.Rows[0].FindControl("REGIS_DATE")).Enabled = true;
          ((TextBox)vendor_wo_Record.Rows[0].FindControl("REGIS_VALID_UPTO")).Enabled = true;
          ((TextBox)vendor_wo_Record.Rows[0].FindControl("NO_EMP_BOCW_OBTAINED")).Enabled = true;

          // CustomValidator19.Enabled = true;

      }
      else
      {
          ((TextBox)vendor_wo_Record.Rows[0].FindControl("REGIS_NUMBER")).Enabled = false;
          ((TextBox)vendor_wo_Record.Rows[0].FindControl("REGIS_DATE")).Enabled = false;
          ((TextBox)vendor_wo_Record.Rows[0].FindControl("REGIS_VALID_UPTO")).Enabled = false;
          ((TextBox)vendor_wo_Record.Rows[0].FindControl("NO_EMP_BOCW_OBTAINED")).Enabled = false;



      }








  }

