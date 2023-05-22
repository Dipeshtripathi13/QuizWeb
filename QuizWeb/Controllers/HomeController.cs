using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuizWeb.Models;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace QuizWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        string baseURL = "https://localhost:7229";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        // this code defines an action method Index() instide teh homecontroller.
        public async Task<IActionResult> Index()// this method retrive a list of questions from external api and convert the response
            //into DataTable and pass it to a view for rendering.
        {
            DataTable dt = new DataTable();
            using(var client = new HttpClient())// httpclient class is used to make an http get request to the apiend point
            {
                client.BaseAddress= new Uri(baseURL);// represent the base url of the api/ This is used as base url forl all the request setn using this instance "httpClient"

                client.DefaultRequestHeaders.Accept.Clear();//clear any previously set "Accept" headers
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // to set the 'Accept' header to accept json responses from the api

                HttpResponseMessage getData = await client.GetAsync("Question/3");

                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    dt = JsonConvert.DeserializeObject<DataTable>(results);// this convert json into table format
                }
                else
                {
                    Console.WriteLine("Error Calling the question Api");
                }
                ViewData.Model= dt; // this property pass the DataTable obect ot the view for rendering
            }
            return View();
        }
        //calling web api using entity model class
        public async Task<IActionResult> Index2()
        {
            IList<Question_Entity> question= new List<Question_Entity>();
            using (var client = new HttpClient())
            {
                client.BaseAddress= new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage getData = await client.GetAsync("Question/random");
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    question = JsonConvert.DeserializeObject<List<Question_Entity>>(results);
                }
                else
                {
                    Console.WriteLine("Error calling web API");
                }
                ViewData.Model = question;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}