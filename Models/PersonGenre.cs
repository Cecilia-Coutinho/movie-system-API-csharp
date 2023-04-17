using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MovieSystemAPI.Models
{
    public class PersonGenre
    {
        [Key]
        public int PersonGenreId { get; set; }
        public int FkPersonId { get; set; }
        public int FkGenreId { get; set; }

        public virtual Person People { get; set; }
        public virtual Genre Genres { get; set; }
    }
}
