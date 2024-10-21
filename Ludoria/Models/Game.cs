using Ludoria.Entities;
using System.ComponentModel.DataAnnotations;

namespace Ludoria.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }
        
        [Required]
        public string? Genre { get; set; }

        [Required]
        public string? Developer { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal Price { get; set; }

        public string? ImageURL { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int PlatformId { get; set; }
        public Platform? Platform { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
