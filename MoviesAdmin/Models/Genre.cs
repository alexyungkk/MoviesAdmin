using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
    }
}
