DataSet ds = new DataSet();
BL_Vendorwodetails blobj = new BL_Vendorwodetails();

string Loc_code = ((TextBox)vendor_wo_Record.Rows[0].FindControl("LOC_CODE")).Text;
string Service_Code = ((TextBox)vendor_wo_Record.Rows[0].FindControl("SERVICE_CODE")).Text;

ds = blobj.GetBocwApplicableAsPerMatrix(Loc_code, Service_Code);

// Value 1: from textbox
string bocw_chk = ((TextBox)vendor_wo_Record.Rows[0].FindControl("BOCW_APPLICABLE")).Text;

// Value 2: from dataset table in memory
string bocwAsPer = "";
if (PageRecordDataSet.Tables["App_WorkOrder_Reg"].Rows.Count > 0)
{
    bocwAsPer = Convert.ToString(PageRecordDataSet.Tables["App_WorkOrder_Reg"].Rows[0]["BOCW_APPLICABLE_AS_PER"]);
}

// Value 3: from query dataset returned (if it has data)
string bocwFromQuery = "";
if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
{
    bocwFromQuery = Convert.ToString(ds.Tables[0].Rows[0]["BOCW_APPLICABLE_AS_PER_MATRIX"]);
}

// --- Convert all to int safely ---
int valTextbox = 0, valAsPer = 0, valQuery = 0;
int.TryParse(bocw_chk, out valTextbox);
int.TryParse(bocwAsPer, out valAsPer);
int.TryParse(bocwFromQuery, out valQuery);

// --- Check if any of them = 2 ---
if (valTextbox == 2 || valAsPer == 2 || valQuery == 2)
{
    EnableBocwFields(true);
}
else
{
    EnableBocwFields(false);
}
