

Line 446:                                        </asp:BoundField>--%>
Line 447:
Line 448:                                        <asp:HiddenField ID="hfAmount" runat="server" Value='<%# Eval("RMWWR") %>' />
Line 449:                                        <asp:HiddenField ID="hfKONNR" runat="server" Value='<%# Eval("KONNR") %>' />
Line 450:

and my checkbox like this - 
                                              <asp:TemplateField HeaderStyle-Width="3%" HeaderText="" HeaderStyle-HorizontalAlign="Left">
                                  <HeaderTemplate>
                                      <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" ToolTip="Select All" />
                                  </HeaderTemplate>

                                  <ItemTemplate>
                                      <asp:CheckBox ID="chkSelect" runat="server" />
                                  </ItemTemplate>
                           
                              </asp:TemplateField>
and target or call id like this
MainContent_bocw_summary_Subjective_balance_0
MainContent_bocw_summary_Total_Base_Amount_0
