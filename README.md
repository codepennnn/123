select option.att-done {
    background-color: #dcfce7;
    color: #166534;
    font-weight: 600;
}


document.getElementById("<%= ddlAadhar.ClientID %>")
    .addEventListener("change", function () {
        if (this.selectedIndex > 0) {
            this.options[this.selectedIndex].classList.add("att-done");
        }
    });

if (!ddl.options[ddl.selectedIndex].classList.contains("att-done")) {
    ddl.options[ddl.selectedIndex].classList.add("att-done");
}
