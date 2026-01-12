/* Container */
.grid-wrapper {
    width: 100%;
    overflow-x: auto;
    border: 1px solid #d0d7de;
    border-radius: 6px;
    background: #ffffff;
}

/* Grid */
.modern-grid {
    table-layout: fixed;              /* 🔥 CRITICAL */
    width: 1600px;                    /* 🔥 FORCE WIDTH */
    border-collapse: collapse;
    font-size: 13px;
}

/* Header */
.modern-grid th {
    background: #0d3b66;
    color: #ffffff !important;
    padding: 10px 8px;
    text-align: center;
    font-weight: 600;
    border-bottom: 2px solid #092c4c;
    white-space: nowrap;
}

/* Body cells */
.modern-grid td {
    padding: 8px 10px;
    border-bottom: 1px solid #e5e7eb;
    vertical-align: middle;
    overflow: hidden;
    text-overflow: ellipsis;
}

/* Alternating rows */
.modern-grid tr:nth-child(even) {
    background-color: #f8fafc;
}

/* Hover */
.modern-grid tr:hover {
    background-color: #eef6ff;
}

/* Text wrap (for long text only) */
.wrap-text {
    white-space: normal;
    word-break: break-word;
}

/* Center content */
.text-center {
    text-align: center;
}

/* Dropdown column */
.modern-grid select {
    width: 100%;
    font-size: 12px;
}

/* Disabled dropdown */
.modern-grid select:disabled {
    background-color: #f1f5f9;
    color: #374151;
}
