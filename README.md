UPDATE App_WorkOrder_Reg
SET 
    BOCW_APPLICABLE_AS_PER_CC = @BOCW,
    Remarks = ISNULL(Remarks, '') +
              '(bocw updated - ' + CONVERT(varchar(10), GETDATE(), 103) + ')'
WHERE WO_NO = @WO_NO


SqlCommand cmd = new SqlCommand(
    @"UPDATE App_WorkOrder_Reg
      SET 
          BOCW_APPLICABLE_AS_PER_CC = @BOCW,
          Remarks = ISNULL(Remarks,'') +
                    '(bocw updated - ' + CONVERT(varchar(10), GETDATE(), 103) + ')'
      WHERE WO_NO = @WO_NO",
    con, tran);

cmd.Parameters.AddWithValue("@BOCW", bocwValue);
cmd.Parameters.AddWithValue("@WO_NO", woNo);

int rows = cmd.ExecuteNonQuery();

