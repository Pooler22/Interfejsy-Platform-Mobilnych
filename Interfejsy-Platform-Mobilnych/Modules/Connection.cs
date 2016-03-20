﻿using Windows.Networking.Connectivity;

namespace Interfejsy_Platform_Mobilnych.Modules
{
    class Connection
    {
        public static bool IsInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            return connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
        }
    }
}
