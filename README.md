<asp:Repeater ID="WAGE_REGIS_FILE1" runat="server">
    <ItemTemplate>
        <a href='<%# Eval("Url") %>' target="_blank"
           style="color:blue;text-decoration:underline">
            <%# Eval("Name") %>
        </a><br />
    </ItemTemplate>
</asp:Repeater>

Repeater rpt = (Repeater)SummaryReport_Record.Rows[0]
                .FindControl("WAGE_REGIS_FILE1");

string file = PageRecordDataSet.Tables["App_Online_Wages"]
              .Rows[0]["WAGE_REGIS_FILE"].ToString();

if (!string.IsNullOrWhiteSpace(file))
{
    DataTable dt = new DataTable();
    dt.Columns.Add("Name");
    dt.Columns.Add("Url");

    string fileName = Path.GetFileName(file);

    dt.Rows.Add(
        fileName,
        ResolveUrl("~/FileDownloadHandler.ashx?file=" +
                   Server.UrlEncode(fileName))
    );

    rpt.DataSource = dt;
    rpt.DataBind();
}

