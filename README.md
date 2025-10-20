


        <div class="col-lg-4 mb-1 form-row">
        <div class="col-lg-5">
            <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" > Total Base Amount :<span class="text-danger">*</span></label>
        </div>
        <div class="col-lg-7">
            <asp:TextBox ID="Total_Base_Amount" runat="server" CssClass="form-control form-control-sm textboxstyle" readonly="true"    />
       
            <asp:CustomValidator ID="CustomValidator15" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Total_Base_Amount" ValidateEmptyText="true"></asp:CustomValidator>
             
        </div>
       
    </div>

    <div class="col-lg-4 mb-1 form-row">
        <div class="col-lg-5">
            <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Cess Amount to be paid :<span class="text-danger">*</span></label>
        </div>
        <div class="col-lg-7">
            <asp:TextBox ID="Cess_Amount" runat="server" CssClass="form-control form-control-sm textboxstyle" readonly="true"   />
            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Cess_Amount" ValidateEmptyText="true"></asp:CustomValidator>
        </div>
    </div>




     protected void btnSave_Click(object sender, EventArgs e)
 {
     bocw_summary.BindData();
     bocw_tab_records.UnbindData();
     bocw_summary.UnbindData();

     
     PageRecordDataSet.Tables["App_BOCW_SAP_to_Local"].AcceptChanges();


     TextBox txtTotalBase = (TextBox)bocw_summary.Rows[0].FindControl("Total_Base_Amount");
     TextBox cess = (TextBox)bocw_summary.Rows[0].FindControl("Cess_Amount");


     Double.TryParse(txtTotalBase.Text, out double totalBase);
     Double.TryParse(cess.Text, out double cessAmt);



     var summary = PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0];
     summary["Total_Base_Amount"] = totalBase;
     summary["Cess_Amount"] = cessAmt;





     for (int i = 0; i < PageRecordDataSet.Tables["App_BOCW_Details"].Rows.Count; i++)
     {
         PageRecordDataSet.Tables["App_BOCW_Details"].Rows[i]["CreatedOn"] = System.DateTime.Now;
         PageRecordDataSet.Tables["App_BOCW_Details"].Rows[i]["CreatedBy"] = Session["UserName"].ToString();
         PageRecordDataSet.Tables["App_BOCW_Details"].Rows[i]["Master_Id"] = PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Id"].ToString();
         PageRecordDataSet.Tables["App_BOCW_Details"].Rows[i].AcceptChanges();
         PageRecordDataSet.Tables["App_BOCW_Details"].Rows[i].SetAdded();
     }
     
     PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Status"] = "Pending with CC";
     
     PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Cess_Amount_Balance"] = Convert.ToDouble(PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Cess_Amount"])-Convert.ToDouble(PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Cess_Amount_Paid"]);
     string a_d = ((DropDownList)bocw_summary.Rows[0].FindControl("Agree_Disagree")).SelectedValue;
     if (a_d == "NO")
     {
         PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Subjective_balance"] = Convert.ToDouble(PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Subjective_cess_amount"]) - Convert.ToDouble(PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Cess_Amount_Paid"]);
     }
     else
     {
         PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Subjective_balance"] = 0.000;
         PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Subjective_cess_amount"] = 0.000;
         PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Subjective_base_amount"] = 0.000;
     }
     PageRecordDataSet.Tables["App_BOCW_SAP_to_Local"].AcceptChanges();




     if (Payment_Attach_Name.HasFile)
     {
         string getFileName = "";
         List<string> FileList = new List<string>();
         foreach (HttpPostedFile htfiles in Payment_Attach_Name.PostedFiles)
         {
             getFileName = PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["ID"].ToString() + "_" + Path.GetFileName(htfiles.FileName);
             getFileName = Regex.Replace(getFileName, @"[,+*/?|><&=\#%:;@[^$?:'()!~}{`]", "");
             htfiles.SaveAs((@"D:/Cybersoft_Doc/CLMS/Attachments/" + getFileName));

             FileList.Add(getFileName);
             getFileName = "";
         }
         PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Payment_Attach_Name"] = string.Join(",", FileList);
     }


     if (Other_attach_name.HasFile)
     {
         string getFileName = "";
         List<string> FileList = new List<string>();
         foreach (HttpPostedFile htfiles in Other_attach_name.PostedFiles)
         {
             getFileName = PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["ID"].ToString() + "_" + Path.GetFileName(htfiles.FileName);
             getFileName = Regex.Replace(getFileName, @"[,+*/?|><&=\#%:;@[^$?:'()!~}{`]", "");
             htfiles.SaveAs((@"D:/Cybersoft_Doc/CLMS/Attachments/" + getFileName));

             FileList.Add(getFileName);
             getFileName = "";
         }
         PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Other_attach_name"] = string.Join(",", FileList);
     }


     bool result = Save();
         if (result)
         {
             //MyMsgBox.show(MRPS.Control.MyMsgBox.MessageType.Success, "Data Save Successfully !");
             //GetRecords(GetFilterCondition(), vendor_reg_Records.PageSize, 0, "");
             //vendor_reg_Records.BindData();
             PageRecordDataSet.Clear();
             bocw_tab_records.BindData();
             WorkOder_wise_records.BindData();
             //Test_Form_Record.BindData();

             btnSave.Visible = false;

             MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Success, "Record saved successfully !");
         }
         else
         {
             MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors, "Error while saving !");

         }
 }


