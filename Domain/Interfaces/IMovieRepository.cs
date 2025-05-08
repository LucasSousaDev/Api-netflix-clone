using movies_api.Domain.Entities;

namespace movies_api.Domain.Interfaces
{
    public interface IMovieRepository
	{
		Task<List<Movie>> GetAllMoviesAsync();
		Task<List<Movie>> GetPaginatedMoviesAsync(int page, int pageSize);
		Task<Movie?> GetMovieByIdAsync(int id);
		Task<Movie?> CreateMovieAsync(Movie newMovie);
		Task<bool> SaveChangesAsync();
		Task<bool> RemoveMovieAsync(Movie movie);
	}
}