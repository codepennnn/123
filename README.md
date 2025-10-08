
                                    <div runat="server" id="blockFromDiv" class="form-group col-md-4 col-margin mb-1">
                                        <asp:Label for="Block_From" runat="server" CssClass="m-0 mr-2 p-0 col-form-label-sm col-sm-4 font-weight-bold fs-6"> From Date :</asp:Label>
                                        <asp:TextBox ID="Block_From" runat="server" autocomplete="off" CssClass="form-control form-control-sm col-sm-7"></asp:TextBox>
                                        <ask:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="TopRight" TargetControlID="Block_From" TodaysDateFormat="dd/MM/yyyy"></ask:CalendarExtender>
                                    </div>


                                    <div  runat="server" id="blockToDiv" class="form-group col-md-4 col-margin mb-1">
                                        <asp:Label for="Block_To" runat="server" CssClass="m-0 mr-2 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6">To Date:</asp:Label>
                                        <asp:TextBox ID="Block_To" runat="server"  autocomplete="off"  CssClass="form-control form-control-sm col-sm-8"></asp:TextBox>
                                        <ask:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" PopupPosition="TopRight" TargetControlID="Block_To" TodaysDateFormat="dd/MM/yyyy"></ask:CalendarExtender>
                                    </div>


  if (Type == "RFQ_U")
  {
      ((TextBox)Vendor_Block_Unblock_RFQ_record.Rows[0].FindControl("Block_From")).Visible = false;
      ((TextBox)Vendor_Block_Unblock_RFQ_record.Rows[0].FindControl("Block_To")).Visible = false;


  }
  else
  {

      ((TextBox)Vendor_Block_Unblock_RFQ_record.Rows[0].FindControl("Block_From")).Visible = true;
      ((TextBox)Vendor_Block_Unblock_RFQ_record.Rows[0].FindControl("Block_To")).Visible = true;
  }
