protected void vendor_wo_Records_SelectedIndexChanged(object sender, EventArgs e)
{
    GetRecord(vendor_wo_Records.SelectedDataKey.Value.ToString());
    GetDropdowns("Locations");

    WO_ATTACH.Visible = true;
    btnSave.Visible = true;
    vendor_wo_Record.BindData();
    formvis.Visible = true;

    // Safety check
    if (vendor_wo_Record.Rows.Count == 0) return;

    // --- Get BOCW_APPLICABLE textbox safely ---
    TextBox txtBocwApplicable = vendor_wo_Record.Rows[0].FindControl("BOCW_APPLICABLE") as TextBox;
    if (txtBocwApplicable == null) return;

    string bocw_chk = txtBocwApplicable.Text;
    Session["bocw"] = bocw_chk;

    string bocwAsPer = "";
    if (PageRecordDataSet.Tables["App_WorkOrder_Reg"].Rows.Count > 0)
    {
        bocwAsPer = Convert.ToString(PageRecordDataSet.Tables["App_WorkOrder_Reg"].Rows[0]["BOCW_APPLICABLE_AS_PER_CC"]);
    }

    int valTextbox = 0, valAsPer = 0;
    int.TryParse(bocw_chk, out valTextbox);
    int.TryParse(bocwAsPer, out valAsPer);

    bool enableFields = (valTextbox == 2 || valAsPer == 2);

    // --- Safely find and enable/disable all controls ---
    DropDownList ddlRegNo = vendor_wo_Record.Rows[0].FindControl("REGIS_NUMBER") as DropDownList;
    TextBox txtRegDate = vendor_wo_Record.Rows[0].FindControl("REGIS_DATE") as TextBox;
    TextBox txtRegValidUpto = vendor_wo_Record.Rows[0].FindControl("REGIS_VALID_UPTO") as TextBox;
    TextBox txtNoEmp = vendor_wo_Record.Rows[0].FindControl("NO_EMP_BOCW_OBTAINED") as TextBox;

    if (ddlRegNo != null) ddlRegNo.Enabled = enableFields;
    if (txtRegDate != null) txtRegDate.Enabled = enableFields;
    if (txtRegValidUpto != null) txtRegValidUpto.Enabled = enableFields;
    if (txtNoEmp != null) txtNoEmp.Enabled = enableFields;
}
