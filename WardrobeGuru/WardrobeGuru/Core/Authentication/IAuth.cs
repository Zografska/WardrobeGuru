using System.Threading.Tasks;

namespace WardrobeGuru.Core.Authentication
{
    public interface IAuthService
    {
        Task<string> LoginWithEmailPassword(string email, string password);
        Task<string> SignUpWithEmailPassword(string email, string password);
        bool Logout();
        string GetCurrentProfile();
        Task ResetPassword(string email);
    }
}