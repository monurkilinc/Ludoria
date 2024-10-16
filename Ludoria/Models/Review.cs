using Ludoria.Models;
using System.ComponentModel.DataAnnotations;

namespace Ludoria.Entities
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [Range(1, 10)]
        public int Rating { get; set; }

        [Required]
        public string Author { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [Required]
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
