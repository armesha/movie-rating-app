using System;

namespace MovieRatingApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int ReviewCount
        {
            get { return Ratings.Count; }
            set { }
        }
        public List<int> Ratings { get; set; } = new List<int>();
        public double AverageRating
        {
            get { return Ratings.Any() ? Ratings.Average() : 0; }
        }
    }
}
