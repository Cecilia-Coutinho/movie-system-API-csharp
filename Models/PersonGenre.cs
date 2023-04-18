using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MovieSystemAPI.Models
{
    public class PersonGenre
    {
        [Key]
        public int PersonGenreId { get; set; }

        [Required]
        public int FkPersonId { get; set; }

        [Required]
        public int FkGenreId { get; set; }

        [JsonIgnore]
        public virtual Person People { get; set; }

        [JsonIgnore]
        public virtual Genre Genres { get; set; }

        [JsonIgnore]
        public List<PersonMovie> PersonMovies { get; set; }
    }
}
