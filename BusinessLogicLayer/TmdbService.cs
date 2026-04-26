using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class TmdbService : ITmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "d61af2195e67c154f110cb3a443c27ff";

        public TmdbService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<MovieDto>> GetMoviesByGenreAsync(int genreId)
        { 
            var response = await _httpClient.GetAsync($"https://api.themoviedb.org/3/discover/movie?api_key={_apiKey}&with_genres={genreId}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TmdbResponse>(content);

            return result.Results;
        }
    }
}
