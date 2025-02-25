﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParkingSystem.State.Authenticators;
using ParkingSystem.State.Navigators;
using State.Accounts;

namespace ParkingSystem.HostBuilders
{
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<IAuthenticator, Authenticator>();
                services.AddSingleton<IAccountStore, AccountStore>();
            });

            return host;
        }
    }
}
