using Microsoft.EntityFrameworkCore;
using PostService.Models;

namespace PostService.DBContext
{
    public class PostContext : DbContext
    {

        public DbSet<Post> Posts { get; set; }

        public DbSet<User> Users { get; set; }

        public PostContext(DbContextOptions<PostContext> options) : base(options) { }
    }
}
