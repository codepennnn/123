       DataSet ds1 = new DataSet();
       BL_App_Bocw_registration blobj_1 = new BL_App_Bocw_registration();
       ds1 = blobj_1.Get_tab2_detail(from, to, Session["UserName"].ToString());
       if (ds1 != null && ds1.Tables["App_BOCW_SAP_to_Local"].Rows.Count > 0)
       {
           PageRecordDataSet.Tables["App_BOCW_SAP_to_Local"].Rows.Clear();
           PageRecordDataSet.Merge(ds1);
       }

       DataSet ds2 = new DataSet();
       BL_App_Bocw_registration blobj_2 = new BL_App_Bocw_registration();
       ds2 = blobj_2.Get_from_detail(from, to, Session["UserName"].ToString());
       if (ds2 != null && ds2.Tables["App_BOCW_SAP_to_Local"].Rows.Count > 0)
       {
           if (PageRecordDataSet.Tables["App_BOCW_Summary"].Rows.Count > 0)
           {
               PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Total_Base_Amount"] = ds2.Tables["App_BOCW_SAP_to_Local"].Rows[0]["RMWWR"].ToString();
               PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Cess_Amount"] = (Convert.ToDouble(ds2.Tables["App_BOCW_SAP_to_Local"].Rows[0]["RMWWR"]) * 1) / 100;
               PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Period_from"] = from;
               PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Period_to"] = to;
               PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0]["Period_year"] = year;
           }


       }



           public DataSet Get_tab2_detail(string from, string to, string v_code)
    {
        string strSQL = " select  KONNR,sum(RMWWR) as RMWWR,sum(WMWST1) as WMWST1  from App_BOCW_SAP_to_Local where BUDAT between @BUDAT and '" + to + "' and LIFNR=@LIFNR  group by KONNR ";
        Dictionary<string, object> objParam = new Dictionary<string, object>();
        objParam.Add("BUDAT", from);
        // objParam.Add("BUDAT", to);
        objParam.Add("LIFNR", "00000" + v_code);
        DataHelper dh = new DataHelper();
        return dh.GetDataset(strSQL, "App_BOCW_SAP_to_Local", objParam);
    }


       public DataSet Get_from_detail(string from, string to, string v_code)
   {
       string strSQL = " select sum(RMWWR) as RMWWR  from  App_BOCW_SAP_to_Local where BUDAT between @BUDAT and '" + to + "' and LIFNR=@LIFNR  ";
       Dictionary<string, object> objParam = new Dictionary<string, object>();
       objParam.Add("BUDAT", from);
       // objParam.Add("BUDAT", to);
       objParam.Add("LIFNR", "00000" + v_code);
       DataHelper dh = new DataHelper();
       return dh.GetDataset(strSQL, "App_BOCW_SAP_to_Local", objParam);
   }


    <cc1:detailscontainer ID="WorkOder_wise_records" runat="server" AutoGenerateColumns="False" 
       AllowPaging="true" CellPadding="4" GridLines="None" Width="100%" DataMember="App_BOCW_SAP_to_Local"
        DataSource="<%# PageRecordDataSet %>" 
       ForeColor="#333333" ShowHeaderWhenEmpty="True" OnPageIndexChanging="WorkOder_wise_records_PageIndexChanging"
       
        PageSize="10" PagerSettings-Visible="True" PagerStyle-HorizontalAlign="Center" 
 PagerStyle-Wrap="False" HeaderStyle-Font-Size="Smaller" RowStyle-Font-Size="Smaller" >
       <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
       <Columns>
          <%-- <asp:TemplateField HeaderText="ID" SortExpression="ID" Visible="False">
               <ItemTemplate>
                   <asp:Label ID="ID" runat="server"></asp:Label>
               </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="WORK ORDER" 
               SortExpression="MANDT">
               <ItemTemplate>
                 <B> <asp:LinkButton  ID="LinkButton1" runat="server" CommandName="select" Text='<%# Bind("V_CODE") %>'></asp:LinkButton> </B>
               </ItemTemplate>
               <HeaderStyle HorizontalAlign="Left" />
           </asp:TemplateField>--%>



               <asp:TemplateField  HeaderText="Sl No" SortExpression="SlNo" HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
           <ItemTemplate >
               <asp:Label ID="Slno" runat="server" Text='<%#Container.DataItemIndex+1 %>' CssClass="form-label"></asp:Label>
           </ItemTemplate>
       </asp:TemplateField>


            <asp:BoundField DataField="KONNR" HeaderText="WorkOrder No." 
                   SortExpression="KONNR" HeaderStyle-HorizontalAlign="Left" 
                   ItemStyle-HorizontalAlign="Left">
               <HeaderStyle HorizontalAlign="Left" />
               <ItemStyle HorizontalAlign="Left" />
           </asp:BoundField>

           <asp:BoundField DataField="RMWWR" HeaderText="Total Base Amount" 
                   SortExpression="RMWWR" HeaderStyle-HorizontalAlign="Left" 
                   ItemStyle-HorizontalAlign="Left">
               <HeaderStyle HorizontalAlign="Left" />
               <ItemStyle HorizontalAlign="Left" />
           </asp:BoundField>

           <asp:BoundField DataField="WMWST1" HeaderText="Total Tax (CGST+SGST)" 
                   SortExpression="WMWST1" HeaderStyle-HorizontalAlign="Left" 
                   ItemStyle-HorizontalAlign="Left">
               <HeaderStyle HorizontalAlign="Left" />
               <ItemStyle HorizontalAlign="Left" />
           </asp:BoundField>
          
         <%--  <asp:BoundField DataField="SL_NO" HeaderText="Total" 
                   SortExpression="SL_NO" HeaderStyle-HorizontalAlign="Left" 
                   ItemStyle-HorizontalAlign="Left">
               <HeaderStyle HorizontalAlign="Left" />
               <ItemStyle HorizontalAlign="Left" />
           </asp:BoundField>--%>

              

                 <asp:TemplateField HeaderStyle-Width="3%" HeaderText="" HeaderStyle-HorizontalAlign="Left">
     <HeaderTemplate>
         <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" ToolTip="Select All" />
     </HeaderTemplate>

     <ItemTemplate>
         <asp:CheckBox ID="chkSelect" runat="server" />
     </ItemTemplate>
                           
 </asp:TemplateField>

        

       </Columns>    
       <EditRowStyle BackColor="#999999" />
       <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
       <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
       <PagerSettings Mode="Numeric" />
       <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Font-Bold="True"  CssClass="pager1" />
       <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
       <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
       <SelectedRowStyle BackColor="#E2DED6" Font-Bold="False" ForeColor="#333333" />
       <SortedAscendingCellStyle BackColor="#E9E7E2" />
       <SortedAscendingHeaderStyle BackColor="#506C8C" />
       <SortedDescendingCellStyle BackColor="#FFFDF8" />
       <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
   </cc1:detailscontainer>


                                               <cc1:formcointainer ID="bocw_summary" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    PageSize="1" ShowHeader="False" Width="100%" DataSource="<%# PageRecordDataSet %>" OnPreRender="bocw_summary_PreRender"
                    DataMember="App_BOCW_Summary" DataKeyNames="ID"  OnNewRowCreatingEvent="bocw_summary_NewRowCreatingEvent"              
                    BindingErrorMessage="aaaaaaaa" GridLines="None"  BorderStyle="Solid" BorderWidth="1px" BorderColor="#bfbebe" >
                    <Columns>
                        <asp:TemplateField SortExpression="ID" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="ID" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>


    <div class="row">

         <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" style="color:red" >Do you agree with the above data:-  <span class="text-danger">*</span></label>
            </div>
            <div class="col-lg-7">

                <asp:DropDownList ID="Agree_Disagree" runat="server"  CssClass="form-control form-control-sm col-sm-8" OnSelectedIndexChanged="Agree_Disagree_SelectedIndexChanged" AutoPostBack="true"     >
                                <%--<asp:ListItem Text="Select" Value="" />--%>
                                        <asp:ListItem Text="YES" Value="YES" />
                                        <asp:ListItem Text="NO" Value="NO" />
                                        
                                       
                        </asp:DropDownList>


                <%--<asp:TextBox ID="Pay_mode" runat="server" CssClass="form-control form-control-sm textboxstyle"   />--%>
                <asp:CustomValidator ID="CustomValidator13" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Agree_Disagree" ValidateEmptyText="true"></asp:CustomValidator>
            </div>
        </div>

        <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Subjective Total Base Amount :<span class="text-danger">*</span></label>
            </div>
            <div class="col-lg-7">
                <asp:TextBox ID="Subjective_base_amount" runat="server" CssClass="form-control form-control-sm textboxstyle" Enabled="false"  onchange="one_percent()"  />
           
                <asp:CustomValidator ID="CustomValidator14" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Subjective_base_amount" ValidateEmptyText="true"></asp:CustomValidator>
                 
            </div>
           
        </div>


         




        <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Subjective Cess Amount to be paid(@ 1%) :<span class="text-danger">*</span></label>
            </div>
            <div class="col-lg-7">
                <asp:TextBox ID="Subjective_cess_amount" runat="server" CssClass="form-control form-control-sm textboxstyle" Enabled="false"   />
           
                <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Subjective_cess_amount" ValidateEmptyText="true"></asp:CustomValidator>
                 
            </div>
           
        </div>


         <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Subjective Balance value :<span class="text-danger">*</span></label>
            </div>
            <div class="col-lg-7">
                <asp:TextBox ID="Subjective_balance" runat="server" CssClass="form-control form-control-sm textboxstyle"  Enabled="false"   />
           
                <asp:CustomValidator ID="CustomValidator16" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Subjective_balance" ValidateEmptyText="true"></asp:CustomValidator>
                 
            </div>
           
        </div>

        <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Subjective Previous Outstanding :<span class="text-danger">*</span></label>
            </div>
            <div class="col-lg-7">
                <asp:TextBox ID="Subjective_Previous_outstanding" runat="server" CssClass="form-control form-control-sm textboxstyle"  Enabled="false"   />
           
                <asp:CustomValidator ID="CustomValidator17" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Subjective_Previous_outstanding" ValidateEmptyText="true"></asp:CustomValidator>
                 
            </div>
           
        </div>




        </div>
            <div class="row m">
                <br />
            </div>
        <div class="row">

            <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" > Total Base Amount :<span class="text-danger">*</span></label>
            </div>
            <div class="col-lg-7">
                <asp:TextBox ID="Total_Base_Amount" runat="server" CssClass="form-control form-control-sm textboxstyle" Enabled="false"   />
           
                <asp:CustomValidator ID="CustomValidator15" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Total_Base_Amount" ValidateEmptyText="true"></asp:CustomValidator>
                 
            </div>
           
        </div>

        <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Cess Amount to be paid :<span class="text-danger">*</span></label>
            </div>
            <div class="col-lg-7">
                <asp:TextBox ID="Cess_Amount" runat="server" CssClass="form-control form-control-sm textboxstyle" Enabled="false"   />
                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Cess_Amount" ValidateEmptyText="true"></asp:CustomValidator>
            </div>
        </div>

        <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Transaction ID :<span class="text-danger">*</span></label>
            </div>
            <div class="col-lg-7">
                <asp:TextBox ID="Transaction_Id" runat="server" CssClass="form-control form-control-sm textboxstyle"    />
                <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Transaction_Id" ValidateEmptyText="true"></asp:CustomValidator>
            </div>
        </div>

        <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Transaction date :<span class="text-danger">*</span></label>
            </div>
            <div class="col-lg-7">
                <asp:TextBox ID="Transaction_Date" runat="server" CssClass="form-control form-control-sm textboxstyle"   />
                <ask:CalendarExtender ID="CalendarExtender32" runat="server" Enabled="True"  Format="dd/MM/yyyy"    TargetControlID="Transaction_Date" TodaysDateFormat="dd/MM/yyyy" ></ask:CalendarExtender>
                <asp:CustomValidator ID="CustomValidator4" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Transaction_Date" ValidateEmptyText="true"></asp:CustomValidator>
            </div>
        </div>

        <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Account No. :<span class="text-danger">*</span></label>
            </div>
            <div class="col-lg-7">
                <asp:TextBox ID="Account_number" runat="server" CssClass="form-control form-control-sm textboxstyle"   />
                <asp:CustomValidator ID="CustomValidator5" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Account_number" ValidateEmptyText="true"></asp:CustomValidator>
            </div>
        </div>

        <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Customer ID / PanCard :<span class="text-danger">*</span></label>
            </div>
            <div class="col-lg-7">
                <asp:TextBox ID="Customer_Id" runat="server" CssClass="form-control form-control-sm textboxstyle"    />
                <asp:CustomValidator ID="CustomValidator6" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Customer_Id" ValidateEmptyText="true"></asp:CustomValidator>
            </div>
        </div>

            <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Bank Name :<span class="text-danger">*</span></label>
            </div>
            <div class="col-lg-7">
                <asp:TextBox ID="Bank_Name" runat="server" CssClass="form-control form-control-sm textboxstyle"    />
                <asp:CustomValidator ID="CustomValidator19" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Bank_Name" ValidateEmptyText="true"></asp:CustomValidator>
            </div>
        </div>
        
        <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Branch :<span class="text-danger">*</span></label>
            </div>
            <div class="col-lg-7">
                <asp:TextBox ID="Branch" runat="server" CssClass="form-control form-control-sm textboxstyle"    />
                <asp:CustomValidator ID="CustomValidator7" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Branch" ValidateEmptyText="true"></asp:CustomValidator>
            </div>
        </div>

        <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Pay Mode :<span class="text-danger">*</span></label>
            </div>
            <div class="col-lg-7">

                <asp:DropDownList ID="Pay_mode" runat="server"  CssClass="form-control form-control-sm col-sm-8"  onchange="cash_chk('Pay_mode')"    >
                                <%--<asp:ListItem Text="Select" Value="" />--%>
                                        <asp:ListItem Text="Please Select" Value="" />
                                        <asp:ListItem Text="Bank" Value="Bank" />
                                        <asp:ListItem Text="Cheque" Value="Cheque" />
                                        <asp:ListItem Text="Cash" Value="Cash" />
                                       
                        </asp:DropDownList>


                <%--<asp:TextBox ID="Pay_mode" runat="server" CssClass="form-control form-control-sm textboxstyle"   />--%>
                <asp:CustomValidator ID="CustomValidator8" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Pay_mode" ValidateEmptyText="true"></asp:CustomValidator>
            </div>
        </div>

        <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Amount paid :<span class="text-danger">*</span></label>
            </div>
            <div class="col-lg-7">
                <asp:TextBox ID="Cess_Amount_Paid" runat="server" CssClass="form-control form-control-sm textboxstyle"   OnTextChanged="Cess_Amount_Paid_TextChanged"  onchange ="Total_esi()"  />
                <asp:CustomValidator ID="CustomValidator9" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Cess_Amount_Paid" ValidateEmptyText="true"></asp:CustomValidator>
            </div>
        </div>

        <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Balance value :<span class="text-danger">*</span></label>
            </div>
            <div class="col-lg-7">
                <asp:TextBox ID="Cess_Amount_Balance" runat="server" CssClass="form-control form-control-sm textboxstyle" Enabled="false"   />
                <asp:CustomValidator ID="CustomValidator10" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Cess_Amount_Balance" ValidateEmptyText="true"></asp:CustomValidator>
            </div>
            
        </div>

         <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Previous Outstanding Balance :<span class="text-danger">*</span></label>
            </div>
           
            <div class="col-lg-7">
                <asp:TextBox ID="Previous_outstanding" runat="server" CssClass="form-control form-control-sm textboxstyle"   Enabled="false"   />
                <asp:CustomValidator ID="CustomValidator11" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Cess_Amount_Balance" ValidateEmptyText="true"></asp:CustomValidator>
            </div>
        </div>

       <div class="col-lg-4 mb-1 form-row">
            <div class="col-lg-5">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Remarks :<span class="text-danger">*</span></label>
            </div>
           
            <div class="col-lg-7">
                <asp:TextBox ID="Remarks" runat="server" CssClass="form-control form-control-sm textboxstyle" TextMode="MultiLine"   />
                <asp:CustomValidator ID="CustomValidator12" runat="server" ClientValidationFunction="Validate" ValidationGroup="Save" ControlToValidate="Cess_Amount_Balance" ValidateEmptyText="true"></asp:CustomValidator>
            </div>
        </div>

      


  </div>
            </ItemTemplate>
        </asp:TemplateField>
                       
    </Columns>
    <PagerSettings Visible="False" />
</cc1:formcointainer>
