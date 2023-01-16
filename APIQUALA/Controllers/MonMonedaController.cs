using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIQUALA.Models;

namespace APIQUALA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonMonedaController : ControllerBase
    {
        private readonly TestDbContext _context;

        public MonMonedaController(TestDbContext context)
        {
            _context = context;
        }

        // GET: api/MonMoneda
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonMonedum>>> GetMonMoneda()
        {
            return await _context.MonMoneda.ToListAsync();
        }

        // GET: api/MonMoneda/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MonMonedum>> GetMonMonedum(int id)
        {
            var monMonedum = await _context.MonMoneda.FindAsync(id);

            if (monMonedum == null)
            {
                return NotFound();
            }

            return monMonedum;
        }

        // PUT: api/MonMoneda/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonMonedum(int id, MonMonedum monMonedum)
        {
            if (id != monMonedum.IdMoneda)
            {
                return BadRequest();
            }

            _context.Entry(monMonedum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonMonedumExists(id))
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

        // POST: api/MonMoneda
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MonMonedum>> PostMonMonedum(MonMonedum monMonedum)
        {
            _context.MonMoneda.Add(monMonedum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MonMonedumExists(monMonedum.IdMoneda))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMonMonedum", new { id = monMonedum.IdMoneda }, monMonedum);
        }

        // DELETE: api/MonMoneda/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonMonedum(int id)
        {
            var monMonedum = await _context.MonMoneda.FindAsync(id);
            if (monMonedum == null)
            {
                return NotFound();
            }

            _context.MonMoneda.Remove(monMonedum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MonMonedumExists(int id)
        {
            return _context.MonMoneda.Any(e => e.IdMoneda == id);
        }
    }
}
