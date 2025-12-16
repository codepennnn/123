    <asp:Panel ID="pnlFilters" runat="server">
        <div class="filters">


             <div class="field">
             <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" for="ddlYear">Year</label>
             <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control form-control-sm col-sm-12" />
         </div>



             <div class="field">
             <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" for="ddlMonth">Month</label>
             <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control form-control-sm col-sm-12" />
         </div>




            <div class="field">
                <%--<label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Work Order Number :<span class="text-danger">*</span></label>--%>
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" for="ddlAadhar">Aadhar</label>
                <asp:DropDownList ID="ddlAadhar" runat="server" CssClass="form-control form-control-sm col-sm-12" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlAadhar_SelectedIndexChanged" />
            </div>

            <div class="field">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" for="ddlName">Name</label>
                <%--<asp:DropDownList ID="ddlName" runat="server" CssClass="row-select" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlName_SelectedIndexChanged" />--%>
                <asp:TextBox ID="txtName" runat="server" ReadOnly="true" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>


            </div>

            <div class="field">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Workman Sl No</label>
                <asp:TextBox ID="txtSlNo" runat="server" ReadOnly="true" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
            </div>

        <div class="field">
            <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6"  >Workman Category</label>
            <asp:TextBox ID="txtCategory" runat="server" ReadOnly="true" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
        </div>


           
             <div class="field">
             <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6"  >Vendor Name</label>
             <asp:TextBox ID="txtVname" runat="server" ReadOnly="true" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
         </div>


                <div class="field">
                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6"  >Vendor Code</label>
                <asp:TextBox ID="txtVcode" runat="server" ReadOnly="true" CssClass="form-control form-control-sm col-sm-12"></asp:TextBox>
            </div>

           

            <div class="field small">
                <label style="visibility:hidden">Action</label>
                <div class="controls">
                    <asp:Button ID="btnView" runat="server" CssClass="btn btn-sm btn-primary" Text="View" OnClick="btnView_Click" />
                 

                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-sm btn-warning" Text="Clear"  OnClick="btnClear_Click" />




                </div>
            </div>

         
        </div>
    </asp:Panel>



    
      <style>


          
.dashboard-container {
    display: flex;
    gap: 16px;
    margin: 10px 0;
    font-family: 'Segoe UI', Tahoma, sans-serif;
}

.dashboard-card {
    flex: 1;
    background: #fff;
    border-radius: 6px;
    padding: 10px 12px;
    box-shadow: 0 1px 4px rgba(0,0,0,0.08);
    text-align: center;
    position: relative;
    overflow: hidden;
}

/* Wider color strip at the top */
.dashboard-card::before {
    content: "";
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 12px; /* Increased height for strip */
    background-color: #60a5fa; /* Default blue */
}

