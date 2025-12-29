<style>
    .ack-box {
        border: 1px solid #e0e0e0;
        border-left: 5px solid #0d6efd;
        background: #f8f9fa;
        padding: 15px 18px;
        border-radius: 6px;
        font-size: 14px;
        margin-top: 15px;
    }

    .ack-title {
        font-weight: 600;
        color: #0d6efd;
        margin-bottom: 8px;
    }

    .ack-box ul {
        margin: 0 0 10px 18px;
        padding: 0;
    }

    .ack-box li {
        margin-bottom: 6px;
    }

    .ack-check {
        display: flex;
        align-items: flex-start;
        margin-top: 12px;
    }

    .ack-check input {
        margin-right: 8px;
        margin-top: 4px;
        transform: scale(1.2);
        cursor: pointer;
    }

    .ack-error {
        color: #dc3545;
        font-size: 13px;
        margin-top: 6px;
        display: none;
    }
</style>

<div class="ack-box">
    <div class="ack-title">
        📌 Mandatory Acknowledgement for Bill Exemption Request
    </div>

    <p>
        As a standard practice, please ensure that the following details / documents
        are shared while requesting for <strong>Bill Exemptions</strong>:
    </p>

    <ul>
        <li>
            Mail copy of <strong>DBSTS compliance dashboard</strong> against each
            work order to be enclosed.
        </li>
        <li>
            <strong>Payment proofs or HR clearance</strong> (for Odisha locations)
            to be enclosed if offline compliance is already done.
        </li>
        <li>
            <strong>Action plan with deadline / timeline</strong> to ensure that
            all pending compliances are updated in the portal within the same
            timelines. This will be reviewed by the <strong>Contractors’ Cell</strong>
            in the next bill exemption application.
        </li>
    </ul>

    <div class="ack-check">
        <input type="checkbox" id="chkAcknowledgement" />
        <label for="chkAcknowledgement">
            I confirm that I have read, understood, and attached all the above
            documents as applicable.
        </label>
    </div>

    <div class="ack-error" id="ackError">
        ⚠ Please acknowledge the above declaration to proceed.
    </div>
</div>
<script>
function validateAcknowledgement() {
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

