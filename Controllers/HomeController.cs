using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjetoEscola.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoEscola.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static HttpClient client = new HttpClient();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CadastroEscola()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<HttpStatusCode> SalvarEscola(EscolaViewModel _escolaViewModel)
        {
            var json = JsonConvert.SerializeObject(_escolaViewModel);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //Aqui vamos chamar o endpoint
            var response = await client.PostAsync("api/products", stringContent);
            response.EnsureSuccessStatusCode();

            ModelState.Clear();
            ViewBag.Message = "Usuário registrado com sucesso.";

            return response.StatusCode;
        }
    }
}
