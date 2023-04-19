using Microsoft.AspNetCore.Mvc;
using MovieSystemAPI.Models;
using MovieSystemAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonMovieController : ControllerBase
    {

        private readonly MyService _myService;
        private readonly DatabaseContext _context;

        public PersonMovieController(MyService myService, DatabaseContext context)
        {
            _myService = myService;
            _context = context;
        }

        // GET: api/<PersonMovieController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PersonMovieController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PersonMovieController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PersonMovieController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PersonMovieController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
