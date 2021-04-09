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
    public class InterventionsController : ControllerBase
    {
        private readonly RestAPIContext _context;

        public InterventionsController(RestAPIContext context)
        {
            _context = context;
        }

        // GET: api/Interventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> Getinterventions()
        {
            return await _context.interventions.ToListAsync();
        }

        // GET: api/Interventions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> GetIntervention(long id)
        {
            var intervention = await _context.interventions.FindAsync(id);

            if (intervention == null)
            {
                return NotFound();
            }

            return intervention;
        }

        // GET: api/Interventions/pending
        [HttpGet("pending")]
        public async Task<ActionResult<List<Intervention>>> GetPendingIntervention()
        {
            var allInterventions = await _context.interventions.Where(i => i.intervention_start_date == null).ToListAsync();
            var newInterventions = allInterventions.Where(i => i.status == "Pending").ToList();
            return newInterventions;
        }

        // PUT: api/Interventions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/inprogress")]
        public async Task<IActionResult> PutInterventionInProgress([FromRoute] long id, Intervention intervention)
        {
            if (id != intervention.id)
            {
                return BadRequest();
            }

            if (intervention.status == "In Progress")
            {
                Intervention interventionFound = await _context.interventions.FindAsync(id);
                interventionFound.status = intervention.status;
                interventionFound.intervention_start_date = DateTime.Now;

                try
                {
                    await _context.SaveChangesAsync();
                    return Content("The requested interventions status has been changed to " + intervention.status + ", with a start date of " + DateTime.Now);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterventionExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Content("Valid status: In Progress");
        }

        // PUT: api/Interventions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/completed")]
        public async Task<IActionResult> PutInterventionCompleted([FromRoute] long id, Intervention intervention)
        {
            if (id != intervention.id)
            {
                return BadRequest();
            }

            if (intervention.status == "Completed")
            {
                Intervention interventionFound = await _context.interventions.FindAsync(id);
                interventionFound.status = intervention.status;
                interventionFound.intervention_end_date = DateTime.Now;

                try
                {
                    await _context.SaveChangesAsync();
                    return Content("The requested interventions status has been changed to " + intervention.status + ", with an end date of " + DateTime.Now);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterventionExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Content("Valid status: Completed");
        }

        // POST: api/interventions
        [HttpPost]
        public async Task<ActionResult<Intervention>> PostIntervention(Intervention intervention)
        {
            _context.interventions.Add(intervention);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIntervention", new { id = intervention.id }, intervention);
        }

        private bool InterventionExists(long id)
        {
            return _context.interventions.Any(e => e.id == id);
        }
    }
}
