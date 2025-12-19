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
    public partial class WorkOrder_Exemption : Classes.basePage
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
            BLObject = new BL_WorkOrder_Exemption();
        }

        private StringDictionary GetFilterCondition()
        {
            StringDictionary d = null;
            d = new StringDictionary();
            d.Add("VendorCode", Session["UserName"].ToString());
            if (txt_WorkOrderNo.Text != "")
            {
                d.Add("WorkOrderNo", txt_WorkOrderNo.Text);
            }
            return d;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            WorkOrder_Exemption_Records.DataSource = PageRecordsDataSet;
            WorkOrder_Exemption_Record.DataSource = PageRecordDataSet;

            if (!IsPostBack)
            {

                GetRecords(GetFilterCondition(), WorkOrder_Exemption_Records.PageSize, 10, "");
                WorkOrder_Exemption_Records.BindData();
                DivInputFields.Visible = false;
                WorkOrder_Exemption_Record.NewRecord();
                WorkOrder_Exemption_Record.BindData();
                Dictionary<string, object> ddlParams = new Dictionary<string, object>();
                ddlParams.Add("V_Code", Session["UserName"].ToString());
                GetDropdowns("Exp_Wo_No", ddlParams);


            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            PageRecordDataSet.Clear();
            GetRecords(GetFilterCondition(), WorkOrder_Exemption_Records.PageSize, 10, "");
            WorkOrder_Exemption_Records.SelectedIndex = -1;
            WorkOrder_Exemption_Records.BindData();
            PageRecordDataSet.Tables[WorkOrder_Exemption_Record.DataMember].Rows.Clear();
            WorkOrder_Exemption_Record.BindData();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "getClickedElement();", true);


            WorkOrder_Exemption_Records.SelectedIndex = -1;
            PageRecordDataSet.EnforceConstraints = false;
            WorkOrder_Exemption_Record.NewRecord();
            WorkOrder_Exemption_Record.BindData();
            DivInputFields.Visible = true;
            btnSave.Visible = true;
            btnSave.Enabled = true;
            Attachment.Enabled = true;
            Remarks.Enabled = true;
            Remarks.Text = "";


            BL_WorkOrder_Exemption blobj = new BL_WorkOrder_Exemption();
            DataSet ds = new DataSet();
            ds = blobj.Getvendor_Name(Session["UserName"].ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["VendorCode"] = Session["UserName"].ToString();
                PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["VendorName"] = ds.Tables["App_VendorMaster"].Rows[0]["V_NAME"].ToString();
            }


        }

        protected void WorkOrder_Exemption_Records_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            WorkOrder_Exemption_Records.PageIndex = e.NewPageIndex;
            GetRecords(GetFilterCondition(), WorkOrder_Exemption_Records.PageSize, WorkOrder_Exemption_Records.PageIndex, "");
            WorkOrder_Exemption_Records.SelectedIndex = -1;

            WorkOrder_Exemption_Records.BindData();
            PageRecordDataSet.Tables[WorkOrder_Exemption_Records.DataMember].Rows.Clear();
            WorkOrder_Exemption_Records.BindData();
        }

        protected void WorkOrder_Exemption_Records_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetRecord(WorkOrder_Exemption_Records.SelectedDataKey.Value.ToString());
            WorkOrder_Exemption_Records.SelectedIndex = -1;


            BL_WorkOrder_Exemption_Approval blobj = new BL_WorkOrder_Exemption_Approval();
            DataSet ds = new DataSet();
            string ID = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["ID"].ToString();
            ds = blobj.BindRemarks(ID);
            Remarks_grid.DataSource = ds.Tables[0];
            Remarks_grid.DataBind();



            WorkOrder_Exemption_Record.BindData();
            DivInputFields.Visible = true;
            btnSave.Visible = true;
            btnSave.Enabled = true;



        }

        protected void WorkOrder_Exemption_Record_NewRowCreatingEvent(System.Data.DataRow oRow, ref bool cancel)
        {
            oRow["ID"] = Guid.NewGuid();
            oRow["CreatedBy"] = Session["UserName"].ToString();
            oRow["CreatedOn"] = System.DateTime.Now;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {


            PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Wage"] = 0;
            PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["PfEsi"] = 0;
            PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Leave"] = 0;
            PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Bonus"] = 0;
            PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["LL"] = 0;
            PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Grievance"] = 0;
            PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Notice"] = 0;


            if (((CheckBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Wage")).Checked)
                PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Wage"] = 1;

            if (((CheckBox)WorkOrder_Exemption_Record.Rows[0].FindControl("PfEsi")).Checked)
                PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["PfEsi"] = 1;

            if (((CheckBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Leave")).Checked)
                PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Leave"] = 1;

            if (((CheckBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Bonus")).Checked)
                PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Bonus"] = 1;

            if (((CheckBox)WorkOrder_Exemption_Record.Rows[0].FindControl("LL")).Checked)
                PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["LL"] = 1;

            if (((CheckBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Grievance")).Checked)
                PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Grievance"] = 1;

            if (((CheckBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Notice")).Checked)
                PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Notice"] = 1;






            string selectedWorkorder = string.Join(",", ((CheckBoxList)WorkOrder_Exemption_Record.Rows[0].FindControl("WorkOrderNo")).Items.
            Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value));

            //PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["WorkOrderNo"] = selectedWorkorder;


            //string strRemarks = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Remarks"].ToString();
            
            
            WorkOrder_Exemption_Record.UnbindData();
            BL_WorkOrder_Exemption blobj = new BL_WorkOrder_Exemption();

            string Workordercomma = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["WorkOrderNo"].ToString();
            string WorkOrderTrim = Workordercomma.TrimEnd(',');
            PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["WorkOrderNo"] = WorkOrderTrim;



            string getFileName2 = "";
            List<string> FileList2 = new List<string>();
            if (((FileUpload)WorkOrder_Exemption_Record.Rows[0].FindControl("DbstsAttachment")).HasFile)
            {
                foreach (HttpPostedFile htfiles in ((FileUpload)WorkOrder_Exemption_Record.Rows[0].FindControl("DbstsAttachment")).PostedFiles)
                {
                    getFileName2 = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["ID"].ToString() + "_" + Path.GetFileName(htfiles.FileName);
                    getFileName2 = Regex.Replace(getFileName2, @"[,+*/?|><&=\#%:;@[^$?:'()!~}{`]", "");
                    htfiles.SaveAs((@"D:/Cybersoft_Doc/CLMS/Attachments/" + getFileName2));
                    FileList2.Add(getFileName2);
                }
                PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["DbstsAttachment"] = string.Join(",", FileList2);
            }



            string getFileName3 = "";
            List<string> FileList3 = new List<string>();
            if (((FileUpload)WorkOrder_Exemption_Record.Rows[0].FindControl("ComplianceAttachment")).HasFile)
            {
                foreach (HttpPostedFile htfiles in ((FileUpload)WorkOrder_Exemption_Record.Rows[0].FindControl("ComplianceAttachment")).PostedFiles)
                {
                    getFileName3 = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["ID"].ToString() + "_" + Path.GetFileName(htfiles.FileName);
                    getFileName3 = Regex.Replace(getFileName3, @"[,+*/?|><&=\#%:;@[^$?:'()!~}{`]", "");
                    htfiles.SaveAs((@"D:/Cybersoft_Doc/CLMS/Attachments/" + getFileName3));
                    FileList3.Add(getFileName3);
                }
                PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["ComplianceAttachment"] = string.Join(",", FileList3);
            }







            DataTable remarksTable = PageRecordDataSet.Tables["App_WorkOrder_exemption_remarks"];
            Guid masterID = Guid.Parse(PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["ID"].ToString());

            if (remarksTable.Rows.Count == 0)
            {
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
                newRow["Remarks"] = Remarks.Text;
                newRow["CreatedOn"] = DateTime.Now;
                newRow["CreatedBy"] = Session["UserName"].ToString();



                remarksTable.Rows.Add(newRow);
            }




            PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Status"] = "Pending With CC";

            if (PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0].RowState.ToString() == "Modified")
            {
                PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["ResubmittedOn"] = System.DateTime.Now;
                PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["ResubmittedBy"] = Session["UserName"].ToString();
            }











            //if (PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Remarks"].ToString() == "")
            //{
            //    PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Remarks"] = strRemarks + "( VENDOR --" + System.DateTime.Now.ToString("dd/MM/yyyy")
            //        + " -- " + ((TextBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Remarks")).Text + ")|";
            //}
            //else
            //{
            //    PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Remarks"] = strRemarks + "( VENDOR --" + System.DateTime.Now.ToString("dd/MM/yyyy")
            //        + " -- " + ((TextBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Remarks")).Text + ")|";
            //}


            //validation for checkboxlist
            int count = 0;
            foreach (ListItem lstitem in ((CheckBoxList)WorkOrder_Exemption_Record.Rows[0].FindControl("WorkOrderNo")).Items)
            {
                if (lstitem.Selected == true)
                    count++;
            }
            if (count == 0)
            {

                MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors, "Please Select atleast one workorder no. !!");
                return;
            }


            //validation - Approved work orders should not be repeated within the exempted period defined by the CC team

            string[] workOrders = PageRecordDataSet.Tables["App_WorkOrder_Exemption"]
                                       .Rows[0]["WorkOrderNo"]
                                       .ToString()
                                       .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string wo in workOrders)
            {
                List<string> conflicts = blobj.GetWorkOrdersInExemptionPeriod(wo.Trim());

                if (conflicts.Any())
                {
                    string joined = string.Join(", ", conflicts);
                    MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors,
                        $"The following work orders are already approved and still within the exemption period: {joined}. Duplicate not allowed!");
                    return;
                }
            }




          




            bool result = Save();

            if (result)
            {

                string v_code = Session["UserName"].ToString();
                string v_name = "";
                string wo_no = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["WorkOrderNo"].ToString();
                string subject = "";
                string msg = "";
                string cc = "";
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                BL_Send_Mail blobj1 = new BL_Send_Mail();
                ds = blobj1.Get_Vendor_mail(Session["UserName"].ToString());
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    cc = ds.Tables["aspnet_Membership"].Rows[0]["Email"].ToString();
                }

                ds1 = blobj1.Get_Vendor_name(v_code);
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {
                    v_name = ds1.Tables["App_vendor_reg"].Rows[0]["v_name"].ToString();
                }



                //subject = "Application for WorkOrder Exemption has been submitted or updated by M/S  " + v_name + " (Vendor Code – " + v_code + " ) ";
                //msg = "<html><body><P><B>"
                //              + "To<BR /> "
                //              + "Contractor Cell <BR/>"
                //              + "<BR />Dear Sir/Madam"
                //              + "<BR /> "

                //              + "M/s " + v_name + " (" + v_code + ") has submitted Work Order Registration application against Work Order " + wo_no + ", request you to please do the needful as soon as possible.<BR/>"

                //              + "<BR />Link :- https://services.tsuisl.co.in/CLMS "
                //              + " <BR /> " + " <BR /> Thanks & Regards " + " <BR /> "
                //              + " <BR /> TATA STEEL UISL Contractor's Cell </B></P></html></body>"
                //              + " <BR /> "
                //              + " <BR /> Note: Please do not reply as it is a system generated mail. ";


                //blobj1.sendmail_request("", cc, subject, msg, "");



                subject = "Submission of Application for Interim Bill Submission – M/s " + v_name + " (Vendor Code: " + v_code + ") – Work Order No(s): " + wo_no;


                msg = "<html><body><p>"
                              + "<b>To: </b><br/>Contractors’ Cell<br/><br/>"
                                   + "<b>Cc:</b> M/s " + v_name + " (Vendor Code: " + v_code + ")<br/><br/>"
                              + "Dear Sir/Madam,<br/><br/>"
                              + "M/s " + v_name + " (Vendor Code: " + v_code + ") has submitted an application through the "
                              + "“WorkOrder Exemption (Billing)” module in CLMS, requesting additional time to complete compliance updates "
                              + "and seeking permission to submit bills in the interim against Work Order No(s): " + wo_no + ".<br/><br/>"
                              + "Kindly review the application and take necessary action at the earliest.<br/><br/>"
                              + "You may access the application via the following link: "
                              + "<a href='https://services.tsuisl.co.in/CLMS'>https://services.tsuisl.co.in/CLMS</a><br/><br/>"
                              + "Thanks & Regards,<br/>"
                              + "<b>TATA Steel UISL</b><br/>"
                              + "<b>Contractors’ Cell</b><br/><br/>"
                              + "<i>Note: This is a system-generated email. Please do not reply.</i>"
                              + "</p></body></html>";







                PageRecordDataSet.Clear();
                remarksTable.Clear();
                Remarks.Text = "";
                WorkOrder_Exemption_Record.BindData();
                GetRecords(GetFilterCondition(), WorkOrder_Exemption_Records.PageSize, 10, "");
                WorkOrder_Exemption_Records.BindData();

                MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Success, "Record saved successfully !");
                btnSave.Visible = false;
                DivInputFields.Visible = false;

            }
            else
            {
                MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors, "Error While Saving !");
            }
        }

        protected void WorkOrder_Exemption_Record_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (WorkOrder_Exemption_Record.Rows.Count > 0)
            //{
            //    ((BulletedList)WorkOrder_Exemption_Record.Rows[0].FindControl("Bull_Attach")).Items.Clear();
            //    string[] attachments;
            //    if (!string.IsNullOrEmpty(PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Attachment"].ToString()))
            //    {
            //        attachments = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Attachment"].ToString().Split(',');

            //        foreach (string image in attachments)
            //        {
            //            ListItem link = new ListItem();
            //            //link.Value = "~/Attachments/" + image;
            //            link.Value = "~/FileDownloadHandler.ashx?file=" + Server.UrlEncode(image);
            //            link.Text = image;
            //            link.Attributes.CssStyle.Add("text-decoration", "underline");
            //            link.Attributes.CssStyle.Add("color", "blue");
            //            link.Text = image.Substring(37);
            //            ((BulletedList)WorkOrder_Exemption_Record.Rows[0].FindControl("Bull_Attach")).Items.Add(link);
            //        }
            //    }

            //}



          





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

        protected void WorkOrder_Exemption_Record_PreRender(object sender, EventArgs e)
        {
            BL_WorkOrder_Exemption blobjs = new BL_WorkOrder_Exemption();
            DataSet ds = new DataSet();
            if (WorkOrder_Exemption_Record.Rows.Count > 0)
            {




                if (PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows.Count > 0)
                {
                    ds = blobjs.BindRemarks(PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["ID"].ToString());
                    Remarks_grid.DataSource = ds;
                    Remarks_grid.DataBind();

                }



            }
            if (PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["WorkOrderNo"].ToString() != "")
            {


                if (PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Status"].ToString() == "Pending With CC" ||
                   PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Status"].ToString() == "Approved")
                {
                    ((CheckBoxList)WorkOrder_Exemption_Record.Rows[0].FindControl("WorkOrderNo")).Enabled = false;
                    ((TextBox)WorkOrder_Exemption_Record.Rows[0].FindControl("Exemption_Vendor")).Enabled = false;
                    ((FileUpload)WorkOrder_Exemption_Record.Rows[0].FindControl("DbstsAttachment")).Enabled = false;
                    ((FileUpload)WorkOrder_Exemption_Record.Rows[0].FindControl("ComplianceAttachment")).Enabled = false;
                    Attachment.Enabled = false;
                    Remarks.Enabled = false;
                    btnSave.Enabled = false;
                }



                else
                {
                    WorkOrder_Exemption_Record.Enabled = true;
                    btnSave.Enabled = true;
                    Remarks.Text = "";
                    Attachment.Enabled = true;
                    Remarks_grid.DataBind();

                }









            }




        }

        protected void WorkOrder_Exemption_Records_PreRender(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "getClickedElement();", true);
        }





        //protected void AttachDownload_ServerClick(object sender, EventArgs e)
        //{
        //    string filename = PageRecordDataSet.Tables["App_WorkOrder_Exemption"].Rows[0]["Attachment"].ToString();
        //    // string filepath = Server.MapPath("~/Attachments/") + filename;

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
        //            //Response.Write("File not found.");
        //            MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Success, "File not found.");
        //        }
        //    }
        //    else
        //    {
        //        MyMsgBox.show(CLMS.Control.MyMsgBox.MessageType.Errors, "File not found.");
        //    }
        //}
    }
}