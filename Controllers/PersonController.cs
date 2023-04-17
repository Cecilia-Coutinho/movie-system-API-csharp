using Microsoft.AspNetCore.Mvc;
using MovieSystemAPI.Models;
using MovieSystemAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PersonController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<PersonController>
        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetPeopleFromDb()
        {
            return Ok(await _context.People.ToListAsync());
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPersonById(int id)
        {
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                return BadRequest("Person not found");
            }
            return Ok(person);
        }

        // POST api/<PersonController>/5
        [HttpPost]
        public async Task<ActionResult<List<Person>>> AddPerson(Person person)
        {
            // retrieve the list of genres already exists in the database
            var existingPeople = await _context.People.Select(e => e.Email).ToListAsync();

            if (existingPeople.Contains(person.Email))
            {
                return BadRequest("Person already exists in the database");
            }

            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return Ok(await _context.People.ToListAsync());
        }
    }
}
