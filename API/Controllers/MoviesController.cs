using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using movies_api.API.Models.Inputs;
using movies_api.API.Models.Views;
using movies_api.Domain.Interfaces;

namespace movies_api.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class MoviesController : ControllerBase
	{
		private readonly IMovieService _movieService;

		public MoviesController(IMovieService movieService)
		{
			_movieService = movieService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllMovies()
		{
			List<MovieViewModel> movies = await _movieService.GetAllMoviesAsync();

			return (movies.Count > 0)
				? Ok(movies)
				: NoContent();
		}

		[HttpGet("{pageSize}/{page}")]
		public async Task<IActionResult> GetPaginatedMovies([FromRoute] int pageSize, [FromRoute] int page)
		{
			List<MovieViewModel> movies = await _movieService.GetPaginatedMoviesAsync(page, pageSize);

			return (movies.Count > 0)
				? Ok(movies)
				: NoContent();
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetMovieById([FromRoute] int id)
		{
			MovieViewModel movie = await _movieService.GetMovieByIdAsync(id);
			return Ok(movie);
		}

		[HttpPost]
		public async Task<IActionResult> CreateMovie([FromBody] MovieInputModel newMovie)
		{
			MovieViewModel movie = await _movieService.CreateMovieAsync(newMovie);
			return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateMovie([FromBody] MovieUpdateModel movieUpdate)
		{
			MovieViewModel movie = await _movieService.UpdateMovieAsync(movieUpdate);
			return Ok(movie);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteMovie([FromRoute] int id)
		{
			await _movieService.RemoveMovieAsync(id);
			return Ok();
		}
	}
}
