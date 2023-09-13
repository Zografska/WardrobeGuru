namespace WardrobeGuru.Model
{
    public class ImageModel
    {
        public string ImagePath { get; set; }

        public string FileName { get; set; }

        public ImageModel(string imagePathV, string fileName)
        {
            ImagePath = imagePathV;
            FileName = fileName;
        }
    }
}