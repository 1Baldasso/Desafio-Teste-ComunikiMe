using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComunikMeAPI;
using ComunikMeAPI.Data;

namespace ComunikMeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoModelsController : ControllerBase
    {
        private readonly ComunikMeAPIContext _context;

        public ProdutoModelsController(ComunikMeAPIContext context)
        {
            _context = context;
        }

        // GET: api/ProdutoModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoModel>>> GetProdutoModel()
        {
            return await _context.ProdutoModel.ToListAsync();
        }

        // GET: api/ProdutoModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoModel>> GetProdutoModel(int id)
        {
            var produtoModel = await _context.ProdutoModel.FindAsync(id);

            if (produtoModel == null)
            {
                return NotFound();
            }

            return produtoModel;
        }

        // PUT: api/ProdutoModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdutoModel(int id, ProdutoModel produtoModel)
        {
            if (id != produtoModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(produtoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProdutoModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProdutoModel>> PostProdutoModel(ProdutoModel produtoModel)
        {
            _context.ProdutoModel.Add(produtoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProdutoModel", new { id = produtoModel.Id }, produtoModel);
        }

        // DELETE: api/ProdutoModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdutoModel(int id)
        {
            var produtoModel = await _context.ProdutoModel.FindAsync(id);
            if (produtoModel == null)
            {
                return NotFound();
            }

            _context.ProdutoModel.Remove(produtoModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoModelExists(int id)
        {
            return _context.ProdutoModel.Any(e => e.Id == id);
        }
    }
}
