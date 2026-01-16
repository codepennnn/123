   function Calculation() {
       
       var Bonus_Generation_Details = document.getElementById('<%=Grid.ClientID %>');
       
       for (i = 0; i < Bonus_Generation_Details.rows.length - 1; i++) {

           var adhar = document.getElementById("MainContent_Grid_lblAdharNo_" + i).innerHTML;

           Wage_Rate = parseFloat(document.getElementById("MainContent_Grid_lblWage_Rate_" + i).innerHTML);                    
           Arrear_Wage = parseFloat(document.getElementById("MainContent_Grid_Arrear_Wage_" + i).value);                    
           Puja_Bonus = parseFloat(document.getElementById("MainContent_Grid_Puja_Bonus_" + i).value);                    
           Interim_Bonus = parseFloat(document.getElementById("MainContent_Grid_Interim_Bonus_" + i).value);                    
           Deduction_misconduct_emp = parseFloat(document.getElementById("MainContent_Grid_Deduction_misconduct_emp_" + i).value);                    
           Total_deduction = parseFloat(document.getElementById("MainContent_Grid_Total_deduction_" + i).value);                    
           Max_Wage_Rate = (Wage_Rate + Arrear_Wage);
           document.getElementById("MainContent_Grid_Max_Wage_Rate_" + i).value = Max_Wage_Rate;                   
           No_of_days_worked = parseFloat(document.getElementById("MainContent_Grid_lblNo_of_days_worked_" + i).innerHTML);                   
           Total_No_of_days_worked = parseFloat(document.getElementById("MainContent_Grid_lblTotal_No_of_days_worked_" + i).innerHTML);                    
           chk_no_of_day = (Total_No_of_days_worked);                    

           Max_wagerate = parseFloat(document.getElementById("MainContent_Grid_Max_Wage_Rate_" + i).value);
           var bns_pay = ((Max_wagerate * 8.33) / 100).toFixed(2);
           var chk_total_deduct = Puja_Bonus + Interim_Bonus + Deduction_misconduct_emp;

           if (chk_total_deduct > bns_pay) {

               document.getElementById("MainContent_Grid_Puja_Bonus_" + i).value = 0.0;
               document.getElementById("MainContent_Grid_Interim_Bonus_" + i).value = 0.0;
               document.getElementById("MainContent_Grid_Deduction_misconduct_emp_" + i).value = 0.0;
               document.getElementById("MainContent_Grid_Total_deduction_" + i).value = 0.0;

               if (chk_no_of_day >= 30) {
                   bonus_payable = ((Max_wagerate * 8.33) / 100).toFixed(2);
                   Total_deduction = 0.0;
                   document.getElementById("MainContent_Grid_Total_deduction_" + i).value = Total_deduction;
                   var net_bonus_payable = (parseFloat(bonus_payable) - parseFloat(Total_deduction)) + (parseFloat(document.getElementById("MainContent_Grid_Additional_Amount_" + i).value));
                   document.getElementById("MainContent_Grid_Bonus_Payable_" + i).value = bonus_payable;
                   if (net_bonus_payable < 0) {

                       document.getElementById("MainContent_Grid_Net_Bonus_Payable_" + i).value = 0.0;
                   } else {
                       document.getElementById("MainContent_Grid_Net_Bonus_Payable_" + i).value = Math.round(net_bonus_payable);
                   }


               }
               else {

                   Total_deduction = 0.0;
                   document.getElementById("MainContent_Grid_Total_deduction_" + i).value = Total_deduction;
                   document.getElementById("MainContent_Grid_Net_Bonus_Payable_" + i).value = 0.0 + (parseFloat(document.getElementById("MainContent_Grid_Additional_Amount_" + i).value));
                   document.getElementById("MainContent_Grid_Bonus_Payable_" + i).value = "0.00";

               }


               alert("Total Sum of deduction can not be greater than Bonus Payable." +"\n"+ "Adhar No. - "+adhar );
           } else {

               if (chk_no_of_day >= 30) {
                   bonus_payable = ((Max_wagerate * 8.33) / 100).toFixed(2);
                   Total_deduction = parseFloat(Puja_Bonus) + parseFloat(Interim_Bonus) + parseFloat(Deduction_misconduct_emp);
                   document.getElementById("MainContent_Grid_Total_deduction_" + i).value = Total_deduction;
                   var net_bonus_payable = (parseFloat(bonus_payable) - parseFloat(Total_deduction)) + (parseFloat(document.getElementById("MainContent_Grid_Additional_Amount_" + i).value));
                   document.getElementById("MainContent_Grid_Bonus_Payable_" + i).value = bonus_payable;
                   if (net_bonus_payable < 0) {

                       document.getElementById("MainContent_Grid_Net_Bonus_Payable_" + i).value = 0.0;
                   } else {
                       document.getElementById("MainContent_Grid_Net_Bonus_Payable_" + i).value = Math.round(net_bonus_payable);
                   }


               }
               else {

                   Total_deduction = parseFloat(Puja_Bonus) + parseFloat(Interim_Bonus) + parseFloat(Deduction_misconduct_emp);
                   document.getElementById("MainContent_Grid_Total_deduction_" + i).value = Total_deduction;


                   document.getElementById("MainContent_Grid_Net_Bonus_Payable_" + i).value = 0.0 + (parseFloat(document.getElementById("MainContent_Grid_Additional_Amount_" + i).value));

                   document.getElementById("MainContent_Grid_Bonus_Payable_" + i).value = "0.00";

               }
           }
           
           
       }
       /*alert("End");*/
       return true;
   }
                                  <asp:GridView ID="Grid" runat="server" Style="text-align: center; width: 3000px; height: 100px;" AutoGenerateColumns="False"
                                  AllowPaging="false" CellPadding="0" GridLines="Both"  OnRowDataBound="Grid_RowDataBound"
                                  ForeColor="#333333" ShowHeaderWhenEmpty="True" SortedAscendingCellStyle-BorderColor="Black" SortedAscendingCellStyle-BorderWidth="1px" SortedAscendingCellStyle-BorderStyle="Solid"
                                  PageSize="100" PagerSettings-Visible="false" PagerStyle-HorizontalAlign="Center" RowStyle-Height="30px"
                                  PagerStyle-Wrap="True" HeaderStyle-Font-Size="small" RowStyle-Font-Size="small" CssClass="freezeColumn"
                                  HeaderStyle-HorizontalAlign="Center" RowStyle-ForeColor="Black" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                                  <AlternatingRowStyle BackColor="White" ForeColor="#284775" />


                                  <Columns>

                                      <asp:TemplateField HeaderText="Sl.No." SortExpression="Sl_No"
                                          HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate><%# Container.DataItemIndex + 1 + "."%></ItemTemplate>
                                      </asp:TemplateField>

                                      




        <asp:TemplateField HeaderText="ID" Visible="false">
            <ItemTemplate><asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>' /></ItemTemplate>
        </asp:TemplateField>



      <%--  <asp:TemplateField HeaderText="Sl.No.">
            <ItemTemplate><asp:Label ID="lblSlno" runat="server" Text='<%# Eval("Slno") %>' /></ItemTemplate>
        </asp:TemplateField>--%>

        <asp:TemplateField HeaderText="Year">
            <ItemTemplate><asp:Label ID="lblYear" runat="server" Text='<%# Eval("Year") %>' /></ItemTemplate>
        </asp:TemplateField>

                                    <%--  //Commented 25 sep--%>

        <asp:TemplateField HeaderText="Vendor Code" Visible="false" >
            <ItemTemplate><asp:Label ID="lblVcode" runat="server" Text='<%# Eval("Vcode") %>' /></ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Vendor Name" Visible="false">
            <ItemTemplate><asp:Label ID="lblVendorName" runat="server"  Text='<%# Eval("VendorName") %>' /></ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Adhar No.">
            <ItemTemplate><asp:Label ID="lblAdharNo" runat="server" Text='<%# Eval("AdharNo") %>' /></ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Workman Name">
            <ItemTemplate><asp:Label ID="lblWorkman_Name" runat="server" Text='<%# Eval("Workman_Name") %>' /></ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Workman Sl.No.">
            <ItemTemplate><asp:Label ID="lblWorkman_Slno" runat="server" Text='<%# Eval("Workman_Slno") %>' /></ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="WorkOrder No.">
            <ItemTemplate><asp:Label ID="lblWorkorderNo" runat="server" Text='<%# Eval("WorkorderNo") %>' /></ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Gender">
            <ItemTemplate><asp:Label ID="lblGender" runat="server" Text='<%# Eval("Gender") %>' /></ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Workman Category">
            <ItemTemplate><asp:Label ID="lblWorkman_Category" runat="server" Text='<%# Eval("Workman_Category") %>' /></ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="No. of Days Worked">
            <ItemTemplate><asp:Label ID="lblNo_of_days_worked" runat="server" Text='<%# Eval("No_of_days_worked") %>' /></ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Cumulative Attendance to check Bonus eligibility">  <%--Total No. of Days Worked 25 Sep--%> 
            <ItemTemplate><asp:Label ID="lblTotal_No_of_days_worked" runat="server" Text='<%# Eval("Total_No_of_days_worked") %>' /></ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Wages Earned During Financial Year( Basic + D. A )">
            <ItemTemplate><asp:Label ID="lblWage_Rate" runat="server" Text='<%# Eval("Wage_Rate") %>' /></ItemTemplate>
        </asp:TemplateField>

      <%--  <asp:TemplateField HeaderText="Arrear Wage">
            <ItemTemplate><asp:Label ID="lblArrear_Wage" runat="server" Text='<%# Eval("Arrear_Wage") %>' OnChange="Calculation()" /></ItemTemplate>
        </asp:TemplateField>--%>

