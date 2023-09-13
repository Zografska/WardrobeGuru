using System;
using System.Collections.ObjectModel;
using System.IO;
using Prism.Mvvm;
using Prism.Navigation;
using WardrobeGuru.Model;
using WardrobeGuru.Services.Network;
using WardrobeGuru.Utility;
using Xamarin.Forms;
using XCT.Popups.Prism;

namespace WardrobeGuru.Pages.ItemDetails
{
    public class ItemDetailsPageViewModel : ViewModelBase
    {
        private ClothingItem _clothingItem;

        public ClothingItem ClothingItem
        {
            get => _clothingItem;
            set { SetProperty(ref _clothingItem, value); }
        }

        public ItemDetailsPageViewModel(INavigationService navigationService, IPopupService popupService,
            INetworkService networkService)
            : base(navigationService, popupService, networkService)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            ClothingItem clothingItem;
            parameters.TryGetValue(Constants.NavigationConstants.ClothingItem, out clothingItem);
            ClothingItem = clothingItem;
        }
    }
}