namespace WardrobeGuru.Model
{
    public class ClothingItem : ModelBase
    {
        public string Name { get; set; }
        public string Description{ get; set; }
        public ImageModel Image { get; set; }
        public string Status { get; set; }
    }
}