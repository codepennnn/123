<style>
/* ================= GRID BASE ================= */
.grid-wrapper {
    width: 100%;
    max-height: 250px;
    overflow: auto;
    border: 1px solid #cfd8e3;
    background: #ffffff;
}

.styled-grid {
    width: 100%;
    border-collapse: collapse;
    font-family: "Segoe UI", Arial, sans-serif;
    font-size: 12px;
}

/* ================= HEADER ================= */
.styled-grid th {
    background: #003570;
    color: #ffffff;
    padding: 8px 6px;
    text-align: center;
    white-space: normal;
    word-break: break-word;
    line-height: 1.3;
    border: 1px solid #d0d7e2;
}

/* ================= ROWS ================= */
.styled-grid td {
    padding: 6px 6px;
    vertical-align: top;
    white-space: normal;
    word-break: break-word;
    line-height: 1.4;
    border: 1px solid #e1e6ef;
}

/* Alternate rows */
.styled-grid tr:nth-child(even) {
    background-color: #f5f8fc;
}

/* Hover */
.styled-grid tr:hover {
    background-color: #eaf2ff;
}

/* ================= COLUMN TYPES ================= */
.wrap-col {
    max-width: 220px;
    min-width: 180px;
}

.num-col {
    width: 70px;
    text-align: center;
}

.ddl-col {
    width: 150px;
}

/* ================= INPUTS ================= */
.styled-grid input,
.styled-grid select {
    font-size: 11px;
    padding: 3px 4px;
}
</style>




<div class="grid-wrapper">

<cc1:DetailsContainer 
    ID="HalfYearly_Records"
    runat="server"
    AutoGenerateColumns="False"
    AllowPaging="False"
    GridLines="Both"
    DataMember="App_Half_Yearly_Details"
    DataKeyNames="ID"
    DataSource="<%# PageRecordDataSet %>"
    CssClass="styled-grid">

    <Columns>

        <!-- Sl No -->
        <asp:TemplateField HeaderText="Sl No">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
            <ItemStyle CssClass="num-col" />
        </asp:TemplateField>

        <!-- Contractor -->
        <asp:BoundField DataField="Name_Address_Of_Contractor"
            HeaderText="Contractor Name & Address">
            <ItemStyle CssClass="wrap-col" />
        </asp:BoundField>

        <!-- Establishment -->
        <asp:TemplateField HeaderText="Establishment">
            <ItemTemplate>
                <asp:DropDownList 
                    ID="Name_Address_Of_Establishment"
                    runat="server"
                    CssClass="form-control form-control-sm ddl-col">
                    <asp:ListItem>Tata Steel UISL Jamshedpur</asp:ListItem>
                    <asp:ListItem>Tata Steel UISL Seraikela</asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>

        <!-- State -->
        <asp:BoundField DataField="State" HeaderText="State">
            <ItemStyle CssClass="num-col" />
        </asp:BoundField>

        <!-- Principal Employer -->
        <asp:TemplateField HeaderText="Principal Employer">
            <ItemTemplate>
                <asp:DropDownList 
                    ID="Name_and_Address_Of_PrincipalEmp"
                    runat="server"
                    CssClass="form-control form-control-sm ddl-col">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Tata Steel Limited</asp:ListItem>
                    <asp:ListItem>Tata Steel UISL</asp:ListItem>
                    <asp:ListItem>Others</asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>

        <!-- Licence -->
        <asp:BoundField DataField="LabourLicNo" HeaderText="Licence No">
            <ItemStyle CssClass="num-col" />
        </asp:BoundField>

        <!-- Capacity -->
        <asp:BoundField DataField="workerno" HeaderText="Capacity">
            <ItemStyle CssClass="num-col" />
        </asp:BoundField>

        <!-- Contractor Establishment Worked -->
        <asp:TemplateField HeaderText="Establishment Worked">
            <ItemTemplate>
                <asp:TextBox 
                    ID="Contractors_Establishment_Worked"
                    runat="server"
                    CssClass="form-control form-control-sm"
                    Style="width:90%;"
                    oninput="validateIntInput(this,154);">
                </asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>

        <!-- Sex -->
        <asp:BoundField DataField="sex_M" HeaderText="M">
            <ItemStyle CssClass="num-col" />
        </asp:BoundField>

        <asp:BoundField DataField="sex_F" HeaderText="F">
            <ItemStyle CssClass="num-col" />
        </asp:BoundField>

    </Columns>

</cc1:DetailsContainer>

</div>

