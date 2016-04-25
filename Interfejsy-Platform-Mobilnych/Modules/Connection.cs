using Windows.Networking.Connectivity;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    internal static class Connection
    {
        public static bool IsInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            return connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
        }
    }
}
