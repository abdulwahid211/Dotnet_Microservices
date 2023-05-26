using System.ComponentModel.DataAnnotations.Schema;

namespace PostService.Models
{
    [Table("Posts")]
    public class Post
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int userID { get; set; }
        public User User { get; set; }
    }
}
