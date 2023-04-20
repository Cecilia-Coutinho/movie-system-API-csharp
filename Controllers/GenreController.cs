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
        private readonly MovieSystemService _myService;
        private readonly DatabaseContext _context;

        public GenreController(MovieSystemService myService, DatabaseContext context)
        {
            _myService = myService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<GenresResponse>> GetAll()
        {
            //ensure to seed with data if is empty:
            var genresList = await _myService.GetGenresTmdb();
            var genreDescriptions = await _myService.GenresDescriptions();

            // retrieve the list of genres already exists in the database, if exists any
            var existingGenres = await _context.Genres.Select(gt => gt.GenreTitle).ToListAsync();

            // iterate through genres and add descriptions
            for (int i = 0; i < genresList.Count; i++)
            {
                Genre genre = genresList[i];

                // check if the title already exists in the db
                if (!existingGenres.Contains(genre.GenreTitle))
                {
                    genre.GenreId = 0; //to not retrieve same id
                    genre.GenreDescription = genreDescriptions.GetValueOrDefault(genre.GenreTitle, ""); //fetch description from the diccionary
                    _context.Genres.Add(genre);
                }
            }

            await _context.SaveChangesAsync();

            //Get Data:
            return Ok(await _context.Genres.ToListAsync());
        }

        // GET api/<GenreController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetById(int id)
        {
            var genre = await _context.Movies.FindAsync(id);

            if (genre == null)
            {
                return BadRequest("Genre not found");
            }
            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult<GenresResponse>> AddGenre(Genre genre)
        {
            // retrieve the list of genres already exists in the database
            var existingGenres = await _context.Genres.Select(gt => gt.GenreTitle).ToListAsync();

            if (existingGenres.Contains(genre.GenreTitle))
            {
                return BadRequest("Genre already exists in the database");
            }

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return Ok(await _context.Genres.ToListAsync());
        }
    }
}
