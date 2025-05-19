using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studentgram.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Image { get; set; }

        public string Title { get; set; }
        public string? Caption { get; set; }
        public int Like {  get; set; } = 0;
        public HashSet<string> LikedUsers { get; set; } = new HashSet<string>();
        [Required]
        public string Username { get; set; } = null!; 

        [ForeignKey("Username")]
        public virtual Users User { get; set; } = null!;
        public virtual ICollection<Commentar> Comments { get; set; } = null!;
    
    }
}
