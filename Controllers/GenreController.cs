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
        public async Task<ActionResult<GenresResponse>> GetGenres()
        {
            var genresList = await _myService.GetGenresTmdb();
            return Ok(genresList);
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenreById(int id)
        {
            var genresList = await _myService.GetGenresTmdb();
            var genre = genresList.Find(g => g.GenreId == id);
            if (genre == null)
            {
                return BadRequest("Genre not found");
            }
            return Ok(genre);
        }

        /// <summary>
        /// insert a new genre to the list TmdbApi's List
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        // POST api/<GenreController>
        [HttpPost]
        public async Task<ActionResult<GenresResponse>> AddGenre(Genre genre)
        {
            var genresList = await _myService.GetGenresTmdb();
            genresList.Add(genre);
            return Ok(genresList);
        }

        // PUT api/<GenreController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GenresResponse>> UpdateGenre(Genre request)
        {
            var genresList = await _myService.GetGenresTmdb();
            var genre = genresList.Find(g => g.GenreId == request.GenreId);

            if (genre == null)
            {
                return BadRequest("Genre not found");
            }

            genre.GenreTitle = request.GenreTitle;
            genre.GenreDescription = request.GenreDescription;
            return Ok(genre);
        }

        // PUT api/<GenreController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GenresResponse>> UpdateGenreDescription(Genre request)
        {
            var genresList = await _myService.GetGenresTmdb();
            var genre = genresList.Find(g => g.GenreId == request.GenreId);

            if (genre == null)
            {
                return BadRequest("Genre not found");
            }

            genre.GenreDescription = request.GenreDescription;
            return Ok(genre);
        }

        // DELETE api/<GenreController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
