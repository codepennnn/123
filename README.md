protected void vendor_wo_Records_SelectedIndexChanged(object sender, EventArgs e)
{
    GetRecord(vendor_wo_Records.SelectedDataKey.Value.ToString());
    GetDropdowns("Locations");

    WO_ATTACH.Visible = true;
    btnSave.Visible = true;
    formvis.Visible = true;

    vendor_wo_Record.BindData(); // <-- bind grid first

    // ✅ Safety check before accessing rows
    if (vendor_wo_Record.Rows.Count == 0)
    {
        return; // No rows → nothing to do
    }

    // ✅ Now safely access controls
    TextBox txtBocwApplicable = (TextBox)vendor_wo_Record.Rows[0].FindControl("BOCW_APPLICABLE");
    TextBox txtRegisNumber = (TextBox)vendor_wo_Record.Rows[0].FindControl("REGIS_NUMBER");
    TextBox txtRegisDate = (TextBox)vendor_wo_Record.Rows[0].FindControl("REGIS_DATE");
    TextBox txtRegisValidUpto = (TextBox)vendor_wo_Record.Rows[0].FindControl("REGIS_VALID_UPTO");
    TextBox txtNoEmp = (TextBox)vendor_wo_Record.Rows[0].FindControl("NO_EMP_BOCW_OBTAINED");

    string bocw_chk = txtBocwApplicable != null ? txtBocwApplicable.Text : "0";

    string bocwAsPer = "0";
    if (PageRecordDataSet != null &&
        PageRecordDataSet.Tables.Contains("App_WorkOrder_Reg") &&
        PageRecordDataSet.Tables["App_WorkOrder_Reg"].Rows.Count > 0)
    {
        bocwAsPer = Convert.ToString(PageRecordDataSet.Tables["App_WorkOrder_Reg"].Rows[0]["BOCW_APPLICABLE_AS_PER_CC"]);
    }

    int valTextbox = 0, valAsPer = 0;
    int.TryParse(bocw_chk, out valTextbox);
    int.TryParse(bocwAsPer, out valAsPer);

    bool enableFields = (valTextbox == 2 || valAsPer == 2);

    // ✅ Safely enable/disable fields
    if (txtRegisNumber != null) txtRegisNumber.Enabled = enableFields;
    if (txtRegisDate != null) txtRegisDate.Enabled = enableFields;
    if (txtRegisValidUpto != null) txtRegisValidUpto.Enabled = enableFields;
    if (txtNoEmp != null) txtNoEmp.Enabled = enableFields;

    // Optionally enable validator
    // CustomValidator19.Enabled = enableFields;
}
