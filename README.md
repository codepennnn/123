.dashboard-container {
    display: flex;
    gap: 16px;
    margin: 10px 0;
    font-family: 'Segoe UI', Tahoma, sans-serif;
}

/* Base card */
.dashboard-card {
    flex: 1;
    background: #ffffff;
    border-radius: 6px;
    padding: 10px 12px;
    box-shadow: 0 1px 4px rgba(0,0,0,0.08);
    text-align: center;
    position: relative;
    overflow: hidden;
}

/* Top color strip */
.dashboard-card::before {
    content: "";
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 12px;
    background-color: #60a5fa; /* default blue */
}

/* ===== STATUS COLORS ===== */
.dashboard-card.total::before    { background-color: #60a5fa; } /* Blue */
.dashboard-card.present::before  { background-color: #22c55e; } /* Green */
.dashboard-card.absent::before   { background-color: #ef4444; } /* Red */
.dashboard-card.od::before       { background-color: #f59e0b; } /* Orange */
.dashboard-card.leave::before    { background-color: #8b5cf6; } /* Purple */
.dashboard-card.holiday::before  { background-color: #0ea5e9; } /* Sky Blue */
.dashboard-card.halfday::before  { background-color: #ec4899; } /* Pink */

/* Title */
.dashboard-title {
    font-size: 14px;
    font-weight: 600;
    color: #374151;
    margin-top: 16px;
}

/* Value */
.dashboard-value {
    font-size: 22px;
    font-weight: 700;
    color: #111827;
    margin-top: 6px;
}
