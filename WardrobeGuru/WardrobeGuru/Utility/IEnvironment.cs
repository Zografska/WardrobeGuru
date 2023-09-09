using System.Drawing;

namespace WardrobeGuru.Utility
{
    public interface IEnvironment
    {
        void SetStatusBarColor(Color color, bool darkStatusBarTint);
    }
}