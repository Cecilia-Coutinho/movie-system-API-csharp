using Microsoft.AspNetCore.Mvc;
using MovieSystemAPI.Models;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonGenreController : ControllerBase
    {

        private readonly DatabaseContext _context;

        public PersonGenreController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<PersonGenreController>
        [HttpGet]
        public async Task<ActionResult<List<PersonGenre>>> GetAll()
        {
            var personGenreList = await _context.PersonGenres.ToListAsync();

            if (personGenreList == null)
            {
                return BadRequest($"PersonGenre is empty.");
            }

            return Ok(personGenreList);
        }

        // GET api/<PersonGenreController>/5
        [HttpGet("{personId}")]
        public async Task<ActionResult<PersonGenre>> GetGenresByPersonId(int personId)
        {

            var person = await _context.People.FindAsync(personId);

            if (person == null)
            {
                return BadRequest($"Person with ID {personId} not found");
            }

            var genres = await _context.Genres
                .Join(_context.PersonGenres,
                    g => g.GenreId,
                    pg => pg.FkGenreId,
                    (g, pg) => new { Genre = g, PersonGenre = pg })
                .Where(p => p.PersonGenre.FkPersonId == personId)
                .Select(g => g.Genre.GenreTitle)
                .ToListAsync();

            if (!genres.Any())
            {
                return BadRequest($"No genres found");
            }

            return Ok(genres);
        }


        // GET api/<PersonGenreController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonGenre>> GetById(int id)
        {
            var personGenre = await _context.PersonGenres.FindAsync(id);

            if (personGenre == null)
            {
                return BadRequest("Person Genre not found");
            }
            return Ok(personGenre);
        }

        // POST api/<PersonGenreController>
        [HttpPost("{personId}/{genreId}")]
        public async Task<ActionResult<List<PersonGenre>>> AddPersonGenre(int personId, int genreId)
        {
            var person = await _context.People.FindAsync(personId);

            if (person == null)
            {
                return BadRequest($"Person with ID {personId} not found");
            }

            var genre = await _context.Movies.FindAsync(genreId);
            if (genre == null)
            {
                return BadRequest($"Genre with ID {genreId} not found");
            }

            // check if person already has the genre
            var existingPersonGenre = await _context.PersonGenres.Where(
                p => p.FkPersonId == personId &&
                p.FkGenreId == genreId).ToListAsync();

            if (existingPersonGenre.Count > 0)
            {
                return BadRequest("This combination already exists in the database");
            }

            // Create a new PersonGenres object and add it to the database
            PersonGenre personGenre = new PersonGenre()
            {
                FkPersonId = personId,
                FkGenreId = genreId
            };

            _context.PersonGenres.Add(personGenre);
            await _context.SaveChangesAsync();

            return Ok(await _context.PersonGenres.ToListAsync());
        }

    }
}
