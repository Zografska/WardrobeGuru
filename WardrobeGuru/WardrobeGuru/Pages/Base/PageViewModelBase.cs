using System;
using Prism.Navigation;
using WardrobeGuru.Core.Authentication;
using WardrobeGuru.Services.Network;
using WardrobeGuru.Utility;
using XCT.Popups.Prism;

namespace WardrobeGuru.Pages.Base
{
    public class PageViewModelBase : ViewModelBase
    {
        protected readonly IAuthService AuthService;
        public PageViewModelBase(INavigationService navigationService, IPopupService popupService,
            IAuthService authService, INetworkService networkService)
            : base(navigationService, popupService, networkService)
        {
            AuthService = authService;
        }
    }
}