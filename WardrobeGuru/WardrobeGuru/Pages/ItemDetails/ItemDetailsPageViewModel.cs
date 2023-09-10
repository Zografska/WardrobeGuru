using System;
using Prism.Mvvm;
using Prism.Navigation;
using WardrobeGuru.Model;

namespace WardrobeGuru.Pages.ItemDetails
{
    public class ItemDetailsPageViewModel : BindableBase, IDisposable, IInitialize
    {
        private ClothingItem _clothingItem;
        public ClothingItem ClothingItem
        {
            get => _clothingItem;
            set { SetProperty(ref _clothingItem, value); }
        }
        public void Initialize(INavigationParameters parameters)
        {
            var clothingItem = parameters["clothingItem"];
            InitializeClothingItem((ClothingItem)clothingItem);
        }
        
        private void InitializeClothingItem(ClothingItem clothingItem)
        {
            ClothingItem.Name = clothingItem.Name;
            ClothingItem.Status = clothingItem.Status;
            ClothingItem.Description = clothingItem.Description;
            ClothingItem.Image = clothingItem.Image;
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}