<asp:TemplateField HeaderText="Arrear Wage">
    <ItemTemplate>
        <asp:TextBox ID="Arrear_Wage" Text='<%# Eval("Arrear_Wage") %>' runat="server" OnChange="Calculation()"  ></asp:TextBox>
    </ItemTemplate>
</asp:TemplateField>


     <%--   <asp:TemplateField HeaderText="Total Earn Gross wages ( Basic + DA )">
            <ItemTemplate><asp:Label ID="lblMax_Wage_Rate" runat="server" Text='<%# Eval("Max_Wage_Rate") %>' /></ItemTemplate>
        </asp:TemplateField>--%>




 <asp:TemplateField HeaderText="Total Earn Gross wages ( Basic + DA )">
    <ItemTemplate>
        <asp:TextBox ID="Max_Wage_Rate" Text='<%# Eval("Max_Wage_Rate") %>' runat="server"  ></asp:TextBox>
    </ItemTemplate>
</asp:TemplateField>

 <asp:TemplateField HeaderText="Bonus Payable"> <%--added 25 sep--%>
    <ItemTemplate>
        <asp:TextBox ID="Bonus_Payable" Text='<%# Eval("Bonus_Payable") %>' runat="server"   ></asp:TextBox>
    </ItemTemplate>
</asp:TemplateField>


