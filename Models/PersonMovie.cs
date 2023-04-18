using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieSystemAPI.Models
{
    public class PersonMovie
    {
        [Key]
        public int PersonMovieId { get; set; }

        [Required]
        public int FkPersonGenreId { get; set; }

        [Required]
        public string MovieLink { get; set; } = string.Empty;

        public int PersonRating { get; set; }

        public int MovieAverageRate { get; set; }

        [JsonIgnore]
        public virtual PersonGenre PersonGenre { get; set; }
    }
}
