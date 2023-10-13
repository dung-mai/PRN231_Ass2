using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using Ultility;

namespace eBookStore.Controllers
{
    public class PublisherController : Controller
    {
        private readonly HttpClient client;
        private string PublisherApiUrl = "";

        public PublisherController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            PublisherApiUrl = $"{Configuration.ApiURL}/Publishers";
        }

        // GET: PublisherController
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(PublisherApiUrl);
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var resultList = JsonSerializer.Deserialize<List<PublisherDTO>>(strData, options);
            var Publishers = resultList ?? new List<PublisherDTO>();

            return View(Publishers);
        }

        // GET: PublisherController/Create
        public ActionResult Create()
        {
            //ViewData["CategoryId"] = new SelectList(await GetCategories(), "CategoryId", "CategoryName");
            return View(new PublisherCreateDTO());
        }

        // POST: PublisherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PublisherCreateDTO publisher)
        {
            PublisherApiUrl = $"{Configuration.ApiURL}/Publishers";
            var response = client.PostAsJsonAsync(PublisherApiUrl, publisher).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        // GET: PublisherController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            PublisherApiUrl = $"{Configuration.ApiURL}/Publishers?$filter=PubId eq {id}";
            HttpResponseMessage responseMessage = await client.GetAsync(PublisherApiUrl);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return NotFound();
            }

            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var result = JsonSerializer.Deserialize<List<PublisherDTO>>(strData, options);
            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            return View(result[0]);
        }

        // POST: PublisherController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PublisherDTO publisher)
        {
            PublisherApiUrl = $"{Configuration.ApiURL}/Publishers/{publisher.PubId}";
            var response = client.PutAsJsonAsync(PublisherApiUrl, publisher).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        // GET: PublisherController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PublisherController/Delete/5
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