<%--                                      <asp:TemplateField HeaderText="Puja Bonus of other quota many Bonus Paid during the accounting year">
    <ItemTemplate><asp:Label ID="lblPuja_Bonus" runat="server" Text='<%# Eval("Puja_Bonus") %>' /></ItemTemplate>
</asp:TemplateField>--%>

 <asp:TemplateField HeaderText="Puja Bonus of other quota many Bonus Paid during the accounting year">
    <ItemTemplate>
        <asp:TextBox ID="Puja_Bonus" Text='<%# Eval("Puja_Bonus") %>' runat="server" OnChange="Calculation()"  ></asp:TextBox>
    </ItemTemplate>
</asp:TemplateField>


<%-- <asp:TemplateField HeaderText="Interim Bonus or Bonus paid in advance">
    <ItemTemplate><asp:Label ID="lblInterim_Bonus" runat="server" Text='<%# Eval("Interim_Bonus") %>' /></ItemTemplate>
</asp:TemplateField>--%>

  <asp:TemplateField HeaderText="Interim Bonus or Bonus paid in advance">
    <ItemTemplate>
        <asp:TextBox ID="Interim_Bonus" Text='<%# Eval("Interim_Bonus") %>' runat="server" OnChange="Calculation()"  ></asp:TextBox>
    </ItemTemplate>
