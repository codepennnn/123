var esi_exempted = document.getElementById("MainContent_EmployeeMasterFormID_Record_EMP_ESI_EXEMPTED_0");
var insuranceNo = document.getElementById("MainContent_EmployeeMasterFormID_Record_InsuranceNo_0");
var insuranceAttach = document.getElementById("InsuranceAttach");
var existingAttachment = document.getElementById("MainContent_EmployeeMasterFormID_Record_lblInsuranceAttach_0");

if (esi_exempted.value === "YES") {

    if (insuranceNo.value.trim() === "") {
        alert("ESI Exempted YES So You must fill Insurance No.");
        return false;
    }

    // if no existing file AND no new file selected
    if ((!existingAttachment || existingAttachment.innerText.trim() === "") 
        && insuranceAttach.value === "") {

        alert("Please Provide Insurance Attachment!");
        return false;
    }
}
