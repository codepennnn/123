<div class="grid-wrapper">
    <cc1:DetailsContainer 
        ID="HalfYearly_Records"
        runat="server"
        CssClass="styled-grid modern-grid"
        AutoGenerateColumns="False"
        GridLines="None"
        Width="100%">
    </cc1:DetailsContainer>
</div>


.grid-wrapper {
    width: 100%;
    overflow-x: auto;
    border-radius: 10px;
    border: 1px solid #dcdcdc;
    background: #ffffff;
    padding: 8px;
}

/* TABLE */
.modern-grid table {
    border-collapse: separate !important;
    border-spacing: 0 !important;
    min-width: 2200px; /* IMPORTANT */
    font-family: "Segoe UI", Arial, sans-serif;
}

/* HEADER */
.modern-grid th {
    position: sticky;
    top: 0;
    z-index: 2;
    background: #003570 !important;
    color: #ffffff !important;
    padding: 10px 8px !important;
    font-size: 13px !important;
    font-weight: 600;
    white-space: nowrap;
    border-bottom: 2px solid #002447;
}

/* ROWS */
.modern-grid td {
    padding: 8px 6px !important;
    font-size: 12px !important;
    text-align: center;
    border-bottom: 1px solid #e3e3e3;
    white-space: nowrap;
}

/* STRIPED ROWS */
.modern-grid tr:nth-child(even) td {
    background: #f9fbfd;
}

.modern-grid tr:nth-child(odd) td {
    background: #ffffff;
}

/* HOVER */
.modern-grid tr:hover td {
    background: #eaf3ff !important;
}

/* INPUTS & DROPDOWNS */
.modern-grid input,
.modern-grid select {
    width: 100%;
    height: 30px;
    font-size: 12px;
    padding: 2px 6px;
    border-radius: 4px;
    border: 1px solid #cfcfcf;
}

/* WELFARE SECTION (ORANGE HEADERS) */
.modern-grid th[style*="#ff9933"] {
    background: #ff9933 !important;
    color: #fff !important;
}

/* SCROLLBAR (OPTIONAL BUT NICE) */
.grid-wrapper::-webkit-scrollbar {
    height: 8px;
}

.grid-wrapper::-webkit-scrollbar-thumb {
    background: #b5c7e3;
    border-radius: 10px;
}

.grid-wrapper::-webkit-scrollbar-track {
    background: #f1f1f1;
}
