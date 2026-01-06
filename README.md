<div class="form-group col-md-6 col-margin">
                                                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6">Insurance Attachment :</label>
                                                
                                                <span class="text-danger">
                                                    <a id="MainContent_EmployeeMasterFormID_Record_lblInsuranceAttach_0" href="javascript:__doPostBack('ctl00$MainContent$EmployeeMasterFormID_Record$ctl02$lblInsuranceAttach','')">06-01-2026_11-04-38_HD_25-26_116134_wage register correction copy.pdf</a></span>
                                              
                                                <input type="file" name="ctl00$MainContent$EmployeeMasterFormID_Record$ctl02$InsuranceAttach" id="InsuranceAttach" title="file format .pdf , max size 2 MB " onchange="return ValidateFileExtension(this)">
                                             
                                            </div>

                                                 var esi_exempted = document.getElementById("MainContent_EmployeeMasterFormID_Record_EMP_ESI_EXEMPTED_0");

     var insuranceNo = document.getElementById("MainContent_EmployeeMasterFormID_Record_InsuranceNo_0");
     var insuranceAttach = document.getElementById("InsuranceAttach");

  
     if (esi_exempted.value == "YES") {
        

         if (insuranceNo.value === "") {
             alert("ESI Exempted YES So You must fill Insurance No.");
             return false;
         }

         if (insuranceAttach.value === "") {
             alert("Please Provide Insurance Attachment !");
             return false;
         }

     }
