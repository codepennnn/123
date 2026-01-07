function validateFileType() {
    var fileInput = document.getElementById('MainContent_Bonus_Register_Upload');
    var file = fileInput.files[0];

    if (!file) return;

    var allowedExtensions = ['pdf', 'xls', 'xlsx'];
    var fileExt = file.name.split('.').pop().toLowerCase();

    if (!allowedExtensions.includes(fileExt)) {
        alert('Invalid file type. Please upload PDF or Excel file only.');
        fileInput.value = '';
    }
}
