protected void Page_Load(object sender, EventArgs e)
{
    if (!IsPostBack)
    {
        // Hide license controls initially
        ddlLabourLicNo.Visible = false;
        lblLabourLicNo.Visible = false;

        // Fetch Vendor Code
        string vcode = Session["UserName"].ToString();
        txtVCode.Text = vcode;

        // Fetch Vendor Name
        BL_Vendor_RFQ_Block_Unblock blobj2 = new BL_Vendor_RFQ_Block_Unblock();
        DataSet dsV = blobj2.GetVName(vcode);

        if (dsV != null && dsV.Tables[0].Rows.Count > 0)
        {
            txtVName.Text = dsV.Tables[0].Rows[0]["V_NAME"].ToString();
        }
        else
        {
            txtVName.Text = "Not Found";
        }
    }
}


<div class="row">
    <div class="form-group col-md-4">
        <label class="m-0 mr-0 p-0 col-form-label-sm font-weight-bold">Vendor Code:</label>
        <asp:TextBox ID="txtVCode" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
    </div>

    <div class="form-group col-md-4">
        <label class="m-0 mr-0 p-0 col-form-label-sm font-weight-bold">Vendor Name:</label>
        <asp:TextBox ID="txtVName" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
    </div>
</div>