.dashboard-card.present::before { background-color: #34d399; } /* Green */
.dashboard-card.absent::before  { background-color: #f87171; } /* Red */

.dashboard-title {
    font-size: 14px;
    font-weight: bold;
    color: #374151;
    margin-top: 16px; /* Push down to clear strip */
}

.dashboard-value {
    font-size: 20px;
    font-weight: 600;
    color: #111827;
    margin-top: 4px;
}








        
/* ---------- Layout container ---------- */
.grid-wrap {
  max-height: 560px;     /* comfortable height */
  overflow: auto;
  border-radius: 10px;
  padding: 6px;
  background: linear-gradient(180deg,#ffffff,#fbfdff);
  box-shadow: 0 8px 22px rgba(11,55,99,0.06);
  border: 1px solid rgba(11,55,99,0.06);
}

/* ---------- Table baseline ---------- */
table.attendance {
  width: 100%;
  border-collapse: separate;
  border-spacing: 0 8px; /* vertical gap between rows */
  min-width: 1000px;    /* allow horizontal scroll only when needed */
  font-family: "Segoe UI", Roboto, Arial, sans-serif;
  font-size: 14px;
}

/* sticky header */
table.attendance thead th {
  position: sticky;
  top: 0;
  z-index: 30;
  background: linear-gradient(90deg,#fff6f6 0%, #fff3f4 40%, #f7fbff 100%);
  padding: 14px 12px;
  text-align: left;
  color: #2b3b3f;
  font-weight: 800;
  font-size: 16px;
  letter-spacing: 0.3px;
  border-bottom: 3px solid rgba(11,55,99,0.06);
  box-shadow: inset 0 -2px 0 rgba(11,55,99,0.03);
}

/* small colored accent under header cells */
table.attendance thead th::after {
  content: "";
  display: block;
  height: 3px;
  width: 46px;
  margin-top: 8px;
  background: linear-gradient(90deg,#2b6cb0,#0c4a6e);
  border-radius: 3px;
}

/* make column separators subtle */
table.attendance thead th,
table.attendance tbody td {
  border-right: 1px solid rgba(11,55,99,0.04);
}
table.attendance thead th:last-child,
table.attendance tbody td:last-child {
  border-right: none;
}

/* ---------- Row cells ---------- */
table.attendance tbody td {
  background: transparent;
  padding: 12px;
  vertical-align: middle;
}

/* card-like rows using pseudo background via tbody tr */
table.attendance tbody tr {
  transition: transform .08s ease, box-shadow .12s ease, background-color .12s ease;
  border-radius: 10px;
  overflow: visible;
}

/* give rows an elevated card look using each cell background */
table.attendance tbody td > * {
  display: inline-block;
  vertical-align: middle;
}

/* the visible cell pill (labels) */
.grid-label {
  display: inline-block;
  padding: 8px 12px;
  border-radius: 8px;
  background: #fff;
  border: 1px solid #eef2f7;
  box-shadow: 0 4px 12px rgba(9,30,66,0.03);
  font-weight:600;
  color:#0f172a;
  min-width: 84px;
  text-align:center;
}

/* inputs/selects style */
table.attendance select,
table.attendance input[type="text"] {
  height: 40px;
  padding: 8px 12px;
  border-radius: 10px;
  border: 1px solid #e6eef7;
  box-shadow: 0 6px 14px rgba(11,55,99,0.03);
  background: #fff;
  font-size: 14px;
  outline: none;
  min-width: 120px;
}
table.attendance select {
  padding-right: 36px; /* allow arrow room */
}

/* OT small box */
.otbox {
  width:86px;
  min-width:72px;
  text-align:right;
}

/* dropdown comfortable width */
.row-select { width:190px; min-width:140px; max-width:270px; }

/* center small columns */
.center { text-align:center; }

/* hover / focus states */
table.attendance tbody tr:hover td { transform: translateY(-2px); }
table.attendance select:focus, table.attendance input[type="text"]:focus {
  border-color: #2b6cb0;
  box-shadow: 0 6px 18px rgba(43,108,176,0.12);
}

/* ---------- Present / Absent states (strong & readable) ---------- */
.present-row td { 
  background: linear-gradient(180deg,#e8fff0,#d1f7d6) !important; 
  box-shadow: inset 0 0 0 2px rgba(16,185,129,0.18);
}
.absent-row td {
  background: linear-gradient(180deg,#fff1f1,#ffd5d5) !important;
  box-shadow: inset 0 0 0 2px rgba(220,38,38,0.12);
}

/* present checkbox style */
.row-present { width:20px; height:20px; transform:scale(1.1); }

/* thin separators between rows (visual grouping) */
table.attendance tbody tr + tr td { padding-top: 6px; }

/* ---------- Column sizing: assign widths using nth-child (adjust as needed) ---------- */
/* NOTE: grid columns order: Date, Aadhar, Workman Name, Work Order, Location, OT, Day Def, Engagement Type, Present */
table.attendance thead th:nth-child(1), table.attendance tbody td:nth-child(1) { width:120px; min-width:100px; }
table.attendance thead th:nth-child(2), table.attendance tbody td:nth-child(2) { width:160px; min-width:140px; }
table.attendance thead th:nth-child(3), table.attendance tbody td:nth-child(3) { width:150px; min-width:140px; }
table.attendance thead th:nth-child(4), table.attendance tbody td:nth-child(4) { width:160px; min-width:140px; }
table.attendance thead th:nth-child(5), table.attendance tbody td:nth-child(5) { width:140px; min-width:120px; }
table.attendance thead th:nth-child(6), table.attendance tbody td:nth-child(6) { width:90px; min-width:80px; }
table.attendance thead th:nth-child(7), table.attendance tbody td:nth-child(7) { width:140px; min-width:120px; }
table.attendance thead th:nth-child(8), table.attendance tbody td:nth-child(8) { width:170px; min-width:140px; }
table.attendance thead th:nth-child(9), table.attendance tbody td:nth-child(9) { width:70px; min-width:60px; }

/* responsive breakpoints: stack / shrink elegantly */
@media (max-width: 1100px) {
  table.attendance { font-size:13px; }
  .row-select { width:150px; }
}
@media (max-width: 800px) {
  .grid-wrap { max-height:620px; }
  table.attendance { min-width:720px; }
  .row-select { width:120px; }
  .grid-label { min-width:70px; font-size:13px; padding:6px 10px; }
}

/* Mobile stacked card layout (keeps your existing stacked layout) */
@media (max-width:640px) {
  table.attendance, thead, tbody, th, td, tr { display:block; width:100%; }
  table.attendance thead { display:none; }
  table.attendance tbody tr { margin:10px 0; padding:10px; border-radius:10px; background:#fff; box-shadow:0 6px 18px rgba(9,30,66,0.04); }
  table.attendance tbody td { display:flex; justify-content:space-between; padding:8px 10px; border-bottom:none; }
  table.attendance tbody td::before { content: attr(data-label); font-weight:700; color:#6b7280; margin-right:8px; width:45%; display:inline-block; }
  .row-select { width:48%; }
  .otbox { width:40%; }
}

/* ---------- Nice thin custom scrollbar for the grid-wrap (modern browsers) ---------- */
.grid-wrap::-webkit-scrollbar { width:12px; height:12px; }
.grid-wrap::-webkit-scrollbar-track { background: transparent; }
.grid-wrap::-webkit-scrollbar-thumb { background: linear-gradient(180deg,#dfe9f3,#b3c7dd); border-radius:10px; border:3px solid transparent; background-clip: padding-box; }

/* ---------- Utility ---------- */
.message { margin-top:12px; padding:10px 12px; border-radius:8px; }




        /* Desktop (default) — already styled as premium */
#<%= gvAttendance.ClientID %> thead th {
    font-size: 18px;
    font-weight: 800;
}

/* Large tablets / small laptops */
@media (max-width: 1200px) {
    #<%= gvAttendance.ClientID %> thead th {
        font-size: 17px;
    }
}

/* Tablets / medium screens */
@media (max-width: 992px) {
    #<%= gvAttendance.ClientID %> thead th {
        font-size: 16px;
    }
}

/* Small tablets / large phones */
@media (max-width: 768px) {
    #<%= gvAttendance.ClientID %> thead th {
        font-size: 15px;
        padding: 10px 8px;
        letter-spacing: 0.3px;
    }
}

/* Phones */
@media (max-width: 600px) {
    #<%= gvAttendance.ClientID %> thead th {
        font-size: 14px;
        padding: 8px 6px;
    }
}

/* Very small phones */
@media (max-width: 480px) {
    #<%= gvAttendance.ClientID %> thead th {
        font-size: 13px;
        padding: 6px 4px;
    }
}

/* Ultra-small screens */
@media (max-width: 360px) {
    #<%= gvAttendance.ClientID %> thead th {
        font-size: 12px;
        padding: 5px 3px;
        letter-spacing: 0.2px;
    }
}









        /* ===== Premium sticky header + gradient + separators for attendance grid ===== */
#<%= gvAttendance.ClientID %> {
    border-collapse: collapse;
}

/* make header sticky and prominent */
#<%= gvAttendance.ClientID %> thead th {
    position: sticky;
    top: 0;
    z-index: 1200; /* ensure it sits above body rows */
    padding: 14px 12px;
    font-weight: 800;
    font-size: 18px;
    text-align: left;
    letter-spacing: 0.4px;
    color: #0b3b2f; /* dark teal text */
    background: linear-gradient(90deg, #fff6f6 0%, #fff3f4 35%, #f7fbff 100%);
    border-bottom: 3px solid rgba(11,55,99,0.08);
    box-shadow: inset 0 -3px 0 rgba(11,55,99,0.04);
}

/* subtle top rounded corners for whole header row */
#<%= gvAttendance.ClientID %> thead th:first-child {
    border-top-left-radius: 12px;
}
#<%= gvAttendance.ClientID %> thead th:last-child {
    border-top-right-radius: 12px;
}

/* colored column separators — thin lines between columns */
#<%= gvAttendance.ClientID %> thead th,
#<%= gvAttendance.ClientID %> tbody td {
    border-right: 1px solid rgba(15,60,60,0.06);
}

