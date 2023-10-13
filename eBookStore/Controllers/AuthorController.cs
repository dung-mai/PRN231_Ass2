using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using Ultility;

namespace eBookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly HttpClient client;
        private string AuthorApiUrl = "";

        public AuthorController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            AuthorApiUrl = $"{Configuration.ApiURL}/Authors";
        }

        // GET: AuthorController
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(AuthorApiUrl);
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var resultList = JsonSerializer.Deserialize<List<AuthorDTO>>(strData, options);
            var Authors = resultList ?? new List<AuthorDTO>();

            return View(Authors);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            //ViewData["CategoryId"] = new SelectList(await GetCategories(), "CategoryId", "CategoryName");
            return View(new AuthorCreateDTO());
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorCreateDTO author)
        {
            AuthorApiUrl = $"{Configuration.ApiURL}/Authors";
            var response = client.PostAsJsonAsync(AuthorApiUrl, author).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            AuthorApiUrl = $"{Configuration.ApiURL}/Authors?$filter=AuthorId eq {id}";
            HttpResponseMessage responseMessage = await client.GetAsync(AuthorApiUrl);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return NotFound();
            }

            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var result = JsonSerializer.Deserialize<List<AuthorDTO>>(strData, options);
            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            return View(result[0]);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuthorDTO author)
        {
            AuthorApiUrl = $"{Configuration.ApiURL}/Authors/{author.AuthorId}";
            var response = client.PutAsJsonAsync(AuthorApiUrl, author).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
