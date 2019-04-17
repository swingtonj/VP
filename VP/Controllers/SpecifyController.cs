using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WinForms;
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
            if (Convert.ToString(Session["Userid"]) == string.Empty)
            {
                Session["alert"] = "Please login to continue";
                return RedirectToAction("Index", "Home");
            }
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
            Session["industry"] = industry;
            Session["businessimperative"] = businessimperative;
            Session["typeofanalytics"] = analytics;
            Session["amount"] = amount;
            try
            {
                if (Convert.ToString(Session["FileId"]) != "")
                    System.IO.File.Delete(Convert.ToString(Session["FileId"]));
            }
            catch (Exception ee)
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
                excelWorksheet.Calculate();
                excelWorksheet.Workbook.Calculate();
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


                    z14_dataProductivityChart = Convert.ToString(Convert.ToInt64(z14_cp) / 1000) + "," + Convert.ToString(Convert.ToInt64(z14_itp) / 1000) + "," + Convert.ToString(Convert.ToInt64(z14_op) / 1000),
                    z14_dataCostsChart = Convert.ToString(Convert.ToInt64(z14_sy) / 1000) + "," + Convert.ToString(Convert.ToInt64(z14_sa) / 1000) + "," + Convert.ToString(Convert.ToInt64(z14_oc) / 1000),
                    z14_dataRevenuesProfitChart = Convert.ToString(Convert.ToInt64(z14_ir) / 1000) + "," + Convert.ToString(Convert.ToInt64(z14_ftv) / 1000) + "," + Convert.ToString(Convert.ToInt64(z14_te) / 1000),
                    z14_dataRisksChart = Convert.ToString(Convert.ToInt64(z14_es) / 1000) + "," + Convert.ToString(Convert.ToInt64(z14_gc) / 1000) + "," + Convert.ToString(Convert.ToInt64(z14_rd) / 1000),

                    x86_dataProductivityChart = Convert.ToString(Convert.ToInt64(x86_cp) / 1000) + "," + Convert.ToString(Convert.ToInt64(x86_itp) / 1000) + "," + Convert.ToString(Convert.ToInt64(x86_op) / 1000),
                    x86_dataCostsChart = Convert.ToString(Convert.ToInt64(x86_sy) / 1000) + "," + Convert.ToString(Convert.ToInt64(x86_sa) / 1000) + "," + Convert.ToString(Convert.ToInt64(x86_oc) / 1000),
                    x86_dataRevenuesProfitChart = Convert.ToString(Convert.ToInt64(x86_ir) / 1000) + "," + Convert.ToString(Convert.ToInt64(x86_ftv) / 1000) + "," + Convert.ToString(Convert.ToInt64(x86_te) / 1000),
                    x86_dataRisksChart = Convert.ToString(Convert.ToInt64(x86_es) / 1000) + "," + Convert.ToString(Convert.ToInt64(x86_gc) / 1000) + "," + Convert.ToString(Convert.ToInt64(x86_rd) / 1000),


                    dataRoiChart = Convert.ToString(Convert.ToInt64(Convert.ToDecimal(x86_roi) * 100)) + "," + Convert.ToString(Convert.ToInt64(Convert.ToDecimal(z14_roi) * 100)),
                    dataPaybackChart = Convert.ToString(Convert.ToDecimal(x86_pp).ToString("0.##")) + "," + Convert.ToString(Convert.ToDecimal(z14_pp).ToString("0.##")),
                    //dataPaybackChart = Convert.ToString("")

                };
                Tbl_T_Specify specify = new Tbl_T_Specify();
                specify.Analytics = analytics;
                specify.BusinessImperative = businessimperative;
                specify.Industry = industry;
                specify.Investment = amount;
                specify.CreatedOn = DateTime.Now;
                specify.CreatedBy = Convert.ToString(Session["Userid"]) != null ? Convert.ToInt32(Session["Userid"]) : 0;
                _context.Tbl_T_Specify.Add(specify);
                _context.SaveChanges();

                List<Tbl_T_Specify_Value> specifyvalue = new List<Tbl_T_Specify_Value>();
                Tbl_T_Specify_Value specify_Value = new Tbl_T_Specify_Value();
                specify_Value.SpecifyId = specify.Id;

                specify_Value.Parameter_Id = 1;
                specify_Value.X86_Value = Convert.ToInt64(Convert.ToDecimal(x86_roi) * 100);
                specify_Value.Z14_Value = Convert.ToInt64(Convert.ToDecimal(z14_roi) * 100);
                specifyvalue.Add(specify_Value);

                Tbl_T_Specify_Value specify_Value1 = new Tbl_T_Specify_Value();
                specify_Value1.SpecifyId = specify.Id;
                specify_Value1.Parameter_Id = 2;
                specify_Value1.X86_Value = Convert.ToDecimal(x86_pp);
                specify_Value1.Z14_Value = Convert.ToDecimal(z14_pp);
                specifyvalue.Add(specify_Value1);

                Tbl_T_Specify_Value specify_Value2 = new Tbl_T_Specify_Value();
                specify_Value2.SpecifyId = specify.Id;
                specify_Value2.Parameter_Id = 3;
                specify_Value2.X86_Value = Convert.ToInt64(x86_cp) / 1000;
                specify_Value2.Z14_Value = Convert.ToInt64(z14_cp) / 1000;
                specifyvalue.Add(specify_Value2);

                Tbl_T_Specify_Value specify_Value3 = new Tbl_T_Specify_Value();
                specify_Value3.SpecifyId = specify.Id;
                specify_Value3.Parameter_Id = 4;
                specify_Value3.X86_Value = Convert.ToInt64(x86_itp) / 1000;
                specify_Value3.Z14_Value = Convert.ToInt64(z14_itp) / 1000;
                specifyvalue.Add(specify_Value3);

                Tbl_T_Specify_Value specify_Value4 = new Tbl_T_Specify_Value();
                specify_Value4.SpecifyId = specify.Id;
                specify_Value4.Parameter_Id = 5;
                specify_Value4.X86_Value = Convert.ToInt64(x86_op) / 1000;
                specify_Value4.Z14_Value = Convert.ToInt64(z14_op) / 1000;
                specifyvalue.Add(specify_Value4);

                Tbl_T_Specify_Value specify_Value5 = new Tbl_T_Specify_Value();
                specify_Value5.SpecifyId = specify.Id;
                specify_Value5.Parameter_Id = 6;
                specify_Value5.X86_Value = Convert.ToInt64(x86_sy) / 1000;
                specify_Value5.Z14_Value = Convert.ToInt64(z14_sy) / 1000;
                specifyvalue.Add(specify_Value5);

                Tbl_T_Specify_Value specify_Value6 = new Tbl_T_Specify_Value();
                specify_Value6.SpecifyId = specify.Id;
                specify_Value6.Parameter_Id = 7;
                specify_Value6.X86_Value = Convert.ToInt64(x86_sa) / 1000;
                specify_Value6.Z14_Value = Convert.ToInt64(z14_sa) / 1000;
                specifyvalue.Add(specify_Value6);

                Tbl_T_Specify_Value specify_Value7 = new Tbl_T_Specify_Value();
                specify_Value7.SpecifyId = specify.Id;
                specify_Value7.Parameter_Id = 8;
                specify_Value7.X86_Value = Convert.ToInt64(x86_oc) / 1000;
                specify_Value7.Z14_Value = Convert.ToInt64(z14_oc) / 1000;
                specifyvalue.Add(specify_Value7);

                Tbl_T_Specify_Value specify_Value8 = new Tbl_T_Specify_Value();
                specify_Value8.SpecifyId = specify.Id;
                specify_Value8.Parameter_Id = 9;
                specify_Value8.X86_Value = Convert.ToInt64(x86_ir) / 1000;
                specify_Value8.Z14_Value = Convert.ToInt64(z14_ir) / 1000;
                specifyvalue.Add(specify_Value8);

                Tbl_T_Specify_Value specify_Value9 = new Tbl_T_Specify_Value();
                specify_Value9.SpecifyId = specify.Id;
                specify_Value9.Parameter_Id = 10;
                specify_Value9.X86_Value = Convert.ToInt64(x86_ftv) / 1000;
                specify_Value9.Z14_Value = Convert.ToInt64(z14_ftv) / 1000;
                specifyvalue.Add(specify_Value9);

                Tbl_T_Specify_Value specify_Value10 = new Tbl_T_Specify_Value();
                specify_Value10.SpecifyId = specify.Id;
                specify_Value10.Parameter_Id = 11;
                specify_Value10.X86_Value = Convert.ToInt64(x86_te) / 1000;
                specify_Value10.Z14_Value = Convert.ToInt64(z14_te) / 1000;
                specifyvalue.Add(specify_Value10);

                Tbl_T_Specify_Value specify_Value11 = new Tbl_T_Specify_Value();
                specify_Value11.SpecifyId = specify.Id;
                specify_Value11.Parameter_Id = 12;
                specify_Value11.X86_Value = Convert.ToInt64(x86_es) / 1000;
                specify_Value11.Z14_Value = Convert.ToInt64(z14_es) / 1000;
                specifyvalue.Add(specify_Value11);

                Tbl_T_Specify_Value specify_Value12 = new Tbl_T_Specify_Value();
                specify_Value12.SpecifyId = specify.Id;
                specify_Value12.Parameter_Id = 13;
                specify_Value12.X86_Value = Convert.ToInt64(x86_gc) / 1000;
                specify_Value12.Z14_Value = Convert.ToInt64(z14_gc) / 1000;
                specifyvalue.Add(specify_Value12);

                Tbl_T_Specify_Value specify_Value13 = new Tbl_T_Specify_Value();
                specify_Value13.SpecifyId = specify.Id;
                specify_Value13.Parameter_Id = 14;
                specify_Value13.X86_Value = Convert.ToInt64(x86_rd) / 1000;
                specify_Value13.Z14_Value = Convert.ToInt64(z14_rd) / 1000;
                specifyvalue.Add(specify_Value13);

                _context.Tbl_T_Specify_Value.AddRange(specifyvalue);
                _context.SaveChanges();

                Session["specifyid"] = Convert.ToString(specify.Id);
                Session["Roi"] = Convert.ToString(Convert.ToInt64((Convert.ToDecimal(x86_roi) * 100) - (Convert.ToDecimal(z14_roi) * 100)));
                Session["Pbp"] = Convert.ToString(Convert.ToInt64((Convert.ToDecimal(x86_pp)) - (Convert.ToDecimal(z14_pp))));
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }


        public ActionResult Industry_select(int industry)
        {
            Session["industry_id"] = industry;
            var Lst_BusinessImperative = _context.Tbl_M_Business_Imperative.Where(x => x.Status == true && x.Industry_id == industry).Select(x => new Common.droplist { Id = x.BM_Id, Text = x.BM_Name, Img_url = x.image_url }).ToList();

            //m_specify.lst_TypesOfAnalytics = _context.t.Where(x => x.Status == true).Select(x => new Common.droplist { id = x.BM_Id, text = x.BM_Name, img_url = x.image_url }).ToList();
            return Json(Lst_BusinessImperative, JsonRequestBehavior.AllowGet);
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public void pdfExport(string graph1, string graph2, string graph3, string graph4, string graph5, string graph6)
        {

            string fileName = DateTime.Now.Ticks.ToString();
            string fileNameWitPath = Path.Combine(Server.MapPath("~/Assets/Images/"), fileName + "1.png");
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(graph1);
                    bw.Write(data);
                    bw.Close();
                }
                fs.Close();
            }

            fileNameWitPath = Path.Combine(Server.MapPath("~/Assets/Images/"), fileName + "2.png");
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(graph2);
                    bw.Write(data);
                    bw.Close();
                }
                fs.Close();
            }

            fileNameWitPath = Path.Combine(Server.MapPath("~/Assets/Images/"), fileName + "3.png");
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(graph3);
                    bw.Write(data);
                    bw.Close();
                }
                fs.Close();
            }

            fileNameWitPath = Path.Combine(Server.MapPath("~/Assets/Images/"), fileName + "4.png");
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(graph4);
                    bw.Write(data);
                    bw.Close();
                }
                fs.Close();
            }

            fileNameWitPath = Path.Combine(Server.MapPath("~/Assets/Images/"), fileName + "5.png");
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(graph5);
                    bw.Write(data);
                    bw.Close();
                }
                fs.Close();
            }

            fileNameWitPath = Path.Combine(Server.MapPath("~/Assets/Images/"), fileName + "6.png");
            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(graph6);
                    bw.Write(data);
                    bw.Close();
                }
                fs.Close();
            }

            var FileName = "Export.Pdf";
            string extension;
            string encoding;
            string mimeType;
            string[] streams;
            Warning[] warnings;

            LocalReport report = new LocalReport();
            report.ReportPath = Server.MapPath(ConfigurationManager.AppSettings["ReportPath"]);
            report.EnableExternalImages = true;
            ReportParameter[] parameter = new ReportParameter[13];
            string filepath = Server.MapPath("~/Assets/Images/" + fileName + "1.png");
            string filepath1 = new Uri(filepath).AbsoluteUri;
            filepath = Server.MapPath("~/Assets/Images/" + fileName + "2.png");
            string filepath2 = new Uri(filepath).AbsoluteUri;
            filepath = Server.MapPath("~/Assets/Images/" + fileName + "3.png");
            string filepath3 = new Uri(filepath).AbsoluteUri;
            filepath = Server.MapPath("~/Assets/Images/" + fileName + "4.png");
            string filepath4 = new Uri(filepath).AbsoluteUri;
            filepath = Server.MapPath("~/Assets/Images/" + fileName + "5.png");
            string filepath5 = new Uri(filepath).AbsoluteUri;
            filepath = Server.MapPath("~/Assets/Images/" + fileName + "6.png");
            string filepath6 = new Uri(filepath).AbsoluteUri;

            parameter[0] = new ReportParameter("UserName", "Result");
            parameter[1] = new ReportParameter("Industry", Convert.ToString(Session["industry"]));
            parameter[2] = new ReportParameter("BusinessImperative", Convert.ToString(Session["businessimperative"]));
            parameter[3] = new ReportParameter("Investment", Convert.ToString(Session["amount"]));
            parameter[4] = new ReportParameter("Analytics", Convert.ToString(Session["typeofanalytics"]));
            parameter[5] = new ReportParameter("Roi", "Result");
            parameter[6] = new ReportParameter("Pbp", "Result");
            parameter[7] = new ReportParameter("Graph1", filepath1);
            parameter[8] = new ReportParameter("Graph2", filepath2);
            parameter[9] = new ReportParameter("Graph3", filepath3);
            parameter[10] = new ReportParameter("Graph4", filepath4);
            parameter[11] = new ReportParameter("Graph5", filepath5);
            parameter[12] = new ReportParameter("Graph6", filepath6);
            report.EnableHyperlinks = true;
            report.SetParameters(parameter);
            report.Refresh();



            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";//This refers to the dataset name in the RDLC file  
            int id = Convert.ToInt32(Session["specifyid"]);
            var s = _context.Tbl_T_Specify_Value
                    .Where(x => x.SpecifyId == id && (x.Parameter_Id == 3 || x.Parameter_Id == 4 || x.Parameter_Id == 5))
                    .Join(_context.Tbl_M_Parameter , y=>y.Parameter_Id,z=>z.id,(y,z)=>new {y.X86_Value,y.Z14_Value,z.Parameter_Name }).ToList()
                    ;
            var s1 = ToDataTable(s);
            rds.Value = s1;
            report.DataSources.Add(rds);
            report.Refresh();


            byte[] mybytes = report.Render("pdf", null,
                            out extension, out encoding,
                            out mimeType, out streams, out warnings); //for exporting to PDF  


            var FilePath = Server.MapPath(ConfigurationManager.AppSettings["ExportPdfFile"]) + FileName;
            bool exists = System.IO.Directory.Exists(Server.MapPath(ConfigurationManager.AppSettings["ExportPdfFile"]));

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(ConfigurationManager.AppSettings["ExportPdfFile"]));
            }
            if (System.IO.File.Exists(FilePath))
            {
                System.IO.File.Delete(FilePath);
            }
            if (!System.IO.File.Exists(FilePath))
            {
                FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
                fs.Write(mybytes, 0, mybytes.Length);
                fs.Close();
            }
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader(
                "content-disposition",
                "attachment; filename= filename" + "." + extension);
            Response.OutputStream.Write(mybytes, 0, mybytes.Length); // create the file  
            Response.Flush(); // send it to the client to download  
            Response.End();
        }

    }
}