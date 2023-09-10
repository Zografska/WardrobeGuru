using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using WardrobeGuru.Model;
using Xamarin.Forms;

namespace WardrobeGuru.Pages.Search
{
    public class SearchPageViewModel
    {
        private readonly INavigationService _navigation;
        public ICommand SelectItemCommand { get; set; }

        public ObservableCollection<ClothingItem> ClothingItems { get; set; } = new ObservableCollection<ClothingItem>
        {
            new ClothingItem
            {
                Name = "name1",
                Description = "test1",
                Image = "image1"
            },
            new ClothingItem
            {
                Name = "name2",
                Description = "test2",
                Image = "image2"
            },
            new ClothingItem
            {
                Name = "name3",
                Description = "test3",
                Image = "image3"
            }
        };

        public SearchPageViewModel(INavigationService navigation)
        {
            _navigation = navigation;
            SelectItemCommand = new Command(async (x) => await NavigateToItem(x));
        }

        public SearchPageViewModel()
        {
            
        }

        private async Task NavigateToItem(object obj)
        {
            var card = obj as ClothingItem;

            var parameter = new NavigationParameters();
            parameter.Add("card", card);
            await _navigation.NavigateAsync(new Uri("ItemDetailsPage", UriKind.Relative), parameter);
        }
        
    }
}