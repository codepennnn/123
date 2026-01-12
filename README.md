<div class="grid-wrapper">
    <cc1:DetailsContainer ID="WODetails_Record" runat="server"
        CssClass="modern-grid"
        AutoGenerateColumns="False"
        AllowPaging="false"
        GridLines="None"
        Width="100%"
        DataMember="App_Bocw_details_workorder"
        DataKeyNames="ID"
        DataSource="<%# PageRecordDataSet %>"
        ShowHeaderWhenEmpty="True">
        ...
    </cc1:DetailsContainer>
</div>


/* Grid container */
.grid-wrapper {
    overflow-x: auto;
    border: 1px solid #dee2e6;
    border-radius: 8px;
    background: #fff;
}

/* Main grid */
.modern-grid {
    border-collapse: separate;
    border-spacing: 0;
    font-size: 13px;
    min-width: 1200px; /* prevents squeeze */
}

/* Header */
.modern-grid th {
    background: linear-gradient(180deg, #0d6efd, #084298);
    color: #fff !important;
    text-align: center;
    padding: 10px 8px;
    font-weight: 600;
    position: sticky;
    top: 0;
    z-index: 2;
    border-bottom: 2px solid #084298;
    white-space: nowrap;
}

/* Body cells */
.modern-grid td {
    padding: 8px 10px;
    border-bottom: 1px solid #e9ecef;
    vertical-align: middle;
    white-space: normal;
}

/* Alternate rows */
.modern-grid tr:nth-child(even) {
    background-color: #f8f9fa;
}

/* Hover effect */
.modern-grid tr:hover {
    background-color: #e9f2ff;
    transition: background-color 0.2s ease-in-out;
}

/* Text wrapping for long content */
.wrap-text {
    white-space: normal;
    word-break: break-word;
}

/* Disabled dropdown look */
.modern-grid select:disabled {
    background-color: #e9ecef;
    color: #495057;
    border: 1px solid #ced4da;
    cursor: not-allowed;
}

/* Improve dropdown width */
.modern-grid select {
    min-width: 260px;
    font-size: 12px;
}

/* Center short fields */
.text-center {
    text-align: center;
}
