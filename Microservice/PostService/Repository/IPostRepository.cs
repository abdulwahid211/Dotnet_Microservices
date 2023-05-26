using PostService.Models;

namespace PostService.Repository
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPosts();
        Post GetPostByID(int Post);
        void AddPost(Post Post);
        void DeletePost(int PostId);
        void UpdatePost(Post Post);
        void Save();
    }
}
