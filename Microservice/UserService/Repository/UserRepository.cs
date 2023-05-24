using Microsoft.EntityFrameworkCore;
using UserService.DBContext;
using UserService.Models;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _dbContext;

        public UserRepository(UserContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteUser(int UserId)
        {
            var User = _dbContext.Users.Find(UserId);
            _dbContext.Users.Remove(User);
            Save();
        }

        public User GetUserByID(int UserId)
        {
            return _dbContext.Users.Find(UserId);
        }

        public IEnumerable<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        public void AddUser(User User)
        {
            _dbContext.Add(User);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateUser(User User)
        {
            _dbContext.Entry(User).State = EntityState.Modified;
            Save();
        }
    }
}
