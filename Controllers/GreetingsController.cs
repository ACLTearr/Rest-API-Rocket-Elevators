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
    public class GreetingsController : ControllerBase
    {
         private readonly RestAPIContext _context;

        public GreetingsController(RestAPIContext context)
        {
            _context = context;
        }

        // GET: api/greetings
        [HttpGet]
        public string Greeting()
        {
            //Setting values to be returned
            int totalElevators = _context.elevators.Count();
            int totalBuildings = _context.buildings.Count();
            int totalBatteries = _context.batteries.Count();
            int totalCustomers = _context.customers.Count();
            int totalQuotes = _context.quotes.Count();
            int totalLeads = _context.leads.Count();
            int inactiveElevators = _context.elevators.Where(elevator => elevator.status != "Active").Count();
            //Getting the number of unique cities in the database (buildings only)
            List<Building> buildings = _context.buildings.ToList();
            List<long?> uniqueAddresses = new List<long?>();
            foreach (Building building in buildings)
            {
                if (!uniqueAddresses.Contains(building.address_id))
                {
                    uniqueAddresses.Add(building.address_id);
                }
            }

            //Setting string value to be sent back for the greeting
            string greeting = "Greetings! There are currently " + totalElevators + " elevators deployed in the " + totalBuildings + " buildings of your " + totalCustomers + " customers. Currently, " + inactiveElevators + " elevators are not in running status and are being serviced. " + totalBatteries + " batteries are deployed across " + uniqueAddresses.Count() + " cities. There are currently " + totalQuotes + " quotes awaiting processing. There are also " + totalLeads + " leads in your contact requests.";

            return greeting;
        }
    }
}