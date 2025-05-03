using movies_api.Domain.Entities;

namespace movies_api.Domain.Interfaces
{
    public interface IMovieRepository
	{
		Task<List<Movie>> GetAllMovies();
		Task<List<Movie>> GetPaginatedMovies(int page, int pageSize);
		Task<Movie?> GetMovieById(int id);
		Task<Movie?> CreateMovie(Movie newMovie);
		Task<bool> RemoveMovie(int id);
	}
}