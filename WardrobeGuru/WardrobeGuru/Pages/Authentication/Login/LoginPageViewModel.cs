using System;
using System.Threading.Tasks;
using System.Windows.Input;
// using System.Windows.Input;
// using Plugin.GoogleClient;
// using Plugin.GoogleClient.Shared;
using Prism.Navigation;
using WardrobeGuru.Core.Authentication;
using WardrobeGuru.Extensions;
using WardrobeGuru.Pages.Authentication.ResetPassword;
using WardrobeGuru.Pages.Authentication.Signup;
using WardrobeGuru.Pages.Base;
using WardrobeGuru.Pages.Search;
using WardrobeGuru.Services;
using WardrobeGuru.Services.Network;
using WardrobeGuru.Utility;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using XCT.Popups.Prism;

namespace WardrobeGuru.Pages.Authentication.Login
{
    public class LoginPageViewModel : PageViewModelBase
    {
        private readonly string SADMIN_EMAIL = "aleksandrazografska@halicea.com";
        private readonly string SADMIN_PASS = "-1380954559";
        // private readonly IGoogleClientManager _googleClientManager;
        private readonly IProfileService _profileService;
        private string _username { get; set; }
        private string _password { get; set; }
        private bool _usernameValid { get; set; }
        private bool _passwordValid { get; set; }

        public bool IsLoginPossible => _usernameValid && _passwordValid;
         public ICommand LoginCommand { get; set; }

        public bool PasswordValid
        {
            set
            {
                _passwordValid = value && !Username.IsNullOrEmpty();
                RaisePropertyChanged(nameof(IsLoginPossible));
            }
        }
        public bool UsernameValid
        {
            set
            {
                _usernameValid = value && !Password.IsNullOrEmpty();
                RaisePropertyChanged(nameof(IsLoginPossible));
            }
        }
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                RaisePropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        // public ICommand LoginWithGoogleCommand { get; }

        public LoginPageViewModel(INavigationService navigationService, IPopupService popupService,
            IAuthService authService, INetworkService networkService, IProfileService profileService)
            : base(navigationService, popupService, authService, networkService)
        { 
            LoginCommand = new SingleClickCommand(Login2);
            _profileService = profileService;

            IsBackButtonVisible = false;
            IsLogoutButtonVisible = false;
        }
        

        private async void LoginAsSadmin()
        {
            if (NetworkService.IsNetworkConnected())
            {
                var token = await AuthService.LoginWithEmailPassword(SADMIN_EMAIL, SADMIN_PASS);
                if (!token.IsNullOrEmpty())
                {
                    await NavigationService.NavigateTo<WelcomePage>();
                }
                else
                {
                    DisplayAlert(Constants.AlertConstants.LoginUnsuccessfulAlert);
                }
            }
            else
            {
                DisplayAlert(Constants.AlertConstants.NoInternet);
            }
            SingleClickCommand.ResetLastClick();
        }

        private async void NavigateToResetPassword()
        {
            await NavigationService.NavigateTo<ResetPasswordPage>();
        }

        private async void NavigateToSignup()
        {
            await NavigationService.NavigateTo<SignupPage>();
        }

        private async void Login2()
        {
            _profileService.CurrentUser = Username;
            await NavigationService.NavigateTo<SearchPage>();
        }
        
        private async void Login()
        {
            if (NetworkService.IsNetworkConnected())
            {
                var token = await AuthService.LoginWithEmailPassword(Username, Password);
                if (!token.IsNullOrEmpty())
                {
                    await NavigationService.NavigateTo<WelcomePage>();
                    await NavigationService.NavigateTo<SearchPage>();

                    ClearCredentials();
                }
                else
                {
                    DisplayAlert(Constants.AlertConstants.LoginUnsuccessfulAlert);
                }
            }
            else
            {
                DisplayAlert(Constants.AlertConstants.NoInternet);
            }
            SingleClickCommand.ResetLastClick();
        }

        private void ClearCredentials()
        {
            Username = Password = string.Empty;
        }
    }
}