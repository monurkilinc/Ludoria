using Ludoria.Models;
using System.ComponentModel.DataAnnotations;

namespace Ludoria.Entities
{
    public class Platform
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Game>? Games { get; set; }
    }
}
