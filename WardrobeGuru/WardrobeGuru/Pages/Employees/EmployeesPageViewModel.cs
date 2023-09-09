using System.Windows.Input;
using Prism.Navigation;
using WardrobeGuru.Core.DatabaseService;
using WardrobeGuru.Extensions;
using WardrobeGuru.Model;
using WardrobeGuru.Services;
using WardrobeGuru.Services.Network;
using WardrobeGuru.Utility;
using Xamarin.Forms;
using XCT.Popups.Prism;

namespace WardrobeGuru.Pages.Employees
{
    public class EmployeesPageViewModel : ListViewModel<User>
    {
        public ICommand NavigateToEmployeeDetailsCommand { get; }
        protected EmployeesPageViewModel(INavigationService navigationService, IPopupService popupService, DatabaseServiceRemote databaseServiceRemote, INetworkService networkService, IProfileService profileService) : base(navigationService, popupService, databaseServiceRemote, networkService)
        {
            Title = XamlConstants.Employees;
            _service = profileService;
            NavigateToEmployeeDetailsCommand = new Command<User>(NavigateToEmployeeDetails);
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            if (NetworkService.IsNetworkConnected())
            {
                Items = await _databaseServiceRemote.GetAll<User>();
            }
            else
            {
                DisplayAlert(Constants.AlertConstants.NoInternet);
            }
        }

        private async void NavigateToEmployeeDetails(User employee)
        {
            await NavigationService.NavigateTo<EmployeeDetail>(new NavigationParameters
            {
                {
                    Constants.NavigationConstants.Employee, employee
                }
            });
            SingleClickCommand.ResetLastClick();
        }
    }
}