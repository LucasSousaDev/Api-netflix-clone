using movies_api.API.Models.Inputs;
using movies_api.API.Models.Views;

namespace movies_api.Domain.Interfaces
{
    public interface IMovieService
    {
        Task<List<MovieViewModel>> GetAllMoviesAsync();
		Task<List<MovieViewModel>> GetPaginatedMoviesAsync(int page, int pageSize);
		Task<MovieViewModel> GetMovieByIdAsync(int id);
		Task<MovieViewModel> CreateMovieAsync(MovieInputModel newMovie);
		Task<MovieViewModel> UpdateMovieAsync(MovieUpdateModel movie);
		Task<bool> RemoveMovieAsync(int id);
    }
}