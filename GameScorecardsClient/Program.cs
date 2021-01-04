using Blazored.LocalStorage;
using GameScorecardsClient.Services.Authentication;
using GameScorecardsClient.Services.Games;
using GameScorecardsClient.Services.Storage;
using GameScorecardsClient.ViewModels.Authentication;
using GameScorecardsClient.ViewModels.Games;
using GameScorecardsClient.ViewModels.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvvmBlazor.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameScorecardsClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl")) });

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddMvvm();
            builder.Services.AddScoped<AuthenticationStateProvider, GameScorecardAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IGamesService, GamesService>();
            builder.Services.AddScoped<IStorageService, StorageService>();

            builder.Services.AddTransient<GamesViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddScoped<NavBarViewModel>();

            await builder.Build().RunAsync();
        }
    }
}
