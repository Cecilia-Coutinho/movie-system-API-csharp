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

        // GET
        [HttpGet]
        public async Task<ActionResult<List<PersonMovie>>> GetAll()
        {
            var personMovieList = await _context.PersonMovies.ToListAsync();

            if (personMovieList == null)
            {
                return BadRequest($"PersonMovie is empty.");
            }

            return Ok(personMovieList);
        }

        [HttpGet("{personId}")]
        public async Task<ActionResult<PersonMovie>> GetByPersonId(int personId)
        {

            var person = await _context.People.FindAsync(personId);

            if (person == null)
            {
                return BadRequest($"Person with ID {personId} not found");
            }

            var movies = await _context.Movies
                .Join(_context.PersonMovies,
                    g => g.MovieId,
                    pg => pg.FkMovieId,
                    (g, pg) => new { Movie = g, PersonMovie = pg })
                .Where(p => p.PersonMovie.FkPersonId == personId)
                .Select(g => g.Movie.MovieTitle)
                .ToListAsync();

            if (!movies.Any())
            {
                return BadRequest($"No Movies found");
            }

            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonMovie>> GetById(int id)
        {
            var personMovie = await _context.PersonMovies.FindAsync(id);

            if (personMovie == null)
            {
                return BadRequest("Person Movie not found");
            }
            return Ok(personMovie);
        }


        [HttpPost("{personId}/{movieId}")]
        public async Task<ActionResult<List<PersonMovie>>> AddPersonMovie(int personId, int movieId)
        {
            var person = await _context.People.FindAsync(personId);

            if (person == null)
            {
                return BadRequest($"Person with ID {personId} not found");
            }

            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null)
            {
                return BadRequest($"Genre with ID {movieId} not found");
            }

            // check if person already has the movie
            var existingPersonMovie = await _context.PersonMovies.Where(
                p => p.FkPersonId == personId &&
                p.FkMovieId == movieId).ToListAsync();

            if (existingPersonMovie.Count > 0)
            {
                return BadRequest("This combination already exists in the database");
            }

            // Create a new PersonGenres object and add it to the database
            PersonMovie personGenre = new PersonMovie()
            {
                FkPersonId = personId,
                FkMovieId = movieId
            };

            _context.PersonMovies.Add(personGenre);
            await _context.SaveChangesAsync();

            return Ok(await _context.PersonMovies.ToListAsync());
        }
    }
}
