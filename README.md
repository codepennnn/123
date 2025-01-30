

insert into App_FNF_Summary ([ID],[VendorCode],[Date_of_Exit],[Date_of_Notice],[AdharNo],[WorkorderNo],[Mode_of_Sepration],[Rate_of_Wages],[Advance_Payment],
[Unpaid_DaysWorked],[Unpaid_BasicDA],[Unpaid_ArearWages],[Total_Leave_Amount_paid],[Total_Bonus_Amount_paid],[Total_Amount_FNF_Settlement],[Total_FNF_Net_Amount],
[Status],[CreatedBy],[CreatedOn],[Attachment],[Remarks],flag,Comp_MasterID ) 
Values ('43873cd0-0932-43c8-a4dd-2b3f20cdfe38','14882','31/03/2024','28/02/2024','983030955867','4700013846','Termination','590.70000000000005','0','0','0','0','0','0',null,null,'','14882','1/30/2025 6:07:47 PM','','','NO','6db7ae9b-2a2e-47da-b817-33dbd282f8c3')
The conversion of a varchar data type to a datetime data type resulted in an out-of-range value.
The statement has been terminated.


-----------------------------------------------------------------
 Select ''as WO_NO,'' as wo_date from App_WorkOrder_Reg union
 select WO_NO,(WO_NO+' - '+ Convert(varchar,TO_DATE,103)) as wo_date from App_WorkOrder_Reg where V_CODE='12307'
 and Status='Approved' and WO_NO Not in (select  WO_NO  from App_Wo_Nil where  NO_WORK='Permanent')

