using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrinterCell.Server.Data;
using PrinterCell.Shared.Models;

namespace PrinterCell.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticoliController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ArticoliController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/articoli
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Articolo>>> GetAll(CancellationToken ct)
        {
            var items = await _db.Articolo
                                 .AsNoTracking()
                                 .OrderBy(a => a.Id)
                                 .ToListAsync(ct);
            return Ok(items);
        }

        // GET: api/articoli/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Articolo>> GetById(int id, CancellationToken ct)
        {
            var articolo = await _db.Articolo.FindAsync(new object?[] { id }, ct);
            if (articolo is null) return NotFound();
            return Ok(articolo);
        }

        // POST: api/articoli
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Articolo>> Create([FromBody] Articolo input, CancellationToken ct)
        {
            if (input is null) return BadRequest("Payload mancante.");

            // Id gestito dal DB (identity): assicuriamoci che non forzi un Id
            input.Id = 0;

            _db.Articolo.Add(input);
            await _db.SaveChangesAsync(ct);

            return CreatedAtAction(nameof(GetById), new { id = input.Id }, input);
        }

        // PUT: api/articoli/5
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] Articolo input, CancellationToken ct)
        {
            if (input is null || id != input.Id)
                return BadRequest("Id non coerente con il payload.");

            var exists = await _db.Articolo.AnyAsync(a => a.Id == id, ct);
            if (!exists) return NotFound();

            // Tracciamento esplicito: aggiorna tutte le proprietà del record
            _db.Entry(input).State = EntityState.Modified;

            await _db.SaveChangesAsync(ct);
            return NoContent();
        }

        // DELETE: api/articoli/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var entity = await _db.Articolo.FindAsync(new object?[] { id }, ct);
            if (entity is null) return NotFound();

            _db.Articolo.Remove(entity);
            await _db.SaveChangesAsync(ct);

            return NoContent();
        }

    }
}
