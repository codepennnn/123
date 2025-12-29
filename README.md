
   <script type="text/javascript">

       document.addEventListener('DOMContentLoaded', function () {

           var allBox = document.querySelector('.select-all input[type="checkbox"]');

           var itemBoxes = document.querySelectorAll('.item-check input[type="checkbox"]');


           allBox.addEventListener('change', function () {
               itemBoxes.forEach(function (cb) {
                   cb.checked = allBox.checked;
               });
           });


           itemBoxes.forEach(function (cb) {
               cb.addEventListener('change', function () {

                   allBox.checked = Array.from(itemBoxes).every(function (c) { return c.checked; });
               });
           });



       });


       function validateCompliance() {

           var checks = document.querySelectorAll(".compliance-check input[type='checkbox']");
           var atLeastOne = Array.from(checks).some(cb => cb.checked);

           if (!atLeastOne) {
               alert("Please select at least one Compliance Type.");
               return false;
           }

           var fileInput = document.getElementById("MainContent_WorkOrder_Exemption_Record_DbstsAttachment_0");
           if (fileInput.value === "") {
               alert("Please upload the DBSTS mail attachment.");
               return false;
           }

           var fileInput2 = document.getElementById("MainContent_WorkOrder_Exemption_Record_ComplianceAttachment_0");
           if (fileInput2.value === "") {
               alert("Please upload the Payment Proof / HR Clearance Copy attachment.");
               return false;
           }

         
           
           var fileInp = [
               document.getElementById("MainContent_WorkOrder_Exemption_Record_DbstsAttachment_0"),
               document.getElementById("MainContent_WorkOrder_Exemption_Record_ComplianceAttachment_0"),
               document.getElementById("MainContent_Attachment")
           ];

           var maxFileNameLength = 20;        
           var maxFileSizeMB = 5;             
           var maxFileSizeBytes = maxFileSizeMB * 1024 * 1024;

           for (var i = 0; i < fileInp.length; i++) {
               var input = fileInp[i];

               if (input && input.files.length > 0) {
                   for (var j = 0; j < input.files.length; j++) {
                       var file = input.files[j];

                      
                       if (file.name.length > maxFileNameLength) {
                           alert("File name must not exceed " + maxFileNameLength + " characters.\nFile: " + file.name);
                           return false;
                       }

                       
                       if (file.size > maxFileSizeBytes) {
                           alert("File size must not exceed " + maxFileSizeMB + " MB.\nFile: " + file.name);
                           return false;
                       }
                   }
               }
           }


           var chk = document.getElementById("chkAcknowledgement");
           var err = document.getElementById("ackError");

           if (!chk.checked) {
               err.style.display = "block";
               chk.focus();
               return false;
           }

           err.style.display = "none";




           return true;



         
       }
   </script>
