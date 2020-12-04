using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Caf.Midden.Components.Common;
using Caf.Midden.Wasm.Services;

namespace Caf.Midden.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            builder.Services.AddScoped<StateContainer>();
            builder.Services.AddScoped<IUpdateMetadata>(x =>
                x.GetRequiredService<StateContainer>());
            builder.Services.AddScoped<IUpdateMessage>(x =>
                x.GetRequiredService<StateContainer>());

            await builder.Build().RunAsync();
        }
    }
}