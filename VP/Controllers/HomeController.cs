using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VP.Models;

namespace VP.Controllers
{
    public class HomeController : Controller
    {
        CP_Analytics_predictorEntities _context = new CP_Analytics_predictorEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Login m_Login)
        {
            var o_validate = _context.Tbl_T_UserManagement.Where(x => x.User_name == m_Login.L_Username && x.Password == m_Login.L_Password && x.Status == true).FirstOrDefault();
            if (o_validate != null)
            {
                Session["Username"] = o_validate.User_name;
                Session["Userid"] = o_validate.User_Id;
                Session["IsAdmin"] = o_validate.Is_Admin;
                return RedirectToAction("index","specify");
            }
            else
            {

            }
            return View();
        }
        [HttpPost]
        public ActionResult Register(Login m_Login)
        {
            Tbl_T_UserManagement insert_user = new Tbl_T_UserManagement();
            insert_user.Email_id = m_Login.R_Email;
            insert_user.Mobile_no = m_Login.R_Mobile;
            insert_user.Organisation_name = m_Login.R_Organisation_Name;
            insert_user.Password = m_Login.R_Passsword;
            insert_user.Registered_on = DateTime.Now;
            insert_user.Status = true;
            insert_user.User_name = m_Login.R_User_Name;
            _context.Tbl_T_UserManagement.Add(insert_user);
            _context.SaveChanges();
            var user_id = insert_user.User_Id;
           // _context.SP_Registration("","","","","");
            return View();
        }
    }
}