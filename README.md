@keyframes welfareBlink {
    0%   { box-shadow: 0 0 0px rgba(255,153,51,0.2); }
    50%  { box-shadow: 0 0 8px rgba(255,153,51,0.9); }
    100% { box-shadow: 0 0 0px rgba(255,153,51,0.2); }
}

.welfare-blink {
    animation: welfareBlink 1.2s infinite;
    border: 2px solid #ff9933 !important;
    background-color: #fff7ec !important;
    font-weight: 600;
}

<script>
    document.addEventListener("DOMContentLoaded", function () {

        document.querySelectorAll("#HalfYearly_Records select").forEach(function (ddl) {

            if (
                ddl.id.includes("Welfare_Canteen") ||
                ddl.id.includes("Welfare_RestRoom") ||
                ddl.id.includes("Welfare_DrinkingWater") ||
                ddl.id.includes("Welfare_Creches") ||
                ddl.id.includes("Welfare_FirstAid")
            ) {
                ddl.classList.add("welfare-blink");
            }

        });

    });
</script>

.ellipsis-cell {
    max-width: 220px;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
    cursor: help;
}
