/* Grid main table */
.custom-grid table {
    width: 100%;
    border-collapse: collapse;
    table-layout: fixed;
    font-size: 12px;
}

/* Header */
.custom-grid th {
    background-color: #5D7B9D;
    color: white;
    text-align: center;
    padding: 6px;
    border: 1px solid #c5c5c5;
    white-space: normal;
}

/* Rows */
.custom-grid td {
    border: 1px solid #d1d1d1;
    padding: 6px;
    vertical-align: top;
    word-wrap: break-word;
    white-space: normal;
}

/* Alternate row */
.custom-grid tr:nth-child(even) {
    background-color: #f9f9f9;
}

/* Label wrapping */
.custom-grid label {
    display: block;
    white-space: normal;
    word-break: break-word;
}

/* Dropdown smaller */
.custom-grid select {
    width: 100% !important;
    font-size: 11px;
}
