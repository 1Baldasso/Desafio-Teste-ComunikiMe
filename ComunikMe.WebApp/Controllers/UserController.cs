using ComunikiMe.Domain;
using ComunikiMe.Domain.DTO;
using ComunikiMe.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ComunikiMe.WebApp.Controllers
{
    public class UserController : Controller
    {
        public static bool IsLogged { get; private set; }
        public static Guid UserSecret { get; private set; }
        private const string ENDPOINT = "https://localhost:7200/api/usuarios/";
        private HttpClient _client;
        private UsuarioInfoDTO _userInfo;

        public UserController()
        {
            _client = new();
            _client.BaseAddress = new Uri(ENDPOINT);
        }
        public static string Username { get; private set; }

        public static bool IsAdmin { get; private set; }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAsync(LoginViewModel login)
        {
            if (UserController.IsLogged)
                return RedirectToAction("Index", "Home");
            var content = CreateContent(login);
            var response = await _client.PostAsync(ENDPOINT+"Login", content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = await response.Content.ReadAsStringAsync();
                return View();
            }
            else
            {
                UserSecret = Guid.Parse(response.Content.ReadAsStringAsync().Result.Replace("\"", ""));
                await UpdateUsuarioAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        private async Task UpdateUsuarioAsync()
        {
            var content = CreateContent(UserSecret);
            var response = await _client.PostAsync(ENDPOINT + "UserInfo", content);
            if(response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                _userInfo = JsonConvert.DeserializeObject<UsuarioInfoDTO>(json);
                UserSecret = (Guid)(_userInfo?.Secret);
                Username = _userInfo.Nome;
                IsAdmin = _userInfo.isAdmin;
                IsLogged = true;
            }
        }

        public IActionResult Logout()
        {
            IsAdmin= false;
            IsLogged= false;
            Username = String.Empty;
            UserSecret = Guid.Empty;
            return RedirectToAction("Index","Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAsync(RegisterViewModel register)
        {
            if (UserController.IsLogged)
                return RedirectToAction("Index", "Home");
            var content = CreateContent(register.ToRegistroDTO());
            var response = await _client.PostAsync(ENDPOINT+"Register", content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = await response.Content.ReadAsStringAsync();
                return View();
            }
            else
            {
                UserSecret = Guid.Parse(response.Content.ReadAsStringAsync().Result.Replace("\"", ""));
                await UpdateUsuarioAsync();
                IsLogged = true;
            }

            return RedirectToAction("Index", "Home");
        }
        private HttpContent CreateContent(object obj)
        {
            var jsonObj = JsonConvert.SerializeObject(obj);
            var content = new StringContent(jsonObj, Encoding.UTF8,"application/json");
            return content;
        }


    }
}
