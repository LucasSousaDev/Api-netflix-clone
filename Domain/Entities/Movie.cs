using movies_api.Domain.Enums;

namespace movies_api.Domain.Entities
{
	public class Movie
	{
		public int Id { get; set; }
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

		public Movie() { }

		public Movie
		(
			string title,
			string description,
			string imageUrl,
			MovieType? type,
			double? duration,
			ushort? seasons,
			ushort? episodes,
			int releaseYear,
			double rating,
			double movieRating,
			List<MovieCategories> categories
		)
		{
			Title = title;
			Description = description;
			ImageUrl = imageUrl;
			Type = type ?? Type;
			Duration = duration ?? Duration;
			Seasons = seasons ?? Seasons;
			Episodes = episodes ?? Episodes;
			ReleaseYear = releaseYear;
			Rating = rating;
			MovieRating = movieRating;
			Categories = categories;
		}
	}
}
