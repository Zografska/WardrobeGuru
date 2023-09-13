using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using WardrobeGuru.Extensions;
using WardrobeGuru.Model;
using WardrobeGuru.Pages.ItemDetails;
using WardrobeGuru.Utility;
using Xamarin.Forms;

namespace WardrobeGuru.Pages.Search
{
    public class SearchPageViewModel
    {
        private readonly INavigationService _navigation;
        public ICommand SelectItemCommand { get; }

        public  const string ImagePath =
            "https://thumbs.dreamstime.com/b/tshirt-icon-vector-black-white-background-47049468.jpg";

        public const string FilePath = "";

        public ObservableCollection<ClothingItem> ClothingItems { get; set; } = new ObservableCollection<ClothingItem>
        {
            new ClothingItem
            {
                Name = "name1",
                Description = "test1",
                Image = new ImageModel(ImagePath, FilePath),
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "name2",
                Description = "test2",
                Image = new ImageModel(ImagePath, FilePath),
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "name3",
                Description = "test3",
                Image = new ImageModel(ImagePath, FilePath),
                Status = "Available"
            }
        };

        public SearchPageViewModel(INavigationService navigation)
        {
            _navigation = navigation;
            SelectItemCommand = new Command<ClothingItem>(NavigateToClothingItemDetails);
        }

        public SearchPageViewModel()
        {
            
        }
        
        private async void NavigateToClothingItemDetails(ClothingItem clothingItem)
        {
            await _navigation.NavigateTo<ItemDetailsPage>(new NavigationParameters
            {
                {
                    Constants.NavigationConstants.ClothingItem, clothingItem
                }
            });
            SingleClickCommand.ResetLastClick();
        }
        
    }
}