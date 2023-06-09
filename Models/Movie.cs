﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MovieSystemAPI.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [Required]
        public string? MovieTitle { get; set; } = string.Empty;
        public decimal MovieRating { get; set; }
        public string? PosterPathTMDB { get; set; }
        public string? Overview { get; set; }

        [JsonIgnore]
        public List<PersonMovie>? PersonMovies { get; set; }
        public List<MovieGenre>? MovieGenres { get; set; }
    }

    public class MoviesResponse
    {
        public List<Movie>? Movies { get; set; }
    }

    public class MovieDetailsResponse
    {
        [JsonPropertyName("title")]
        public string? MovieTitle { get; set; } = string.Empty;

        [JsonPropertyName("genre_ids")]
        public List<int> GenresTmdbId { get; set; } = new List<int>();
    }

    public class MovieTMDB
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; }
        public string ReleaseDate { get; set; }
        public double VoteAverage { get; set; }
    }

}
