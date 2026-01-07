  function validateFileType() {
      var selectedFile = document.getElementById('MainContent_Bonus_Register_Upload').files[0];
     var allowedTypes = ['application/pdf'];
      /*var allowedTypes = ['image/jpeg', 'image/png', 'application/pdf'];*/
     if (!allowedTypes.includes(selectedFile.type)) {
        alert('Invalid file type. Please upload PDF file only.');
         document.getElementById('MainContent_Bonus_Register_Upload').value = '';
     }
    }
