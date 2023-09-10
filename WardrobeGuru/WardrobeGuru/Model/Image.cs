namespace WardrobeGuru.Model
{
    public class Image
    {
        public string ImagePath { get; set; }

        public string FileName { get; set; }

        public Image(string imagePathV, string fileName)
        {
            ImagePath = imagePathV;
            FileName = fileName;
        }
    }
}