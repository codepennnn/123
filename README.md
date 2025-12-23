 function validateCompliance() {

     var checks = document.querySelectorAll(".compliance-check input[type='checkbox']");
     var atLeastOne = Array.from(checks).some(cb => cb.checked);

     if (!atLeastOne) {
         alert("Please select at least one Compliance Type.");
         return false;
     }
     return true;
 }

           <div class="form-inline row">
               <div class="form-group col-md-6 mb-2">
                     <label for="lbl_Attachment" class="m-0 mr-2 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6 justify-content-start" >Attachment:</label>
                         <asp:FileUpload ID="Attachment" runat="server" AllowMultiple="true" Font-Size="Small"/>
                         <asp:BulletedList ID="Bull_Attach" runat="server" CssClass="font-smaller text-primary attachment-list" DisplayMode="HyperLink" Target="_blank"/>
          
       
                   </div>

          

             </div>


                 <div runat="server" id="DbstsAttachmentLbl">
              <div class="form-inline row mt-4 mb-2">
                  <div class="form-group col-md-4 mb-2">
                      <label for="lbl_Attachment"  class="m-0 mr-2 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6 justify-content-start">DBSTS Compliance Mail Attachment:<span style="color: #FF0000;">*</span></label>
                      <asp:FileUpload ID="DbstsAttachment" runat="server"  Font-Size="Small" />
                 </div>

                   <div class="form-group col-md-4 mb-2">
                       <label for="lbl_Attachment" class="m-0 mr-2 p-0 col-form-label-sm col-sm-3 font-weight-bold fs-6 justify-content-start">Offline Compliance Attachment:</label>
                       <asp:FileUpload ID="ComplianceAttachment" runat="server"  Font-Size="Small"/>
                    </div>

              </div>
      </div>


      i want validation that length and size of file nama should be 20
