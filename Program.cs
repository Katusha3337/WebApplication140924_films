using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<MovieService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/Movie/GetPopularMovies", async (MovieService movieService, int page) =>
{
    var movies = await movieService.GetPopularMoviesAsync(page);
    return Results.Content(movies, "application/json");
});

app.MapGet("/Movie/GetMovieDetails/{id}", async (MovieService movieService, int id) =>
{
    var movie = await movieService.GetMovieDetailsAsync(id);
    return Results.Content(movie, "application/json");
});

app.MapPost("/Movie/SearchMovies", async (MovieService movieService, [FromBody] string query, int page) =>
{
    var movies = await movieService.SearchMoviesAsync(query, page);
    return Results.Content(movies, "application/json");
});

app.MapGet("/Movie/GetSimilarMovies/{id}", async (MovieService movieService, int id) =>
{
    var movies = await movieService.GetSimilarMoviesAsync(id);
    return Results.Content(movies, "application/json");
});

app.Run();
public class MovieService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public MovieService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["TMDB:ApiKey"];
    }

    public async Task<string> GetPopularMoviesAsync(int page = 1)
    {
        var response = await _httpClient.GetStringAsync($"https://api.themoviedb.org/3/movie/popular?api_key={_apiKey}&page={page}");
        return response;
    }

    public async Task<string> GetMovieDetailsAsync(int id)
    {
        var response = await _httpClient.GetStringAsync($"https://api.themoviedb.org/3/movie/{id}?api_key={_apiKey}");
        return response;
    }

    public async Task<string> SearchMoviesAsync(string query, int page = 1)
    {
        var response = await _httpClient.GetStringAsync($"https://api.themoviedb.org/3/search/movie?api_key={_apiKey}&query={query}&page={page}");
        return response;
    }

    public async Task<string> GetSimilarMoviesAsync(int id)
    {
        var response = await _httpClient.GetStringAsync($"https://api.themoviedb.org/3/movie/{id}/similar?api_key={_apiKey}");
        return response;
    }
}