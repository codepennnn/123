<%@ Page Title="UserRelation Master" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserRelationMaster.aspx.cs" Inherits="CLMS.App.Master.UserRelationMaster" %>

<%@ Register Assembly="CustomControls" Namespace="GridViewasContainer" TagPrefix="cc1" %>
<%@ Register src="~/Control/MyMsgBox.ascx" tagname="MyMsgBox" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
   
         <div class="card m-2 shadow-lg">
              <div class="card-header bg-info text-light">
                <h6 class="m-0">User Relation Master</h6>
              </div>

                <div class="row m-0 justify-content-end">
                    
                </div>

              <div class="card-body pt-1">
                   <div>
                    <uc1:MyMsgBox ID="MyMsgBox" runat="server"/>
                   </div>
                   <fieldset class="" style="border:1px solid #bfbebe;padding:5px 20px 5px 20px;border-radius:6px">
                    <legend style="width:auto;border:0;font-size:14px;margin:0px 6px 0px 6px;padding:0px 5px 0px 5px;color:#0000FF"><b>Search</b></legend>

                    <div class="row">
                        <div class="form-group col-xl-2 col-lg-2 col-md-2 col-sm-6  mb-1">
                            <label for="WorkManSLNo" class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >UserID (P.No.) :</label>
                            <asp:TextBox ID="TxtSearch" runat="server" CssClass="form-control form-control-sm font-smaller"/>
                             <%--<asp:CustomValidator ID="CustomValidator4" runat="server" ClientValidationFunction="Validate" ValidationGroup="MRP" ControlToValidate="MobSetName" ValidateEmptyText="true"></asp:CustomValidator>--%>
                        </div>
                        <div class="form-group col-xl-2 col-lg-2 col-md-2 col-sm-6  mb-1">
                            <label for="WorkManSLNo" class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Location :</label>
                            <asp:DropDownList ID="txtLocation" runat="server" CssClass="form-control form-control-sm font-smaller" DataMember="Locations" DataSource="<%# PageDDLDataset %>" DataTextField="Location" DataValueField="LocationCode" AutoPostBack="true" OnSelectedIndexChanged="txtLocation_SelectedIndexChanged" />
                             <%--<asp:CustomValidator ID="CustomValidator4" runat="server" ClientValidationFunction="Validate" ValidationGroup="MRP" ControlToValidate="MobSetName" ValidateEmptyText="true"></asp:CustomValidator>--%>
                        </div>
                        <div class="form-group col-xl-2 col-lg-2 col-md-2 col-sm-6  mb-1">
                            <label for="WorkManSLNo" class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Site :</label>
                            <asp:DropDownList ID="txtSite" runat="server" CssClass="form-control form-control-sm font-smaller" DataMember="SiteByLocation" DataSource="<%# PageDDLDataset %>" DataTextField="Site" DataValueField="ID"/>
                             <%--<asp:CustomValidator ID="CustomValidator4" runat="server" ClientValidationFunction="Validate" ValidationGroup="MRP" ControlToValidate="MobSetName" ValidateEmptyText="true"></asp:CustomValidator>--%>
                        </div>
                        <div class="form-group col-xl-2 col-lg-2 col-md-3 col-sm-6  mb-1">
                            <label for="WorkManSLNo" class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Department:</label>
                            <asp:DropDownList ID="txtDepartment" runat="server" CssClass="form-control form-control-sm font-smaller" DataMember="Departments" DataSource="<%# PageDDLDataset %>" DataTextField="Department" DataValueField="ID" />
                             <%--<asp:CustomValidator ID="CustomValidator4" runat="server" ClientValidationFunction="Validate" ValidationGroup="MRP" ControlToValidate="MobSetName" ValidateEmptyText="true"></asp:CustomValidator>--%>
                        </div>

                        <div class="form-inline form-group col-xl-2 col-lg-2 col-md-4 col-sm-6 mb-1">
                            <div class="col-xs-5 pr-2">
                                <asp:Button ID="btnSearch" runat="server" Text="Search"  OnClick="btnSearch_Click" CssClass="btn btn-sm btn-primary" />
                            </div>
                            <div class="col-xs-5">
                                <asp:Button ID="BtnNew" runat="server"  Text="New" OnClick="BtnNew_Click" CssClass=" btn btn-success btn-sm" />
                            </div>
                            
                        </div>
                    </div>

                   </fieldset>

                  <!--  For Multiple Records Display -->

                  <fieldset class="" style="border:1px solid #bfbebe;padding:5px 20px 5px 20px;border-radius:6px">
                    <legend style="width:auto;border:0;font-size:14px;margin:0px 6px 0px 6px;padding:0px 5px 0px 5px;color:#0000FF"><b>Records</b></legend>

                      <div class="w-100 border" style="overflow:auto;height:280px;">
                          <cc1:detailscontainer ID="UserRelationViewRecords" runat="server" 
                            AutoGenerateColumns="False" AllowPaging="false" 
                            CellPadding="4" GridLines="None" Width="100%" DataMember="aspnet_Users_Department"
                             DataKeyNames="ID" 
                             DataSource="<%# PageRecordsDataSet %>" 
                             ForeColor="#333333" ShowHeaderWhenEmpty="True"
                             OnPageIndexChanging="UserRelationViewRecords_PageIndexChanging"
                             OnSelectedIndexChanged="UserRelationViewRecords_SelectedIndexChanged" OnRowDataBound="UserRelationViewRecords_RowDataBound"
                             PageSize="10" PagerSettings-Mode="Numeric" HeaderStyle-Font-Size="Small" RowStyle-Font-Size="13px">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="False" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="ID" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UserID" SortExpression="WorkManSLNo">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="UserID" runat="server" CommandName="select" style="color:blue;font-weight:700" Text='<%# Bind("UserID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Location" HeaderText="Location" 
                                     SortExpression="WorkMan_Name" HeaderStyle-HorizontalAlign="Left" 
                                     ItemStyle-HorizontalAlign="Left">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Site" HeaderText="Site" 
                                     SortExpression="VendorName" HeaderStyle-HorizontalAlign="Left" 
                                     ItemStyle-HorizontalAlign="Left">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DepartmentName" HeaderText="Department" 
                                     SortExpression="Department" HeaderStyle-HorizontalAlign="Left" 
                                     ItemStyle-HorizontalAlign="Left">
                                     <HeaderStyle HorizontalAlign="Left" />
                                     <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                
                               
                            
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
               
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" 
                                Font-Bold="True" CssClass="pager1" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Font-Bold="True" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="False" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </cc1:detailscontainer>
                      </div>

                  </fieldset>

                  <!--  For Single Record Display -->


                 <div id="DivInputFields" class=" mt-1 p-2 border rounded rounded-2" runat="server" style="display:none;border-color:#bfbebe;">
                   
                  <%--<fieldset">
                    <legend><b>Record Details and Input </b></legend>--%>
                       <div class="w-100 text-center  mb-2"><asp:Label runat="server" ID="Label1" class="m-0 mr-2 mt-1 p-1 col-form-label-sm  font-weight-bold  w-100 font-x-smaller label-bg-color text-light  text-center rounded" style="" >User Relation Details:-</asp:Label></div>
                       <cc1:formcointainer ID="UserRelationRecord" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                           OnNewRowCreatingEvent="UserRelationRecord_NewRowCreatingEvent"
                           PageSize="1" ShowHeader="False" Width="100%" DataSource="<%# PageRecordDataSet %>"
                           OnPreRender="UserRelationRecord_PreRender"
                           DataMember="aspnet_Users_Department" DataKeyNames="ID"
                           BindingErrorMessage="aaaaaaaaa" GridLines="None"
                           OnRowDataBound="UserRelationRecord_RowDataBound" Font-Size="XX-Small" > 
                        <Columns>
                            <asp:TemplateField SortExpression="ID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="ID" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="row" style="font-size:x-small">
                                       
                                        <div class="form-group col-lg-3 col-md-4 mb-1">
                                            <div class="">
                                                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >UserID (P.No.) :<span class="text-danger">*</span></label>
                                            </div>
                                            <div class="">
                                                <asp:DropDownList ID="UserID" runat="server" CssClass="form-control form-control-sm font-small" DataMember="EmployeeName" DataSource="<%# PageDDLDataset %>" DataTextField="Name" DataValueField="Pno" />
                                                <asp:RequiredFieldValidator ID="rvf_VendorName" class="invalid-tooltip" ValidationGroup="save" runat="server" ErrorMessage="Required" ControlToValidate="UserID" ForeColor="White" Font-Size="X-Small" InitialValue=""></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-2 col-md-4 mb-1">
                                            <div class="">
                                                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Location :<span class="text-danger">*</span></label>
                                            </div>
                                            <div class="">
                                                <asp:DropDownList ID="LocationCode" runat="server" CssClass="form-control form-control-sm font-small" DataMember="Locations" DataSource="<%# PageDDLDataset %>" DataTextField="Location" DataValueField="LocationCode" AutoPostBack="true" OnSelectedIndexChanged="Location_SelectedIndexChanged" />
                                                <asp:RequiredFieldValidator ID="rvf_Location" class="invalid-tooltip" ValidationGroup="save" runat="server" ErrorMessage="Required" ControlToValidate="LocationCode" ForeColor="White" Font-Size="X-Small" InitialValue=""></asp:RequiredFieldValidator>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" class="invalid-tooltip" ValidationGroup="BtnSave" runat="server" ErrorMessage="Required" ControlToValidate="Location" ForeColor="White" Font-Size="X-Small" InitialValue=""></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-2 col-md-4 mb-1">
                                            <div class="">
                                                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Site :<span class="text-danger">*</span></label>
                                            </div>
                                            <div class="">
                                                <asp:DropDownList ID="SiteID" runat="server" CssClass="form-control form-control-sm textboxstyle font-small" DataMember="SiteByLocation" DataSource="<%# PageDDLDataset %>" DataTextField="Site" DataValueField="ID"  />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" class="invalid-tooltip" ValidationGroup="save" runat="server" ErrorMessage="Required" ControlToValidate="SiteID" ForeColor="White" Font-Size="X-Small" InitialValue=""></asp:RequiredFieldValidator>
                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" class="invalid-tooltip" ValidationGroup="BtnSave" runat="server" ErrorMessage="Required" ControlToValidate="Site" ForeColor="White" Font-Size="X-Small" InitialValue=""></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>

                                        <div class="form-group col-lg-3 col-md-4 mb-1">
                                            <div class="">
                                                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Department :<span class="text-danger">*</span></label> 
                                            </div>
                                            <div class="">
                                                <asp:DropDownList ID="DepartmentID" runat="server" CssClass="form-control form-control-sm font-small" DataMember="Departments" DataSource="<%# PageDDLDataset %>" DataTextField="Department" DataValueField="ID"  />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" class="invalid-tooltip" ValidationGroup="save" runat="server" ErrorMessage="Required" ControlToValidate="DepartmentID" ForeColor="White" Font-Size="X-Small" InitialValue=""></asp:RequiredFieldValidator>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" class="invalid-tooltip" ValidationGroup="BtnSave" runat="server" ErrorMessage="Required" ControlToValidate="WorkOrderNo" ForeColor="White" Font-Size="X-Small" InitialValue="select"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        
                                       

                                    </div>

                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <PagerSettings Visible="False" />
                    </cc1:formcointainer>
                    
                    <%--<asp:Button runat="server" ID="BindGridBtn" style="display:none" OnClick="BindGridBtn_Click" />--%>

                    <%--<div class="w-100 text-center "><asp:Label runat="server" ID="AttendanceHeading" class="m-0 mr-2 mt-1 p-1 col-form-label-sm  font-weight-bold   font-x-smaller label-bg-color text-light  text-center rounded" Visible="false" >Attendance Details (Date Wise):-</asp:Label></div>--%>
                    <%--<div class="ddlcontainer mt-2 border border-lightdark">
                          <cc1:detailscontainer ID="UserRelationRecords" runat="server" AutoGenerateColumns="False"
                                    AllowPaging="True" CellPadding="0" GridLines="Both" Width="100%" DataMember="aspnet_Users_Department"
                                    DataKeyNames="ID" DataSource="<%# PageRecordDataSet %>"
                                    ForeColor="#333333" ShowHeaderWhenEmpty="False" 
                                    PageSize="31" PagerSettings-Visible="True" PagerStyle-HorizontalAlign="Center" 
                                    PagerStyle-Wrap="True" HeaderStyle-Font-Size="Smaller" RowStyle-Font-Size="XX-Small" OnRowDataBound="UserRelationRecords_RowDataBound" OnDataRowAddingEvent="UserRelationRecords_DataRowAddingEvent" CssClass="m-auto border border-info" 
                                    HeaderStyle-HorizontalAlign="Center" OnPreRender="UserRelationRecords_PreRender" RowStyle-ForeColor="Black"   >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField  Visible="False">
                                            <ItemTemplate >
                                                <asp:Label ID="ID" runat="server"></asp:Label>
                                                <asp:Label ID="UserID" runat="server"></asp:Label>
                                                <asp:Label ID="LocationCode" runat="server"></asp:Label>
                                                <asp:Label ID="SiteID" runat="server"></asp:Label>
                                                <asp:Label ID="DepartmentID" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="SlNo" SortExpression="SlNo" HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate >
                                                <asp:Label ID="SlNo" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Location" SortExpression="Location" HeaderStyle-Width="9%"  >
                                            <ItemTemplate>
                                                <asp:TextBox ID="LocationNM" runat="server" CssClass="form-control form-control-sm"  Width="95%" Font-Size="X-Small" Enabled="false"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Site" SortExpression="Location" HeaderStyle-Width="9%"  >
                                            <ItemTemplate>
                                                <asp:TextBox ID="SiteNM" runat="server" CssClass="form-control form-control-sm"  Width="95%" Font-Size="X-Small" Enabled="false"  />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department" SortExpression="WorkOrderNo" HeaderStyle-Width="10%" >
                                            <ItemTemplate>
                                               
                                                  <asp:TextBox ID="DeptNM" runat="server" CssClass="form-control form-control-sm " Font-Size="X-Small" Enabled="false"    />
                                             
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         
                                    </Columns>    
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                                    <HeaderStyle BackColor="#328cda" Font-Bold="True" ForeColor="White" />
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

                            
                            </div>--%>
                       
                 <%-- </fieldset>--%>



                  </div>




                  <div class="row no-gutters  mt-2">
                      <div class="col-md-4"></div>
                      <div class="col-md-4 justify-content-center d-flex">
                          <asp:Button ID="BtnSave" runat="server" Text="Submit" CssClass=" col-sm-3 btn btn-sm btn-primary mr-1"  OnClick="BtnSave_Click" ValidationGroup="save" />
                          <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass=" col-sm-3 btn btn-sm btn-danger ml-1"  OnClick="btnDelete_Click"  />
                      </div>
                      <div class="col-md-4"></div>
                  </div>
              </div>
           </div>
        
         
           
           
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

