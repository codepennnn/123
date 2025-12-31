protected void btnSave_Click(object sender, EventArgs e)
{
    // 🔐 SERVER-SIDE ACKNOWLEDGEMENT VALIDATION
    if (!chkAcknowledgement.Checked)
    {
        MyMsgBox.show(
            CLMS.Control.MyMsgBox.MessageType.Errors,
            "Please confirm the acknowledgement before submitting the request."
        );

        return; // ⛔ Stop processing
    }

    // ✅ Continue with existing save/update logic below
