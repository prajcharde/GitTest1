using ImagePractice.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImagePractice.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null)
            {
                //get the bytes from the uploaded file
                byte[] data = GetBytesFromFile(file);

                using (Context db = new Context())
                {
                    db.FileTable.Add(new ProfileViewModel() { UploadedFile = data });
                    db.SaveChanges();
                }
            }

            return View();
        }

        //Method to convert file into byte array
        private byte[] GetBytesFromFile(HttpPostedFileBase file)
        {
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                return memoryStream.ToArray();
            }
        }
    }
}
    
