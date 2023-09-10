using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WardrobeGuru.Pages.Search
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        private SearchPageViewModel _viewModel = new SearchPageViewModel();
        
        public SearchPage()
        {
            InitializeComponent();
        }
        
        void OnTextChanged(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            ClothingItemsView.ItemsSource = _viewModel.ClothingItems;
            
            //ClothingItemsView.ItemsSource = GetSearchResults(searchBar.Text);
        }
    }
}