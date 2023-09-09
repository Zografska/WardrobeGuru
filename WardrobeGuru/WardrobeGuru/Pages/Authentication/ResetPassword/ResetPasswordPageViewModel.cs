using System.Windows.Input;
using Prism.Navigation;
using WardrobeGuru.Core.Authentication;
using WardrobeGuru.Extensions;
using WardrobeGuru.Pages.Base;
using WardrobeGuru.Services.Network;
using WardrobeGuru.Utility;
using XCT.Popups.Prism;

namespace WardrobeGuru.Pages.Authentication.ResetPassword
{
    public class ResetPasswordPageViewModel : PageViewModelBase
    {
        public ICommand ResetPasswordCommand { get; }
        public string Email { get; set; }
        public ResetPasswordPageViewModel(INavigationService navigationService, IPopupService popupService,
            IAuthService authService, INetworkService networkService)
            : base(navigationService, popupService, authService, networkService)
        {
            ResetPasswordCommand = new SingleClickCommand(ResetPassword);
            IsLogoutButtonVisible = false;
        }

        private async void ResetPassword()
        {
            if (Email.IsNullOrEmpty())
            {
                return;
            }

            if (NetworkService.IsNetworkConnected())
            {
                await AuthService.ResetPassword(Email);
                DisplayAlert(Constants.AlertConstants.ResetInstructionsSent);
            }
            else
            {
                DisplayAlert(Constants.AlertConstants.NoInternet);
            }
        }
    }
}