its saving Total_Base_Amount = 25000 while in my UI screen its showing 100000. similarly for cess amount why this happend



--------------------------------------------------------------------------
// 🔹 Create lists to track totals and selected records
List<string> selectedWorkOrders = new List<string>();
double totalCheckedAmount = 0;

// 🔹 Loop through every row in your grid
foreach (GridViewRow row in WorkOder_wise_records.Rows)
{
    CheckBox chk = (CheckBox)row.FindControl("chkSelect");
    HiddenField hfKONNR = (HiddenField)row.FindControl("hfKONNR");
    HiddenField hfAmount = (HiddenField)row.FindControl("hfAmount");

    // Defensive null checks
    if (hfKONNR == null || hfAmount == null || chk == null)
        continue;

    string konnr = hfKONNR.Value;
    double amount = 0;
    Double.TryParse(hfAmount.Value, out amount);
    bool isChecked = chk.Checked;

    // 🔹 Find if this work order already exists in dataset
    DataRow[] existing = PageRecordDataSet.Tables["App_BOCW_Details"].Select("KONNR = '" + konnr + "'");
    DataRow rowData;

    if (existing.Length > 0)
    {
        // Update existing
        rowData = existing[0];
        rowData["Checked"] = isChecked;
        rowData["Amount"] = amount;
        rowData["CreatedOn"] = DateTime.Now;
        rowData["CreatedBy"] = Session["UserName"].ToString();
    }
    else
    {
        // Add new row if not exists
        rowData = PageRecordDataSet.Tables["App_BOCW_Details"].NewRow();
        rowData["KONNR"] = konnr;
        rowData["Amount"] = amount;
        rowData["Checked"] = isChecked;
        rowData["CreatedOn"] = DateTime.Now;
        rowData["CreatedBy"] = Session["UserName"].ToString();
        rowData["Master_Id"] = PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Id"].ToString();
        PageRecordDataSet.Tables["App_BOCW_Details"].Rows.Add(rowData);
    }

    // 🔹 Totals — only count checked rows
    if (isChecked)
    {
        totalCheckedAmount += amount;
        selectedWorkOrders.Add(konnr);
    }
}

// 🔹 Update summary totals
var summary = PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0];
summary["Selected_WorkOrders"] = string.Join(",", selectedWorkOrders);
summary["Total_Base_Amount"] = totalCheckedAmount;
summary["Cess_Amount"] = totalCheckedAmount * 0.01; // example: 1% cess
summary["Status"] = "Pending with CC";

// 🔹 Accept all changes before final save
PageRecordDataSet.Tables["App_BOCW_Details"].AcceptChanges();
PageRecordDataSet.Tables["App_BOCW_Summary"].AcceptChanges();
