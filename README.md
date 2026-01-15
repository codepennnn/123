2b569210-e839-4c37-bd17-b56ed7f5e011_ca0f0758-a5a9-4f4f-83ed-000469cba23e_pay.pdf,2b569210-e839-4c37-bd17-b56ed7f5e011_ca0f0758-a5a9-4f4f-83ed-000469cba23e_WAGE-05.pdf


    <asp:FileUpload ID="WAGE_REGIS_FILE" runat="server" AllowMultiple="true" CssClass="form-control-sm col-sm-9" onchange="return ValidateFileExtension(this)"></asp:FileUpload>
     <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator66" Display="Dynamic" ControlToValidate="WAGE_REGIS_FILE" ErrorMessage="*" ValidationGroup="save" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
     <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="Validate" ValidationGroup="save" ControlToValidate="WAGE_REGIS_FILE" ValidateEmptyText="true"></asp:CustomValidator>
    
              <asp:Repeater ID="WAGE_REGIS_FILE1" runat="server">
                  <ItemTemplate>
                      <a href='<%# Eval("Url") %>' target="_blank"
                         style="color:blue;text-decoration:underline">
                          <%# Eval("Name") %>
                      </a><br />
                  </ItemTemplate>
              </asp:Repeater>
      

 </div>

    Repeater rpt = (Repeater)SummaryReport_Record.Rows[0].FindControl("WAGE_REGIS_FILE1");

   string file = PageRecordDataSet.Tables["App_Online_Wages"]
                 .Rows[0]["WAGE_REGIS_FILE"].ToString();

   if (!string.IsNullOrWhiteSpace(file))
   {
       DataTable dt = new DataTable();
       dt.Columns.Add("Name");
       dt.Columns.Add("Url");


       string displayName = file.Contains("_")
           ? file.Substring(file.IndexOf('_') + 1)
           : file;


       dt.Rows.Add(
           displayName,
           ResolveUrl("~/FileDownloadHandler.ashx?file=" + file)
       );

       rpt.DataSource = dt;
       rpt.DataBind();
   }
