using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;
using Ultility;
using Utility;

namespace eBookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient client;
        private string MemberApiUrl = "";
        private string UserApiUrl = "";

        public AccountController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [Route("/login")]
        public IActionResult Login()
        {
            return CheckHasLogin();
        }

        [Route("/register")]
        public async Task<IActionResult> Register()
        {
            ViewData["PubId"] = new SelectList(await GetPublishers(), "PubId", "PublisherName");
            return CheckHasLogin();
        }
        
        [Route("/register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserCreateDTO user)
        {
            UserApiUrl = $"{Configuration.ApiURL}/Users";
            var response = client.PostAsJsonAsync(UserApiUrl, user).Result;
            if (response.IsSuccessStatusCode)
            {
                return Redirect("/login");
            }
            else
            {
                ViewData["PubId"] = new SelectList(await GetPublishers(), "PubId", "PublisherName");
                return View();
            }
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

        [Route("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(ConstantValues.LOGIN_ACCOUNT_SESSION_NAME);
            return Redirect("/login");
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            MemberApiUrl = $"{Configuration.ApiURL}/Accounts/login";
            HttpResponseMessage response = client.PostAsJsonAsync(MemberApiUrl, loginDTO).Result;
            if (!response.IsSuccessStatusCode)
            {
                TempData["error"] = "Vui lòng kiểm tra lại email và mật khẩu!";
                return View(loginDTO);
            }
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            if (strData.Contains("fail"))
            {
                TempData["error"] = "Vui lòng kiểm tra lại email và mật khẩu!";
                return View(loginDTO);
            }
            else if (strData.Contains("admin"))
            {
                HttpContext.Session.SetString(ConstantValues.LOGIN_ACCOUNT_SESSION_NAME, ConstantValues.ADMIN_ROLE);
                return DefaultPageByRole(ConstantValues.ADMIN_ROLE);
            }
            else
            {
                var result = JsonSerializer.Deserialize<UserResponseDTO>(strData, options);
                HttpContext.Session.SetString(ConstantValues.LOGIN_ACCOUNT_SESSION_NAME, result.UserId.ToString());
                return DefaultPageByRole(ConstantValues.MEMBER_ROLE);
            }
        }

        private IActionResult DefaultPageByRole(string account)
        {
            if (account == ConstantValues.ADMIN_ROLE)
            {
                return Redirect(ConstantValues.DEFAULT_ADMIN_PAGE);
            }
            else
            {
                return Redirect(ConstantValues.DEFAULT_MEMBER_PAGE);
            }
        }

        private IActionResult CheckHasLogin()
        {
            string? account = HttpContext.Session.GetString(ConstantValues.LOGIN_ACCOUNT_SESSION_NAME);
            if (account is null)
            {
                return View();
            }
            else
            {
                return DefaultPageByRole(account);
            }
        }
    }
}
