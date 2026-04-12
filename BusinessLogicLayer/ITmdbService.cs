using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface ITmdbService
    {
        Task<List<MovieDto>> GetMoviesByGenreAsync(int genreId);
    }

}
