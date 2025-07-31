// When checkboxes are changed, update the DeptDropdown display and hidden field
$(document).on('change', '.Dept-checkbox', function () {
    updateHiddenFieldFromCheckboxes();
});
