using movies_api.Domain.Enums;

namespace movies_api.Domain.Entities
{
	public class Movie
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public double Duration { get; set; }
		public ushort Seasons { get; set; }
		public ushort Episodes { get; set; }
		public int ReleaseYear { get; set; }
		public string[] Actors { get; set; }
		public double Rating { get; set; }
		public double MovieRating { get; set; }
		public List<MovieCategories> Categories { get; set; }

		public Movie() { }

		public Movie
		(
			string title,
			string description,
			string imageUrl,
			double duration,
			ushort seasons,
			ushort episodes,
			int releaseYear,
			string[] actors,
			double rating,
			double movieRating,
			List<MovieCategories> categories
		)
		{
			Title = title;
			Description = description;
			ImageUrl = imageUrl;
			Duration = duration;
			Seasons = seasons;
			Episodes = episodes;
			ReleaseYear = releaseYear;
			Actors = actors;
			Rating = rating;
			MovieRating = movieRating;
			Categories = categories;
		}
	}
}
