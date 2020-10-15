using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotesWebApp.Data;

namespace QuotesWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuoteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Quote
        [HttpGet]
        public async Task<ActionResult<Quote>> GetQuote()
        {
            var r = new Random().Next(1, 100);
            var q = await _context.Quotes.SingleOrDefaultAsync(q => q.Id == r);

            if (q != null)
            {
                return Ok(q);
            }

            else
            {
                RedirectToAction("GetQuote");
            }

            return Ok();
        }

        // GET: api/Quote/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quote>> GetQuote(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);

            if (quote == null)
            {
                return NotFound();
            }

            return Ok(quote);
        }

        // PUT: api/Quote/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuote(int id, Quote quote)
        {
            if (id != quote.Id)
            {
                return BadRequest();
            }

            _context.Entry(quote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuoteExists(id))
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

        // POST: api/Quote
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Quote>> PostQuote([FromBody]Quote quote)
        {
            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuote", new { id = quote.Id }, quote);
        }

        // DELETE: api/Quote/5
        [HttpDelete("{id?}")]
        public async Task<ActionResult<Quote>> DeleteQuote(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();

            return Ok(quote);
        }

        // POST api/<QuoteController/5/tags>
        [HttpPost("{id}/tags")]
        public async Task<ActionResult<IEnumerable<Tag>>> InsertTags(int id, [FromBody] IEnumerable<int> tagIds) 
        {
            var q = await _context.Quotes.FindAsync(id);
            q.Tags.Add(new QuoteTag { QuoteId = q.Id, TagId = tagIds });
            _context.Quotes.Update(q);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTags", new { id = q.Id }, q.Tags);
        }

        // DELETE api/<QuoteController/5/tags>
        // unlink tags connected with quote 5
        [HttpDelete("{id}/tags")]
        public async Task<ActionResult<ICollection<QuoteTag>>> DeleteTags(int id, [FromBody] IEnumerable<int> tagIds) 
        {
            var q = await _context.Quotes.FindAsync(id);
            if (q == null)
            {
                return NotFound();
            }

            q.Tags.Remove(new QuoteTag { QuoteId = q.Id, TagId = tagIds });
            _context.Quotes.Update(q);
            await _context.SaveChangesAsync();

            return Ok(q.Tags);
        }

        // GET api/<QuoteController/5/tags>
        // get linked tags with quote 5
        [HttpGet("{id}/tags")]
        public async Task<ActionResult<ICollection<QuoteTag>>> GetTags(int id) 
        {
            var q = await _context.Quotes.FindAsync(id);

            if (q == null)
            {
                return NotFound();
            }

            return Ok(q.Tags);
        }

        private bool QuoteExists(int id)
        {
            return _context.Quotes.Any(e => e.Id == id);
        }
    }
}
