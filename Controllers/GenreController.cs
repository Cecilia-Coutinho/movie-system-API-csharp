using Microsoft.AspNetCore.Mvc;
using MovieSystemAPI.Models;
using MovieSystemAPI.Services;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly MyService _myService;

        public GenreController(MyService myService)
        {
            _myService = myService;
        }

        // GET: api/<GenreController>
        [HttpGet]
        public async Task<IActionResult> GetGenresAsync()
        {
            var genres = await _myService.GetGenresAsync();
            return Ok(genres);
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GenreController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GenreController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GenreController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
