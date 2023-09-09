using System;
using System.Windows.Input;
using Prism.Navigation;
using WardrobeGuru.Core.Authentication;
using WardrobeGuru.Extensions;
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