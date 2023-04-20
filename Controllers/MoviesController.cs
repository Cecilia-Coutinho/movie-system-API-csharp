using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieSystemAPI.Models;
using MovieSystemAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieSystemService _myService;
        private readonly DatabaseContext _context;

        public MoviesController(MovieSystemService myService, DatabaseContext context)
        {
            _myService = myService;
            _context = context;
        }

        // GET:
        [HttpGet]
        public async Task<ActionResult<MoviesResponse>> GetAll()
        {
            //ensure to seed data if is empty
            var moviesList = await _myService.GetMoviesTmdb();
            var addMovies = new List<Movie>();

            // retrieve the list of movies already exists in the database, if exists any
            var existingTitles = await _context.Movies.Select(mt => mt.MovieTitle).ToListAsync();

            for (int i = 0; i < moviesList.Count; i++)
            {
                Movie movie = moviesList[i];

                // check if the title already exists in the db
                if (!existingTitles.Contains(movie.MovieTitle))
                {
                    movie.MovieId = 0;
                    addMovies.Add(movie);
                    _context.Movies.Add(movie);
                }
            }

            await _context.SaveChangesAsync();

            //Get Data:
            return Ok(await _context.Movies.ToListAsync());
        }


        [HttpGet("{personId}")]
        public async Task<ActionResult<Movie>> GetRatingsByPersonId(int personId)
        {
            var person = await _context.People.FindAsync(personId);

            if (person == null)
            {
                return BadRequest($"Person with ID {personId} not found");
            }

            var movies = await _myService.GetRatingsByPersonId(personId);

            if (!movies.Any())
            {
                return BadRequest($"No Movies found");
            }

            return Ok(movies);
        }

        [HttpGet("recommendation/{genre}")]
        public async Task<ActionResult<Movie>> GetRatingsByPersonId(string genre)
        {
            var movies = await _myService.GetMovieRecommendationsByGenre(genre);

            if (!movies.Any())
            {
                return BadRequest($"No Movies found");
            }
            return Ok(movies);
        }
    }
}
