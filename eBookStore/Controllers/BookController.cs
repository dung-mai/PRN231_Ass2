using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using Ultility;

namespace eBookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient client;
        private string BookApiUrl = "";

        public BookController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            BookApiUrl = $"{Configuration.ApiURL}/Books";
        }

        // GET: BookController
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(BookApiUrl);
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var resultList = JsonSerializer.Deserialize<List<BookResponseDTO>>(strData, options);
            var Books = resultList ?? new List<BookResponseDTO>();

            return View(Books);
        }

        //// GET: BookController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: BookController/Create

        public ActionResult Create()
        {
            //ViewData["CategoryId"] = new SelectList(await GetCategories(), "CategoryId", "CategoryName");
            return View(new BookCreateDTO());
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookCreateDTO publisher)
        {
            BookApiUrl = $"{Configuration.ApiURL}/Books";
            var response = client.PostAsJsonAsync(BookApiUrl, publisher).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            BookApiUrl = $"{Configuration.ApiURL}/Books/{id}";
            HttpResponseMessage responseMessage = await client.GetAsync(BookApiUrl);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return NotFound();
            }

            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var result = JsonSerializer.Deserialize<BookResponseDTO>(strData, options);
            if (result == null)
            {
                return NotFound();
            }

            //ViewData["CategoryId"] = new SelectList(await GetCategories(), "CategoryId", "CategoryName");
            return View(result);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookUpdateDTO publisher)
        {
            BookApiUrl = $"{Configuration.ApiURL}/Books/{publisher.PubId}";
            var response = client.PutAsJsonAsync(BookApiUrl, publisher).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookController/Delete/5
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
