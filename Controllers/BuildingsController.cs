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
    public class BuildingsController : ControllerBase
    {
        private readonly RestAPIContext _context;

        public BuildingsController(RestAPIContext context)
        {
            _context = context;
        }  

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuildings()
        {
            return await _context.buildings.ToListAsync();
        } 

        // GET: api/Buildings by customer email
        [HttpGet("{email}/customer")]
        public object GetBuildingsByCustomerEmail(string email)
        {
            return _context.buildings.Where(b => b.email_of_the_administrator_of_the_building == email);
        }     

//----------------------------------- Retrieving all information from a specific Building -----------------------------------\\

        // GET: api/Buildings/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> GetBuilding(long id)
        {
            var building = await _context.buildings.FindAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }

//--------- Retrieving a list of Buildings that contain at least one battery, column or elevator requiring intervention ---------\\

        // GET: api/Buildings/InterventionList
        [HttpGet("InterventionList")]
        public ActionResult<List<Building>> GetToFixBuildings()
        {
            IQueryable<Building> InterventionList = from BuildingsList in _context.buildings
            join batteries in _context.batteries on BuildingsList.id equals batteries.building_id
            join columns in _context.columns on batteries.id equals columns.battery_id
            join elevators in _context.elevators on columns.id equals elevators.column_id
            where (batteries.status == "Intervention") || (columns.status == "Intervention") || (elevators.status == "Intervention")
            select BuildingsList;

            return InterventionList.Distinct().ToList();
        }

//-------------------------------WEEK 13 AI ENDPOINTS-------------------------------------------------------------------------//


        // GET: api/buildings/count
        [HttpGet("count")]
        public object GetBuildingsCount()
        {
            return (_context.buildings).Count();
            
        }

        // GET: api/buildings/cities
        [HttpGet("cities.")]
        public object GetUniqueCitiess()
        {
            return (_context.buildings).Count();
            
        }

        // GET: api/elevators/find-elevators/{id}
        [HttpGet("cities")]
        public int GetUniqueCities()
        {
            List<Building> buildings = _context.buildings.ToList();
            List<long?> uniqueAddresses = new List<long?>();
            foreach (Building building in buildings)
            {
                if (!uniqueAddresses.Contains(building.address_id))
                {
                    uniqueAddresses.Add(building.address_id);
                }
            }
            return uniqueAddresses.Count();
        }


//=============================================WEEK 13 AI ENDPOINTS------------------------------------------------------------//


        private bool BuildingExists(long id)
        {
            return _context.buildings.Any(e => e.id == id);
        }
    }
}
