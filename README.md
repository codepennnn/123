  <div class="SearchCheckBoxList" style="width:287%;">
      <button class="btn btn-sm btn-default selectArea w-100" type="button" onclick="togglefloatDiv(this);" 
              id="btn-dwn1" style="border:0.5px solid #ced2d5;">
          <span class="filter-option float-left">No item Selected</span>
          <span class="caret"></span>
      </button>
      <div class="floatDiv" runat="server" style="border:1px solid #ced2d5; position:absolute; z-index:1000;
              box-shadow:0 6px 12px rgb(0 0 0 / 18%); background-color:white; padding:5px; display:none; width:100%;">
                    
          <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control form-control-sm col-sm-6"  
                       oninput="filterCheckBox(this)" AutoComplete="off" ViewStateMode="Disabled" 
                       placeholder="Enter Workorder" Font-Size="Smaller" />
                    
          <div style="overflow:auto; max-height:210px; overflow-y:auto; overflow-x:hidden;" class="searchList p-0">
              <asp:CheckBoxList ID="CC_WorkOrderNo" runat="server" DataMember="CC_Wo_No" DataSource="<%# PageDDLDataset %>"
                                DataTextField="WorkOrderNo" DataValueField="WorkOrderNo" CssClass="form-control-sm radio">
              </asp:CheckBoxList>
          </div>
                    
          <asp:TextBox runat="server" ID="TextBox3" Width="0%" style="display:none" />
      </div>
  </div>

  i just only give you for reference and logic here i already in my another module in which dropdwon with chekbox with facility of search can you do some like these my that code
