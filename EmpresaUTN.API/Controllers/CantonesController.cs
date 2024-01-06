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
    public class CantonesController : ControllerBase
    {
        private readonly DataContext _context;

        public CantonesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Cantones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Canton>>> GetCanton()
        {
          if (_context.Cantones == null)
          {
              return NotFound();
          }
            //return await _context.Cantones.ToListAsync();
           return await _context.Cantones.Include(c => c.Provincia).ToListAsync();
           



          


        }

        // GET: api/Cantones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Canton>> GetCanton(int id)
        {
          if (_context.Cantones == null)
          {
              return NotFound();
          }
            //var canton = await _context.Cantones.FindAsync(id);

            var canton = await _context.Cantones.Include(c => c.Provincia).FirstOrDefaultAsync(x => x.Id == id);

            if (canton == null)
            {
                return NotFound();
            }

            return canton;
        }

        // PUT: api/Cantones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCanton(int id, Canton canton)
        {
            if (id != canton.Id)
            {
                return BadRequest();
            }

            _context.Entry(canton).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CantonExists(id))
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

        // POST: api/Cantones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Canton>> PostCanton(Canton canton)
        {
          if (_context.Cantones == null)
          {
              return Problem("Entity set 'DataContext.Canton'  is null.");
          }
            _context.Cantones.Add(canton);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCanton", new { id = canton.Id }, canton);
        }

        // DELETE: api/Cantones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCanton(int id)
        {
            if (_context.Cantones == null)
            {
                return NotFound();
            }
            var canton = await _context.Cantones.FindAsync(id);
            if (canton == null)
            {
                return NotFound();
            }

            _context.Cantones.Remove(canton);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CantonExists(int id)
        {
            return (_context.Cantones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
