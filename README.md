

    <script>




        document.addEventListener("DOMContentLoaded", function () {

            var ddl = document.getElementById("<%= ddlAadhar.ClientID %>");
        var container = ddl.closest(".SearchDropDown");
        var list = container.querySelector(".searchList");
        var selectedText = container.querySelector(".selected-text");

        list.innerHTML = ""; // clear

        // Build list from dropdown
        for (var i = 0; i < ddl.options.length; i++) {
            var opt = ddl.options[i];
            if (opt.value === "") continue;

            var div = document.createElement("div");
            div.className = "dropdown-item";
            div.style.cursor = "pointer";
            div.innerText = opt.text;
            div.dataset.value = opt.value;
            div.dataset.text = opt.text;

            div.onclick = function () {
                ddl.value = this.dataset.value;
                selectedText.innerText = this.dataset.text;

                container.querySelector(".floatDiv").style.display = "none";

                // Trigger postback
                ddl.dispatchEvent(new Event('change'));
            };

            list.appendChild(div);
        }

       
        if (ddl.value !== "") {
            for (var j = 0; j < ddl.options.length; j++) {
                if (ddl.options[j].value === ddl.value) {
                    selectedText.innerText = ddl.options[j].text;
                    break;
                }
            }
        }
    });



        function toggleAadharDD(btn) {
            var panel = btn.nextElementSibling;
            panel.style.display = panel.style.display === "block" ? "none" : "block";
        }

        function filterAadhar(input) {
            var filter = input.value.toUpperCase();
            var items = input.parentElement.querySelectorAll(".dropdown-item");

            items.forEach(function (item) {
                item.style.display = item.innerText.toUpperCase().indexOf(filter) > -1 ? "" : "none";
            });
        }

        // close when clicking outside
        document.addEventListener("click", function (e) {
            document.querySelectorAll(".SearchDropDown").forEach(function (dd) {
                if (!dd.contains(e.target)) {
                    dd.querySelector(".floatDiv").style.display = "none";
                }
            });
        });

                        document.querySelector(".floatDiv").style.display = "none";
                };

                list.appendChild(div);
            }
        }
                }
  });


    </script>
