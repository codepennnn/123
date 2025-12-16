        <asp:Panel ID="pnlGrid" runat="server" Visible="false" Style="margin-top:18px">
            <div class="grid-wrap">
                <asp:GridView ID="gvAttendance" runat="server" CssClass="attendance" AutoGenerateColumns="false"
                    OnRowDataBound="gvAttendance_RowDataBound" GridLines="None">
                    <Columns>
                       <%-- <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date","{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <!-- what user sees -->
                                <asp:Label ID="lblDateDisplay" runat="server"
                                    Text='<%# Eval("Date", "{0:dd-MM-yyyy}") %>'></asp:Label>

                                <!-- hidden ISO value used by C# code -->
                                <asp:HiddenField ID="hfDateIso" runat="server"
                                    Value='<%# Eval("Date", "{0:yyyy-MM-dd}") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Aadhar">
                            <ItemTemplate>
                                <asp:Label ID="lblRowAadhar" runat="server" CssClass="grid-label row-aadhar-label"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Workman Name">
                            <ItemTemplate>
                                <asp:Label ID="lblRowName" runat="server" CssClass="grid-label row-name-label"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Work Order">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlRowWorkOrder" runat="server" CssClass="row-select row-wo" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Location">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlRowLocation" runat="server" CssClass="row-select row-loc" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="OT(hrs)">
                            <ItemTemplate>
                                <asp:TextBox ID="txtOT" runat="server" CssClass="otbox row-ot" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Day Def">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlDayDef" runat="server" CssClass="small row-day">
                                    <asp:ListItem Value="WD">Working Day</asp:ListItem>
                                    <asp:ListItem Value="OD">Off Day</asp:ListItem>
                                    <asp:ListItem Value="L">Leave</asp:ListItem>
                                    <asp:ListItem Value="NH">Holiday</asp:ListItem>
                                    <asp:ListItem Value="HF">Half Day</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Engagement Type">
    <ItemTemplate>
        <asp:DropDownList ID="ddlEngagementType" runat="server" CssClass="row-select row-engtype">
            <asp:ListItem Value="">-- Select --</asp:ListItem>
            <asp:ListItem Value="ManPowerSupply">Manpower Supply</asp:ListItem>
            <asp:ListItem Value="ItemRate">Item Rate</asp:ListItem>
        </asp:DropDownList>
    </ItemTemplate>
</asp:TemplateField>


                        <asp:TemplateField HeaderText="Present" ItemStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkPresent" runat="server" CssClass="row-present" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <div style="margin-top:10px; display:flex; justify-content:space-between; align-items:center;">
                    <div class="muted">Rows: <asp:Label ID="lblRowCount" runat="server" Text="0" /></div>
                    <div style="display:flex; gap:8px;">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-sm btn-success" Text="Save Attendance" OnClick="btnSave_Click" />
                        <asp:Button ID="btnExport" runat="server" CssClass="btn btn-sm btn-warning" Text="Export CSV" OnClientClick="exportCsv(); return false;" />
                    </div>
                </div>
            </div>
        </asp:Panel>
