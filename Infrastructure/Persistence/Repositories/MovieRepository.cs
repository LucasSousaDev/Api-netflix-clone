using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using movies_api.Domain.Entities;
using movies_api.Domain.Interfaces;

namespace movies_api.Infrastructure.Persistence.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _appDbContext;

		public MovieRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<List<Movie>> GetAllMoviesAsync()
		{
			return await _appDbContext.Movies
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<List<Movie>> GetPaginatedMoviesAsync(int page, int pageSize)
		{
			return await _appDbContext.Movies
				.AsNoTracking()
				.Skip((page-1)*pageSize)
				.Take(pageSize)
				.ToListAsync();
		}

		public async Task<Movie?> GetMovieByIdAsync(int id)
		{
			return await _appDbContext.Movies
				.FirstOrDefaultAsync(movie => movie.Id == id);
		}

		public async Task<Movie?> CreateMovieAsync(Movie newMovie)
		{
			EntityEntry<Movie> movieCreated = await _appDbContext.Movies.AddAsync(newMovie);
			int createdResult = await _appDbContext.SaveChangesAsync();

			return (createdResult > 0)
				? movieCreated.Entity
				: null;
		}

		public async Task<bool> SaveChangesAsync()
		{
			int saveResult = await _appDbContext.SaveChangesAsync();
			
			return (saveResult > 0);
		}

		public async Task<bool> RemoveMovieAsync(Movie movie)
		{
			_appDbContext.Remove(movie);
			int deleteResult = await _appDbContext.SaveChangesAsync();

			return (deleteResult > 0);
		}
	}
}
