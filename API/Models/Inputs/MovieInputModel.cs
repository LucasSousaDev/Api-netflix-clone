using movies_api.Domain.Entities;
using movies_api.Domain.Enums;

namespace movies_api.API.Models.Inputs
{
    public class MovieInputModel
    {
		public string Title { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public MovieType Type { get; set; } = MovieType.FILM;
		public double? Duration { get; set; } = null;
		public ushort? Seasons { get; set; } = null;
		public ushort? Episodes { get; set; } = null;
		public int ReleaseYear { get; set; }
		public double Rating { get; set; }
		public double MovieRating { get; set; }
		public List<MovieCategories> Categories { get; set; }

		public MovieInputModel() {}

		public MovieInputModel(Movie movie)
		{
			Title = movie.Title;
			Description = movie.Description;
			ImageUrl = movie.ImageUrl;
			Type = movie.Type;
			Duration = movie.Duration;
			Seasons = movie.Seasons;
			Episodes = movie.Episodes;
			ReleaseYear = movie.ReleaseYear;
			Rating = movie.Rating;
			MovieRating = movie.MovieRating;
			Categories = movie.Categories;
		}

		public Movie ToEntity()
		{
			return new Movie {
				Title = Title,
				Description = Description,
				ImageUrl = ImageUrl,
				Type = Type,
				Duration = Duration,
				Seasons = Seasons,
				Episodes = Episodes,
				ReleaseYear = ReleaseYear,
				Rating = Rating,
				MovieRating = MovieRating,
				Categories = Categories
			};
		}
    }
}