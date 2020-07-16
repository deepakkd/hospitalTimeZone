using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using hospitalTimeZone.Models;
using hospitalTimeZone.View_Model;

namespace hospitalTimeZone.Controllers
{
    public class DepController : Controller
    {
        string db = ConfigurationManager.ConnectionStrings["HospitalDb"].ConnectionString;

        List<SelectListItem> DepName = new List<SelectListItem>();
        List<SelectListItem> DoctorName = new List<SelectListItem>();
        List<SelectListItem> time = new List<SelectListItem>();


        public ActionResult Index()
        {
            DoctorVM d = new DoctorVM();
            DepartmentCls dc = new DepartmentCls();
            //dc.DepID = 1;
            //dc.DepName = "abc";

            //d.DepartmentCls.Add(dc);
            GetOptions();
            ViewBag.var1 = DepName;
            ViewBag.var2 = DoctorName;
            ViewBag.var3 = time;
            return View(d);
        }
        private void GetOptions()
        {
            using (SqlConnection conn = new SqlConnection(db))
            {
                conn.Open();
                SqlDataReader rdr = null;
                SqlCommand cmd = new SqlCommand("SELECT DepId ,DepName FROM Deparment", conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DepName.Add(new SelectListItem { Text = rdr["DepName"].ToString(), Value = rdr["DepId"].ToString() });
                }
                conn.Close();
            }

        }
        public JsonResult Doctor(String name)
        {
            using (SqlConnection con = new SqlConnection(db))
            {
                con.Open();
                SqlDataReader rdr = null;
                SqlCommand cmd = new SqlCommand("SELECT DoctorName,DrId FROM Doctor WHERE DepId ='" + name + "' ", con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    DoctorName.Add(new SelectListItem { Text = rdr["DoctorName"].ToString(), Value = rdr["DrId"].ToString() });

                }
                return Json(DoctorName, JsonRequestBehavior.AllowGet);
            }


        }
        public ActionResult Timespent(string id)
        {
            DoctorVM d = new DoctorVM();
            List<TimeSpent> time = new List<TimeSpent>();
            DataTable dttimespent = new DataTable();
            using (SqlConnection con = new SqlConnection(db))
            {

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter();

                SqlCommand cmd = new SqlCommand("SELECT * FROM TimeSpent WHERE DrId = '" + id + "'", con);
                da.SelectCommand = cmd;
                da.Fill(dttimespent);
            }
            foreach (DataRow datatime in dttimespent.Rows)
            {
                TimeSpent timezone = new TimeSpent();
                timezone.DepId = Convert.ToInt32(datatime["DepId"].ToString());
                timezone.TimeSpentID = Convert.ToInt32(datatime["TimeSpentID"].ToString());
                timezone.Sun = datatime["Sun"].ToString();
                timezone.Mon = datatime["Mon"].ToString();
                timezone.Tue = datatime["Tue"].ToString();
                timezone.Wed = datatime["Wed"].ToString();
                timezone.Thus = datatime["Thus"].ToString();
                timezone.Fri = datatime["Fri"].ToString();
                timezone.Sat = datatime["Sat"].ToString();
                timezone.TotalTime = datatime["TotalTime"].ToString();
                time.Add(timezone);

            }
            GetOptions();
            ViewBag.var1 = DepName;
            ViewBag.var2 = DoctorName;

            d.Timepents = time;
            return PartialView("~/Views/Dep/timespent.cshtml", d);
        }
        public ActionResult Update(string Sun, string Mon, string Tue, string Wed, string Thus, string Fri, string Sat, string TotalTime, int TimeSpentID)
        {

            using (SqlConnection con = new SqlConnection(db))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("UPDATE TimeSpent SET Sun=@Sun, Mon=@Mon, Tue=@Tue,Wed=@Wed,Thus=@Thus,Fri=@Fri,Sat=@Sat ,TotalTime=@TotalTime WHERE TimeSpentID=@TimeSpentID", con);
                cmd.Parameters.AddWithValue("@Sun", Sun);
                cmd.Parameters.AddWithValue("@Mon", Mon);
                cmd.Parameters.AddWithValue("@Tue", Tue);
                cmd.Parameters.AddWithValue("@Wed", Wed);
                cmd.Parameters.AddWithValue("@Thus", Thus);
                cmd.Parameters.AddWithValue("@Fri", Fri);
                cmd.Parameters.AddWithValue("@Sat", Sat);
                cmd.Parameters.AddWithValue("@TimeSpentID", TimeSpentID);
                cmd.Parameters.AddWithValue("@TotalTime", TotalTime);
                cmd.ExecuteNonQuery();
                con.Close();

            }
            return Json(new { Success = true });
        }
        [HttpPost]
        public ActionResult Submit(string Sun, string Mon, string Tue, string Wed, string Thus, string Fri, string Sat,string TotalTime,string DoctorId,string DptmId)
        {
            using (SqlConnection con = new SqlConnection(db))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO TimeSpent(Sun,Mon,Tue,Wed,Thus,Fri,Sat,TotalTime,DepId,DrId) values(@Sun,@Mon,@Tue,@Wed,@Thus,@Fri,@Sat,@TotalTime,@DepId,@DrId)", con);
                cmd.Parameters.AddWithValue("@Sun", Sun);
                cmd.Parameters.AddWithValue("@Mon", Mon);
                cmd.Parameters.AddWithValue("@Tue", Tue);
                cmd.Parameters.AddWithValue("@Wed", Wed);
                cmd.Parameters.AddWithValue("@Thus", Thus);
                cmd.Parameters.AddWithValue("@Fri", Fri);
                cmd.Parameters.AddWithValue("@Sat", Sat);
                cmd.Parameters.AddWithValue("@TotalTime", TotalTime);
                cmd.Parameters.AddWithValue("@DepId",Convert.ToInt32(DptmId));
                cmd.Parameters.AddWithValue("@DrId", Convert.ToInt32(DoctorId));
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return RedirectToAction("Timespent", new { id= Convert.ToInt32(DoctorId) });
            //return Json(new { Success = true });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(db))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete From TimeSpent Where TimeSpentID = '" + id + "'", con);
                cmd.ExecuteNonQuery();
            }
            return Json(new { Success = true }); 
       
        }
      
    }
     


}




   


     