using Microsoft.AspNetCore.Mvc;
using MovieSystemAPI.Models;
using MovieSystemAPI.Services;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonMovieController : ControllerBase
    {
        private readonly MovieSystemService _myService;
        private readonly DatabaseContext _context;

        public PersonMovieController(MovieSystemService myService, DatabaseContext context)
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
        [HttpGet("person/{personId}")]
        public async Task<ActionResult<PersonMovie>> GetRatingsByPersonId(int personId)
        {
            var person = await _context.People.FindAsync(personId);

            if (person == null)
            {
                return BadRequest($"Person with ID {personId} not found");
            }

            var personMovies = await _context.PersonMovies
                .Where(pm => pm.FkPersonId == personId)
                .Select(pm => new PersonMovie
                {
                    PersonMovieId = pm.PersonMovieId,
                    FkPersonId = pm.FkPersonId,
                    FkMovieId = pm.FkMovieId,
                    PersonRating = pm.PersonRating,
                })
               .ToListAsync();

            if (!personMovies.Any())
            {
                return BadRequest($"No Movies found");
            }

            return Ok(personMovies);
        }

        [HttpGet("movies/person/{personId}")]
        public async Task<ActionResult<PersonMovie>> GetMoviesByPersonId(int personId)
        {
            var person = await _context.People.FindAsync(personId);

            if (person == null)
            {
                return BadRequest($"Person with ID {personId} not found");
            }

            var movies = await _myService.GetPersonMovieByPersonId(personId);

            if (!movies.Any())
            {
                return BadRequest($"No Movies found");
            }

            return Ok(movies);
        }


        [HttpPost("person/{personId}/movie/{movieId}")]
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

        [HttpPost("person/{personId}/movie/{movieId}/rating/{rating}")]
        public async Task<ActionResult<PersonMovie>> AddRating(int personId, int movieId, decimal rating)
        {
            var person = await _context.People.FindAsync(personId);

            if (person == null)
            {
                return BadRequest($"Person with ID {personId} not found");
            }

            var movie = await _context.Movies.FindAsync(movieId);
            if (movie == null)
            {
                return BadRequest($"Movie with ID {movieId} not found");
            }

            // check if person already has the movie
            var personMovie = await _context.PersonMovies.FirstOrDefaultAsync(
                pm => pm.FkPersonId == personId && pm.FkMovieId == movieId);

            if (personMovie == null)
            {
                return BadRequest($"Person with ID {personId} does not have movie with ID {movieId}");
            }

            if (rating < 0 || rating > 10)
            {
                return BadRequest("Rating must be between 0 and 10");
            }

            personMovie.PersonRating = rating; //update rating

            await _myService.CalculateAverageRate(movieId); // update the movie's average rating
            await _context.SaveChangesAsync();

            return Ok(personMovie);
        }

        [HttpPost("person/{personId}/genre/{genreId}/movies")]
        public async Task<ActionResult<PersonMovie>> AddMoviesForPersonAndGenre(int personId, int genreId)
        {
            var person = await _context.People.FindAsync(personId);
            if (person == null)
            {
                return NotFound($"Person with Id {personId} not found.");
            }

            var personGenre = await _context.PersonGenres
                .FirstOrDefaultAsync(pg => pg.FkPersonId == personId && pg.FkGenreId == genreId);
            if (personGenre == null)
            {
                return BadRequest($"Person with Id {personId} does not prefer genre with Id {genreId}.");
            }

            var movieIds = await _context.MovieGenres
                .Where(mg => mg.FkGenreId == genreId)
                .Select(mg => mg.FkMovieId)
                .ToListAsync();

            var existingMovieIds = await _context.PersonMovies
                .Where(pm => pm.FkPersonId == personId)
                .Select(pm => pm.FkMovieId)
                .ToListAsync();

            var newMovieIds = movieIds.Except(existingMovieIds);

            var newMovies = await _context.Movies
                .Where(m => newMovieIds.Contains(m.MovieId))
                .ToListAsync();

            var personMoviesToAdd = newMovies
                .Select(m => new PersonMovie
                {
                    FkPersonId = personId,
                    FkMovieId = m.MovieId
                })
                .ToList();

            _context.PersonMovies.AddRange(personMoviesToAdd);

            await _context.SaveChangesAsync();
            return Ok(newMovies);
        }
    }
}
