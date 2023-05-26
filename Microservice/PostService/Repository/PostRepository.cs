using Microsoft.EntityFrameworkCore;
using PostService.DBContext;
using PostService.Models;

namespace PostService.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly PostContext _dbContext;

        public PostRepository(PostContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeletePost(int PostId)
        {
            var Post = _dbContext.Posts.Find(PostId);
            _dbContext.Posts.Remove(Post);
            Save();
        }

        public Post GetPostByID(int PostId)
        {
            return _dbContext.Posts.Find(PostId);
        }

        public IEnumerable<Post> GetPosts()
        {
            return _dbContext.Posts.Include(x => x.User).ToList();
        }

        public void AddPost(Post Post)
        {
            _dbContext.Add(Post);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdatePost(Post Post)
        {
            _dbContext.Entry(Post).State = EntityState.Modified;
            Save();
        }
    }
}
