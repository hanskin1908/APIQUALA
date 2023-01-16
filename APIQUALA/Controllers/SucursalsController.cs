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

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SucursalsController : ControllerBase
    {
        private readonly TestDbContext _context;

        public SucursalsController(TestDbContext context)
        {
            _context = context;
        }

        // GET: api/Sucursals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moneda_sucursal>>> GetSucursales()
        {
            return await _context.Sucursals.Join(_context.MonMoneda, moneda => moneda.IdMonedaSuc, sucur => sucur.IdMoneda, (_sucur, _moneda) => new Moneda_sucursal
            {
                Codigo = _sucur.Codigo,
                Descripcion = _sucur.Descripcion,
                Direccion = _sucur.Direccion,
                Identificacion = _sucur.Identificacion,
                Monedadesc = _moneda.Descripcion
            }

                  ).ToListAsync();
        }

        // GET: api/Sucursals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sucursal>> GetSucursal(int id)
        {
            var sucursal = await _context.Sucursals.FindAsync(id);

            if (sucursal == null)
            {
                return NotFound();
            }

            return sucursal;
        }

        // PUT: api/Sucursals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> updatesucursal(int id, Sucursal sucursal)
        {
            if (id != sucursal.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(sucursal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SucursalExists(id))
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

        // POST: api/Sucursals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sucursal>> guardarsucursal(Sucursal sucursal)
        {
            _context.Sucursals.Add(sucursal);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SucursalExists(sucursal.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("guardarsucursal", new { id = sucursal.Codigo }, sucursal);
        }

        // DELETE: api/Sucursals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> eliminarsucursal(int id)
        {
            var sucursal = await _context.Sucursals.FindAsync(id);
            if (sucursal == null)
            {
                return NotFound();
            }

            _context.Sucursals.Remove(sucursal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SucursalExists(int id)
        {
            return _context.Sucursals.Any(e => e.Codigo == id);
        }
    }
}
