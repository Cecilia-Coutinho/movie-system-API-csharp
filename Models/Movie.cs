using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieSystemAPI.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        public int MovieTmdbId { get; set; }

        [Required]
        public string MovieTitle { get; set; } = string.Empty;
        public decimal MovieRating { get; set; }

        [JsonIgnore]
        public List<PersonMovie>? PersonMovies { get; set; }
    }

    public class MoviesResponse
    {
        public List<Movie> Movies { get; set; }
    }

}
