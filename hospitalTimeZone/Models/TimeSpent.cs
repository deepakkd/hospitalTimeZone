using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace hospitalTimeZone.Models
{
    public class TimeSpent
    {
        public int TimeSpentID { get; set; }
        public int DepId { get; set; }
        public int DrId { get; set; }
        public String Sun { get; set; }
        public String Mon  { get; set; }
        public String Tue { get; set; }
        public String Wed { get; set; }
        public String Thus { get; set; }
        public String Fri { get; set; }
        public String Sat { get; set; }
        public String TotalTime { get; set; }


    }
}