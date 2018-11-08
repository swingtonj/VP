using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VP.Models;

namespace VP.Controllers
{
    public class SensitivityController : Controller
    {
        CP_Analytics_predictorEntities _context = new CP_Analytics_predictorEntities();
        // GET: Sensitivity
        public ActionResult Index()
        {
            try
            {
                string industry = Convert.ToString(Session["industry"]);
                int industry_id = Convert.ToInt32(Session["industry_id"]);
                string businessimperative = Convert.ToString(Session["businessimperative"]);
                string typeofanalytics = Convert.ToString(Session["typeofanalytics"]);
                Int64 amount = string.IsNullOrWhiteSpace(Convert.ToString(Session["amount"])) ? 0 : Convert.ToInt64(Session["amount"]);
                Specify m_specify = new Specify
                {
                    Lst_Industry = _context.Tbl_M_Industry.Where(x => x.Status == true && x.Industry_id == industry_id).Select(x => new Common.droplist { Id = x.Industry_id, Text = x.Industry_name, Img_url = x.Image_url }).ToList(),
                    Lst_BusinessImperative = _context.Tbl_M_Business_Imperative.Where(x => x.Status == true && x.Industry_id == industry_id).Select(x => new Common.droplist { Id = x.BM_Id, Text = x.BM_Name, Img_url = x.image_url }).ToList(),
                    Lst_TypesOfAnalytics = _context.Tbl_M_Analytics.Where(x => x.Status == true).Select(x => new Common.droplist { Id = x.Analytics_Id, Text = x.Analytics_Name, Img_url = x.Image_url }).ToList(),
                    Amount = amount,
                    Industry = industry,
                    Businessimperative = _context.Tbl_M_Business_Imperative.Where(x => x.Status == true && x.BM_Name == businessimperative).Select(x => x.BM_Id).FirstOrDefault(),
                    TypesofAnalytics = _context.Tbl_M_Analytics.Where(x => x.Status == true && x.Analytics_Name == typeofanalytics).Select(x => x.Analytics_Id).FirstOrDefault()
                };
                if (!string.IsNullOrWhiteSpace(businessimperative) && !string.IsNullOrWhiteSpace(typeofanalytics) && !string.IsNullOrWhiteSpace(industry) && amount > 0)
                    return View(m_specify);
                else
                {
                    return RedirectToAction("Index", "Specify");

                }
            }
            catch(Exception ee)
            {
                return null;
            }
        }
    }
}