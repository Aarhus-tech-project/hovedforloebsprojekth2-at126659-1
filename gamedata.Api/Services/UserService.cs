using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
                new AppUser { Id = 1, CreatedAt = DateTime.UtcNow, Username = "JohnDoe", Avatar = null, Password = "password123" },
                new AppUser { Id = 2, CreatedAt = DateTime.UtcNow, Username = "JaneSmith", Avatar = null, Password = "password456" }
            };
        }
        public static List<AppUser> GetAll() => Users;
        public static AppUser? Get(int id) => Users.FirstOrDefault(u => u.Id == id);
        public static AppUser? Post(string username, int? avatar, string password)
        {
            var user = new AppUser { Id = Users.Count + 1, CreatedAt = DateTime.UtcNow, Username = username, Avatar = avatar, Password = password };
            Users.Add(user);
            return user;
        }
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