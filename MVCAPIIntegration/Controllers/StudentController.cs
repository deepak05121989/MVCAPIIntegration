using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MVCAPIIntegration.Models;
using Newtonsoft.Json;

namespace MVCAPIIntegration.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            List<Student> student=new List<Student>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7084/api/");
              var response=  client.GetAsync("Home").GetAwaiter().GetResult();
                if(response.IsSuccessStatusCode && HttpStatusCode.OK==response.StatusCode)
                {
                    student= JsonConvert.DeserializeObject<List<Student>>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                }

            }
            
            return View(student);
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            Student student = new Student();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7084/api/");
                var response = client.GetAsync($"Home/{id}").GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode && HttpStatusCode.OK == response.StatusCode)
                {
                    student = JsonConvert.DeserializeObject<Student>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                }

            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7084/api/Home");
                    using (HttpRequestMessage requestMessage = new HttpRequestMessage())
                    {
                        requestMessage.Method = HttpMethod.Post;
                        requestMessage.Content = new StringContent(JsonConvert.SerializeObject(student),Encoding.UTF8,"application/json");
                        var response = client.SendAsync(requestMessage).GetAwaiter().GetResult();
                        if (response.IsSuccessStatusCode && HttpStatusCode.OK == response.StatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }

                }

              
            }
            catch
            {
                return View();
            }
            return View();
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
