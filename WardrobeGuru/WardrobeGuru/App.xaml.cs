using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using WardrobeGuru.Core.DatabaseService;
using WardrobeGuru.Extensions;
using WardrobeGuru.Pages.Authentication.Login;
using WardrobeGuru.Pages.Authentication.ResetPassword;
using WardrobeGuru.Pages.Authentication.Signup;
using WardrobeGuru.Pages.Employees;
using WardrobeGuru.Pages.Settings;
using WardrobeGuru.Pages.Welcome;
using WardrobeGuru.Services;
using WardrobeGuru.Services.Network;
using WardrobeGuru.Utility;
using Xamarin.Forms;
using XCT.Popups.Prism;

namespace WardrobeGuru
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IProfileService, ProfileService>();
            containerRegistry.RegisterSingleton<IPopupService, PopupService>();

            containerRegistry.RegisterSingleton<INetworkService, NetworkService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<SignupPage, SignupPageViewModel>();
            containerRegistry.RegisterForNavigation<ResetPasswordPage, ResetPasswordPageViewModel>();
            containerRegistry.RegisterForNavigation<WelcomePage, WelcomePageViewModel>();

            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<EmployeesPage, EmployeesPageViewModel>();
            containerRegistry.RegisterForNavigation<EmployeeDetail, EmployeeDetailViewModel>();

            containerRegistry.Register<DatabaseServiceRemote>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            Settings.Theme = Settings.Theme;
            await NavigationService.NavigateTo<LoginPage>(true);
        }
    }
}
