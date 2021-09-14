using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        //public ActionResult FindRecipe()
        //{
        //    return View();
        //}


        [HttpGet]
        public async Task<IActionResult> FindRecipe(string ingre)
        {
            using (var client = new HttpClient())
            {
                if (ingre != null)
                {
                    List<RecipeClass> rc = new List<RecipeClass>();

                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync("https://localhost:44349/api/Recipes/item/" + ingre))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            rc = JsonConvert.DeserializeObject<List<RecipeClass>>(apiResponse);
                        }
                    }
                    return View(rc);
                }

                else
                {
                    List<RecipeClass> rc = new List<RecipeClass>();

                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync("https://localhost:44349/api/Recipes"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            rc = JsonConvert.DeserializeObject<List<RecipeClass>>(apiResponse);
                        }
                    }

                    return View(rc);
                }


            }

        }

        [HttpGet]
        public async Task<IActionResult> MyRecipes()
        {
            using (var client = new HttpClient())
            {

                List<RecipeClass> rc = new List<RecipeClass>();

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("https://localhost:44349/myrecipe"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        rc = JsonConvert.DeserializeObject<List<RecipeClass>>(apiResponse);
                    }
                }
                return View(rc);
            }

        }

        [HttpGet]
        public async Task<ActionResult> Starred(int id)
        {
            
            RecipeClass r = new RecipeClass();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44349/api/Recipes/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    r = JsonConvert.DeserializeObject<RecipeClass>(apiResponse);
                    StringContent content1 = new StringContent(JsonConvert.SerializeObject(r), Encoding.UTF8, "application/json");
                    var response_2 = await httpClient.PostAsync("https://localhost:44349/myrecipe/" + id, content1);
                }

            }
            return RedirectToAction("MyRecipes");
        }


  
    }

}

