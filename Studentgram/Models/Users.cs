using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studentgram.Models
{
    public class Users
    {
        
        public int Id { get; set; }
        [Key]
        public string Username { get; set; }
        public string HashPassword { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
        public ICollection<Commentar> Comments { get; set; } = null!;
    }
    
}