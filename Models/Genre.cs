using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;

namespace MovieSystemAPI.Models
{
    public class Genre
    {
        [Key]
        [JsonPropertyName("id")]
        public int GenreId { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("name")]
        [JsonPropertyName("name")]
        public string GenreTitle { get; set; } = string.Empty;

        [MaxLength(70)]
        [DisplayName("description")]
        public string GenreDescription { get; set; } = string.Empty;
    }

    public class GenresResponse
    {
        public List<Genre>? Genres { get; set; }
    }
}
