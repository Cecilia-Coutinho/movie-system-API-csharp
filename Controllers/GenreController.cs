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
        private readonly DatabaseContext _context;

        public GenreController(MyService myService, DatabaseContext context)
        {
            _myService = myService;
            _context = context;
        }

        // GET: api/<GenreController>
        [HttpGet]
        public async Task<ActionResult<GenresResponse>> GetGenres()
        {
            var genresList = await _myService.GetGenresTmdb();
            return Ok(genresList);

            //return Ok(await _context.Genres.ToListAsync());
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenreById(int id)
        {
            var genresList = await _myService.GetGenresTmdb();
            var genre = genresList.Find(g => g.GenreId == id);

            //var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
            {
                return BadRequest("Genre not found");
            }
            return Ok(genre);
        }

        // POST api/<GenreController>
        //[HttpPost]
        //public async Task<ActionResult<GenresResponse>> AddGenre(Genre genre)
        //{
        //    var genresList = await _myService.GetGenresTmdb();
        //    genresList.Add(genre);
        //    return Ok(genresList);
        //}

        [HttpPost]
        public async Task<ActionResult<GenresResponse>> FetchAndUpdateDB()
        {
            var genresList = await _myService.GetGenresTmdb();

            for (int i = 0; i < genresList.Count; i++)
            {
                Genre genre = genresList[i];
                genre.GenreId = 0;
                _context.Genres.Add(genre);
            }

            await _context.SaveChangesAsync();

            return Ok(await _context.Genres.ToListAsync());
        }

        //[HttpPost]
        //public async Task<ActionResult<GenresResponse>> AddDatabaseItem(Genre genre)
        //{
        //    _context.Genres.Add(genre);
        //    await _context.SaveChangesAsync();
        //    return Ok(await _context.Genres.ToListAsync());
        //}

        // POST api/<GenreController>
        //[HttpPost]
        //public async Task<ActionResult<GenresResponse>> AddGenre(Genre genre)
        //{
        //    _context.Genres.Add(genre);
        //    await _context.SaveChangesAsync();
        //    return Ok(await _context.Genres.ToListAsync());
        //}

        //PUT api/<GenreController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GenresResponse>> UpdateGenre(Genre request, int id)
        {
            var genresList = await _myService.GetGenresTmdb();
            var genre = genresList.Find(g => g.GenreId == id);

            if (genre == null)
            {
                return BadRequest("Genre not found");
            }

            //genre.GenreId = request.GenreId;
            //genre.GenreTitle = request.GenreTitle;
            genre.GenreDescription = request.GenreDescription;
            return Ok(genresList);
        }
    }
}
