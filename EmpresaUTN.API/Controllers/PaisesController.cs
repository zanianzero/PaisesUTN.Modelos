using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmpresaUTN.Modelos;

namespace EmpresaUTN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        private readonly DataContext _context;

        public PaisesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Paises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pais>>> GetPais()
        {
          if (_context.Paises == null)
          {
              return NotFound();
          }
            //return await _context.Paises.ToListAsync();

          // retornar pais con sus provinicas
          return await _context.Paises.Include(p => p.Provincias).ThenInclude(p => p.Cantones).ToListAsync();
           
        

        }

        // GET: api/Paises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pais>> GetPais(int id)
        {
          if (_context.Paises == null)
          {
              return NotFound();
          }
            ///var pais = await _context.Paises.FindAsync(id);
           var pais = await _context.Paises.Include(p => p.Provincias).ThenInclude(p => p.Cantones).FirstOrDefaultAsync(x => x.CodigoPais == id);


            if (pais == null)
            {
                return NotFound();
            }

            return pais;
        }

        // PUT: api/Paises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPais(int id, Pais pais)
        {
            if (id != pais.CodigoPais)
            {
                return BadRequest();
            }

            _context.Entry(pais).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaisExists(id))
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

        // POST: api/Paises
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pais>> PostPais(Pais pais)
        {
          if (_context.Paises == null)
          {
              return Problem("Entity set 'DataContext.Pais'  is null.");
          }
            _context.Paises.Add(pais);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPais", new { id = pais.CodigoPais }, pais);
        }

        // DELETE: api/Paises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePais(int id)
        {
            if (_context.Paises == null)
            {
                return NotFound();
            }
            var pais = await _context.Paises.FindAsync(id);
            if (pais == null)
            {
                return NotFound();
            }

            _context.Paises.Remove(pais);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaisExists(int id)
        {
            return (_context.Paises?.Any(e => e.CodigoPais == id)).GetValueOrDefault();
        }
    }
}
