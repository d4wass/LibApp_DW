using System;
using System.ComponentModel.DataAnnotations;


namespace LibApp.Models
{
    public class Book
    {
	    public int Id { get; set; }
	    [Required(ErrorMessage = "Name is required")]
	    [StringLength(255)]
	    public string Name { get; set; }
	    [Required(ErrorMessage = "AuthorName is required")]
	    public string AuthorName { get; set; }
	    [Required(ErrorMessage = "Genre is required")]
	    public Genre Genre { get; set; }
	    [Display(Name="Genre")]
	    public byte GenreId { get; set; }
	    [Required(ErrorMessage = "DateTime is required")]
	    public DateTime DateAdded { get; set; }
	    [Display(Name="Release Date")]
	    [Required(ErrorMessage = "ReleaseDate is required")]
	    public DateTime ReleaseDate { get; set; }
	    [Display(Name = "Number in Stock")]
	    [Required(ErrorMessage = "NumberInStock is required")]
	    [Range(1, 20, ErrorMessage = "NumberInStock must be between 1 to 20")]
	    public int NumberInStock { get; set; }
	    [Required(ErrorMessage = "NumberAvailable is required")]
	    public int NumberAvailable { get; set; }
	}
      
}
