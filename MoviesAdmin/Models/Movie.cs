using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Movie
    {   
        public int Id { get; set; }

        //foreign key
        
        public int GenreId { get; set; }

        
        public int AudienceRatingId { get; set; }

        [ValidateNever]
        [Display(Name = "Genre")]
        public Genre Genre { get; set; } = null!;

        [ValidateNever]
        [Display(Name = "Audience Rating")]
        public AudienceRating AudienceRating { get; set; } = null!;

        [StringLength(100)]
        [Required]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        [Required]
        public string Synopsis { get; set; } = string.Empty;

        //[StringLength(50)]
        //[Required]
        //public string Genre {  get; set; }  = string.Empty;

        [StringLength(20)]
        [Required]
        public string Rating {  get; set; } = string.Empty;

        [Required]
        public int Runtime { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RelaseDate { get; set; } = DateTime.Today;
    }
}
