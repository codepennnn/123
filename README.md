   <script type="text/javascript">
       function validateIntInput(txt, max) {

       
           txt.value = txt.value.replace(/[^0-9]/g, '');

           if (txt.value === '') return;

           var val = parseInt(txt.value, 10);

           if (val > max) {
               alert("Maximum value allowed is " + max);
               txt.value = max;   
           }
       }

       function applyCanteenAndCrechesRules() {

        
           var grid = document.querySelector(".styled-grid");
           if (!grid) return;

           var rows = grid.querySelectorAll("tr");

           rows.forEach(function (row, index) {

              
               if (index === 0) return;

               
               var maleCell = row.cells[12];   
               var femaleCell = row.cells[13]; 
             
               if (!maleCell || !femaleCell) return;

               var male = parseInt(maleCell.innerText.trim()) || 0;
               var female = parseInt(femaleCell.innerText.trim()) || 0;

               var totalManpower = male + female;

          
               var canteenDDL = row.querySelector("select[id*='Welfare_Canteen']");
               var crechesDDL = row.querySelector("select[id*='Welfare_Creches']");

            
               if (canteenDDL) {
                   canteenDDL.value = (totalManpower >= 100) ? "YES" : "NA";
                   
               }

             
               if (crechesDDL) {
                   crechesDDL.value = (female >= 50) ? "YES" : "NA";
                   
               }
           });
       }

   
       document.addEventListener("DOMContentLoaded", applyCanteenAndCrechesRules);


     
   </script>







    <cc1:DetailsContainer ID="HalfYearly_Records" runat="server" AutoGenerateColumns="False" AllowPaging="False" CellPadding="0" GridLines="Both" DataMember="App_Half_Yearly_Details"
     DataKeyNames="ID" DataSource="<%# PageRecordDataSet %>" ForeColor="#333333" ShowHeaderWhenEmpty="False"
     OnRowDataBound="PF_ESI_Records_RowDataBound" PagerSettings-Visible="False" PagerStyle-HorizontalAlign="Center" RowStyle-ForeColor="Black"
     PagerStyle-Wrap="False" HeaderStyle-Font-Size="13px" RowStyle-Font-Size="11px" class="border " Style="margin-top: 12px;"  CssClass="styled-grid modern-grid">
     <AlternatingRowStyle BackColor="White" ForeColor="#284775"  />
     <Columns>

         <asp:TemplateField HeaderText="ID" SortExpression="ID" Visible="False">
             <ItemTemplate>
                 <asp:Label ID="ID" runat="server"></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Sl. No.">
             <ItemTemplate>&emsp;<%# Container.DataItemIndex + 1 + "."%></ItemTemplate>
             <ItemStyle Width="4%" />
         </asp:TemplateField>
         <asp:BoundField DataField="Name_Address_Of_Contractor" HeaderText="Name and Address Of Contractor" SortExpression="Name_Address_Of_Contractor">
             <ItemStyle CssClass="wrap-text" HorizontalAlign="Left" />
         </asp:BoundField>
        
         
         <asp:TemplateField HeaderText="Name and Address Of Establishment" HeaderStyle-ForeColor="White">
             <ItemTemplate>
                                                   
                 <asp:DropDownList ID="Name_Address_Of_Establishment" runat="server" CssClass="form-control form-control-sm font-small">

                     <asp:ListItem Value="Tata Steel UISL Jamshedpur">Tata Steel UISL Jamshedpur</asp:ListItem>
                     <asp:ListItem Value="Tata Steel UISL Seraikela">Tata Steel UISL Seraikela</asp:ListItem>
                     


                 </asp:DropDownList>
             </ItemTemplate>
         </asp:TemplateField>


                                  

              <asp:BoundField DataField="State" HeaderText="State" SortExpression="State">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>






         <asp:TemplateField HeaderText="Principal Employer" HeaderStyle-ForeColor="White">
             <ItemTemplate>
                 <asp:DropDownList ID="Name_and_Address_Of_PrincipalEmp" runat="server" CssClass="form-control form-control-sm font-small" Width="200px">
                     <asp:ListItem></asp:ListItem>
                     <asp:ListItem Value="Tata Steel Limited">Tata Steel Limited</asp:ListItem>
                     <asp:ListItem Value="Tata Steel UISL">Tata Steel UISL</asp:ListItem>
                     <asp:ListItem Value="JUIDCO,Ranchi">JUIDCO,Ranchi</asp:ListItem>
                     <asp:ListItem Value="Manipal Tata Medical College">Manipal Tata Medical College</asp:ListItem>
                     <asp:ListItem Value="Tata Steel Foundation">Tata Steel Foundation</asp:ListItem>
                     <asp:ListItem Value="NINL">NINL</asp:ListItem>
                     <asp:ListItem Value="Tata Steel FAP, Jajpur">Tata Steel FAP, Jajpur</asp:ListItem>
                     <asp:ListItem Value="TRF">TRF</asp:ListItem>
                     <asp:ListItem Value="Tata Pigments Limited">Tata Pigments Limited</asp:ListItem>
                     <asp:ListItem Value="Tata Steel IBMD">Tata Steel IBMD</asp:ListItem>
                     <asp:ListItem Value="Others">Others</asp:ListItem>
                 </asp:DropDownList>

                 
           <asp:RequiredFieldValidator 
                         ID="rfvPrincipalEmployer"
                         runat="server"
                         ControlToValidate="Name_and_Address_Of_PrincipalEmp"
                         InitialValue=""
                         ErrorMessage="Principal Employer is required."
                         Display="Dynamic"
                         ForeColor="Red"
                         ValidationGroup="save" />

             </ItemTemplate>
         </asp:TemplateField>


 
         <asp:BoundField DataField="LabourLicNo" HeaderText="Lic No" SortExpression="LabourLicNo">
             <ItemStyle HorizontalAlign="Center" />
         </asp:BoundField>


            <asp:BoundField DataField="workerno" HeaderText="Capacity of Lic" SortExpression="workerno">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

    

       
         <asp:TemplateField HeaderText="Weekly Holiday" HeaderStyle-ForeColor="White">
             <ItemTemplate>
                <asp:DropDownList ID="Weekly_Holiday" runat="server" CssClass="form-control form-control-sm font-small">
     
                  <asp:ListItem Value="YES">YES</asp:ListItem>
                  <asp:ListItem Value="NO">NO</asp:ListItem>
                  <asp:ListItem Value="NA">NA</asp:ListItem>
   

                 </asp:DropDownList>
             </ItemTemplate>
         </asp:TemplateField>


         <asp:BoundField DataField="WorkOrderNo" HeaderText="WorkOrderNo" SortExpression="WorkOrderNo">
             <ItemStyle HorizontalAlign="Center" />
         </asp:BoundField>


         <asp:BoundField DataField="Workorder_FromDate"
             HeaderText="Workorder_FromDate"
             SortExpression="Workorder_FromDate"
             DataFormatString="{0:dd/MM/yyyy}"
             HtmlEncode="false">
             <ItemStyle HorizontalAlign="Center" />
         </asp:BoundField>

         <asp:BoundField DataField="Workorder_ToDate"
             HeaderText="Workorder_ToDate"
             SortExpression="Workorder_ToDate"
             DataFormatString="{0:dd/MM/yyyy}"
             HtmlEncode="false">
             <ItemStyle HorizontalAlign="Center" />
         </asp:BoundField>


          <asp:BoundField DataField="Establishment_Of_Principal_Emp_Worked" HeaderText="Establishment of principal emp worked" SortExpression="Establishment_Of_Principal_Emp_Worked">
          <ItemStyle HorizontalAlign="Center" />
      </asp:BoundField>



         <asp:TemplateField HeaderText="Contractor establishment had worked" HeaderStyle-ForeColor="White">
                 <ItemTemplate>
                     <asp:TextBox ID="Contractors_Establishment_Worked" runat="server" CssClass="form-control form-control-sm font-small"  oninput="validateIntInput(this,154);">>
                     </asp:TextBox>

                      <asp:RequiredFieldValidator 
                             ID="rfvCEW"
                             runat="server"
                             ControlToValidate="Contractors_Establishment_Worked"
                             ErrorMessage="This field is required."
                             ValidationGroup="save"
                             Display="Dynamic"
                             ForeColor="Red" />

                       


                 </ItemTemplate>
         </asp:TemplateField>

         <asp:BoundField DataField="sex_M" HeaderText="Sex (M)" SortExpression="sex_M">
             <ItemStyle HorizontalAlign="Center" />
         </asp:BoundField>

         <asp:BoundField DataField="sex_F" HeaderText="Sex (F)" SortExpression="sex_F">
             <ItemStyle HorizontalAlign="Center" />
         </asp:BoundField>


         <asp:BoundField DataField="No_Of_Mandays_Worked_Men" HeaderText="Total Mandays (M)" SortExpression="TotalMandays_Male">
             <ItemStyle HorizontalAlign="Center" />
         </asp:BoundField>


         <asp:BoundField DataField="No_Of_Mandays_Worked_Women" HeaderText="Total Mandays (F)" SortExpression="TotalMandays_Female">
             <ItemStyle HorizontalAlign="Center" />
         </asp:BoundField>



          <asp:BoundField DataField="No_Of_Mandays_Worked_children" HeaderText="Total Mandays (Children)" SortExpression="No_Of_Mandays_Worked_children">
              <ItemStyle HorizontalAlign="Center" />
          </asp:BoundField>


      




         <asp:BoundField DataField="Amt_Of_Deduct_From_Wages_Men" HeaderText="Wage Deduction (M)" SortExpression="Male_Deduction">
             <ItemStyle HorizontalAlign="Center" />
         </asp:BoundField>


         <asp:BoundField DataField="Amt_Of_Deduct_From_Wages_Women" HeaderText="Wage Deduction (F)" SortExpression="Female_Deduction">
             <ItemStyle HorizontalAlign="Center" />
         </asp:BoundField>


           <asp:BoundField DataField="Amt_Of_Deduct_From_Wages_Children" HeaderText="Wage Deduction (Children)" SortExpression="Amt_Of_Deduct_From_Wages_Children">
               <ItemStyle HorizontalAlign="Center" />
           </asp:BoundField>



         <asp:BoundField DataField="Amount_Of_Wages_Paid_Men" HeaderText="Gross Wage (M)" SortExpression="Male_Gross">
             <ItemStyle HorizontalAlign="Center" />
         </asp:BoundField>


         <asp:BoundField DataField="Amount_Of_Wages_Paid_Women" HeaderText="Gross Wage (F)" SortExpression="Female_Gross">
             <ItemStyle HorizontalAlign="Center" />
         </asp:BoundField>


             <asp:BoundField DataField="Amount_Of_Wages_Paid_Children" HeaderText="Gross Wage (Children)" SortExpression="Amount_Of_Wages_Paid_Children">
             <ItemStyle HorizontalAlign="Center" />
         </asp:BoundField>


         <asp:TemplateField HeaderText="Canteen">
             <HeaderStyle BackColor="#ff9933" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
             <ItemTemplate>
                 <asp:DropDownList ID="Welfare_Canteen" runat="server" CssClass="form-control form-control-sm font-small welfare-blink" Width="110px">
                     <asp:ListItem Value="YES">YES</asp:ListItem>
                     <asp:ListItem Value="NA">NA</asp:ListItem>

                 </asp:DropDownList>

             </ItemTemplate>
         </asp:TemplateField>


         <asp:TemplateField HeaderText="Rest Rooms">
             <HeaderStyle BackColor="#ff9933" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
             <ItemTemplate>
                 <asp:DropDownList ID="Welfare_RestRoom" runat="server" CssClass="form-control form-control-sm font-small welfare-blink">
   
                   <asp:ListItem Value="YES">YES</asp:ListItem>
                     <asp:ListItem Value="NA">NA</asp:ListItem>

                 </asp:DropDownList>

             </ItemTemplate>
         </asp:TemplateField>


         <asp:TemplateField HeaderText="Drinking Water">
             <HeaderStyle BackColor="#ff9933" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
             <ItemTemplate>
                 <asp:DropDownList ID="Welfare_DrinkingWater" runat="server" CssClass="form-control form-control-sm font-small welfare-blink">
                     
                     <asp:ListItem Value="YES">YES</asp:ListItem>
                     <asp:ListItem Value="NA">NA</asp:ListItem>
                   

                 </asp:DropDownList>

             </ItemTemplate>
         </asp:TemplateField>


         <asp:TemplateField HeaderText="Creches" >
             <HeaderStyle BackColor="#ff9933" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
             <ItemTemplate>
                 <asp:DropDownList ID="Welfare_Creches" runat="server" CssClass="form-control form-control-sm font-small welfare-blink" Width="110px">
                  <asp:ListItem Value="YES">YES</asp:ListItem>
                     <asp:ListItem Value="NA">NA</asp:ListItem>

                 </asp:DropDownList>

             </ItemTemplate>
         </asp:TemplateField>


           <asp:TemplateField HeaderText="First Aid">
               <HeaderStyle BackColor="#ff9933" ForeColor="White" Font-Bold="True" HorizontalAlign="Center" />
       <ItemTemplate>
           <asp:DropDownList ID="Welfare_FirstAid" runat="server" CssClass="form-control form-control-sm font-small welfare-blink" Width="110px">
            <asp:ListItem Value="YES">YES</asp:ListItem>
             <asp:ListItem Value="NA">NA</asp:ListItem>
              

           </asp:DropDownList>

       </ItemTemplate>
   </asp:TemplateField>












     </Columns>
     <EditRowStyle BackColor="#999999" />
     <FooterStyle BackColor="#003570" ForeColor="White" Font-Bold="True" />
     <HeaderStyle BackColor="#003570" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
     <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
     <SelectedRowStyle BackColor="#E2DED6" Font-Bold="False" ForeColor="#333333" />
     <SortedAscendingCellStyle BackColor="#E9E7E2" />
     <SortedAscendingHeaderStyle BackColor="#506C8C" />
     <SortedDescendingCellStyle BackColor="#FFFDF8" />
     <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
 </cc1:DetailsContainer>
