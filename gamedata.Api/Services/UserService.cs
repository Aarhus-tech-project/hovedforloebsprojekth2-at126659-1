using User.Models;

namespace User.Services
{
    public static class UserService
    {
        static List<AppUser> Users { get; }
        static UserService()
        {
            Users = new List<AppUser>
            {
                new AppUser { Id = 1, Username = "JohnDoe", Avatar = null, Email = null, Password = "password123" },
                new AppUser { Id = 2, Username = "JaneSmith", Avatar = null, Email = null, Password = "password456" }
            };
        }
        public static List<AppUser> GetAll() => Users;
        public static AppUser? Get(int id) => Users.FirstOrDefault(u => u.Id == id);
    }
    
    // public interface IUserService
    // {
    //     Task<IEnumerable<AppUser>> GetAllUsersAsync();
    //     Task<AppUser> GetUserByIdAsync(int id);
    //     Task<AppUser> CreateUserAsync(AppUser user);
    //     Task UpdateUserAsync(int id, AppUser user);
    //     Task DeleteUserAsync(int id);
    // }
}