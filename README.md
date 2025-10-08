   protected void Block_unblock_SelectedIndexChanged(object sender, EventArgs e)
   {
       string Type = ((DropDownList)Vendor_Block_Unblock_RFQ_record.Rows[0].FindControl("Block_Unblock")).SelectedValue;
     
       Dictionary<string, object> ddlParams = new Dictionary<string, object>();
       ddlParams.Add("type", Type);

       GetDropdowns("RFQ_Reason", ddlParams);
       ((DropDownList)Vendor_Block_Unblock_RFQ_record.Rows[0].FindControl("Reason")).DataBind();
   }

after this condtion i want when user select unblock from dropdown it should be hide my this two fields- 
   <div class="form-group col-md-4 col-margin mb-1">
       <asp:Label for="Block_From" runat="server" CssClass="m-0 mr-2 p-0 col-form-label-sm col-sm-4 font-weight-bold fs-6"> From Date :</asp:Label>
       <asp:TextBox ID="Block_From" runat="server" autocomplete="off" CssClass="form-control form-control-sm col-sm-7"></asp:TextBox>
       <ask:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="TopRight" TargetControlID="Block_From" TodaysDateFormat="dd/MM/yyyy"></ask:CalendarExtender>
   </div>


   <div class="form-group col-md-4 col-margin mb-1">
       <asp:Label for="Block_To" runat="server" CssClass="m-0 mr-2 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6">To Date:</asp:Label>
       <asp:TextBox ID="Block_To" runat="server"  autocomplete="off"  CssClass="form-control form-control-sm col-sm-8"></asp:TextBox>
       <ask:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="TopRight" TargetControlID="Block_To" TodaysDateFormat="dd/MM/yyyy"></ask:CalendarExtender>
   </div>

