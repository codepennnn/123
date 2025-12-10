     private bool Check()
     {
         foreach (GridViewRow row in HalfYearly_Records.Rows)
         {
             string licNo = row.Cells[6].Text.Trim();
             int capacity = Convert.ToInt32(row.Cells[7].Text);
             int sexM = Convert.ToInt32(row.Cells[13].Text);
             int sexF = Convert.ToInt32(row.Cells[14].Text);

             if (sexM + sexF > capacity)
             {
                
                 ScriptManager.RegisterStartupScript(
                     this, this.GetType(),
                     "alert",
                     "alert(' Maxium No. of Workers (Male+Female+children) cannot be greater than Capacity of  License - {licNo}');",
                     true);

              
                 btnSave.Enabled = false;

                 return false;
             }


             double mandaysMale = Convert.ToDouble(row.Cells[15].Text);
             double mandaysFemale = Convert.ToDouble(row.Cells[16].Text);
             double daysWorked = Convert.ToDouble(row.Cells[12].Text);

             double totalMandays = mandaysMale + mandaysFemale;
             double maxAllowedDays = capacity * daysWorked;


             if (maxAllowedDays > capacity)
             {

                 ScriptManager.RegisterStartupScript(
                   this, this.GetType(),
                   "alert",
                   "alert(' Maxium No. of Mandays (Male+Female+children) cannot be greater than Capacity of  License - {licNo}');",
                   true);


                 btnSave.Enabled = false;

                 return false;
             }

         }

         
         btnSave.Enabled = true;

         return true;
     }
