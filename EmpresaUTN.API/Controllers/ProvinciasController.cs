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
    public class ProvinciasController : ControllerBase
    {
        private readonly DataContext _context;

        public ProvinciasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Provincias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provincia>>> GetProvincia()
        {
          if (_context.Provincias == null)
          {
              return NotFound();
          }
            //return await _context.Provincias.ToListAsync();

          //retornar provincia con el pais que pertenece
         // return await _context.Provincias.Include(x => x.Pais).ToListAsync();

            //RETORNAR PROVINCIA CON EL PAIS Y EL CANTON QUE PERTENECE
           // return await _context.Provincias.Include(x => x.Pais).Include(x => x.Cantones).ToListAsync();
           //RETORNAR LOS CANTONES DE LA PROVINCIA
           return await _context.Provincias.Include(x => x.Cantones).ToListAsync();
           


        }

        // GET: api/Provincias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Provincia>> GetProvincia(int id)
        {
          if (_context.Provincias == null)
          {
              return NotFound();
          }
            //var provincia = await _context.Provincias.FindAsync(id);
            //buscar provincia con el pais y los cantones que pertenece
            var provincia = await _context.Provincias.Include(x => x.Pais).Include(x => x.Cantones).FirstOrDefaultAsync(x => x.Id == id);






            if (provincia == null)
            {
                return NotFound();
            }

            return provincia;
        }

        // PUT: api/Provincias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvincia(int id, Provincia provincia)
        {
            if (id != provincia.Id)
            {
                return BadRequest();
            }

            _context.Entry(provincia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvinciaExists(id))
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

        // POST: api/Provincias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Provincia>> PostProvincia(Provincia provincia)
        {
          if (_context.Provincias == null)
          {
              return Problem("Entity set 'DataContext.Provincia'  is null.");
          }
            _context.Provincias.Add(provincia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProvincia", new { id = provincia.Id }, provincia);
        }

        // DELETE: api/Provincias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvincia(int id)
        {
            if (_context.Provincias == null)
            {
                return NotFound();
            }
            var provincia = await _context.Provincias.FindAsync(id);
            if (provincia == null)
            {
                return NotFound();
            }

            _context.Provincias.Remove(provincia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProvinciaExists(int id)
        {
            return (_context.Provincias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
