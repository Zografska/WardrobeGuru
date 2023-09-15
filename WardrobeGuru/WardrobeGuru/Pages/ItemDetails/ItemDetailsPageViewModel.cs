using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Mvvm;
using Prism.Navigation;
using WardrobeGuru.Model;
using WardrobeGuru.Services.Network;
using WardrobeGuru.Utility;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
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
        
        private string _imageUrl;
        
        public ICommand AddPictureCommand { get; set; }
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                _imageUrl = value;
                RaisePropertyChanged(nameof(ImageUrl));
            }
        }

        
        private async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                ImageUrl = photo.FullPath;
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {photo.FullPath}");
            }
            catch (FeatureNotSupportedException)
            {
                DisplayAlert(Constants.FeatureConstants.FeatureNotImplemented);
            }
            catch (PermissionException)
            {
                DisplayAlert(Constants.FeatureConstants.PermissionsNotGranted);
            }
            catch (Exception ex)
            {
                DisplayAlert("Photo Upload unsuccessful");
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }
        
        public ItemDetailsPageViewModel(INavigationService navigationService, IPopupService popupService,
            INetworkService networkService)
            : base(navigationService, popupService, networkService)
        {
            AddPictureCommand = new AsyncCommand(TakePhotoAsync);
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