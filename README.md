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

          if (vendor_wo_Record.Rows.Count == 0) return;

          bocw_chk = ((TextBox)vendor_wo_Record.Rows[0].FindControl("BOCW_APPLICABLE")).Text;
          Session["bocw"] = bocw_chk;


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
              ((DropDownList)vendor_wo_Record.Rows[0].FindControl("REGIS_NUMBER")).Enabled = false;
              ((TextBox)vendor_wo_Record.Rows[0].FindControl("REGIS_DATE")).Enabled = false;
              ((TextBox)vendor_wo_Record.Rows[0].FindControl("REGIS_VALID_UPTO")).Enabled = false;
              ((TextBox)vendor_wo_Record.Rows[0].FindControl("NO_EMP_BOCW_OBTAINED")).Enabled = false;



          }





     <div class="col-lg-4 mb-1 form-row">
         <div class="col-lg-5">
             <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6">Bocw Applicable Number :<span class="text-danger"></span></label>
         </div>
         <div class="col-lg-7">
             <asp:TextBox ID="BOCW_APPLICABLE" Enabled="false" runat="server" CssClass="form-control form-control-sm textboxstyle" MaxLength="3" />
             <asp:CustomValidator ID="CustomValidator19" ErrorMessage="Required" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="BOCW_APPLICABLE" ValidateEmptyText="true" BackColor="Wheat" ForeColor="Red"></asp:CustomValidator>
         </div>
     </div>


     <div class="col-lg-4 mb-1 form-row">
         <div class="col-lg-5">
             <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6">Bocw Registration Number :<span class="text-danger"></span></label>
         </div>
         <div class="col-lg-7">
          
             <asp:DropDownList ID="REGIS_NUMBER" runat="server" CssClass="form-control form-control-sm"
                 DataSource="<%# PageDDLDataset %>" DataMember="BOCW_Reg_No"
                 DataTextField="BOCW_RegNo" DataValueField="BOCW_RegNo"
                 OnSelectedIndexChanged="REGIS_NUMBER_SelectedIndexChanged"
                 AutoPostBack="true">
             </asp:DropDownList>
              <asp:CustomValidator ID="CustomValidator001" ErrorMessage="Required" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="REGIS_NUMBER" ValidateEmptyText="true" BackColor="Wheat" ForeColor="Red"></asp:CustomValidator>
         </div>
     </div>

     <div class="col-lg-4 mb-1 form-row">
         <div class="col-lg-5">
             <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6">Bocw Registration Date :<span class="text-danger"></span></label>
         </div>
         <div class="col-lg-7">
             <asp:TextBox ID="REGIS_DATE" runat="server" CssClass="form-control form-control-sm textboxstyle" Enabled="false" AutoComplete="off" />
              <asp:CustomValidator ID="CustomValidator002" ErrorMessage="Required" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="REGIS_DATE" ValidateEmptyText="true" BackColor="Wheat" ForeColor="Red"></asp:CustomValidator>
         </div>
     </div>

     <div class="col-lg-4 mb-1 form-row">
         <div class="col-lg-5">
             <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6">Bocw Registration Valid Upto :<span class="text-danger"></span></label>
         </div>
         <div class="col-lg-7">
             <asp:TextBox ID="REGIS_VALID_UPTO" runat="server" CssClass="form-control form-control-sm textboxstyle" Enabled="false" AutoComplete="off" />
            
         </div>
     </div>

     <div class="col-lg-4 mb-1 form-row">
         <div class="col-lg-5">
             <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6">Number of Employee for which Bocw obtained :<span class="text-danger"></span></label>
         </div>
         <div class="col-lg-7">
             <asp:TextBox ID="NO_EMP_BOCW_OBTAINED" runat="server" CssClass="form-control form-control-sm textboxstyle" Enabled="false" TextMode="Number" placeholder="only Numbers" />
           
         </div>
     </div>


  <cc1:FormCointainer ID="vendor_wo_Record" runat="server" AllowPaging="True" AutoGenerateColumns="False"
      PageSize="1" ShowHeader="False" Width="100%" DataSource="<%# PageRecordDataSet %>" OnRowDataBound="vendor_wo_Record_RowDataBound"
      DataMember="App_WorkOrder_Reg" DataKeyNames="ID" OnNewRowCreatingEvent="vendor_wo_Record_NewRowCreatingEvent"
      BindingErrorMessage="aaaaaaaa" GridLines="None" BorderStyle="Solid" BorderWidth="1px" BorderColor="#bfbebe" OnPreRender="vendor_wo_Record_PreRender">
 <div class="col-lg-4 mb-1 form-row">
     <div class="col-lg-5">
         <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6">BOCW APPLICABLE AS PER CC :<span class="text-danger"></span></label>
     </div>
     <div class="col-lg-7">
         <asp:TextBox ID="BOCW_APPLICABLE_AS_PER_CC" runat="server" CssClass="form-control form-control-sm textboxstyle" Enabled="false"  />

     </div>
 </div>






      }

