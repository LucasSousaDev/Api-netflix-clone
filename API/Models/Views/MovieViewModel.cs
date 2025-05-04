using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using movies_api.Domain.Entities;
using movies_api.Domain.Enums;

namespace movies_api.API.Models.Views
{
    public class MovieViewModel
    {
        public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }
		public MovieType Type { get; set; } = MovieType.FILM;
		public double? Duration { get; set; } = null;
		public ushort? Seasons { get; set; } = null;
		public ushort? Episodes { get; set; } = null;
		public string ReleaseYearFormatted { get; set; }
		public string[] Actors { get; set; }
		public double Rating { get; set; }
		public double MovieRating { get; set; }
		public IEnumerable<MovieCategories> Categories { get; set; }

		public MovieViewModel() {}

		public MovieViewModel(Movie movie)
		{
			Id = movie.Id;
			Title = movie.Title;
			Description = movie.Description;
			ImageUrl = movie.ImageUrl;
			Type = movie.Type;
			Duration = movie.Duration;
			Seasons = movie.Seasons;
			Episodes = movie.Episodes;
			ReleaseYearFormatted = movie.ReleaseYear.ToString();
			Actors = movie.Actors;
			Rating = movie.Rating;
			MovieRating = movie.MovieRating;
			Categories = movie.Categories;
		}
    }
}