Vendor_attendance.aspx:1595  POST http://localhost:15720/App/Input/Vendor_attendance.aspx/GetPresentAadhars 500 (Internal Server Error)


        [System.Web.Services.WebMethod]
        public static List<string> GetPresentAadhars(int month, int year)
        {
            var list = new List<string>();

            
            string connStr = ConfigurationManager
                .ConnectionStrings["Connect"].ConnectionString;

            DateTime fromDate = new DateTime(year, month, 1);
            DateTime toDate = fromDate.AddMonths(1);

            using (var cn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(@"
        SELECT DISTINCT AadharNo
        FROM App_AttendanceDetails
        WHERE Dates >= @FromDate
          AND Dates <  @ToDate
          AND Present = 1", cn))
            {
                cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = fromDate;
                cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = toDate;

                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        list.Add(dr.GetString(0));
                }
            }

            return list;
        }
