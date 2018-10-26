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
            try
            {
                if (Convert.ToString(Session["FileId"]) != "")
                    System.IO.File.Delete(Convert.ToString(Session["FileId"]));
            }catch(Exception ee)
            {

            }
            Session["FileId"] = DateTime.Now.Ticks.ToString();
            System.IO.File.Copy(Server.MapPath("~/Documents/calc.xlsm"), Server.MapPath("~/Documents/z14" + Convert.ToString(Session["FileId"]) + ".xlsm"));
            String excelFile = Convert.ToString(Server.MapPath("~/Documents/z14" + Convert.ToString(Session["FileId"]) + ".xlsm"));
            FileInfo file = new FileInfo(excelFile);
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets["Input"];
                excelWorksheet.Cells[10, 6].Value = industry;
                excelWorksheet.Cells[12, 6].Value = businessimperative;
                excelWorksheet.Cells[14, 6].Value = Convert.ToDouble(amount);
                excelWorksheet.Cells[16, 6].Value = analytics;

                excelPackage.Save();
            }
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets["Results"];

                var z14_roi = excelWorksheet.Cells[5, 4].Value;
                var z14_pp = excelWorksheet.Cells[9, 4].Value;
                var z14_sa = excelWorksheet.Cells[14, 4].Value;
                var z14_sy = excelWorksheet.Cells[15, 4].Value;
                var z14_oc = excelWorksheet.Cells[16, 4].Value;
                var z14_cp = excelWorksheet.Cells[18, 4].Value;
                var z14_itp = excelWorksheet.Cells[19, 4].Value;
                var z14_op = excelWorksheet.Cells[20, 4].Value;
                var z14_ir = excelWorksheet.Cells[22, 4].Value;
                var z14_ftv = excelWorksheet.Cells[23, 4].Value;
                var z14_te = excelWorksheet.Cells[24, 4].Value;
                var z14_es = excelWorksheet.Cells[26, 4].Value;
                var z14_gc = excelWorksheet.Cells[27, 4].Value;
                var z14_rd = excelWorksheet.Cells[28, 4].Value;

                var x86_roi = excelWorksheet.Cells[5, 5].Value;
                var x86_pp = excelWorksheet.Cells[9, 5].Value;
                var x86_sa = excelWorksheet.Cells[14, 5].Value;
                var x86_sy = excelWorksheet.Cells[15, 5].Value;
                var x86_oc = excelWorksheet.Cells[16, 5].Value;
                var x86_cp = excelWorksheet.Cells[18, 5].Value;
                var x86_itp = excelWorksheet.Cells[19, 5].Value;
                var x86_op = excelWorksheet.Cells[20, 5].Value;
                var x86_ir = excelWorksheet.Cells[22, 5].Value;
                var x86_ftv = excelWorksheet.Cells[23, 5].Value;
                var x86_te = excelWorksheet.Cells[24, 5].Value;
                var x86_es = excelWorksheet.Cells[26, 5].Value;
                var x86_gc = excelWorksheet.Cells[27, 5].Value;
                var x86_rd = excelWorksheet.Cells[28, 5].Value;
                var result = new
                {
                    //z14_dataProductivityChart =  Convert.ToString(Convert.ToDecimal(z14_cp).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(z14_itp).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(z14_op).ToString("0.##")) ,
                    //z14_dataCostsChart = Convert.ToString(Convert.ToDecimal(z14_sy).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(z14_sa).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(z14_oc).ToString("0.##")) ,
                    //z14_dataRevenuesProfitChart =  Convert.ToString(Convert.ToDecimal(z14_ir).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(z14_ftv).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(z14_te).ToString("0.##")) ,
                    //z14_dataRisksChart = Convert.ToString(Convert.ToDecimal(z14_es).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(z14_gc).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(z14_rd).ToString("0.##")) ,

                    //x86_dataProductivityChart =  Convert.ToString(Convert.ToDecimal(x86_cp).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(x86_itp).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(x86_op).ToString("0.##")) ,
                    //x86_dataCostsChart =  Convert.ToString(Convert.ToDecimal(x86_sy).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(x86_sa).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(x86_oc).ToString("0.##")) ,
                    //x86_dataRevenuesProfitChart =  Convert.ToString(Convert.ToDecimal(x86_ir).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(x86_ftv).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(x86_te).ToString("0.##")) ,
                    //x86_dataRisksChart =  Convert.ToString(Convert.ToDecimal(x86_es).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(x86_gc).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(x86_rd).ToString("0.##")) ,


                    z14_dataProductivityChart = Convert.ToString(Convert.ToInt64(z14_cp)) + "," + Convert.ToString(Convert.ToInt64(z14_itp)) + "," + Convert.ToString(Convert.ToInt64(z14_op)),
                    z14_dataCostsChart = Convert.ToString(Convert.ToInt64(z14_sy)) + "," + Convert.ToString(Convert.ToInt64(z14_sa)) + "," + Convert.ToString(Convert.ToInt64(z14_oc)),
                    z14_dataRevenuesProfitChart = Convert.ToString(Convert.ToInt64(z14_ir)) + "," + Convert.ToString(Convert.ToInt64(z14_ftv)) + "," + Convert.ToString(Convert.ToInt64(z14_te)),
                    z14_dataRisksChart = Convert.ToString(Convert.ToInt64(z14_es)) + "," + Convert.ToString(Convert.ToInt64(z14_gc)) + "," + Convert.ToString(Convert.ToInt64(z14_rd)),

                    x86_dataProductivityChart = Convert.ToString(Convert.ToInt64(x86_cp)) + "," + Convert.ToString(Convert.ToInt64(x86_itp)) + "," + Convert.ToString(Convert.ToInt64(x86_op)),
                    x86_dataCostsChart = Convert.ToString(Convert.ToInt64(x86_sy)) + "," + Convert.ToString(Convert.ToInt64(x86_sa)) + "," + Convert.ToString(Convert.ToInt64(x86_oc)),
                    x86_dataRevenuesProfitChart = Convert.ToString(Convert.ToInt64(x86_ir)) + "," + Convert.ToString(Convert.ToInt64(x86_ftv)) + "," + Convert.ToString(Convert.ToInt64(x86_te)),
                    x86_dataRisksChart = Convert.ToString(Convert.ToInt64(x86_es)) + "," + Convert.ToString(Convert.ToInt64(x86_gc)) + "," + Convert.ToString(Convert.ToInt64(x86_rd)),


                    dataRoiChart = Convert.ToString(Convert.ToInt64(Convert.ToDecimal(x86_roi)*100)) + "," + Convert.ToString(Convert.ToInt64( Convert.ToDecimal(z14_roi)*100)),
                    dataPaybackChart = Convert.ToString(Convert.ToDecimal(x86_pp).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(z14_pp).ToString("0.##"))

                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult Industry_select(int industry)
        {
            Specify m_specify = new Specify
            {
                Lst_BusinessImperative = _context.Tbl_M_Business_Imperative.Where(x => x.Status == true).Select(x => new Common.droplist { Id = x.BM_Id, Text = x.BM_Name, Img_url = x.image_url }).ToList(),
                Lst_TypesOfAnalytics = _context.Tbl_M_Analytics.Where(x => x.Status == true).Select(x => new Common.droplist { Id = x.Analytics_Id, Text = x.Analytics_Name, Img_url = x.Image_url }).ToList()
            };
            //m_specify.lst_TypesOfAnalytics = _context.t.Where(x => x.Status == true).Select(x => new Common.droplist { id = x.BM_Id, text = x.BM_Name, img_url = x.image_url }).ToList();
            return View(m_specify);
        }

    }
}