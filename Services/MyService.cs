using MovieSystemAPI.Models;
using System.Text.Json;

namespace MovieSystemAPI.Services
{
    public class MyService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;  //to read from JSON

        public MyService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
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
    }
}