namespace CLMS.App.Master
{
    public partial class UserRelationMaster : Classes.basePage
    {
        App_UserRelationMasterDataset dsRecords = new App_UserRelationMasterDataset();
        App_UserRelationMasterDataset dsRecord = new App_UserRelationMasterDataset();

        DataSet dsDDL = new DataSet();
        BL_UserRelation blobj = new BL_UserRelation();

        protected override void SetBaseControls()
        {
            base.SetBaseControls();

            PageRecordsDataSet = dsRecords;
            PageRecordDataSet = dsRecord;
            PageDDLDataset = dsDDL;
            BLObject = new BL_UserRelation();

        }

        private StringDictionary GetFilterCondition()
        {
            StringDictionary d = null;
            d = new StringDictionary();
            //if (Session["UserName"] != null)
            //    d.Add("VendorCode", Session["UserName"].ToString());

            if (TxtSearch.Text != "")
                d.Add("UserID", TxtSearch.Text);

            if (txtLocation.SelectedIndex > 0)
                d.Add("LocationCode", txtLocation.SelectedValue);
            if (txtSite.SelectedIndex > 0)
                d.Add("SiteID", txtSite.SelectedValue);
            if (txtDepartment.SelectedIndex > 0)
                d.Add("DepartmentID", txtDepartment.SelectedValue);

            return d;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserRelationViewRecords.DataSource = PageRecordsDataSet;
            UserRelationRecord.DataSource = PageRecordDataSet;

            if (!IsPostBack)
            {
                GetRecords(GetFilterCondition(), UserRelationViewRecords.PageSize, 10, "");
                UserRelationViewRecords.DataBind();

                Dictionary<string, object> ddlParams = new Dictionary<string, object>();
                ddlParams.Add("Location", "abc");

                GetDropdowns("Locations");
                GetDropdowns("SiteByLocation", ddlParams);
                GetDropdowns("Departments");
                GetDropdowns("EmployeeName");

                txtLocation.DataBind();
                txtSite.DataBind();
                txtDepartment.DataBind();
            }
        }

