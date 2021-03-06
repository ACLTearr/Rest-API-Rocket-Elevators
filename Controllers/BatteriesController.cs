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
    public class BatteriesController : ControllerBase
    {
        private readonly RestAPIContext _context;

        public BatteriesController(RestAPIContext context)
        {
            _context = context;
        }  

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Battery>>> GetBatteries()
        {
            return await _context.batteries.ToListAsync();
        } 

        // GET: api/batteries/find-columns/{id}
        [HttpGet("find-batteries/{id}")]
        public ActionResult<List<Battery>> GetBatteriesFromBuilding(long id)
        {
            List<Battery> batteries = _context.batteries.ToList();
            List<Battery> buildingBatteries = new List<Battery>();
            foreach (Battery battery in batteries)
            {
                if (battery.building_id == id)
                {
                    buildingBatteries.Add(battery);
                }
            }
            return buildingBatteries;
        }


//----------------------------------- Retrieving all information from a specific Battery -----------------------------------\\

        //GET: api/Batteries/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Battery>> GetBattery(long id)
        {
            var battery = await _context.batteries.FindAsync(id);

            if (battery == null)
            {
                return NotFound();
            }

            return battery;
        }

//----------------------------------- Retrieving the current status of a specific Battery -----------------------------------\\
        
        // GET: api/Batteries/id/Status
        [HttpGet("{id}/Status")]
        public async Task<ActionResult<string>> GetBatteryStatus([FromRoute] long id)
        {
            var battery = await _context.batteries.FindAsync(id);

            if (battery == null)
            {
                return NotFound();
            }

            return battery.status;
        }

//----------------------------------- Changing the status of a specific Battery -----------------------------------\\
         
        // PUT: api/Batteries/id/Status        
        [HttpPut("{id}/Status")]
        public async Task<IActionResult> PutBattery([FromRoute] long id, Battery battery)
        {
            if (id != battery.id)
            {
                return BadRequest();
            }
            
            if (battery.status == "Active" || battery.status == "Inactive" || battery.status == "Intervention")
            {
                Battery batteryFound = await _context.batteries.FindAsync(id);
                batteryFound.status = battery.status;
                
                try
                {
                    await _context.SaveChangesAsync();
                    return Content("Battery: " + battery.id + ", status as been change to: " + battery.status);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BatteryExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Content("Valid status: Intervention, Inactive, Active. Try again!  ");
        }

//-------------------------------WEEK 13 AI ENDPOINTS-------------------------------------------------------------------------//


        // GET: api/batteries/count
        [HttpGet("count")]
        public object GetBatteriesCount()
        {
            return (_context.batteries).Count();
            
        }


//=============================================WEEK 13 AI ENDPOINTS------------------------------------------------------------//


        private bool BatteryExists(long id)
        {
            return _context.batteries.Any(e => e.id == id);
        }
    }
}
