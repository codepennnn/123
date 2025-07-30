   <div class="mb-3">
       <label for="Pno" class="form-label">PNO</label>
       <select name="Coordinators[0].Pno" id="Pno" class="form-control">
           <option value="">-- Select PNO --</option>
           @foreach (var item in ViewBag.PnoList as List<SelectListItem>)
           {
               <option value="@item.Value">@item.Text</option>
           }
       </select>
   </div>
