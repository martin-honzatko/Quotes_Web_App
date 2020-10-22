using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotesWebApp.Models;
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
        public async Task<ActionResult<IEnumerable<Quote>>> GetQuote()
        {
            return await _context.Quotes.ToListAsync();
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

        [HttpGet("{id}/tags")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetQuoteTags(int id)
        {
            var quote = await _context.Quotes.Where(q => q.Id == id)
                    .Include(s => s.Tags)
                    .ThenInclude(tag => tag.Tag).AsNoTracking().SingleOrDefaultAsync();

            if (quote == null)
            {
                return NotFound();
            }

            return quote.Tags.Select(tag => tag.Tag).ToList();
        }

        [HttpGet("{id}/full")]
        public async Task<ActionResult<QuoteTagsVM>> GetQuoteFull(int id)
        {
            var quote = await _context.Quotes.Where(q => q.Id == id)
                    .Include(s => s.Tags)
                    .ThenInclude(tag => tag.Tag).AsNoTracking().SingleOrDefaultAsync();

            if (quote == null)
            {
                return NotFound();
            }

            QuoteTagsVM result = new QuoteTagsVM
            {
                Id = quote.Id,
                Text = quote.Text,
                Tags = quote.Tags.Select(tag => tag.Tag).ToList()
            };
            return result;
        }

        [HttpPost("{id}/tags")]
        public async Task<ActionResult<Quote>> PostTags(int id, [FromBody] IEnumerable<int> tagIds)
        {
            IList<QuoteTag> quoteTags = new List<QuoteTag>();
            foreach (var item in tagIds)
            {
                QuoteTag newQuote = new QuoteTag
                {
                    QuoteId = id,
                    TagId = item
                };
                quoteTags.Add(newQuote);
            }
            _context.QuoteTags.AddRange(quoteTags);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTags", quoteTags);
        }

        // DELETE: api/Quotes/5/tags/1
        [HttpDelete("{quoteId}/tags/{quoteTagId}")]
        public async Task<ActionResult<QuoteTag>> DeleteQuote(int quoteId, int tagId)
        {
            var quoteTag = await _context.QuoteTags.Where(x => x.QuoteId == quoteId && x.TagId == tagId).SingleOrDefaultAsync();
            if (quoteTag == null)
            {
                return NotFound();
            }

            _context.QuoteTags.Remove(quoteTag);
            await _context.SaveChangesAsync();

            return quoteTag;
        }

        private bool QuoteExists(int id)
        {
            return _context.Quotes.Any(e => e.Id == id);
        }
    }
}
