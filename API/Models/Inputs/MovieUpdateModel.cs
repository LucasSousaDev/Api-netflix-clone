using movies_api.Domain.Entities;
using movies_api.Domain.Enums;

namespace movies_api.API.Models.Inputs
{
    public class MovieUpdateModel
    {
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? ImageUrl { get; set; }
		public MovieType? Type { get; set; } = MovieType.FILM;
		public double? Duration { get; set; } = null;
		public ushort? Seasons { get; set; } = null;
		public ushort? Episodes { get; set; } = null;
		public int? ReleaseYear { get; set; }
		public double? Rating { get; set; }
		public double? MovieRating { get; set; }
		public List<MovieCategories>? Categories { get; set; }

		public MovieUpdateModel() {}

		public void ApplyToEntity(Movie movie)
		{
			movie.Title = Title ?? movie.Title;
			movie.Description = Description ?? movie.Description;
			movie.ImageUrl = ImageUrl ?? movie.ImageUrl;
			movie.Type = Type ?? movie.Type;
			movie.Duration = Duration ?? movie.Duration;
			movie.Seasons = Seasons ?? movie.Seasons;
			movie.Episodes = Episodes ?? movie.Episodes;
			movie.ReleaseYear = ReleaseYear ?? movie.ReleaseYear;
			movie.Rating = Rating ?? movie.Rating;
			movie.MovieRating = MovieRating ?? movie.MovieRating;
			movie.Categories = Categories ?? movie.Categories;
		}
    }
}