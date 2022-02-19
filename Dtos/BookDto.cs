using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;

namespace LibApp.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public Genre Genre { get; set; }
        [Required]
        public byte GenreId { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [Range(1, 20)]
        public int NumberInStock { get; set; }
        [Required]
        public int NumberAvailable { get; set; }
    }
}
