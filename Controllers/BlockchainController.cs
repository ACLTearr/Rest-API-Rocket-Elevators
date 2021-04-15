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
    public class BlockchainController : ControllerBase
    {
        private readonly RestAPIContext _context;

        public BlockchainController(RestAPIContext context)
        {
            _context = context;
        }

        // GET: api/Blockchain
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blockchain>>> Getblockchain()
        {
            return await _context.blockchain.ToListAsync();
        }

        // PUT: api/Blockchain/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlockchain(long id, Blockchain blockchain)
        {
            if (id != blockchain.id)
            {
                return BadRequest();
            }

            _context.Entry(blockchain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlockchainExists(id))
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

        // POST: api/Blockchain
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Blockchain>> PostBlockchain(Blockchain blockchain)
        {
            _context.blockchain.Add(blockchain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlockchain", new { id = blockchain.id }, blockchain);
        }

        // DELETE: api/Blockchain/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlockchain(long id)
        {
            var blockchain = await _context.blockchain.FindAsync(id);
            if (blockchain == null)
            {
                return NotFound();
            }

            _context.blockchain.Remove(blockchain);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // WEEK 10 BLOCKCHAIN ENDPOINTS

        // GET: api/Blockchain/{contract_name}
        [HttpGet("{contract_name}")]
        public object GetBlockchain([FromRoute] string contract_name)
        {
            return _context.blockchain
            .Where(blockchain => blockchain.contract_name == contract_name);
        }
        

        // POST: api/blockchain/project-office
        [HttpPost("project-office/{contract_address}")]
        public async Task<ActionResult<Blockchain>> PostBlockchainProjectOffice(Blockchain blockchain, [FromRoute] string contract_address)
        {

            blockchain.contract_name = "Project Office";
            blockchain.contract_address = contract_address;

            _context.blockchain.Add(blockchain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlockchain", new { id = blockchain.id }, blockchain);
        }
        

        // POST: api/blockchain/material-provider
        [HttpPost("material-provider/{contract_address}")]
        public async Task<ActionResult<Blockchain>> PostBlockchainMaterialProvider(Blockchain blockchain, [FromRoute] string contract_address)
        {

            blockchain.contract_name = "Material Provider";
            blockchain.contract_address = contract_address;

            _context.blockchain.Add(blockchain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlockchain", new { id = blockchain.id }, blockchain);
        }
        

        // POST: api/blockchain/solution-manufacturing
        [HttpPost("solution-manufacturing/{contract_address}")]
        public async Task<ActionResult<Blockchain>> PostBlockchainSolutionManufacturing(Blockchain blockchain, [FromRoute] string contract_address)
        {

            blockchain.contract_name = "Solution Manufacturing";
            blockchain.contract_address = contract_address;

            _context.blockchain.Add(blockchain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlockchain", new { id = blockchain.id }, blockchain);
        }
        

        // POST: api/blockchain/quality-security
        [HttpPost("quality-security/{contract_address}")]
        public async Task<ActionResult<Blockchain>> PostBlockchainQualitySecurity(Blockchain blockchain, [FromRoute] string contract_address)
        {

            blockchain.contract_name = "Quality, Security and Homologation";
            blockchain.contract_address = contract_address;

            _context.blockchain.Add(blockchain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlockchain", new { id = blockchain.id }, blockchain);
        }

        private bool BlockchainExists(long id)
        {
            return _context.blockchain.Any(e => e.id == id);
        }
    }
}
