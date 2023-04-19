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
        private readonly MyService _myService;
        private readonly DatabaseContext _context;

        public MoviesController(MyService myService, DatabaseContext context)
        {
            _myService = myService;
            _context = context;
        }

        // GET:
        [HttpGet("TMDB/FetchMovies")]
        public async Task<ActionResult<MoviesResponse>> GetAllFromTmdb()
        {
            var moviesList = await _myService.GetMoviesTmdb();
            return Ok(moviesList);
        }

        // POST:
        [HttpPost("AddFetchedMovies")]
        public async Task<ActionResult<MoviesResponse>> FetchAndAddMoviesToDb()
        {
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
            return Ok(await _context.Movies.ToListAsync());
        }
    }
}
