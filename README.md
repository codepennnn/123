<script>
function formatDOB(input) {
    let v = input.value.replace(/\D/g, '');

    if (v.length > 8) v = v.substring(0, 8);

    if (v.length >= 5)
        input.value = v.substring(0, 2) + '/' + v.substring(2, 4) + '/' + v.substring(4);
    else if (v.length >= 3)
        input.value = v.substring(0, 2) + '/' + v.substring(2);
    else
        input.value = v;
}

function validateDOB(input) {
    const regex = /^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])\/\d{4}$/;
    if (input.value !== "" && !regex.test(input.value)) {
        alert("Please enter date in dd/MM/yyyy format");
        input.value = "";
        input.focus();
    }
}
</script>
