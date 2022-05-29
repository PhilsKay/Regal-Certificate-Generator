using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RegalHubCertificate.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RegalHubCertificate.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment Environment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            Environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetCertificate(Information obj)
        {
            if (ModelState.IsValid && obj.Pass == "Daddy123")
            {
                string rootPath = this.Environment.WebRootPath;
                byte[] InByte;
                FileInfo path = new FileInfo(rootPath + "\\file\\pdfcertificate.pdf");
                using (var reader = new PdfReader(path))
                using (MemoryStream memory = new MemoryStream())
                using (PdfWriter writer = new PdfWriter(memory))
                using (PdfDocument pdf = new PdfDocument(reader, writer))
                using (Document doc = new Document(pdf))
                {
                    PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                    Paragraph para = new Paragraph(obj.Name).SetFontSize(60).SetFont(font);
                    para.SetFixedPosition(0, 255, 850).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    doc.Add(para);
                    doc.Close();
                    doc.Flush();
                    InByte = memory.ToArray();
                }
                   
                return new FileContentResult(InByte, "application/pdf");
            }
            ModelState.AddModelError(obj.Pass, "Yo? Could not generate Certificate");
            return View("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
