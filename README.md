<asp:Repeater ID="WAGE_REGIS_FILE1" runat="server">
    <ItemTemplate>
        <a href='<%# Eval("Url") %>' target="_blank"
           style="color:blue;text-decoration:underline">
            <%# Eval("Name") %>
        </a><br />
    </ItemTemplate>
</asp:Repeater>

<asp:Repeater ID="BANK_PAY_FILE1" runat="server">
    <ItemTemplate>
        <a href='<%# Eval("Url") %>' target="_blank"
           style="color:blue;text-decoration:underline">
            <%# Eval("Name") %>
        </a><br />
    </ItemTemplate>
</asp:Repeater>

<asp:Repeater ID="GP_DECL_FILE1" runat="server">
    <ItemTemplate>
        <a href='<%# Eval("Url") %>' target="_blank"
           style="color:blue;text-decoration:underline">
            <%# Eval("Name") %>
        </a><br />
    </ItemTemplate>
</asp:Repeater>


private void BindAttachments(string dbValue, Repeater rpt)
{
    if (string.IsNullOrWhiteSpace(dbValue))
    {
        rpt.DataSource = null;
        rpt.DataBind();
        return;
    }

    DataTable dt = new DataTable();
    dt.Columns.Add("Name");
    dt.Columns.Add("Url");

    string[] files = dbValue.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

    foreach (string file in files)
    {
        string cleanFile = file.Trim();

        string displayName = cleanFile.Contains("_")
            ? cleanFile.Substring(cleanFile.IndexOf('_') + 1)
            : cleanFile;

        dt.Rows.Add(
            displayName,
            ResolveUrl("~/FileDownloadHandler.ashx?file=" + cleanFile)
        );
    }

    rpt.DataSource = dt;
    rpt.DataBind();
}



protected void SummaryReport_Record_RowDataBound(object sender, GridViewRowEventArgs e)
{
    if (e.Row.RowType != DataControlRowType.DataRow) return;

    DataRow dr = PageRecordDataSet.Tables["App_Online_Wages"].Rows[0];

    BindAttachments(
        dr["WAGE_REGIS_FILE"].ToString(),
        (Repeater)e.Row.FindControl("WAGE_REGIS_FILE1")
    );

    BindAttachments(
        dr["BANK_PAY_FILE"].ToString(),
        (Repeater)e.Row.FindControl("BANK_PAY_FILE1")
    );

    BindAttachments(
        dr["GP_DECL_FILE"].ToString(),
        (Repeater)e.Row.FindControl("GP_DECL_FILE1")
    );
}


