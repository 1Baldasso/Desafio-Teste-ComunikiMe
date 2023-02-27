using ComunikiMe.Domain;
using ComunikiMe.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ComunikiMe.WebApp.Controllers
{
    public class ProdutosController : Controller
    {
        const string ENDPOINT = "https://localhost:7200/api/produtos/";
        private HttpClient _client;
        public ProdutosController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ENDPOINT);
        }
        // GET: ProdutosController
        public async Task<ActionResult> IndexAsync()
        {
            if (!UserController.IsLogged)
                return RedirectToAction("Login", "User");

            var response = await _client.GetAsync(ENDPOINT);
            if (response.IsSuccessStatusCode)
            {
                var produtos = JsonConvert
                    .DeserializeObject<List<Produto>>(response
                    .Content.ReadAsStringAsync()
                    .Result);
                return View(produtos);
            }
            return RedirectToAction("Home", "Index");
        }

        [HttpGet]
        public async Task<ActionResult> ComprarAsync(int id)
        {
            if (!UserController.IsLogged)
                return RedirectToAction("Login", "User");
            var response = await _client.PostAsync(ENDPOINT+"Compra/"+id,CreateContent(""));
            if(response.IsSuccessStatusCode)
            {
                var content = response.Content;
                var json = await content.ReadAsStringAsync();
                var valor = decimal.Parse(json.Replace("\"",""));
                var compra = new CompraViewModel(valor/100);
                compra.Produto = id;
                return View(compra);
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<ActionResult> ComprarAsync(CompraViewModel compra)
        {
            if (!UserController.IsLogged)
                return RedirectToAction("Login", "User");
            var response = await _client.PostAsync(ENDPOINT+"Compra/Confirmar/"+compra.Produto,CreateContent(compra.ValorPago));
            var content = response.Content;
            var json = await content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Produtos");
            }
            ViewBag.ErrorMessage = "Valor pago insuficiente";
            return View(compra);
        }
        // GET: ProdutosController/Details/5
        [HttpGet(Name = "Detalhes/{id}")]
        public async Task<ActionResult> DetailsAsync(int id)
        {
            if (!UserController.IsLogged)
                return RedirectToAction("Login", "User");
            
            var response = await _client.GetAsync(ENDPOINT + id);
            if(response.IsSuccessStatusCode)
            {
                var produto = JsonConvert
                    .DeserializeObject<Produto>(response
                    .Content.ReadAsStringAsync()
                    .Result);
                return View(produto);
            }
            return RedirectToAction("Index", "Produtos");
        }

        // GET: ProdutosController/Create
        public ActionResult Create()
        {
            if (UserController.IsAdmin)
                return View();
            return RedirectToAction("Index", "Produtos");
        }

        // POST: ProdutosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(ProdutoViewModel produto)
        {
            if (!UserController.IsLogged)
                return RedirectToAction("Login", "User");
            //produto.Valor *= 100;
            var content = CreateContent(produto);
            var response = await _client.PostAsync(ENDPOINT, content);
            if (!response.IsSuccessStatusCode)
                ViewBag.ErrorMessage = await response.Content.ReadAsStringAsync();   
            return RedirectToAction("Index");
        }

        // GET: ProdutosController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            if (!UserController.IsLogged)
                return RedirectToAction("Login", "User");

            var response = await _client.GetAsync(ENDPOINT + id);
            if (response.IsSuccessStatusCode)
            {
                var produto = JsonConvert
                    .DeserializeObject<Produto>(response
                    .Content.ReadAsStringAsync()
                    .Result);
                return View(new ProdutoViewModel(produto));
            }
            return RedirectToAction("Index");
        }

        // POST: ProdutosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, ProdutoViewModel produto)
        {
            if (!UserController.IsLogged)
                return RedirectToAction("Login", "User");
            var content = CreateContent(produto);
            var response = await _client.PutAsync(ENDPOINT+id, content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = await response.Content.ReadAsStringAsync();
                return View();
            }
            return RedirectToAction("Index");
        }
        private HttpContent CreateContent(object obj)
        {
            var jsonObj = JsonConvert.SerializeObject(obj);
            var content = new StringContent(jsonObj, Encoding.UTF8, "application/json");
            content.Headers.Add("secret", UserController.UserSecret.ToString());
            return content;
        }
    }
}