</asp:TemplateField>


                     <%--<asp:TemplateField HeaderText="Deduction on a/c of financial if any caused by misconduct of the employee">
    <ItemTemplate><asp:Label ID="lblDeduction_misconduct_emp" runat="server" Text='<%# Eval("Deduction_misconduct_emp") %>' /></ItemTemplate>
</asp:TemplateField>--%>


  <asp:TemplateField HeaderText="Deduction on a/c of financial if any caused by misconduct of the employee">
    <ItemTemplate>
        <asp:TextBox ID="Deduction_misconduct_emp" Text='<%# Eval("Deduction_misconduct_emp") %>' runat="server" OnChange="Calculation()"  ></asp:TextBox>
    </ItemTemplate>
</asp:TemplateField>


                           <%--           <asp:TemplateField HeaderText="Total Sum of deduction">
    <ItemTemplate><asp:Label ID="lblTotal_deduction" runat="server" Text='<%# Eval("Total_deduction") %>' /></ItemTemplate>
</asp:TemplateField>--%>

<asp:TemplateField HeaderText="Total Sum of deduction">
    <ItemTemplate>
        <asp:TextBox ID="Total_deduction" Text='<%# Eval("Total_deduction") %>' runat="server"   ></asp:TextBox>
    </ItemTemplate>
</asp:TemplateField>

                                 <%--    <asp:TemplateField HeaderText="Bonus Payable">
    <ItemTemplate><asp:Label ID="lblBonus_Payable" runat="server" Text='<%# Eval("Bonus_Payable") %>' /></ItemTemplate>
</asp:TemplateField>--%>




                    <%--   <asp:TemplateField HeaderText="Net Bonus Payable">
    <ItemTemplate><asp:Label ID="lblNet_Bonus_Payable" runat="server" Text='<%# Eval("Net_Bonus_Payable") %>' /></ItemTemplate>
</asp:TemplateField>--%>

                                      <%--26sep--%>

                                       <asp:TemplateField HeaderText="Additional Amount">
    <ItemTemplate>
        <asp:TextBox ID="Additional_Amount" Text='<%# Eval("Additional_Amount") %>' runat="server"  OnChange="Calculation()"   ></asp:TextBox>
    </ItemTemplate>
</asp:TemplateField>



                                       <asp:TemplateField HeaderText="Net Bonus Payable">
    <ItemTemplate>
        <asp:TextBox ID="Net_Bonus_Payable" Text='<%# Eval("Net_Bonus_Payable") %>' runat="server"   ></asp:TextBox>
    </ItemTemplate>
</asp:TemplateField>



                                  </Columns>


                                  <EditRowStyle BackColor="#999999" />
                                  <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                  <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                  <PagerSettings Mode="Numeric" />
                                  <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Font-Bold="True" CssClass="pager1" />
                                  <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                  <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                  <SelectedRowStyle BackColor="#E2DED6" Font-Bold="False" ForeColor="#333333" />
                                  <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                  <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                  <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                  <SortedDescendingHeaderStyle BackColor="#6F8DAE" />


                              </asp:GridView>
explain this alert -  Total Sum of deduction can not be greater than Bonus Payable
