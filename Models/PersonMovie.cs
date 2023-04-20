using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieSystemAPI.Models
{
    public class PersonMovie
    {
        [Key]
        public int PersonMovieId { get; set; }

        [Required]
        public int FkPersonId { get; set; }

        [Required]
        public int FkMovieId { get; set; }

        public double PersonRating { get; set; }


        [JsonIgnore]
        public virtual Person People { get; set; }

        [JsonIgnore]
        public virtual Movie Movies { get; set; }
    }
}
