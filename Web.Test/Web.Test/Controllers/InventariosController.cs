using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Test.Data;
using Web.Test.Entities;

namespace Web.Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventariosController : ControllerBase
    {
        private readonly DataContext _context;

        public InventariosController(DataContext context)
        {
            _context = context;
        }



        /// <summary>
        /// Recupera todos os inventarios
        /// </summary>
        /// <returns>Recupera todos os inventarios</returns>
        /// <response code="200">Retorna os inventarios cadastrados</response>
        [HttpGet]
        //[Route("/inventarios")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Inventario>>> GetInventarios()
        {
            return await _context.Inventarios.ToListAsync();
        }

        /// <summary>
        /// Recupera um item a partir do id informado
        /// </summary>
        /// <param name="id"></param>
        /// <returns code="200">Recupera um item a partir do id informado</returns>
        [HttpGet("{id}")]
        //[Route("/inventarios")]
        [Authorize]
        public async Task<ActionResult<Inventario>> GetInventario(int id)
        {
            var inventario = await _context.Inventarios.FindAsync(id);

            if (inventario == null)
            {
                return NotFound();
            }

            return inventario;
        }

        /// <summary>
        /// Atualiza um item a partir do id informado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inventario"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        // [Route("/inventario/update")]
        [Authorize]
        public async Task<IActionResult> PutInventario(int id, Inventario inventario)
        {
            if (id != inventario.Id)
            {
                return BadRequest();
            }

            _context.Entry(inventario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventarioExists(id))
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

        /// <summary>
        /// Adiciona um novo inventario
        /// </summary>
        /// <param name="inventario"></param>
        /// <returns></returns>
        [HttpPost]
        // [Route("/cadastrar")]
        [Authorize]
        public async Task<ActionResult<Inventario>> PostInventario(Inventario inventario)
        {
            _context.Inventarios.Add(inventario);
            await _context.SaveChangesAsync();            

            return CreatedAtAction("GetInventario", new { id = inventario.Id }, inventario);
        }

        /// <summary>
        /// Deleta um item a partir do id informado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        // [Route("/deletar")]
        [Authorize]
        public async Task<IActionResult> DeleteInventario(int id)
        {
            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }

            _context.Inventarios.Remove(inventario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventarioExists(int id)
        {
            return _context.Inventarios.Any(e => e.Id == id);
        }
    }
}
