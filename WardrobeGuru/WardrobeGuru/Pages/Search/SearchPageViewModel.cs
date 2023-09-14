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
                Name = "Classic Denim Jacket",
                Description = "A timeless classic, this denim jacket is perfect for layering and adding a touch of rugged style to any outfit.",
                Image = new ImageModel(ImagePath, FilePath),
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Cozy Knit Sweater",
                Description = "Stay warm and stylish with this cozy knit sweater. Its soft, textured fabric and relaxed fit make it a must-have for chilly days.",
                Image = new ImageModel(ImagePath, FilePath),
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Floral Maxi Dress",
                Description = "Embrace the beauty of nature with this elegant floral maxi dress. It features a flattering A-line silhouette and a vibrant floral print.",
                Image = new ImageModel(ImagePath, FilePath),
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Slim-Fit Tailored Suit",
                Description = "Elevate your formal attire with this slim-fit tailored suit. It exudes sophistication and is perfect for business meetings or special occasions.",
                Image = new ImageModel(ImagePath, FilePath),
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Vintage Graphic T-Shirt",
                Description = "Add a touch of nostalgia to your wardrobe with this vintage graphic t-shirt. Its retro design and soft cotton fabric make it a comfortable and stylish choice.",
                Image = new ImageModel(ImagePath, FilePath),
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Active Performance Leggings",
                Description = "Stay active and comfortable in these performance leggings. They're designed to wick away moisture and provide flexibility for your workouts.",
                Image = new ImageModel(ImagePath, FilePath),
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Leather Moto Jacket",
                Description = "Make a bold statement with this leather moto jacket. Its edgy design and high-quality leather will turn heads wherever you go.",
                Image = new ImageModel(ImagePath, FilePath),
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Striped Cotton Polo Shirt",
                Description = "Look effortlessly stylish in this striped cotton polo shirt. It's a versatile piece that pairs well with jeans or shorts.",
                Image = new ImageModel(ImagePath, FilePath),
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Bohemian Fringe Vest",
                Description = "Channel your inner bohemian spirit with this fringe vest. Its free-spirited design adds a touch of whimsy to your look.",
                Image = new ImageModel(ImagePath, FilePath),
                Status = "Available"
            },
            new ClothingItem
            {
                Name = "Cargo Shorts with Pockets",
                Description = "Stay practical and stylish with these cargo shorts. Multiple pockets provide ample storage while the relaxed fit ensures comfort for your outdoor adventures.",
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