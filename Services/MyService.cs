using MovieSystemAPI.Models;
using System.Text.Json;

namespace MovieSystemAPI.Services
{
    public class MyService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;  //to read from JSON
        private readonly DatabaseContext _context;

        public MyService(HttpClient httpClient, IConfiguration configuration, DatabaseContext context)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _context = context;
        }

        private string GetSearchQueryUri(string searchQuery)
        {
            //get default url values from appsettings.json to build the url
            var apiKey = _configuration.GetValue<string>("TMDBApiKey");
            var apiLanguage = _configuration.GetValue<string>("ApiLanguage");
            var url = $"{searchQuery}{apiKey}{apiLanguage}";
            return url;
        }
        public async Task<List<Genre>> GetGenresTmdb()
        {
            var searchGenreUrl = _configuration.GetValue<string>("SearchGenreUrl");
            var url = GetSearchQueryUri(searchGenreUrl);

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode(); //throw exception if is false

            var responseContent = await response.Content.ReadAsStringAsync();
            //Console.WriteLine($"Response content: {responseContent}");

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase //JSON object keys should be converted to camelCase when deserializing
            };
            var result = JsonSerializer.Deserialize<GenresResponse>(responseContent, options);
            var genres = result?.Genres;
            return genres ?? new List<Genre>(); //if null return an empty list
        }

        public async Task<List<Movie>> GetMoviesTmdb()
        {
            var searchMoviesUrl = _configuration.GetValue<string>("SearchMoviesUrl");
            int totalPages = 3;
            int page = 1;
            var movies = new List<Movie>();

            while (page <= totalPages)
            {
                var searchMoviesAppend = $"&sort_by=vote_average.desc&vote_count.gte=5000&with_original_language=en&page={page}";
                var url = GetSearchQueryUri(searchMoviesUrl);
                var finalUrl = url + searchMoviesAppend;
                var response = await _httpClient.GetAsync(finalUrl);

                response.EnsureSuccessStatusCode(); //throw exception if is false

                var responseContent = await response.Content.ReadAsStringAsync();
                //Console.WriteLine($"Response content: {responseContent}");

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase //JSON object keys should be converted to camelCase when deserializing
                };
                var result = JsonSerializer.Deserialize<MoviesResponse>(responseContent, options);

                //retrive correct property from json
                var jsonDoc = JsonDocument.Parse(responseContent);
                var root = jsonDoc.RootElement;
                var results = root.GetProperty("results");

                foreach (var movieJson in results.EnumerateArray())
                {
                    var movie = new Movie
                    {
                        MovieTmdbId = movieJson.GetProperty("id").GetInt32(),
                        MovieTitle = movieJson.GetProperty("title").GetString(),
                        MovieRating = movieJson.GetProperty("vote_average").GetDecimal()
                    };
                    movies.Add(movie);
                }

                page++;
            }

            return movies ?? new List<Movie>(); //if null return an empty list
        }

        public async Task<Dictionary<string, string>> GenresDescriptions()
        {
            var genreDescriptions = new Dictionary<string, string>
            {
                {"Action", "Non-stop adrenaline. Explosive action."},
                {"Adventure", "Journey to the unknown. Thrilling exploration."},
                {"Animation", "Imaginative world. Animated characters."},
                {"Biography", "Real-life stories. Dramatic portrayals."},
                {"Comedy", "Laugh-out-loud humor. Witty and entertaining."},
                {"Crime", "Gritty underworld. Intricate investigations."},
                {"Documentary", "Real-life stories. Informative."},
                {"Drama", "Emotional depth. Compelling storytelling."},
                {"Family", "Heartwarming tales. Suitable for all ages."},
                {"Fantasy", "Enchanting worlds. Mythical creatures."},
                {"Film Noir", "Dark and moody. Brooding dramas."},
                {"History", "Rich historical context. Time-traveling."},
                {"Horror", "Creepy and frightening. Scary monsters."},
                {"Music", "Rhythmic melodies. Musical performances."},
                {"Musical", "Singing and dancing. Show-stopping numbers."},
                {"Mystery", "Mysterious whodunits. Puzzling plot twists."},
                {"Romance", "Heartfelt love stories. Passionate romances."},
                {"Science Fiction", "Futuristic worlds. Imaginative concepts."},
                {"Short Film", "Concise storytelling. Creative expression."},
                {"Sport", "Competitive athletics. Inspiring achievements."},
                {"Superhero", "Extraordinary abilities. Epic battles."},
                {"Suspense", "Nail-biting tension. Suspenseful plot."},
                {"TV Movie", "Feature-length productions for TV."},
                {"Thriller", "Intense and exciting. Edge-of-your-seat action."},
                {"War", "Battleground stories. Heroic sacrifices."},
                {"Western", "Cowboys and outlaws. Action-packed shootouts."},
                {"Epic", "Grand and majestic. Epic in scale."},
                {"Erotic", "Sensual and provocative. Sexual content."},
                {"Experimental", "Innovative and unconventional. Artistic exploration."},
                {"Mockumentary", "Hilarious satire. Mocking documentaries."},
                {"Satire", "Social commentary. Humorous ridicule."}
            };

            return await Task.FromResult(genreDescriptions);
        }
    }
}
