using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace hospitalTimeZone.Models
{
    public class DepartmentCls
    {
       
        public int DepID { get; set; }
         public int DrID { get; set; }
        public String DepName { get; set; }
       // public String DoctorName { get; set; }


    }
   
}