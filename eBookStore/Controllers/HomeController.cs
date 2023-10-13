using eBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Utility;

namespace eBookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
                return Redirect("/login");
            }
            else
            {
                return DefaultPageByRole(account);
            }
        }
    }
}