using UserService.Models;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserByID(int User);
        void AddUser(User User);
        void DeleteUser(int UserId);
        void UpdateUser(User User);
        void Save();
    }
}
