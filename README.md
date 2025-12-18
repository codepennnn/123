<script type="text/javascript">
    function validateIntInput(txt, max) {

        // Remove non-numeric characters
        txt.value = txt.value.replace(/[^0-9]/g, '');

        if (txt.value === '') return;

        var val = parseInt(txt.value, 10);

        if (val > max) {
            alert("Maximum value allowed is " + max);
            txt.value = max;   // force max value
        }
    }
</script>
