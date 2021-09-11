using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using RecipesMVC.Data;
using RecipesMVC.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace RecipesMVC.Controllers
{
    public class RecipesController : Controller
    {
        // GET: RecipesController
            

            // GET: Recipes
            [HttpGet]
            public async Task<IActionResult> Index()
            {
                List<RecipeClass> RecipeInfo = new List<RecipeClass>();

                using (var client = new HttpClient())
                {
                    string Baseurl = "https://localhost:44349/";
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("api/Recipes");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var ProductResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        RecipeInfo = JsonConvert.DeserializeObject<List<RecipeClass>>(ProductResponse);

                    }
                    //returning the employee list to view  
                    return View(RecipeInfo);


                }
            }

            public ActionResult AddEmployee()
            {
                return View();
            }

          /*  [HttpPost]
            public async Task<ActionResult> AddEmployee(Accioemp e)
            {
                Accioemp Emplobj = new Accioemp();

                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync("https://localhost:44314/api/Accigoemps/", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Emplobj = JsonConvert.DeserializeObject<Accioemp>(apiResponse);
                    }
                }
                return RedirectToAction("Index");
            }

            [HttpGet]
            public async Task<ActionResult> UpdateEmployee(int id)
            {
                Accioemp emp = new Accioemp();
                _log4net.Info("Get Product by " + id + " is invoked");
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:44314/api/Accigoemps/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        emp = JsonConvert.DeserializeObject<Accioemp>(apiResponse);
                    }
                }
                return View(emp);
            }

            [HttpPost]
            public async Task<ActionResult> UpdateEmployee(Accioemp e)
            {
                Accioemp receivedemp = new Accioemp();

                using (var httpClient = new HttpClient())
                {
                    #region
                    //var content = new MultipartFormDataContent();
                    //content.Add(new StringContent(reservation.Empid.ToString()), "Empid");
                    //content.Add(new StringContent(reservation.Name), "Name");
                    //content.Add(new StringContent(reservation.Gender), "Gender");
                    //content.Add(new StringContent(reservation.Newcity), "Newcity");
                    //content.Add(new StringContent(reservation.Deptid.ToString()), "Deptid");
                    #endregion
                    int id = e.Empid;
                    StringContent content1 = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("https://localhost:44314/api/Accigoemps/" + id, content1))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        receivedemp = JsonConvert.DeserializeObject<Accioemp>(apiResponse);
                    }
                }
                return RedirectToAction("Index");
            }

            [HttpGet]
            public async Task<ActionResult> DeleteEmployee(int id)
            {
                TempData["empid"] = id;
                _log4net.Info("Get Product by " + id + " is invoked");
                Accioemp e = new Accioemp();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:44314/api/Accigoemps/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        e = JsonConvert.DeserializeObject<Accioemp>(apiResponse);
                    }
                }
                return View(e);
            }


            [HttpPost]
            // [ActionName("DeleteEmployee")]
            public async Task<ActionResult> DeleteEmployee(Accioemp e)
            {
                int empid = Convert.ToInt32(TempData["empid"]);
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync("https://localhost:44314/api/Accigoemps/" + empid))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }

                return RedirectToAction("Index");
            }*/
        }
}
