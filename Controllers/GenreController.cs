using Microsoft.AspNetCore.Mvc;
using MovieSystemAPI.Models;
using MovieSystemAPI.Services;
using System.Net.Http;
using System.Net.WebSockets;

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
        public async Task<ActionResult<GenresResponse>> GetGenresAsync()
        {
            var genresList = await _myService.GetGenresAsync();
            return Ok(genresList);
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenreByIdAsync(int id)
        {
            var genresList = await _myService.GetGenresAsync();
            var genre = genresList.Find(g => g.GenreId == id);
            if (genre == null)
            {
                return BadRequest("Genre not found");
            }
            return Ok(genre);
        }

        // POST api/<GenreController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // POST api/<GenreController>
        [HttpPost]
        public async Task<ActionResult<GenresResponse>> AddGenre(Genre newGenre)
        {
            var genres = await _myService.GetGenresAsync();
            genres.Add(newGenre);
            return Ok(genres);
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
