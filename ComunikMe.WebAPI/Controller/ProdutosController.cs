using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComunikiMe.Domain;
using ComunikiMe.WebAPI.Data;
using ComunikiMe.Domain.DTO;
using Microsoft.AspNetCore.DataProtection;
using System.Net.Sockets;

namespace ComunikiMe.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ComunikiMeWebAPIContext _context;

        public ProdutosController(ComunikiMeWebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProduto()
        {
            return Ok(await _context.Produto.ToListAsync());
        }

        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _context.Produto.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPost("Compra/{id}")]
        public async Task<ActionResult<decimal>> ComprarProduto(int id, [FromHeader] Guid secret)
        {
            var usuario = _context.Usuario.FirstOrDefault(x => x.Secret == secret);
            if (usuario is null)
                return Forbid("Usuário não encontrado");
            var produto = _context.Produto.Find(id);
            if(produto is not null)
            {
                return Ok(produto.Valor);
            }
            return BadRequest("Produto não encontrado");
        }
        [HttpPost("Compra/Confirmar/{id}")]
        public async Task<ActionResult<decimal>> ConfirmarCompra(int id, [FromBody]decimal valorPago, [FromHeader] Guid secret)
        {
            var usuario = _context.Usuario.FirstOrDefault(x => x.Secret == secret);
            if (usuario is null)
                return Forbid("Usuário não encontrado");
            var produto = _context.Produto.Find(id);
            if(produto is null)
                return BadRequest("Produto não encontrado");
            if (valorPago < produto.Valor)
                return BadRequest("Valor pago incompatível com valor do produto");
            if (produto.Estoque==0)
                return StatusCode(500,"Produto indisponível.");
            produto.Estoque--;
            _context.Update(produto);
            await _context.SaveChangesAsync();
            return Ok(new { troco = valorPago - produto.Valor });
            
        }
        // PUT: api/Produtos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, ProdutoDTO produto, [FromHeader] Guid secret)
        {
            var usuario = _context.Usuario.FirstOrDefault(x => x.Secret == secret);
            if (usuario is null || !usuario.isAdmin)
                return Forbid("Usuário não é admin");
            if (!ProdutoExists(id))
            {
                return NotFound();
            }
            var produtoFinal = _context.Produto.Find(id);
            produtoFinal.Nome = produto.Nome;
            produtoFinal.Valor = produto.Valor;
            produtoFinal.Estoque = produto.Estoque;
            _context.Update(produtoFinal);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Produtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(ProdutoDTO produto,[FromHeader] Guid secret)
        {
            var usuario = _context.Usuario.FirstOrDefault(x => x.Secret == secret);
            if (usuario is null || !usuario.isAdmin)
                return Forbid("Usuário não é admin");
            var produtoFinal = produto.ToProduto();
            _context.Produto.Add(produtoFinal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduto", new { id = produtoFinal.Id }, produtoFinal);
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.Id == id);
        }
    }
}
