using System.ComponentModel.DataAnnotations.Schema;

namespace Studentgram.Models
{
    public class Commentar
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string Username { get; set; }
        [ForeignKey("Username")]
        public Users User { get; set; }

        public int PhotoId {get; set;}
        public Photo Photos {  get; set; }
    }
}
