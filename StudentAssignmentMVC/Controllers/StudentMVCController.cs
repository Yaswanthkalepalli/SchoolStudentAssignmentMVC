using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using Newtonsoft.Json;
using StudentAssignmentMVC.Models;

namespace StudentAssignmentMVC.Controllers
{
    public class StudentMVCController : Controller
    {
        // GET: StudentMVC
        [EnableCors(headers: "*", methods: "*", origins: "*")]
        public ActionResult Index()
        {
            IEnumerable<StudentModelMVC> supdata = null;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44352/api/";
                var json = webClient.DownloadString("Students");
                var list = JsonConvert.DeserializeObject<List<StudentModelMVC>>(json);
                supdata = list.ToList();
                return View(supdata);
            }
        }

        // GET: StudentMVC/Details/5
        public ActionResult Details(int id)
        {
            StudentModelMVC supdata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44352/api/";
                var json = webClient.DownloadString("Students/" + id);
                supdata = JsonConvert.DeserializeObject<StudentModelMVC>(json);
            }
            return View(supdata);
        }

        // GET: StudentMVC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentMVC/Create
        [HttpPost]
        public ActionResult Create(StudentModelMVC model)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.BaseAddress = "https://localhost:44352/api/";
                    var url = "Students/POST";
                    //webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string data = JsonConvert.SerializeObject(model);
                    var response = webClient.UploadString(url, data);
                    JsonConvert.DeserializeObject<StudentModelMVC>(response);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        // GET: StudentMVC/Edit/5
        public ActionResult Edit(int id)
        {
            StudentModelMVC empdata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44352/api/";

                var json = webClient.DownloadString("Students/" + id);
                //  var list = emp 
                empdata = JsonConvert.DeserializeObject<StudentModelMVC>(json);
            }
            return View(empdata);
        }

        // POST: StudentMVC/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, StudentModelMVC model)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.BaseAddress = "https://localhost:44352/api/Students/" + id;
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string data = JsonConvert.SerializeObject(model);
                    var response = webClient.UploadString(webClient.BaseAddress, "PUT", data);
                    StudentModelMVC modeldata = JsonConvert.DeserializeObject<StudentModelMVC>(response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        // GET: StudentMVC/Delete/5
        public ActionResult Delete(int id)
        {
            StudentModelMVC empdata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44352/api/";

                var json = webClient.DownloadString("Students/" + id);
                //  var list = emp 
                empdata = JsonConvert.DeserializeObject<StudentModelMVC>(json);
            }
            return View(empdata);
        }

        // POST: StudentMVC/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, StudentModelMVC model)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    NameValueCollection nv = new NameValueCollection();
                    var url = "https://localhost:44352/api/Students/" + id;
                    var response = Encoding.ASCII.GetString(webClient.UploadValues(url, "Delete", nv));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }
    }
}
