using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Model
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public Movie(int movieId, string name, string genre, int year)
        {
            MovieId = movieId;
            Name = name;
            Genre = genre;
            Year = year;
        }
    }
}