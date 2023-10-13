using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Net.Http.Headers;
using System.Text.Json;
using Ultility;

namespace eBookStore.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient client;
        private string UserApiUrl = "";

        public UserController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            UserApiUrl = $"{Configuration.ApiURL}/Users";
        }

        // GET: UserController
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(UserApiUrl);
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var resultList = JsonSerializer.Deserialize<List<UserResponseDTO>>(strData, options);
            var Users = resultList ?? new List<UserResponseDTO>();

            return View(Users);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            //ViewData["CategoryId"] = new SelectList(await GetCategories(), "CategoryId", "CategoryName");
            return View(new UserCreateDTO());
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCreateDTO user)
        {
            UserApiUrl = $"{Configuration.ApiURL}/Users";
            var response = client.PostAsJsonAsync(UserApiUrl, user).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            UserApiUrl = $"{Configuration.ApiURL}/Users?$filter=UserId eq {id}";
            HttpResponseMessage responseMessage = await client.GetAsync(UserApiUrl);
            if (!responseMessage.IsSuccessStatusCode)
            {
                return NotFound();
            }

            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var result = JsonSerializer.Deserialize<List<UserResponseDTO>>(strData, options);
            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            ViewData["PubId"] = new SelectList(await GetPublishers(), "PubId", "PublisherName");
            return View(result[0]);
        }

        private async Task<List<PublisherDTO>> GetPublishers()
        {
            string PublisherApiUrl = $"{Configuration.ApiURL}/Publishers";

            HttpResponseMessage responseMessage = await client.GetAsync(PublisherApiUrl);
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var resultList = JsonSerializer.Deserialize<List<PublisherDTO>>(strData, options);
            return resultList ?? new List<PublisherDTO>();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserUpdateDTO user)
        {
            UserApiUrl = $"{Configuration.ApiURL}/Users/{user.UserId}";
            var response = client.PutAsJsonAsync(UserApiUrl, user).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["PubId"] = new SelectList(await GetPublishers(), "PubId", "PublisherName");
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
