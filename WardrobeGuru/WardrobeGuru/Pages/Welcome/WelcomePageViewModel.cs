using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Windows.Input;
using Newtonsoft.Json;
using Prism.Navigation;
using WardrobeGuru.Core.Authentication;
using WardrobeGuru.Extensions;
using WardrobeGuru.Model;
using WardrobeGuru.Pages.Base;
using WardrobeGuru.Pages.Employees;
using WardrobeGuru.Pages.Settings;
using WardrobeGuru.Services;
using WardrobeGuru.Services.Network;
using WardrobeGuru.Utility;
using Xamarin.Essentials;
using XCT.Popups.Prism;

namespace WardrobeGuru.Pages.Welcome
{
    public class WelcomePageViewModel : PageViewModelBase
    {
        private readonly IProfileService _profileService;

        private string _username;
        
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                RaisePropertyChanged(nameof(Username));
            }
        }
        
        public ObservableCollection<ClothingItem> ClothingItems1 { get; set; } = new ObservableCollection<ClothingItem>
        {
            new ClothingItem
            {
                Name = "Classic Denim Jacket",
                Description = "A timeless classic, this denim jacket is perfect for layering and adding a touch of rugged style to any outfit.",
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Cozy Knit Sweater",
                Description = "Stay warm and stylish with this cozy knit sweater. Its soft, textured fabric and relaxed fit make it a must-have for chilly days.",
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Floral Maxi Dress",
                Description = "Embrace the beauty of nature with this elegant floral maxi dress. It features a flattering A-line silhouette and a vibrant floral print.",
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Slim-Fit Tailored Suit",
                Description = "Elevate your formal attire with this slim-fit tailored suit. It exudes sophistication and is perfect for business meetings or special occasions.",
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Vintage Graphic T-Shirt",
                Description = "Add a touch of nostalgia to your wardrobe with this vintage graphic t-shirt. Its retro design and soft cotton fabric make it a comfortable and stylish choice.",
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Active Performance Leggings",
                Description = "Stay active and comfortable in these performance leggings. They're designed to wick away moisture and provide flexibility for your workouts.",
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Leather Moto Jacket",
                Description = "Make a bold statement with this leather moto jacket. Its edgy design and high-quality leather will turn heads wherever you go.",
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Striped Cotton Polo Shirt",
                Description = "Look effortlessly stylish in this striped cotton polo shirt. It's a versatile piece that pairs well with jeans or shorts.",
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Bohemian Fringe Vest",
                Description = "Channel your inner bohemian spirit with this fringe vest. Its free-spirited design adds a touch of whimsy to your look.",
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Cargo Shorts with Pockets",
                Description = "Stay practical and stylish with these cargo shorts. Multiple pockets provide ample storage while the relaxed fit ensures comfort for your outdoor adventures.",
                Status = "Available"
            }
        };  
        
        public ObservableCollection<ClothingItem> ClothingItems2 { get; set; } = new ObservableCollection<ClothingItem>
        {
            new ClothingItem
            {
                Name = "Vintage Graphic T-Shirt",
                Description = "Add a touch of nostalgia to your wardrobe with this vintage graphic t-shirt. Its retro design and soft cotton fabric make it a comfortable and stylish choice.",
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Active Performance Leggings",
                Description = "Stay active and comfortable in these performance leggings. They're designed to wick away moisture and provide flexibility for your workouts.",
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Leather Moto Jacket",
                Description = "Make a bold statement with this leather moto jacket. Its edgy design and high-quality leather will turn heads wherever you go.",
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Striped Cotton Polo Shirt",
                Description = "Look effortlessly stylish in this striped cotton polo shirt. It's a versatile piece that pairs well with jeans or shorts.",
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Bohemian Fringe Vest",
                Description = "Channel your inner bohemian spirit with this fringe vest. Its free-spirited design adds a touch of whimsy to your look.",
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Cargo Shorts with Pockets",
                Description = "Stay practical and stylish with these cargo shorts. Multiple pockets provide ample storage while the relaxed fit ensures comfort for your outdoor adventures.",
                Status = "Available"
            }
        };
        
        private string _temperature;

        public string Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                RaisePropertyChanged(nameof(Temperature));
            }
        }
        
        public ICommand NavigateToEmployeesCommand { get; }
        public ICommand NavigateToSettingsCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand MapClickCommand { get; }

        public WelcomePageViewModel(INavigationService navigationService, IPopupService popupService,
            IAuthService authService, IProfileService profileService, INetworkService networkService)
            : base(navigationService, popupService, authService, networkService)
        {
            NavigateToSettingsCommand = new SingleClickCommand(NavigateToSettingsPage);
            NavigateToEmployeesCommand = new SingleClickCommand(NavigateToEmployeesPage);
            MapClickCommand = new SingleClickCommand(NavigateToRestaurant);
            LogoutCommand = new SingleClickCommand(Logout);
            _profileService = profileService;
            IsBackButtonVisible = false;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            Username = (await _profileService.GetCurrentUser()).FullName;
            var location = await Geolocation.GetLocationAsync();
            var url = "https://api.open-meteo.com/v1/forecast?latitude=" + location.Latitude.ToString("0.0", CultureInfo.InvariantCulture) + "&longitude=" +
                      location.Longitude.ToString("0.0", CultureInfo.InvariantCulture) + "&current_weather=true";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            var httpClient = new HttpClient();
            try
            {
                var message = await httpClient.SendAsync(httpRequest);
                var result = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
                var o = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);
                var temp = o.Last.First;
                var temperature = temp.Value<int>("temperature");
                Temperature = (temperature > 20 ? "☀️" : "❄️") + temperature + "°C";
            }catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Logout()
        {
            if (NetworkService.IsNetworkConnected())
            {
                var successful = AuthService.Logout();
                if (successful)
                {
                    NavigationService.GoBackToRootAsync();
                }
                else
                {
                    DisplayAlert(Constants.AlertConstants.LogoutUnsuccessful);
                }
            }
            else
            {
                DisplayAlert(Constants.AlertConstants.NoInternet);
            }
            SingleClickCommand.ResetLastClick();
        }

        private async void NavigateToSettingsPage()
        {
            await NavigationService.NavigateTo<SettingsPage>();
            SingleClickCommand.ResetLastClick();
        }

        private async void NavigateToRestaurant()
        {
            var location = new Location(42.00417398712801, 21.409539851372777);
            var options = new MapLaunchOptions { Name = "FCSE Building" };

            try
            {
                await Map.OpenAsync(location, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private async void NavigateToEmployeesPage()
        {
            await NavigationService.NavigateTo<EmployeesPage>();
            SingleClickCommand.ResetLastClick();
        }
    }
}