        protected void UserRelationViewRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UserRelationViewRecords.PageIndex = e.NewPageIndex;
            GetRecords(GetFilterCondition(), UserRelationViewRecords.PageSize, 10, "");
            UserRelationViewRecords.DataBind();

            PageRecordDataSet.Clear();
            UserRelationRecord.BindData();
        }

        protected void UserRelationViewRecords_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageRecordDataSet.Clear();
            GetRecord(UserRelationViewRecords.SelectedDataKey.Value.ToString());
            UserRelationRecord.BindData();
        }

        protected void BtnNew_Click(object sender, EventArgs e)
        {
            PageRecordDataSet.Clear();
            UserRelationRecord.NewRecord();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetRecords(GetFilterCondition(), UserRelationViewRecords.PageSize, 10, "");
            //LocationFormID_Records.SelectedIndex = -1;
            UserRelationViewRecords.BindData();
            PageRecordDataSet.Clear();
            UserRelationRecord.BindData();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            UserRelationRecord.UnbindData();
            bool result = Save();
            if (result)
            {
                //MyMsgBox.show(MRPS.Control.MyMsgBox.MessageType.Success, "Data Save Successfully !");
                GetRecords(GetFilterCondition(), UserRelationViewRecords.PageSize, 0, "");

                PageRecordDataSet.Clear();

                UserRelationRecord.BindData();
                UserRelationViewRecords.BindData();
                MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Success, "Record saved successfully !");
            }
            else
            {
                MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors, "Error while saving !");

            }
        }

        protected void UserRelationRecord_NewRowCreatingEvent(DataRow oRow,ref bool cancel)
        {
            oRow["ID"] = System.Guid.NewGuid();
            oRow["CreatedBy"] = Session["UserName"].ToString();
            oRow["CreatedOn"] = System.DateTime.Now;
        }

        protected void UserRelationRecord_PreRender(object sender, EventArgs e)
        {

        }

        protected void UserRelationRecord_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType.ToString().ToUpper() == "DATAROW")
            {
                DataRow oRow = ((DataRowView)e.Row.DataItem).Row;
                System.Web.UI.Control oSiteID = e.Row.FindControl("SiteID");

                Dictionary<string, object> ddlParam = new Dictionary<string, object>();
                ddlParam.Add("Location", oRow["LocationCode"].ToString());
                GetDropdowns("SiteByLocation", ddlParam);

                ((DropDownList)oSiteID).DataBind();
                ((DropDownList)oSiteID).SelectedValue = oRow["SiteID"].ToString();

            }
        }

        protected void UserRelationViewRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void Location_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(((DropDownList)sender).SelectedIndex > 0)
            {
                Dictionary<string, object> ddlParam = new Dictionary<string, object>();
                ddlParam.Add("Location", ((DropDownList)sender).SelectedValue);
                GetDropdowns("SiteByLocation", ddlParam);

                ((DropDownList)UserRelationRecord.Rows[0].FindControl("SiteID")).DataBind();
            }
        }

        protected void Site_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void Page_PreRenderComplete(object sender, EventArgs e)
        {
            DivInputFields.Style.Add("display", ((PageRecordDataSet.Tables["aspnet_Users_Department"].Rows.Count > 0) ? "block" : "none"));
           
            BtnNew.Visible =  PagePermission.AllowWrite;
            UserRelationRecord.Enabled = PagePermission.AllowWrite;
            if (UserRelationViewRecords.SelectedIndex == -1)
                BtnSave.Enabled = (UserRelationRecord.Rows.Count > 0) && PagePermission.AllowWrite;
            else
                BtnSave.Enabled = (UserRelationRecord.Rows.Count > 0) && PagePermission.AllowModify;

            
            btnDelete.Enabled = (UserRelationRecord.Rows.Count > 0 && UserRelationViewRecords.SelectedIndex != -1) && PagePermission.AllowDelete;

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            bool result = Delete();
            if (result)
            {
                MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Success, "Data Deleted Successfully !");
                GetRecords(GetFilterCondition(), UserRelationViewRecords.PageSize, 10, "");
                UserRelationViewRecords.BindData();
                UserRelationRecord.BindData();
            }
            else
            {
                MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors, "Error while deleting !");
            }
        }

        protected void txtLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((DropDownList)sender).SelectedIndex > 0)
            {
                Dictionary<string, object> ddlParam = new Dictionary<string, object>();
                ddlParam.Add("Location", ((DropDownList)sender).SelectedValue);
                GetDropdowns("SiteByLocation", ddlParam);

                txtSite.DataBind();
            }
        }
    }
}
-------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using BusinessData;

