using Assignment7.Repository;
using Assignment7.Repository.Entity;
using Assignment7.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Assignment7.Controllers
{
    public class HomeController : Controller
    {
        private Employeecontext db = new Employeecontext();

        // GET: Home
        private readonly IEmployeeService employeeservice;

        public ActionResult Index()
        {
            return View(db.Employees.ToList());//get the list of records from database
        }
        public HomeController(IEmployeeService employeeService)
        {
            this.employeeservice = employeeService;
        }
        public ActionResult Create()
        {
            ViewBag.StateList = db.StateList;
            var model = new Employee();
            return View(model);
        }


        // POST: Employee/Create


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Phone,MaritalStatus,State,City,image")] Employee emp, HttpPostedFileBase imageFile)
        {
            
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(imageFile.FileName);

                    emp.image = fileName;
                    db.Employees.Add(emp);//Adding into the database
                    db.SaveChanges();
                    var path = Path.Combine(Server.MapPath("~/Content/img/"));
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    imageFile.SaveAs(path + "/" + fileName);
                }
                return RedirectToAction("Index");
                //return RedirectToAction("Index");
            }
            ViewBag.StateList = db.StateList;
            ViewBag.CityList = db.CityList;
            return View(emp);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);//find the record by id
           
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateList = db.StateList;
            ViewBag.CityList = db.CityList;
            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Phone,MaritalStatus,State,City")] Employee employee,string src)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;//record with editable stage
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateList = db.StateList;
            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee emp = db.Employees.Find(id);//find the record by id
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee product = db.Employees.Find(id);
            db.Employees.Remove(product);//delete record from database
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FillCity(int state)
        {
            
            var cities = db.CityList.Where(c => c.StateId == state);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

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