using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using UserService.Models;
using UserService.Repository;
namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepository;

        private readonly IPublishEndpoint _publishEndpoint;

        public UserController(IUserRepository userRepository, IPublishEndpoint publishEndpoint)
        {
            _userRepository = userRepository;
            _publishEndpoint = publishEndpoint;
        }

        // GET: api/User
        [HttpGet]
        public IActionResult Get()
        {
            var Users = _userRepository.GetUsers();
            return new OkObjectResult(Users);
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var User = _userRepository.GetUserByID(id);
            return new OkObjectResult(User);
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User User)
        {

            _userRepository.AddUser(User);
            await _publishEndpoint.Publish(User);
            //send the inserted product data to the queue and consumer will listening this data from queue
            return CreatedAtAction(nameof(Get), new { id = User.ID }, User);

        }

        // PUT: api/User/5
        [HttpPut]
        public IActionResult Put([FromBody] User User)
        {
            if (User != null)
            {
                using (var scope = new TransactionScope())
                {
                    _userRepository.UpdateUser(User);
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
            _userRepository.DeleteUser(id);
            return new OkResult();
        }
    }
}
