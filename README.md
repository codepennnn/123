..
<div class="mb-3">
  <label for="pnoSearch" class="form-label">PNO</label>
  <input type="text" id="pnoSearch" class="form-control" placeholder="Search PNO..." autocomplete="off">
  <ul id="pnoList" class="list-group" style="max-height: 150px; overflow-y: auto; display: none; position: absolute; z-index: 10;"></ul>

  <!-- Hidden select to preserve form structure -->
  <select name="Coordinators[0].Pno" id="Pno" style="display: none;">
    <option value="">-- Select PNO --</option>
    @foreach (var item in ViewBag.PnoList as List<SelectListItem>)
    {
        <option value="@item.Value">@item.Text</option>
    }
  </select>
</div>


<style>
  #pnoList li {
    cursor: pointer;
    padding: 5px 10px;
  }
  #pnoList li:hover {
    background-color: #f0f0f0;
  }
</style>



<script>
  document.addEventListener("DOMContentLoaded", function () {
    const input = document.getElementById("pnoSearch");
    const select = document.getElementById("Pno");
    const ul = document.getElementById("pnoList");

    // Load options from the select element
    const options = Array.from(select.options)
      .filter(opt => opt.value !== "")
      .map(opt => ({ value: opt.value, text: opt.text }));

    input.addEventListener("input", function () {
      const search = input.value.toLowerCase();
      ul.innerHTML = "";

      if (search === "") {
        ul.style.display = "none";
        return;
      }

      const filtered = options.filter(opt => opt.text.toLowerCase().includes(search));
      if (filtered.length === 0) {
        ul.style.display = "none";
        return;
      }

      filtered.forEach(opt => {
        const li = document.createElement("li");
        li.className = "list-group-item";
        li.textContent = opt.text;
        li.dataset.value = opt.value;

        li.addEventListener("click", () => {
          input.value = opt.text;
          select.value = opt.value;
          ul.style.display = "none";
        });

        ul.appendChild(li);
      });

      ul.style.display = "block";
    });

    document.addEventListener("click", function (e) {
      if (!ul.contains(e.target) && e.target !== input) {
        ul.style.display = "none";
      }
    });
  });
</script>
