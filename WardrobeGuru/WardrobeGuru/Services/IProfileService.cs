using System.Collections.Generic;
using System.Threading.Tasks;
using WardrobeGuru.Model;

namespace WardrobeGuru.Services
{
    public interface IProfileService : IServiceBase<User>
    {
        string CurrentUser { get; set; }
        Task<User> GetCurrentUser();
        Task<User> CreateUser(string userId, string name, string surname, string email, string photoUrl);
        Task<bool> IsUserExistent(string email);
        string GetGoogleUserPassword(string googleUserEmail);
    }
}