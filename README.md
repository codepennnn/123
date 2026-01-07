                                        <div class="col-md-4 mb-1 form-row">
                                            <div class="col-md-5">
                                                <label class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6" >Date of Joining :</label>
                                                <asp:label id="Dojlbl" runat="server"  class="m-0 mr-2 p-0 col-form-label-sm  font-weight-bold fs-6"><span class="text-danger">*</span></asp:label>

                                                </div>
                                            <div class="col-md-7">
                                                <asp:TextBox ID="DOJ" runat="server" CssClass="form-control form-control-sm textboxstyle" />
                                                <ask:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"  Format="dd/MM/yyyy" PopupPosition="TopRight" TargetControlID="DOJ" TodaysDateFormat="dd/MM/yyyy" ></ask:CalendarExtender>
                                               
                                            </div>
                                        </div>
