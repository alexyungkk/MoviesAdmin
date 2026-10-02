using Microsoft.AspNetCore.Mvc;
using MoviesAdmin.Models;
using System.Diagnostics;

namespace MoviesAdmin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AllMovies()
        {
            var movies = new List<Movie>()
            {
                new Movie { Id = 3, Title = "abc", Synopsis = "qwe", Genre = "zxcv",
                Rating = "A", Runtime = 123, RelaseDate = DateTime.Now}

            };

            var movie1 = new Movie();
            movie1.Id = 1;
            movie1.Title = "The Wolf of Wall Street";
            movie1.Synopsis = "In 1987, Jordan Belfort takes an entry-level job at a Wall Street brokerage firm.";
            movie1.Genre = "Comedy, Drama, Biography";
            movie1.Rating = "R";
            movie1.Runtime = 179;
            movie1.RelaseDate = DateTime.Now;

            var movie2 = new Movie();
            movie2.Id = 2;
            movie2.Title = "The Super Mario Bros. Movie";
            movie2.Synopsis = "Mario and Luigi go on a whirlwind adventure through Mushroom Kingdom, uniting with a cast of familiar characters to defeat Bowser.";
            movie2.Genre = "Kids & Family, Comedy, Adventure, Animation";
            movie2.Rating = "PG";
            movie2.Runtime = 92;
            movie2.RelaseDate = DateTime.Now; ;



            movies.Add(movie1);
            movies.Add(movie2);

            return View(movies);
        }
        
    }
}
