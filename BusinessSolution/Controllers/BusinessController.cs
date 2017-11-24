using BusinessSolution.Models;
using Codaxy.WkHtmlToPdf;
using Pechkin;
using Pechkin.Synchronized;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace BusinessSolution.Controllers
{
    public class BusinessController : Controller
    {
        //
        // GET: /Business/

        public ActionResult Index()
        {
            //PaymentDetails paymentDetails = new PaymentDetails()
            //{
            //    PaymentID = 0,
            //    FirstName = "abhishek",
            //    MiddleName = "Dineshbhai",
            //    LastName = "khetani",
            //    Description = "asdasd",
            //    City = "Ahmebabad",
            //    Phone = "9924047260",
            //    Rupees = "2000",
            //    PaymentStatus = true
            //};
            //using (var context = new EFCodeFirstContext())
            //{
            //    context.PaymentDetails.Add(paymentDetails);
            //    context.SaveChanges();
            //}

            return View();
        }

        [HttpPost]
        public ActionResult GetPaymentInfo()
        {
            var paymentList = new List<PaymentDetails>();
            int recordsTotal = 0;
            using (var context = new EFCodeFirstContext())
            {
                paymentList = context.PaymentDetails.ToList();
                recordsTotal = context.PaymentDetails.Count();
            }

            return Json(new { recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = paymentList });
        }

        [HttpPost]
        public ActionResult AddPaymentInfo(PaymentDetails paymentDetails)
        {
            ModelState.Remove("PaymentID");
            if (ModelState.IsValid)
            {
                paymentDetails.Rupees = Regex.Replace(paymentDetails.Rupees, @"[^0-9a-zA-Z]+", "");

                PaymentDetails existPaymentDetails;
                using (var ctx = new EFCodeFirstContext())
                {
                    existPaymentDetails = ctx.PaymentDetails.Where(s => s.PaymentID == paymentDetails.PaymentID).FirstOrDefault<PaymentDetails>();
                }

                if (existPaymentDetails == null)
                {
                    using (var context = new EFCodeFirstContext())
                    {
                        context.PaymentDetails.Add(paymentDetails);
                        context.SaveChanges();
                    }
                }
                else
                {
                    using (var context = new EFCodeFirstContext())
                    {
                        existPaymentDetails = paymentDetails;
                        context.Entry(existPaymentDetails).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeletePaymentInfo(PaymentDetails paymentDetails)
        {
            if (paymentDetails.PaymentID > 0)
            {
                PaymentDetails existPaymentDetails;
                using (var ctx = new EFCodeFirstContext())
                {
                    existPaymentDetails = ctx.PaymentDetails.Where(s => s.PaymentID == paymentDetails.PaymentID).FirstOrDefault<PaymentDetails>();
                }

                if (existPaymentDetails != null)
                {
                    using (var context = new EFCodeFirstContext())
                    {
                        context.Entry(existPaymentDetails).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("No Data", JsonRequestBehavior.AllowGet);
                }
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PrintPaymentInfo(PaymentDetails paymentDetails)
        {
            PaymentDetails existPaymentDetails;
            using (var ctx = new EFCodeFirstContext())
            {
                existPaymentDetails = ctx.PaymentDetails.Where(s => s.PaymentID == paymentDetails.PaymentID).FirstOrDefault<PaymentDetails>();
            }

            if (existPaymentDetails != null)
            {


                string html = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Content/template/invoice.html"));
                html = html.Replace("###PaymentID###", Convert.ToString(existPaymentDetails.PaymentID))
                    .Replace("###TodayDate###", Convert.ToString(DateTime.Now.Date))
                    .Replace("###FirstName###", existPaymentDetails.FirstName)
                    .Replace("###MiddleName###", existPaymentDetails.MiddleName)
                    .Replace("###LastName###", existPaymentDetails.LastName)
                    .Replace("###Phone###", existPaymentDetails.Phone)
                    .Replace("###Description###", existPaymentDetails.Description)
                    .Replace("###Price###", existPaymentDetails.Rupees)
                    .Replace("###Rupees###", existPaymentDetails.Rupees);

                var savePath = Server.MapPath("~/Pdf");
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                var fileName = "Payment-Details_" + existPaymentDetails.PaymentID + ".pdf";
                var saveFile = System.IO.Path.Combine(savePath, fileName);


                var globalConfig = new GlobalConfig()
                    .SetMargins(0, 0, 0, 0)
                    .SetPaperSize(PaperKind.A4);

                var pdfWriter = new SynchronizedPechkin(globalConfig);

                pdfWriter.Error += OnError;
                pdfWriter.Warning += OnWarning;

                var objectConfig = new ObjectConfig()
                    .SetPrintBackground(true)
                    .SetIntelligentShrinking(false);

                var pdfBuffer = pdfWriter.Convert(objectConfig, html);

                System.IO.File.WriteAllBytes(saveFile, pdfBuffer);

                return Json(new { fileName = fileName, errorMessage = "" });
            }

            return Json(new { fileName = "", errorMessage = "" });
        }

        [HttpGet]
        [DeleteFileAttribute]
        public ActionResult Download(string file)
        {
            //get the temp folder and file path in server
            string fullPath = Path.Combine(Server.MapPath("~/Pdf"), file);

            //return the file for download, this is an Excel 
            //so I set the file content type to "application/vnd.ms-excel"
            return File(fullPath, System.Net.Mime.MediaTypeNames.Application.Octet, file);
        }

        private void OnWarning(SimplePechkin converter, string warningtext)
        {
            throw new NotImplementedException();
        }

        private void OnError(SimplePechkin converter, string errortext)
        {
            throw new NotImplementedException();
        }
    }
}
