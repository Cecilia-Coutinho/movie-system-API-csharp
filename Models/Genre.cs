using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json.Serialization;

namespace MovieSystemAPI.Models
{
    public class Genre
    {
        //[Key]
        [JsonPropertyName("id")]
        public int GenreId { get; set; }

        //[Required]
        //[MaxLength(50)]
        //[DisplayName("First Name")]
        [JsonPropertyName("name")]
        public string GenreTitle { get; set; } = string.Empty;

        //[Required]
        //[MaxLength(50)]
        //[DisplayName("Last Name")]
        public string GenreDescription { get; set; } = string.Empty;
    }

    public class GenresResponse
    {
        public List<Genre> Genres { get; set; }
    }
}