namespace BusinessLayer
{
    public class BL_UserRelation : IBLDataProvider
    {
        public DataSet GetDropdowns(string DropdownNames, Dictionary<string, object> ddlParam)
        {
            return DropDownHelper.GetDropDownsDataset(DropdownNames, ddlParam, false);
        }

        public DataSet GetRecord(string recordId)
        {
            string strSQL = "select * FROM aspnet_Users_Department where ID=@ID";
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("ID", recordId);

            DataHelper dh = new DataHelper();
            
            return dh.GetDataset(strSQL, "aspnet_Users_Department", objParam);
        }

        public DataSet GetRecords(StringDictionary FilterConditions, int totalPagesize, string sortExpression)
        {
            string strSQL = "select " + (totalPagesize == 0 ? "" : "top " + totalPagesize.ToString()) + " aud.*,LM.Location,SM.Site,DM.DepartmentName FROM aspnet_Users_Department as aud left join App_LocationMaster as LM on LM.LocationCode = aud.LocationCode left join App_SiteMaster as SM on SM.ID = aud.SiteID left join App_DepartmentMaster as DM on DM.DepartmentCode = aud.DepartmentID ";
            DataHelper dh = new DataHelper();
            Dictionary<string, object> objParam = null;
            int count = FilterConditions.Count;
            if (FilterConditions != null && FilterConditions.Count > 0)
            {
                strSQL += " where ";
                objParam = new Dictionary<string, object>();
                foreach (string dickey in FilterConditions.Keys)
                {
                    objParam.Add(dickey, FilterConditions[dickey] + '%');
                    if (count > 0 && count != FilterConditions.Count)
                        strSQL += " and aud." + dickey + " like @" + dickey;
                    else
                        strSQL += " aud."+dickey + " like @" + dickey;

                    count--;
                }
            }

            strSQL += " order by UserID desc";
            return dh.GetDataset(strSQL, "aspnet_Users_Department", objParam);
        }

        public bool SaveRecord(ref DataSet recordDataset)
        {
            DataHelper dh = new DataHelper();
            bool result = dh.SaveData(recordDataset);
            return result;
        }
    }
}
