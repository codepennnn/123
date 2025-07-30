<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>

<script>
	// On form submit, collect checked worksites
	$('form').on('submit', function () {
		var selected = [];
		$('.worksite-checkbox:checked').each(function () {
			selected.push($(this).val());
		});
		$('#Worksite').val(selected.join(','));
	});

	function setAction(action, event) {
		if (action === 'Delete' && !confirm("Are you sure you want to delete this record?")) {
			event.preventDefault();
			return;
		}
		document.getElementById('actionType').value = action;
	}

	var NewButton = document.getElementById("newButton");
	var FormContainer = document.getElementById("formContainer");
	var refNoLinks = document.querySelectorAll(".refNoLink");

	if (NewButton) {
		NewButton.addEventListener("click", function () {
			FormContainer.style.display = "block";
			document.getElementById("Pno").value = "";
			document.getElementById("Position").value = "";
			document.getElementById("worksiteDropdown").value = "";

			// Uncheck all worksites
			document.querySelectorAll(".worksite-checkbox").forEach(cb => cb.checked = false);
		});
	}

	refNoLinks.forEach(link => {
		link.addEventListener("click", function (event) {
			event.preventDefault();
			FormContainer.style.display = "block";

			document.getElementById("Pno").value = this.getAttribute("data-Pno");
			document.getElementById("Position").value = this.getAttribute("data-Position");
			document.getElementById("worksiteDropdown").value = this.getAttribute("data-Worksites");

			// Reset all
			document.querySelectorAll(".worksite-checkbox").forEach(cb => cb.checked = false);

			// Check selected
			const worksites = this.getAttribute("data-Worksites");
			if (worksites) {
				worksites.split(',').forEach(id => {
					const checkbox = document.getElementById("worksite_" + id.trim());
					if (checkbox) checkbox.checked = true;
				});
			}

			if (typeof deleteButton !== "undefined" && deleteButton) {
				deleteButton.style.display = "inline-block";
			}
		});
	});
</script>
