using System.Diagnostics.CodeAnalysis;

namespace WardrobeGuru.Services
{
    [ExcludeFromCodeCoverage]
    public class NetworkStatusMessage
    {
        public bool IsConnected { get; set; }

        public NetworkStatusMessage(bool isConnected)
        {
            IsConnected = isConnected;
        }
    }
}