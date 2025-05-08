using Microsoft.AspNetCore.Http.HttpResults;
using movies_api.API.Models.Inputs;
using movies_api.API.Models.Views;
using movies_api.Domain.Entities;
using movies_api.Domain.Interfaces;

namespace movies_api.Application.Services
{
    public class MovieService : IMovieService
    {
		private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
		{
			_movieRepository = movieRepository;
		}

		public async Task<List<MovieViewModel>> GetAllMoviesAsync()
		{
			List<Movie> movies = await _movieRepository.GetAllMoviesAsync();

			return [.. movies.Select(movie => new MovieViewModel(movie))];
		}

		public async Task<List<MovieViewModel>> GetPaginatedMoviesAsync(int page, int pageSize)
		{
			List<Movie> movies = await _movieRepository.GetPaginatedMoviesAsync(page, pageSize);

			return [.. movies.Select(movie => new MovieViewModel(movie))];
		}

		public async Task<MovieViewModel> GetMovieByIdAsync(int id)
		{
			Movie? movie = await _movieRepository.GetMovieByIdAsync(id)
				?? throw new KeyNotFoundException($"Filme com ID {id} não encontrado.");

			return new MovieViewModel(movie);
		}

		public async Task<MovieViewModel> CreateMovieAsync(MovieInputModel newMovie)
		{
			Movie movie = newMovie.ToEntity();

			Movie createdMovie = await _movieRepository.CreateMovieAsync(movie)
				?? throw new Exception($"Não foi possível criar o filme {newMovie.Title}.");

			return new MovieViewModel(createdMovie!);
		}

		public async Task<MovieViewModel> UpdateMovieAsync(MovieUpdateModel movie)
		{
			Movie foundMovie = await _movieRepository.GetMovieByIdAsync(movie.Id)
				?? throw new KeyNotFoundException($"Filme com ID {movie.Id} não encontrado.");
			
			movie.ApplyToEntity(foundMovie);

			return (await _movieRepository.SaveChangesAsync())
				? new MovieViewModel(foundMovie)
				: throw new Exception($"Não foi possível atualizar o filme {foundMovie.Title}.");
		}

		public async Task<bool> RemoveMovieAsync(int id)
		{
			Movie? foundMovie = await _movieRepository.GetMovieByIdAsync(id)
				?? throw new KeyNotFoundException($"Filme com ID {id} não encontrado.");

			return await _movieRepository.RemoveMovieAsync(foundMovie);
		}
	}
}
