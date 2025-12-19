using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Configuration;
using System.Web.UI.HtmlControls;
namespace CLMS.App.Input
{
    public partial class WorkOrder_Exemption_Approval : Classes.basePage
    {
        App_WorkOrder_Exemption_Ds dsRecords = new App_WorkOrder_Exemption_Ds();
        App_WorkOrder_Exemption_Ds dsRecord = new App_WorkOrder_Exemption_Ds();

        DataSet dsDDL = new DataSet();



        protected override void SetBaseControls()
        {
            base.SetBaseControls();

            PageRecordsDataSet = dsRecords;
            PageRecordDataSet = dsRecord;
            PageDDLDataset = dsDDL;
            BLObject = new BL_WorkOrder_Exemption_Approval();
        }

        private StringDictionary GetFilterCondition()
        {
            StringDictionary d = null;
            d = new StringDictionary();
            if (Enter_Detail.Text != "")
            {
                d.Add("Enter_Detail", Enter_Detail.Text.Trim());
            }
            if (STATUS_search.SelectedIndex >= 0)
            {
                d.Add("Status", STATUS_search.SelectedValue.Trim());
            }
            if (Search_With.SelectedIndex > 0)
            {
                d.Add("Search_With", Search_With.SelectedValue.Trim());
            }

            return d;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            WorkOrder_Exemption_Records.DataSource = PageRecordsDataSet;
            WorkOrder_Exemption_Record.DataSource = PageRecordDataSet;
            Action_Record.DataSource = PageRecordDataSet;
            if (!IsPostBack)
            {

                GetRecords(GetFilterCondition(), WorkOrder_Exemption_Records.PageSize, 10, "");
                WorkOrder_Exemption_Records.BindData();
                btnSave.Visible = false;
                div_newRemarks.Visible = false;


            }
        }

