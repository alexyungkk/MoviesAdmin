using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Movie
    {   
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        [Required]
        public string Synopsis { get; set; } = string.Empty;

        [StringLength(50)]
        [Required]
        public string Genre {  get; set; }  = string.Empty;

        [StringLength(20)]
        [Required]
        public string Rating {  get; set; } = string.Empty;

        [Required]
        public int Runtime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RelaseDate { get; set; } = DateTime.Today;
    }
}
