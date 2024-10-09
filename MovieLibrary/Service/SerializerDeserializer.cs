using MovieLibrary.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Service
{
    public class SerializerDeserializer
    {
        private readonly string _filePath;

        public SerializerDeserializer()
        {
            _filePath = @"D:\monocept\tasks\MovieLibrary\movies.json";
        }

        //this method loads list of movies from a json file
        public List<Movie> LoadMovies()
        {
            //check if the movie file exists if not return empty list
            if (!File.Exists(_filePath))
            {
                return new List<Movie>();
            }

            //open file for reading
            using (StreamReader reader = new StreamReader(_filePath))
            {
                //read all text from the file into a string
                string json = reader.ReadToEnd();

                //convert json text back into list of movie objects
                return JsonConvert.DeserializeObject<List<Movie>>(json) ?? new List<Movie>();
            }
        }

        //this method saves list of movies to a json file
        public void SaveMovies(List<Movie> movies)
        {
            //convert list of movies into a json string making it pretty easy to read
            string json = JsonConvert.SerializeObject(movies, Newtonsoft.Json.Formatting.Indented);

            //open file for writing this will erase any existing content if we put false
            using (StreamWriter writer = new StreamWriter(_filePath))
            {
                writer.Write(json);
            }
        }


        //or  

        /*
        public List<Movie> LoadMovies()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Movie>();
            }

            string json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Movie>>(json);
        }

        public void SaveMovies(List<Movie> movies)
        {
            string json = JsonConvert.SerializeObject(movies, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
        */
    }
}