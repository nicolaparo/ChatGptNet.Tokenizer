using ChatGptNet.Tokenizer;
using System.Text.Json;
using System.Text.Json.Serialization;
using ChatGptNet;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ChatGptTokenizerConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices(ConfigureServices)
                .Build();

            var application = host.Services.GetRequiredService<Application>();
            await application.ExecuteAsync();

        }

        static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddSingleton<Application>();

            services.AddChatGpt(options =>
            {
                options.ApiKey = "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";
                options.MessageLimit = 16;
                options.MessageExpiration = TimeSpan.FromMinutes(5);
            });
        }
    }
}