using Microsoft.AspNetCore.Mvc;

namespace WebApplication140924_films.Controllers
{
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> GetPopularMovies(int page = 1)
        {
            var movies = await _movieService.GetPopularMoviesAsync(page);
            return Content(movies, "application/json");
        }

        public async Task<IActionResult> GetMovieDetails(int id)
        {
            var movie = await _movieService.GetMovieDetailsAsync(id);
            return Content(movie, "application/json");
        }

        public async Task<IActionResult> SearchMovies([FromBody] string query, int page = 1)
        {
            var movies = await _movieService.SearchMoviesAsync(query, page);
            return Content(movies, "application/json");
        }

        public async Task<IActionResult> GetSimilarMovies(int id)
        {
            var movies = await _movieService.GetSimilarMoviesAsync(id);
            return Content(movies, "application/json");
        }
    }
}
