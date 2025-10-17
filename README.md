// 🔹 Get JS-calculated values first
Double.TryParse(Total_Base_Amount.Text, out double totalBase);
Double.TryParse(Cess_Amount.Text, out double cessAmt);
Double.TryParse(Subjective_base_amount.Text, out double subjBase);
Double.TryParse(Subjective_cess_amount.Text, out double subjCess);
Double.TryParse(Subjective_balance.Text, out double subjBal);

// 🔹 Update dataset
var summary = PageRecordDataSet.Tables["App_BOCW_Summary"].Rows[0];
summary["Total_Base_Amount"] = totalBase;
summary["Cess_Amount"] = cessAmt;
summary["Subjective_base_amount"] = subjBase;
summary["Subjective_cess_amount"] = subjCess;
summary["Subjective_balance"] = subjBal;

// Continue your existing logic
summary["Status"] = "Pending with CC";
...
