using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using Spire.Xls;
using VP.Models;

namespace VP.Controllers
{
    public class SpecifyController : Controller
    {
        CP_Analytics_predictorEntities _context = new CP_Analytics_predictorEntities();
        // GET: Specify
        public ActionResult Index()
        {
            Specify m_specify = new Specify
            {
                Lst_Industry = _context.Tbl_M_Industry.Where(x => x.Status == true).Select(x => new Common.droplist { Id = x.Industry_id, Text = x.Industry_name, Img_url = x.Image_url }).ToList(),
                Lst_BusinessImperative = _context.Tbl_M_Business_Imperative.Where(x => x.Status == true).Select(x => new Common.droplist { Id = x.BM_Id, Text = x.BM_Name, Img_url = x.image_url }).ToList(),
                Lst_TypesOfAnalytics = _context.Tbl_M_Analytics.Where(x => x.Status == true).Select(x => new Common.droplist { Id = x.Analytics_Id, Text = x.Analytics_Name, Img_url = x.Image_url }).ToList()
            };
            //m_specify.lst_TypesOfAnalytics = _context.t.Where(x => x.Status == true).Select(x => new Common.droplist { id = x.BM_Id, text = x.BM_Name, img_url = x.image_url }).ToList();
            return View(m_specify);
        }
        public ActionResult Result()
        {
            return RedirectToAction("Index", "Specify", new { input = "#step-5" });
        }
        [HttpPost]
        public ActionResult Result_report(string industry, string businessimperative, string amount, string analytics)
        {
            Session["FileId"] = DateTime.Now.Ticks.ToString();
            //System.IO.File.Copy(Server.MapPath("~/Documents/IGFCalculatorImplementationV310-19-2016.xlsm"), Server.MapPath("~/Documents/IGF"+Convert.ToString(Session["FileId"]) + ".xlsm"));
            System.IO.File.Copy(Server.MapPath("~/Documents/z14.xlsm"), Server.MapPath("~/Documents/z14" + Convert.ToString(Session["FileId"]) + ".xlsm"));
            ////String excelFile = Convert.ToString(Server.MapPath("~/Documents/IGFCalculatorImplementationV1.xlsx"));
            String excelFile = Convert.ToString(Server.MapPath("~/Documents/z14" + Convert.ToString(Session["FileId"]) + ".xlsm"));
            //Workbook workbook = new Workbook();
            //workbook.LoadFromFile(excelFile);
            ////The first sheet of excel file
            //// Worksheet sheet = workbook.Worksheets["Input"];
            ////sheet.Range["F10"].Text = industry;
            ////sheet.Range["F12"].Text = businessimperative;
            ////sheet.Range["F14"].NumberValue = Convert.ToUInt64(amount);
            ////sheet.Range["F16"].Text = analytics;
            ////workbook.CalculateAllValue();
            //workbook.SaveToFile(excelFile);


            FileInfo file = new FileInfo(excelFile);
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets["Input"];
                excelWorksheet.Cells[10,6].Value = industry;
                excelWorksheet.Cells[12, 6].Value = businessimperative;
                excelWorksheet.Cells[14, 6].Value = Convert.ToDouble(amount);
                excelWorksheet.Cells[16, 6].Value = analytics;

                excelPackage.Save();
            }
            return RedirectToAction("Index", "Specify", new { input = "#step-5" });
        }
    }
}