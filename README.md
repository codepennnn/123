        if (totalMandays > maxAllowedMandays)
        {
            string msg = $"Total Mandays(Male+Female=Children) for License {licNo} exceeds allowed limit.\n\n" +
                         $"Total Employees: {totalEmp}\n" +
                         $"Days Worked: {item.Value.daysWorked}\n" +
                         $"Max Allowed Mandays: {maxAllowedMandays}\n" +
                         $"Entered Mandays: {totalMandays}";

            ScriptManager.RegisterStartupScript(
                this, this.GetType(),
                "alert",
                $"alert('{msg}');",
                true);

            //btnSave.Enabled = false;
            return false;
        }


          if (totalEmp > capacity)
  {
      string msg = $"Total Workers (Male+Female=Children) for License {licNo} : {totalEmp} exceeds Capacity : {capacity}";

      ScriptManager.RegisterStartupScript(
          this, this.GetType(),
          "alert",
          $"alert('{msg}');",
          true);

      //btnSave.Enabled = false;
      return false;
  }
