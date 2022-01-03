using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using TodoMVCApp.Models;

namespace TodoMVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "GetAll");
            var client = _httpClientFactory.CreateClient("TodoApi");

            var response = await client.SendAsync(request);
            var data = await response.Content.ReadAsStringAsync();

            var todo = JsonConvert.DeserializeObject<List<ToDo>>(data);

            return View(todo);

        }

        public async Task<IActionResult> Update(int serialNo)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"GetTodo/{serialNo}");
            var client = _httpClientFactory.CreateClient("TodoApi");

            var response = await client.SendAsync(request);
            var data = await response.Content.ReadAsStringAsync();

            var todo = JsonConvert.DeserializeObject<ToDo>(data);

            return View(todo);

        }
        [HttpPost]
        public async Task<IActionResult> Update(ToDo todo)
        {
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "UpdateTodo");
                //var value = new ToDo { SerialNo = SerialNo, Description = Description, IsCompleted = IsCompleted, Title = Title };
                request.Content = new StringContent(JsonConvert.SerializeObject(todo), Encoding.UTF8, "application/json");
                var client = _httpClientFactory.CreateClient("TodoApi");

                var response = await client.SendAsync(request);
                return RedirectToAction("Index");
            }
             return View(todo);
        }
        // HTTP GET VERSION
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ToDo todo)
        {
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "CreateTodo");
                request.Content = new StringContent(JsonConvert.SerializeObject(todo), Encoding.UTF8, "application/json");
                var client = _httpClientFactory.CreateClient("TodoApi");

                var response = await client.SendAsync(request);
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }
        public async Task<IActionResult> Delete(int serialNo)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"DeleteToDo/{serialNo}");
            var client = _httpClientFactory.CreateClient("TodoApi");

            var response = await client.SendAsync(request);

            return RedirectToAction("Index");

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}