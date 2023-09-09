using System;

namespace WardrobeGuru.Services.Network
{
    public interface INetworkService
    {
        bool IsNetworkConnected();
        IObservable<NetworkStatusMessage> OnNetworkStatusChanged { get; }
    }
}