        protected void WorkOrder_Exemption_Records_SelectedIndexChanged(object sender, EventArgs e)
        {


            GetRecord(WorkOrder_Exemption_Records.SelectedDataKey.Value.ToString());
            WorkOrder_Exemption_Record.BindData();


            Dictionary<string, object> ddlParams = new Dictionary<string, object>();
            ddlParams.Add("ID", WorkOrder_Exemption_Records.SelectedDataKey.Value.ToString());
            GetDropdowns("CC_Wo_No", ddlParams);





            Action_Record.DataBind();


           

            btnSave.Visible = false;

        

            BL_WorkOrder_Exemption_Approval blobj = new BL_WorkOrder_Exemption_Approval();
            DataSet ds = new DataSet();
            string ID = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["ID"].ToString();
            ds = blobj.BindRemarks(ID);
            Remarks_grid.DataSource = ds.Tables[0];
            Remarks_grid.DataBind();



        

            WorkOrder_Exemption_Record.Visible = true;
            Action_Record.Visible = true;
            ((HtmlGenericControl)Action_Record.Rows[0].FindControl("div_exp_cc")).Visible = false;
            //((HtmlGenericControl)Action_Record.Rows[0].FindControl("div_newRemarks")).Visible = false;
            ((HtmlGenericControl)Action_Record.Rows[0].FindControl("WoDiv")).Visible = false;

            if (PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Status"].ToString() == "Approved")
            {
                //WorkOrder_Exemption_Record.Enabled = false;
                //Action_Record.Visible=false;

                //j
                WorkOrder_Exemption_Record.DataBind();
                ((TextBox)WorkOrder_Exemption_Record.Rows[0].FindControl("WorkOrderNo")).Enabled = false;
                ((TextBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Exemption_Vendor")).Enabled = false;
                ((BulletedList)WorkOrder_Exemption_Record.Rows[0].FindControl("Bull_Attach")).Enabled = false;
                btnSave.Visible = false;
                ((TextBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Remarks")).Enabled = false;
                Action_Record.Visible = false;
            }
            else if (PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Status"].ToString() == "Pending With CC")
            {

                //WorkOrder_Exemption_Record.Enabled = false;
                //Action_Record.Enabled = true;
                //btnSave.Visible = true;

                //j
                WorkOrder_Exemption_Record.DataBind();
                ((TextBox)WorkOrder_Exemption_Record.Rows[0].FindControl("WorkOrderNo")).Enabled = false;
                ((TextBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Exemption_Vendor")).Enabled = false;
                ((BulletedList)WorkOrder_Exemption_Record.Rows[0].FindControl("Bull_Attach")).Enabled = false;
                btnSave.Visible = true;
                ((TextBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Remarks")).Enabled = false;
                Action_Record.Enabled = true;


            }
            else if (PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Status"].ToString() == "Return")
            {
                //WorkOrder_Exemption_Record.Enabled = false;
                //Action_Record.Visible = false;

                //j
                WorkOrder_Exemption_Record.DataBind();
                ((TextBox)WorkOrder_Exemption_Record.Rows[0].FindControl("WorkOrderNo")).Enabled = false;
                ((TextBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Exemption_Vendor")).Enabled = false;
                ((BulletedList)WorkOrder_Exemption_Record.Rows[0].FindControl("Bull_Attach")).Enabled = false;
                btnSave.Visible = false;
                ((TextBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Remarks")).Enabled = false;
                Action_Record.Visible = false;

            }
            else
            {

            }
        }




        protected void ReturnAttachment_Click(object sender, BulletedListEventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            if (filePath != "")
            {
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.TransmitFile(@"D:/Cybersoft_Doc/CLMS/Attachments/" + filePath);
                Response.End();
            }
            else
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('There is no file to Download');", true);
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {



            WorkOrder_Exemption_Record.UnbindData();
            Action_Record.UnbindData();

     







            string selectedWorkorder = string.Join(",", ((CheckBoxList)Action_Record.Rows[0].FindControl("CC_WorkOrderNo")).Items.
           Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value));

            string Workordercomma = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["CC_WorkOrderNo"].ToString();
            string WorkOrderTrim = Workordercomma.TrimEnd(',');
            PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["CC_WorkOrderNo"] = WorkOrderTrim;



            //string Remarks_CC = ((TextBox)Action_Record.Rows[0].FindControl("NewRemarks")).Text;

            //PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Remarks"] = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Remarks"].ToString() + "( CC --" + System.DateTime.Now.ToString("dd/MM/yyyy") + " -- " + Remarks_CC + ")|";




            //string Remarks_CC = NewRemarks.Text;



            DataTable remarksTable = PageRecordDataSet.Tables["App_WorkOrder_exemption_remarks"];
            Guid masterID = Guid.Parse(PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["ID"].ToString());

           
                DataRow newRow = remarksTable.NewRow();

                newRow["ID"] = Guid.NewGuid();



                string getFileName = "";
                List<string> FileList = new List<string>();
                if (((FileUpload)Attachment).HasFile)
                {
                    foreach (HttpPostedFile htfiles in ((FileUpload)Attachment).PostedFiles)
                    {
                        getFileName = newRow["ID"].ToString() + "_" + Path.GetFileName(htfiles.FileName);
                        getFileName = Regex.Replace(getFileName, @"[,+*/?|><&=\#%:;@[^$?:'()!~}{`]", "");
                        htfiles.SaveAs((@"D:/Cybersoft_Doc/CLMS/Attachments/" + getFileName));
                        FileList.Add(getFileName);
                    }
                    newRow["Attachment"] = string.Join(",", FileList);
                }






                newRow["MASTER_ID"] = masterID;
                newRow["Remarks"] = NewRemarks.Text;
                newRow["CreatedOn"] = DateTime.Now;
                newRow["CreatedBy"] = Session["UserName"].ToString();
            


                remarksTable.Rows.Add(newRow);
            







            if (PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Status"].ToString() == "Approved")
            {
                PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Approved_On"] = System.DateTime.Now;
                PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Approved_By"] = Session["UserName"].ToString();
            }





            bool result = Save();

            if (result)
            {

                if (PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Status"].ToString() == "Approved")
                {
                    string subject = "";
                    string msg = "";
                    string to = "";

                    DataSet ds = new DataSet();
                    DataSet ds1 = new DataSet();
                    BL_Send_Mail blobj = new BL_Send_Mail();
                    ds = blobj.Get_Vendor_mail(PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["VendorCode"].ToString());
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        to = ds.Tables["aspnet_Membership"].Rows[0]["Email"].ToString();
                    }
                    string v_name = "";

                    ds1 = blobj.Get_Vendor_name(PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["VendorCode"].ToString());
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        v_name = ds1.Tables["App_vendor_reg"].Rows[0]["v_name"].ToString();
                    }

                    string v_code = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["VendorCode"].ToString();
                    string remarks = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Remarks"].ToString();
                    string wo_no = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["WorkOrderNo"].ToString();



                    //subject = "Application of Work Order Exemption of work order no " + wo_no + " has been approved of M/S " + v_name + "(Vendor Code – " + v_code + ") ";
                    //msg = "<html><body><P><B>"
                    //    + "To<BR /> "
                    //    + v_name
                    //    + "<BR />" + v_code
                    //    + "<BR /><BR />Dear Sir/Madam"
                    //    + "<BR /> "

                    //    + "Based on submission of your documents and details, your application against the Work Order Exemption of work order no " + wo_no + " has been approved by Contractor Cell with remarks (" + remarks + ")."
                    //    + " <BR />Point is to be noted and recorded that approval is based on your details/documents which is submitted to us. "

                    //    + "<BR />You may visit and open link  :-  https://services.tsuisl.co.in/CLMS/Account/Login.aspx  or you may approach Contractors’ Cell at 0657-6652063/ 6652064 for clarifications, if any. "
                    //    + " <BR /> " + " <BR /> Thanks & Regards " + " <BR /> "
                    //    + " <BR /> TATA STEEL UISL Contractor's Cell </B></P></html></body>"
                    //    + " <BR /> "
                    //    + " <BR /> Note: Please do not reply as it is a system generated mail. ";


                    subject = "Conditional Approval for Interim Bill Submission – M/s " + v_name +  " (Vendor Code: " + v_code + ") – Work Order No(s): " + wo_no;

                    msg = "<html><body><p>"
                            + "<b>To:</b> M/s " + v_name + "<br/>"
                            + "<b>Vendor Code:</b> " + v_code + "<br/><br/>"
                            + "Dear Sir/Madam,<br/><br/>"
                            + "Your application submitted through the “WorkOrder Exemption (Billing)” module in CLMS for interim bill submission against Work Order No(s): "
                            + wo_no + " has been conditionally approved by the Contractors’ Cell.<br/><br/>"
                            + "<b>Remarks:</b> " + remarks + "<br/><br/>"
                            + "Please note that this approval is based on the documents and information provided by you.<br/>"
                            + "Kindly ensure that all compliance requirements are fulfilled within the stipulated time.<br/><br/>"
                            + "You may view the status of your application at: "
                            + "<a href='https://services.tsuisl.co.in/CLMS'>https://services.tsuisl.co.in/CLMS</a><br/><br/>"
                            + "For any queries, you may contact the Contractors’ Cell at 0657-6652063 / 6652064.<br/><br/>"
                            + "Thanks & Regards,<br/>"
                            + "<b>TATA Steel UISL</b><br/>"
                            + "<b>Contractors’ Cell</b><br/><br/>"
                            + "<i>Note: This is a system-generated email. Please do not reply.</i>"
                            + "</p></body></html>";





                    BL_Send_Mail blobj1 = new BL_Send_Mail();
                    blobj1.sendmail_approve(to, "", subject, msg, "");

                }
                else if (PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Status"].ToString() == "Return")
                {









                    string subject = "";
                    string msg = "";
                    string to = "";

                    DataSet ds = new DataSet();
                    DataSet ds1 = new DataSet();
                    BL_Send_Mail blobj = new BL_Send_Mail();
                    ds = blobj.Get_Vendor_mail(PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["VendorCode"].ToString());
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        to = ds.Tables["aspnet_Membership"].Rows[0]["Email"].ToString();
                    }
                    string v_name = "";

                    ds1 = blobj.Get_Vendor_name(PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["VendorCode"].ToString());
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        v_name = ds1.Tables["App_vendor_reg"].Rows[0]["v_name"].ToString();
                    }

                    string v_code = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["VendorCode"].ToString();
                    string remarks = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Remarks"].ToString();
                    string wo_no = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["WorkOrderNo"].ToString();



                    //subject = "Application of Work Order Exemption of work order no " + wo_no + " has been approved of M/S " + v_name + "(Vendor Code – " + v_code + ") ";
                    //msg = "<html><body><P><B>"
                    //    + "To<BR /> "
                    //    + v_name
                    //    + "<BR />" + v_code
                    //    + "<BR />Dear Sir/Madam"
                    //    + "<BR /> "

                    //    + "Application against the Work Order Exemption of Work Order No " + wo_no + " has been retuned because of (" + remarks + "). Kindly resubmit the application along with correct data/ details/ attachments as per remarks given by Contractors’ Cell. "

                    //    + "<BR />You may visit and open link  :-  https://services.tsuisl.co.in/CLMS/Account/Login.aspx  or you may approach Contractors’ Cell at 0657-6652063/ 6652064 for clarifications, if any. "
                    //    + " <BR /> " + " <BR /> Thanks & Regards " + " <BR /> "
                    //    + " <BR /> TATA STEEL UISL Contractor's Cell </B></P></html></body>"
                    //    + " <BR /> "
                    //    + " <BR /> Note: Please do not reply as it is a system generated mail. ";


                    subject = "Application Returned – Interim Bill Submission Request – M/s " + v_name + " (Vendor Code: " + v_code + ") – Work Order No(s): " + wo_no;

                    msg = "<html><body><p>"
                      + "<b>To:</b> M/s " + v_name + "<br/>"
                      + "<b>Vendor Code:</b> " + v_code + "<br/><br/>"
                      + "Dear Sir/Madam,<br/><br/>"
                      + "Your application submitted through the “WorkOrder Exemption (Billing)” module in CLMS for interim bill submission against Work Order No(s): "
                      + wo_no + " has been <b>returned</b> by the Contractors’ Cell.<br/><br/>"
                      + "<b>Remarks:</b> " + remarks + "<br/><br/>"
                      + "You are requested to review the remarks and resubmit the application with the necessary corrections or additional information/documents.<br/><br/>"
                      + "You may access the application at: "
                      + "<a href='https://services.tsuisl.co.in/CLMS'>https://services.tsuisl.co.in/CLMS</a><br/><br/>"
                      + "For assistance, please contact the Contractors’ Cell at 0657-6652063 / 6652064.<br/><br/>"
                      + "Thanks & Regards,<br/>"
                      + "<b>TATA Steel UISL</b><br/>"
                      + "<b>Contractors’ Cell</b><br/><br/>"
                      + "<i>Note: This is a system-generated email. Please do not reply.</i>"
                      + "</p></body></html>";



                    BL_Send_Mail blobj1 = new BL_Send_Mail();
                    blobj1.sendmail_approve(to, "", subject, msg, "");


                }

                PageRecordDataSet.Clear();
                remarksTable.Clear();
                NewRemarks.Text = "";
                WorkOrder_Exemption_Record.BindData();
                Action_Record.BindData();
                GetRecords(GetFilterCondition(), WorkOrder_Exemption_Records.PageSize, 10, "");
                WorkOrder_Exemption_Records.BindData();

                MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Success, "Record saved successfully !");
                //hideDiv.Visible = false;
                btnSave.Visible = false;
                div_newRemarks.Visible = false;
                div_ReturnAttachment.Visible = false;
                Remarks_grid.Visible = false;
                //Response.Redirect(Request.RawUrl);

            }
        }

        protected void Remarks_grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.Cells[1].Text.StartsWith("( VENDOR --")) || (e.Row.Cells[1].Text.StartsWith("\n( VENDOR --")))
            {
                e.Row.Attributes.CssStyle.Value = "color: Blue;Font-weight:bold";
            }
            if ((e.Row.Cells[1].Text.StartsWith("( CC --")) || (e.Row.Cells[1].Text.StartsWith("\n( CC --")))
            {
                e.Row.Attributes.CssStyle.Value = "color: DarkRed;Font-weight:bold";
            }
            if ((e.Row.Cells[1].Text.StartsWith("( CC --")) || (e.Row.Cells[1].Text.StartsWith("\n( CC --")))
            {
                e.Row.Attributes.CssStyle.Value = "color: DarkRed;Font-weight:bold";
            }




            if (string.Equals(e.Row.RowType.ToString(), "DATAROW", StringComparison.CurrentCultureIgnoreCase))
            {
                DataRow oRow = ((DataRowView)e.Row.DataItem).Row;

                System.Web.UI.Control Attachment = e.Row.FindControl("Attachment");

                if (Attachment != null)
                {
                    string[] attachments;
                    if (!string.IsNullOrEmpty(oRow["Attachment"].ToString()))
                    {
                        attachments = oRow["Attachment"].ToString().Split(',');


                        foreach (string image in attachments)
                        {
                            ListItem link = new ListItem();
                            //link.Value = "~/Attachments/" + image;
                            link.Value = "~/FileDownloadHandler.ashx?file=" + Server.UrlEncode(image);
                            link.Text = image.Substring(37);
                            link.Attributes.CssStyle.Add("text-decoration", "underline");
                            link.Attributes.CssStyle.Add("color", "blue");

                            ((BulletedList)Attachment).Items.Add(link);
                        }
                    }
                }

                else
                {

                }



            }




        }

        //protected void AttachDownload_ServerClick(object sender, EventArgs e)
        //{
        //    string filename = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Attachment"].ToString();

        //    if (!string.IsNullOrEmpty(filename))
        //    {
        //        string filepath = Server.MapPath("~/Attachments/" + filename);
        //        if (File.Exists(filepath))
        //        {
        //            Response.Clear();

        //            Response.ContentType = MimeMapping.GetMimeMapping(filename);
        //            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(filename) + "\"");
        //            Response.TransmitFile(filepath);
        //            Response.Flush();
        //            Response.End();
        //        }
        //        else
        //        {

        //            MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Success, "File not found.");
        //        }
        //    }
        //    else
        //    {
        //        MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors, "File not found.");
        //    }
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetRecords(GetFilterCondition(), WorkOrder_Exemption_Records.PageSize, 10, "");
            WorkOrder_Exemption_Records.BindData();
            PageRecordDataSet.Tables[WorkOrder_Exemption_Records.DataMember].Rows.Clear();
            WorkOrder_Exemption_Records.BindData();
            PageRecordDataSet.Tables[WorkOrder_Exemption_Record.DataMember].Rows.Clear();
            WorkOrder_Exemption_Record.BindData();
            PageRecordDataSet.Tables[Action_Record.DataMember].Rows.Clear();
            Action_Record.BindData();
            Remarks_grid.DataBind();
            return;
        }

        protected void WorkOrder_Exemption_Record_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (WorkOrder_Exemption_Record.Rows.Count > 0)
            {

                //TextBox txtWO = (TextBox)WorkOrder_Exemption_Record.Rows[0].FindControl("WorkOrderNo");

                //CheckBoxList chkWO = (CheckBoxList)Action_Record.Rows[0].FindControl("cc_wo");

                //if (chkWO != null)
                //    chkWO.Items.Clear();

                //if (txtWO != null && !string.IsNullOrWhiteSpace(txtWO.Text))
                //{
                //    string[] workorders = txtWO.Text.Split(new char[] { ',', '|' },
                //                                           StringSplitOptions.RemoveEmptyEntries);

                //    foreach (string wo in workorders)
                //    {
                //        chkWO.Items.Add(new ListItem(wo.Trim(), wo.Trim()));
                //    }

                //    foreach (ListItem item in chkWO.Items)
                //        item.Selected = true;
                //}


                //TextBox txtWO = (TextBox)WorkOrder_Exemption_Record.Rows[0].FindControl("WorkOrderNo");

                //if (txtWO != null)
                //{
                //    ViewState["WORKORDER_LIST"] = txtWO.Text;
                //}


                ((BulletedList)WorkOrder_Exemption_Record.Rows[0].FindControl("Bull_Attach")).Items.Clear();
                    string[] attachments;
                    if (!string.IsNullOrEmpty(PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Attachment"].ToString()))
                    {
                        attachments = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Attachment"].ToString().Split(',');

                        foreach (string image in attachments)
                        {
                            ListItem link = new ListItem();
                            //link.Value = "~/Attachments/" + image;
                            link.Value = "~/FileDownloadHandler.ashx?file=" + Server.UrlEncode(image);
                            link.Text = image;
                            link.Attributes.CssStyle.Add("text-decoration", "underline");
                            link.Attributes.CssStyle.Add("color", "blue");
                            link.Text = image.Substring(37);
                            ((BulletedList)WorkOrder_Exemption_Record.Rows[0].FindControl("Bull_Attach")).Items.Add(link);
                        }




                    }




            }
        }


     


        protected void Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stat = ((RadioButtonList)Action_Record.Rows[0].FindControl("Status")).SelectedValue;
            if (stat == "Approved")
            {

              





                ((HtmlGenericControl)Action_Record.Rows[0].FindControl("div_exp_cc")).Visible = true;
              //  ((HtmlGenericControl)Action_Record.Rows[0].FindControl("div_newRemarks")).Visible = true;
                ((HtmlGenericControl)Action_Record.Rows[0].FindControl("WoDiv")).Visible = true;
                div_newRemarks.Visible = true;
                div_ReturnAttachment.Visible = false;
                btnSave.Visible = true;

            }
            else if (stat == "Return")
            {
                ((HtmlGenericControl)Action_Record.Rows[0].FindControl("div_exp_cc")).Visible = false;
                //  ((HtmlGenericControl)Action_Record.Rows[0].FindControl("div_newRemarks")).Visible = true;
                div_newRemarks.Visible = true;
                div_ReturnAttachment.Visible = true;
                //  ((HtmlGenericControl)Action_Record.Rows[0].FindControl("div_ReturnAttachment")).Visible = true;
                ((HtmlGenericControl)Action_Record.Rows[0].FindControl("WoDiv")).Visible = false;
                btnSave.Visible = true;

            }
        }
    }
}