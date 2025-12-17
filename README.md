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