/* remove right-border on last column for neat edge */
#<%= gvAttendance.ClientID %> thead th:last-child,
#<%= gvAttendance.ClientID %> tbody td:last-child {
    border-right: none;
}

/* slightly darker divider under header to emphasize separation */
#<%= gvAttendance.ClientID %> thead th::after {
    content: "";
    display:block;
    height:4px;
    width:100%;
    margin-top:8px;
    background: linear-gradient(90deg, rgba(43,108,176,0.18), rgba(12,74,110,0.14));
    opacity:0.95;
    border-radius:2px;
}

/* row hover + spacing tweaks to match premium header */
#<%= gvAttendance.ClientID %> tbody td {
    padding: 12px;
    vertical-align: middle;
}

/* ensure the scroll container has a max height so sticky header is useful */
.grid-wrap {
    max-height: 520px; /* adjust as you like */
    overflow: auto;
}

/* small responsive tweak: keep sticky header visible above mobile stacked rows */
@media (max-width:640px) {
    #<%= gvAttendance.ClientID %> thead th { position: sticky; top:0; }
}


        /* Strong, bold, premium-looking GridView header */
#<%= gvAttendance.ClientID %> thead th {
    background: #f8d7d7;        /* light red / pink theme */
    font-weight: 700;           /* bold */
    font-size: 18px;            /* bigger */
    color: #3b0d0c;             /* dark header text */
    padding: 10px 6px;
    border-bottom: 2px solid #d77;
    text-align: center;
    letter-spacing: 0.5px;
}

