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

		public async Task<List<Movie>> GetAllMovies()
		{
			return await _appDbContext.Movies
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<List<Movie>> GetPaginatedMovies(int page, int pageSize)
		{
			return await _appDbContext.Movies
				.AsNoTracking()
				.Skip((page-1)*pageSize)
				.Take(pageSize)
				.ToListAsync();
		}

		public async Task<Movie?> GetMovieById(int id)
		{
			return await _appDbContext.Movies
				.FirstOrDefaultAsync(movie => movie.Id == id);
		}

		public async Task<Movie?> CreateMovie(Movie newMovie)
		{
			EntityEntry<Movie> movieCreated = await _appDbContext.Movies.AddAsync(newMovie);
			int createdResult = await _appDbContext.SaveChangesAsync();

			return (createdResult > 0)
				? movieCreated.Entity
				: null;
		}

		public async Task<bool> RemoveMovie(int id)
		{
			Movie? foundMovie = await GetMovieById(id);

			//Criar um Middleware para exceções
			if (foundMovie is null)
				throw new KeyNotFoundException($"Filme com ID {id} não encontrado.");
			
			_appDbContext.Remove(foundMovie);
			int deleteResult = await _appDbContext.SaveChangesAsync();

			return (deleteResult > 0);
		}
	}
}
