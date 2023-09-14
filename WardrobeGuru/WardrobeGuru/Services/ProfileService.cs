using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WardrobeGuru.Core.Authentication;
using WardrobeGuru.Core.DatabaseService;
using WardrobeGuru.Model;
using WardrobeGuru.Services.Network;

namespace WardrobeGuru.Services
{
    public class ProfileService : BaseCrudService<User>, IProfileService
    {
        public string CurrentUser { get; set; }
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>
        {
            new User
            {
                Name = "John",
                Surname = "Smith",
                Email = "john.smith@gmail.com",
                Uid= "uid12345",
                FullName= "John Smith",
                PhotoUrl = ""
            },
            new User  {
                Name = "Emily",
                Surname = "Johnson",
                Email = "emily.johnson@gmail.com",
                Uid = "uid54321",
                PhotoUrl = ""
            },
            new User  {
                Name = "David",
                Surname = "Brown",
                Email = "david.brown@gmail.com",
                Uid = "uid67890",
                PhotoUrl = ""
            },
            new User  {
                Name = "Sarah",
                Surname = "Davis",
                Email = "sarah.davis@gmail.com",
                Uid = "uid24680",
                PhotoUrl = ""
            },
            new User {
                Name = "Michael",
                Surname = "Wilson",
                Email = "michael.wilson@gmail.com",
                Uid = "uid13579",
                PhotoUrl = ""
            }, 
            new User  {
                Name = "Linda",
                Surname = "Johnson",
                Email = "linda.johnson@gmail.com",
                Uid = "uid98765",
                PhotoUrl = ""
            }, new User  {
                Name = "Robert",
                Surname = "Clark",
                Email = "robert.clark@gmail.com",
                Uid = "uid11223",
                PhotoUrl = ""
            },
            new User {
                Name = "Susan",
                Surname = "Martinez",
                Email = "susan.martinez@gmail.com",
                Uid = "uid33445",
                PhotoUrl = ""
            }, 
            new User{
                Name = "James",
                Surname = "White",
                Email = "james.white@gmail.com",
                Uid = "uid55667",
                PhotoUrl = ""
            }, 
            new User {
                Name = "Karen",
                Surname = "Lee",
                Email = "karen.lee@gmail.com",
                Uid = "uid77889",
                PhotoUrl = ""
            }
        };
        public ProfileService(DatabaseServiceRemote databaseServiceRemote, IAuthService authService,
            INetworkService networkService) : base(databaseServiceRemote, authService, networkService)
        { }

        public async Task<User> GetCurrentUser()
        {
            return Users.FirstOrDefault(user => user.Email == CurrentUser);
        }
        

        public async Task<User> CreateUser(string userId, string name, string surname, string email, string photoUrl)
        {
            var newUser = new User
            {
                Uid = userId,
                Name = name,
                Surname = surname,
                Email = email,
                FullName = $"{name} {surname}",
                JobTitle = "Worker",
                PhotoUrl = photoUrl
            };

            return await Save(newUser);
        }

        public async Task<bool> IsUserExistent(string email)
        {
            var users = await GetAll();
            return users.FirstOrDefault(user => user.Email == email) != default;
        }

        // TODO: find a smarter way to create a password for the google user
        public string GetGoogleUserPassword(string googleUserEmail)
        {
            return googleUserEmail.GetHashCode().ToString();
        }
    }
}