/* Optional: rounded top corners like your design */
#<%= gvAttendance.ClientID %> thead th:first-child {
    border-top-left-radius: 12px;
}
#<%= gvAttendance.ClientID %> thead th:last-child {
    border-top-right-radius: 12px;
}




        /* Validation modal styles (no external libs) */
.va-modal-backdrop {
    position: fixed;
    inset: 0;
    background: rgba(2,6,23,0.55);
    z-index: 1090;
}

.va-modal {
    position: fixed;
    left: 50%;
    top: 50%;
    transform: translate(-50%,-50%);
    background: #fff;
    width: min(820px, calc(100% - 32px));
    border-radius: 10px;
    box-shadow: 0 12px 40px rgba(2,6,23,0.35);
    z-index: 1091;
    overflow: hidden;
    font-family: "Segoe UI", Roboto, Arial, sans-serif;
}

.va-modal-header {
    display:flex;
    align-items:center;
    justify-content:space-between;
    padding:14px 16px;
    border-bottom:1px solid #eef2f7;
    background: linear-gradient(90deg,#fff,#f6fbff);
}

.va-modal-header h3 { margin:0; font-size:1.05rem; color:#0b4a6e; }
.va-close { background:none; border:none; font-size:22px; line-height:1; cursor:pointer; color:#6b7280; }

.va-modal-body { padding:14px 16px; max-height:360px; overflow:auto; background:#fbfdff; }
.va-pre { white-space:pre-wrap; word-break:break-word; margin:0; font-size:0.95rem; color:#111827; }

.va-modal-footer { display:flex; gap:8px; padding:12px 16px; justify-content:flex-end; border-top:1px solid #eef2f7; background:#fff; }

.va-btn { padding:8px 12px; border-radius:8px; cursor:pointer; border:1px solid #e6eef7; background:#fff; color:#0b4a6e; font-weight:600; }
.va-btn-primary { background: linear-gradient(180deg,#2b6cb0,#0c4a6e); color:#fff; border:none; }
.va-btn-ghost { background:transparent; color:#0c4a6e; border:1px solid #e6eef7; }










        :root{
            --accent:#2b6cb0;
            --accent-2:#0c4a6e;
            --muted:#6b7280;
            --card-bg:#ffffff;
            --page-bg:#f3f6fb;
            --success:#16a34a;
            --danger:#dc2626;
            --absent:#f8d7da;
            --present:#ecfdf5;
            --radius:10px;
            --shadow: 0 6px 18px rgba(29,41,53,0.08);
            --focus-ring: 0 0 0 4px rgba(43,108,176,0.12);
        }

        /* Page + Card */
        body { background: var(--page-bg); font-family: "Segoe UI", Roboto, Arial, sans-serif; color:#111827; }
        .card { background:var(--card-bg); border-radius:var(--radius); padding:18px; box-shadow:var(--shadow); margin-bottom:18px; max-width:98%; margin-inline:auto; }
        h2 { margin:0 0 6px 0; font-weight:600; color:var(--accent-2) }
        .muted { color:var(--muted); font-size:0.95rem; }

        /* Filters */
        .filters { display:grid; grid-template-columns: repeat(12, 1fr); gap:12px; align-items:center; margin-top:12px; }
        .filters .field { grid-column: span 3; display:flex; flex-direction:column; gap:6px; }
        .filters .field.small { grid-column: span 2; }
        label { font-size:0.85rem; color:var(--muted); }
        select, input[type="text"], .btn {
          /*  padding:8px 10px; border-radius:8px; border:1px solid #e6eef7; box-sizing:border-box;
            font-size:0.95rem; outline:none; background:#fff;*/
        }
        select:focus, input:focus { box-shadow:var(--focus-ring); border-color:var(--accent); }

      /*  .btn {
            background: linear-gradient(180deg,var(--accent),var(--accent-2));
            color:#fff; border:none; cursor:pointer; font-weight:600; border-radius:8px;
            box-shadow: 0 6px 14px rgba(11,55,99,0.12); transition: transform .08s ease, box-shadow .08s ease;
        }
        .btn:hover { transform: translateY(-1px); box-shadow: 0 10px 20px rgba(11,55,99,0.14); }
        .btn.ghost { background:transparent; color:var(--accent-2); border:1px solid #e2e8f0; box-shadow:none; padding:7px 10px; }
*/
        /* Table / Grid */
        .grid-wrap { margin-top:14px; overflow:auto; border-radius:8px; }
        table.attendance { width:100%; border-collapse:collapse; min-width:920px; font-size:0.95rem; }
        table.attendance thead th {
            position: sticky; top:0; padding:12px 14px; text-align:left;
            background: linear-gradient(90deg, #ffffff 0%, #f1f7ff 50%, #eef6ff 100%);
            border-bottom:3px solid rgba(11,55,99,0.06);
            color:var(--accent-2); font-weight:700; text-transform:uppercase; letter-spacing:0.06em;
            font-size:0.78rem;
        }
        table.attendance thead th::after {
            content: "";
            display:block;
            height:3px;
            width:42px;
            margin-top:8px;
            background: linear-gradient(90deg,var(--accent) 0%, var(--accent-2) 100%);
            border-radius:3px;
            opacity:0.85;
        }
        table.attendance tbody td { padding:10px 12px; border-bottom:1px solid #f1f5f9; vertical-align:middle; }
        table.attendance tbody tr { transition: background-color .18s ease, box-shadow .18s ease; }
        table.attendance tbody tr:nth-child(odd) { background:#ffffff; }
        table.attendance tbody tr:nth-child(even) { background:#fbfdff; }
        table.attendance tbody tr:hover { background:#eff8ff; }

        /* Present / Absent row states */
        .present-row 
        {
            background: #d1f7d6 !important;                /* Strong green */
            box-shadow: inset 0 0 0 2px rgba(16, 185, 129, 0.35);
            transition: background 0.2s ease, box-shadow 0.2s ease;
        }
        
        .absent-row {
            background: #ffd5d5 !important;                /* Strong red */
            box-shadow: inset 0 0 0 2px rgba(220, 38, 38, 0.35);
            transition: background 0.2s ease, box-shadow 0.2s ease;
        }

        /* Tiny UI pieces */
        .small { width:110px; }
        .otbox { width:70px; }
        .row-select { width:160px; max-width:200px; }
        .center { text-align:center; }
        .grid-label { display:inline-block; padding:6px 8px; border-radius:6px; background:#f8fafc; border:1px solid #eef2f7; font-weight:600; color:#0f172a; }

        .message { margin-top:12px; padding:10px 12px; border-radius:8px; }
        .message.ok { background:#ecfdf5; color:var(--success); border:1px solid rgba(16,185,129,0.12); }
        .message.err { background:#fff1f2; color:var(--danger); border:1px solid rgba(220,38,38,0.08); }

        .controls { display:flex; gap:8px; align-items:center; }

        /* Focus styles for interactive controls inside table */
        table.attendance input:focus, table.attendance select:focus, table.attendance button:focus {
            box-shadow: var(--focus-ring);
            outline: none;
            border-color: var(--accent);
        }

        /* Mobile-friendly: stacked cards for small screens */
        @media (max-width:640px) {
            table.attendance, thead, tbody, th, td, tr { display:block; width:100%; }
            table.attendance thead { display:none; } /* hide header row */
            table.attendance tbody tr { margin: 10px 0; border-radius:8px; padding:10px; box-shadow: 0 4px 12px rgba(9,30,66,0.04); background:#fff; }
            table.attendance tbody td { display:flex; justify-content:space-between; padding:8px 10px; border-bottom: none; }
            table.attendance tbody td::before { content: attr(data-label); font-weight:700; color:var(--muted); margin-right:8px; width:45%; display:inline-block; }
            .row-select { width:48%; }
            .grid-label { display:inline-block; padding:6px 8px; }
        }

        @media (max-width:900px){
            .filters .field { grid-column: span 6; }
            .filters .field.small { grid-column: span 4; }
            .row-select { width:140px; }
            table.attendance { min-width:800px; }
        }
    </style>
