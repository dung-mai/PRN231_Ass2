using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepository;

namespace eBookStoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IUserRepository _userRepository;

        public AccountsController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public IActionResult Post(LoginDTO loginDTO)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            string adminEmail = configuration.GetSection("Account:DefaultAccount:Email").Value;
            string adminPassword = configuration.GetSection("Account:DefaultAccount:Password").Value;

            if (adminEmail == loginDTO.Email && adminPassword == loginDTO.Password)
            {
                return Ok("admin");
            }
            else
            {
                var result = _userRepository.Login(loginDTO.Email, loginDTO.Password);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return Ok("fail");
                }
            }
        }

        [HttpPost("register")]
        public IActionResult PostUser(UserCreateDTO user)
        {
            if (_userRepository.SaveUser(user))
            {
                return NoContent();
            }
            else
            {
                return Problem("Problem when Adding User");
            }
        }
    }
}
