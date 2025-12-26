.wrap-text {
    white-space: normal !important;
    word-break: break-word;
    max-width: 260px;
    line-height: 1.4;
}

@keyframes directBlink {
    0%   { background-color: #fff3e0; }
    50%  { background-color: #ffcc80; }
    100% { background-color: #fff3e0; }
}

.welfare-blink {
    animation: directBlink 1s infinite;
    border: 2px solid #ff9800 !important;
    font-weight: bold;
}
