using MovieLibrary.Exceptions;
using MovieLibrary.Model;
using MovieLibrary.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Controller
{
    public class MovieController
    {
        private readonly SerializerDeserializer _movieFileService;
        private const int MaxMovies = 5;

        public MovieController(SerializerDeserializer movieFileService)
        {
            _movieFileService = movieFileService;
        }

        public List<Movie> LoadMovies()
        {
            return _movieFileService.LoadMovies();
        }

        public void SaveMovies(List<Movie> movies)
        {
            _movieFileService.SaveMovies(movies);
        }

        public bool AddMovie(List<Movie> movies, Movie movie)
        {
            if (movies.Any(m => m.MovieId == movie.MovieId))
            {
                throw new MovieNotFoundException($"\nMovie with Id {movie.MovieId} already exists");
            }

            if (movies.Count >= MaxMovies)
            {
                throw new MovieStoreFullException("\nMovie store limit reached and cannot add more movies");
            }

            movies.Add(movie);
            SaveMovies(movies); 
            return true; 
        }

        public Movie FindMovieById(List<Movie> movies, int id)
        {
            var movie = movies.Find(m => m.MovieId == id);
            if (movie == null)
            {
                throw new MovieNotFoundException($"\nMovie with Id {id} not found");
            }
            return movie;
        }

        public Movie FindMovieByName(List<Movie> movies, string name)
        {
            var movie = movies.Find(m => m.Name.ToLower() == name.ToLower());
            if (movie == null)
            {
                throw new MovieNotFoundException($"\nMovie with name '{name}' not found");
            }
            return movie;
        }

        public bool RemoveMovieById(List<Movie> movies, int id)
        {
            var movie = FindMovieById(movies, id);
            movies.Remove(movie);
            SaveMovies(movies);
            return true;
        }

        public void ClearMovies(List<Movie> movies)
        {
            if (movies.Count == 0)
            {
                throw new MovieStoreEmptyException("\nNo movies to clear");
            }

            movies.Clear();
            SaveMovies(movies);
        }

        public void DisplayMovies(List<Movie> movies)
        {
            if (movies.Count == 0)
            {
                throw new MovieStoreEmptyException("\nCurrently movie store is empty");
            }
        }
        
        public bool HasMovieListFull(List<Movie> movies)
        {
            return movies.Count >= MaxMovies;
        }
    }
}