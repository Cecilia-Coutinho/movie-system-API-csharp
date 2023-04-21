using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieSystemAPI.Models
{
    public class MovieGenre
    {
        [Key]
        public int MovieGenreId { get; set; }

        [Required]
        public int FkMovieId { get; set; }

        [Required]
        public int FkGenreId { get; set; }

        [JsonIgnore]
        public virtual Movie Movies { get; set; }

        [JsonIgnore]
        public virtual Genre Genres { get; set; }
    }
}
