using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using AppView.Models;

namespace AppView.Controllers
{
    public class GiaoDichController : Controller
    {
        private readonly HttpClient _httpClient;

        public GiaoDichController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7263/api/GiaoDichTienMaHoa");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var giaoDichList = JsonConvert.DeserializeObject<List<GiaoDichTienMaHoa>>(content);
                return View(giaoDichList);
            }
            return View(new List<GiaoDichTienMaHoa>());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GiaoDichTienMaHoa giaoDich)
        {
            var content = JsonConvert.SerializeObject(giaoDich);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://localhost:7263/api/GiaoDichTienMaHoa", stringContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<List<object>>(errorContent); 
                ViewBag.Errors = errors;
                return View(giaoDich);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7263/api/GiaoDichTienMaHoa/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var giaoDich = JsonConvert.DeserializeObject<GiaoDichTienMaHoa>(content);
                return View(giaoDich);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, GiaoDichTienMaHoa giaoDich)
        {
            if (id != giaoDich.ID)
            {
                return BadRequest("ID không khớp");
            }

            var content = JsonConvert.SerializeObject(giaoDich);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"https://localhost:7263/api/GiaoDichTienMaHoa/{id}", stringContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<List<object>>(errorContent); 
                ViewBag.Errors = errors;
                return View(giaoDich);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7263/api/GiaoDichTienMaHoa/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
