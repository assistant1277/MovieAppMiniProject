using MovieLibrary.Controller;
using MovieLibrary.Exceptions;
using MovieLibrary.Model;
using MovieLibrary.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppApplication.Presentation
{
    internal class MoviePresentation
    {
        private static MovieController _movieController;

        public static void StartMovieApp()
        {
            Console.WriteLine("***** Movie App *****");
            var movieFileService = new SerializerDeserializer();
            _movieController = new MovieController(movieFileService);

            List<Movie> movies = _movieController.LoadMovies();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nMovie App Menu =>");
                Console.WriteLine("1) Add new movie");
                Console.WriteLine("2) Edit movie");
                Console.WriteLine("3) Find movie by Id");
                Console.WriteLine("4) Find movie by name");
                Console.WriteLine("5) Display all movies");
                Console.WriteLine("6) Remove movie by Id");
                Console.WriteLine("7) Clear all movies");
                Console.WriteLine("8) Exit");
                Console.Write("Enter your choice => ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewMovie(movies);
                        break;
                    case "2":
                        EditMovie(movies);
                        break;
                    case "3":
                        FindMovieById(movies);
                        break;
                    case "4":
                        FindMovieByName(movies);
                        break;
                    case "5":
                        DisplayMovies(movies);
                        break;
                    case "6":
                        RemoveMovieById(movies);
                        break;
                    case "7":
                        ClearAllMovies(movies);
                        break;
                    case "8":
                        exit = true;
                        Console.WriteLine("Exiting..");
                        break;
                    default:
                        Console.WriteLine("Invalid choice try again");
                        break;
                }
            }
        }

        public static void AddNewMovie(List<Movie> movies)
        {
            try
            {
                Console.Write("Enter movie id -> ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter movie name -> ");
                string name = Console.ReadLine();

                Console.Write("Enter genre means (action,drama,comedy,horror) -> ");
                string genre = Console.ReadLine();

                Console.Write("Enter year -> ");
                int year = Convert.ToInt32(Console.ReadLine());

                Movie newMovie = new Movie(id, name, genre, year);

                _movieController.AddMovie(movies, newMovie); 
                Console.Write("\nMovie added successfully\n");
            }
            catch (MovieStoreFullException ex)
            {
                Console.WriteLine(ex.Message); 
            }
            catch (MovieNotFoundException ex)
            {
                Console.WriteLine(ex.Message); 
            }
            catch (FormatException fe)
            {
                Console.WriteLine($"Invalid input format and enter numeric values for id and year {fe.Message}\n"); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }

        public static void EditMovie(List<Movie> movies)
        {
            try
            {
                Console.Write("Enter movie id to edit -> ");
                int id = Convert.ToInt32(Console.ReadLine());
                var movie = _movieController.FindMovieById(movies, id); 

                Console.Write("Enter new movie name -> ");
                string name = Console.ReadLine();

                Console.Write("Enter new genre means (action,drama,comedy,horror) -> ");
                string genre = Console.ReadLine();

                Console.Write("Enter new year -> ");
                int year = Convert.ToInt32(Console.ReadLine());

                movie.Name = name;
                movie.Genre = genre;
                movie.Year = year;

                _movieController.SaveMovies(movies); 
                Console.Write("\nMovie details updated successfully\n");
            }
            catch (MovieNotFoundException ex)
            {
                Console.WriteLine(ex.Message); 
            }
            catch (MovieStoreEmptyException ee)
            {
                Console.WriteLine(ee.Message); 
            }
            catch (FormatException fe)
            {
                Console.WriteLine($"Invalid input format and enter numeric values for id and year {fe.Message}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }

        public static void FindMovieById(List<Movie> movies)
        {
            try
            {
                Console.Write("Enter movie Id -> ");
                int id = Convert.ToInt32(Console.ReadLine());
                var movie = _movieController.FindMovieById(movies, id); 

                Console.Write($"Movie found -> {movie.Name}, Genre -> {movie.Genre}, Year -> {movie.Year}\n");
            }
            catch (MovieNotFoundException ex)
            {
                Console.WriteLine(ex.Message); 
            }
            catch (MovieStoreEmptyException ee)
            {
                Console.WriteLine(ee.Message); 
            }
            catch (FormatException fe)
            {
                Console.WriteLine($"Invalid input format and enter numeric values {fe.Message}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }

        public static void FindMovieByName(List<Movie> movies)
        {
            try
            {
                Console.Write("Enter movie name -> ");
                string name = Console.ReadLine();

                var movie = _movieController.FindMovieByName(movies, name); 
                Console.Write($"Movie found => Id -> {movie.MovieId}, Genre -> {movie.Genre}, Year -> {movie.Year}\n");
            }
            catch (MovieNotFoundException ex)
            {
                Console.WriteLine(ex.Message); 
            }
            catch (MovieStoreEmptyException ee)
            {
                Console.WriteLine(ee.Message); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }

        public static void DisplayMovies(List<Movie> movies)
        {
            try
            {
                _movieController.DisplayMovies(movies); 

                foreach (var movie in movies)
                {
                    Console.Write($"Id -> {movie.MovieId}, Name -> {movie.Name}, Genre -> {movie.Genre}, Year -> {movie.Year}\n");
                }
            }
            catch (MovieStoreEmptyException ex)
            {
                Console.WriteLine(ex.Message); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while displaying movies -> {ex.Message}\n");
            }
        }

        public static void RemoveMovieById(List<Movie> movies)
        {
            try
            {
                Console.Write("Enter movie Id to remove -> ");
                int id = Convert.ToInt32(Console.ReadLine());
                bool removed = _movieController.RemoveMovieById(movies, id); 

                Console.Write("\nMovie removed successfully\n");
            }
            catch (MovieNotFoundException ex)
            {
                Console.WriteLine(ex.Message); 
            }
            catch (MovieStoreEmptyException ee)
            {
                Console.WriteLine(ee.Message); 
            }
            catch (FormatException fe)
            {
                Console.WriteLine($"Invalid input format and enter a valid movie Id {fe.Message}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }

        public static void ClearAllMovies(List<Movie> movies)
        {
            try
            {
                _movieController.ClearMovies(movies); 
                Console.Write("\nAll movies cleared successfully\n");
            }
            catch (MovieStoreEmptyException ex)
            {
                Console.WriteLine(ex.Message); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }
    }
}
