using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Filter;
using WebApiTest.Models;
using WebApiTest.Services;
using WebApiTest.Wrappers;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationRestController : ControllerBase
    {
        private readonly RestaurantContext _context;
        private readonly IUriService uriService;

        public LocationRestController(RestaurantContext context, IUriService uriService)
        {
            _context = context;
            this.uriService = uriService;
        }

        // GET: api/LocationRest?pageNumber=1&pageSize=5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _context.LocationRest
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.LocationRest.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse(pagedData, validFilter, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }

        // GET: api/LocationRest/1
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<object>>> GetLocationRestaurantById(int id)
        {
            var locationRestaurant = await _context.LocationRest.Where(w => w.Id == id)
                .Select(s => new { s.Id, city = s._city.Name, rest = s._restaurant.Name }).ToListAsync();

            if (locationRestaurant == null)
            {
                return NotFound();
            }

            return locationRestaurant;
        }

        // GET: api/LocationRest/city/Москва
        [HttpGet("city/{name}")]
        public async Task<ActionResult<IEnumerable<object>>> GetLocationRestaurantByName(string name)
        {
            var locationRestaurant = await _context.LocationRest
                .Where(w => w._city.Name == name)
                .Select(s => new { s.Id, city = s._city.Name, rest = s._restaurant.Name }).ToListAsync();

            if (locationRestaurant == null)
            {
                return NotFound();
            }

            return locationRestaurant;
        }

        // PUT: api/LocationRest/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocationRestaurant(int id, LocationRestaurant locationRestaurant)
        {
            if (id != locationRestaurant.Id)
            {
                return BadRequest();
            }

            _context.Entry(locationRestaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationRestaurantExists(id))
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

        // POST: api/LocationRest
        [HttpPost]
        public async Task<ActionResult<LocationRestaurant>> PostLocationRestaurant(LocationRestaurant locationRestaurant)
        {
            _context.LocationRest.Add(locationRestaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocationRestaurant", new { id = locationRestaurant.Id }, locationRestaurant);
        }

        // DELETE: api/LocationRest/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<LocationRestaurant>> DeleteLocationRestaurant(int id)
        {
            var locationRestaurant = await _context.LocationRest.FindAsync(id);
            if (locationRestaurant == null)
            {
                return NotFound();
            }

            _context.LocationRest.Remove(locationRestaurant);
            await _context.SaveChangesAsync();

            return locationRestaurant;
        }

        private bool LocationRestaurantExists(int id)
        {
            return _context.LocationRest.Any(e => e.Id == id);
        }
    }
}
