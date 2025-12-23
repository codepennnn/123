<script>
function validateCompliance() {

    // Existing checkbox validation
    var checks = document.querySelectorAll(".compliance-check input[type='checkbox']");
    var atLeastOne = Array.from(checks).some(cb => cb.checked);

    if (!atLeastOne) {
        alert("Please select at least one Compliance Type.");
        return false;
    }

    // File inputs to validate
    var fileInputs = [
        document.getElementById('<%= Attachment.ClientID %>'),
        document.getElementById('<%= DbstsAttachment.ClientID %>'),
        document.getElementById('<%= ComplianceAttachment.ClientID %>')
    ];

    var maxFileNameLength = 20;          // characters
    var maxFileSizeMB = 20;              // MB
    var maxFileSizeBytes = maxFileSizeMB * 1024 * 1024;

    for (var i = 0; i < fileInputs.length; i++) {
        var input = fileInputs[i];

        if (input && input.files.length > 0) {
            for (var j = 0; j < input.files.length; j++) {
                var file = input.files[j];

                // File name length check
                if (file.name.length > maxFileNameLength) {
                    alert("File name must not exceed " + maxFileNameLength + " characters.\nFile: " + file.name);
                    return false;
                }

                // File size check
                if (file.size > maxFileSizeBytes) {
                    alert("File size must not exceed " + maxFileSizeMB + " MB.\nFile: " + file.name);
                    return false;
                }
            }
        }
    }

    return true;
}
</script>
