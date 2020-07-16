using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using hospitalTimeZone.Models;

namespace hospitalTimeZone.View_Model
{
    public class DoctorVM
    {
        public int DepId { get; set; }
        public int DrId { get; set; }
        public DepartmentCls Department { get; set; }
        public TimeSpent TimeSpent { get; set; }
        public List<DepartmentCls> DepartmentCls { get; set; }
        public List<TimeSpent>  Timepents { get; set; }

        public DoctorVM()
        {
            DepartmentCls = new List<DepartmentCls>();
            Timepents = new List<TimeSpent>();
        }
    }
  
}