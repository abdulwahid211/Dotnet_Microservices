using Microsoft.AspNetCore.Mvc;
using PostService.Models;
using PostService.Repository;
using System.Transactions;

namespace PostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {

        private readonly IPostRepository _PostRepository;

        public PostController(IPostRepository PostRepository)
        {
            _PostRepository = PostRepository;
        }

        // GET: api/Post
        [HttpGet]
        public IActionResult Get()
        {
            var Posts = _PostRepository.GetPosts();
            return new OkObjectResult(Posts);
        }

        // GET: api/Post/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var Post = _PostRepository.GetPostByID(id);
            return new OkObjectResult(Post);
        }

        // POST: api/Post
        [HttpPost]
        public IActionResult Post([FromBody] Post Post)
        {
            using (var scope = new TransactionScope())
            {
                _PostRepository.AddPost(Post);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = Post.ID }, Post);
            }
        }

        // PUT: api/Post/5
        [HttpPut]
        public IActionResult Put([FromBody] Post Post)
        {
            if (Post != null)
            {
                using (var scope = new TransactionScope())
                {
                    _PostRepository.UpdatePost(Post);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _PostRepository.DeletePost(id);
            return new OkResult();
        }
    }
}
