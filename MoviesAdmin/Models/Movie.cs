using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Movie
    {   
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Synopsis { get; set; } = string.Empty;

        [Required]
        public string Genre {  get; set; }  = string.Empty;

        [Required]
        public string Rating {  get; set; } = string.Empty;

        [Required]
        public int Runtime { get; set; }

        [Required]
        public DateTime RelaseDate { get; set; } = DateTime.Now;
    }
}
