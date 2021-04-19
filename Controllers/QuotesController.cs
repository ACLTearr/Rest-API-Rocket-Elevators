using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly RestAPIContext _context;

        public QuotesController(RestAPIContext context)
        {
            _context = context;
        }

        // GET: api/Quotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quote>>> Getquotes()
        {
            return await _context.quotes.ToListAsync();
        }

        // GET: api/Quotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quote>> GetQuote(long id)
        {
            var quote = await _context.quotes.FindAsync(id);

            if (quote == null)
            {
                return NotFound();
            }

            return quote;
        }

        // PUT: api/Quotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuote(long id, Quote quote)
        {
            if (id != quote.id)
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

        // POST: api/Quotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quote>> PostQuote(Quote quote)
        {
            _context.quotes.Add(quote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuote", new { id = quote.id }, quote);
        }

        // DELETE: api/Quotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(long id)
        {
            var quote = await _context.quotes.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            _context.quotes.Remove(quote);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
//-------------------------------WEEK 13 AI ENDPOINTS-------------------------------------------------------------------------//


        // GET: api/quotes/count
        [HttpGet("count")]
        public object GetQuotesCount()
        {
            return (_context.quotes).Count();
            
        }


//=============================================WEEK 13 AI ENDPOINTS------------------------------------------------------------//

        private bool QuoteExists(long id)
        {
            return _context.quotes.Any(e => e.id == id);
        }
    }
}
