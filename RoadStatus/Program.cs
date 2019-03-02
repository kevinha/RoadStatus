using System;
using System.Net.Http;
using System.Threading.Tasks;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RoadStatus.DTO;
using RoadStatus.Services;
using static System.Console;
using static RoadStatus.AsyncHelper;

namespace RoadStatus
{
    class Program
    {
        public class Options
        {       
            [Value(0, Required = true, MetaName = "road id", HelpText = "Road Id.")]
            public string RoadId { get; set; }

            [Value(1, Required = true, MetaName = "app id", HelpText = "App Id.")]
            public string AppId { get; set; }

            [Value(2, Required = true, MetaName = "app key", HelpText = "App Key.")]
            public string AppKey { get; set; }
         
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }
        }
        
        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<Options>(args)
                .MapResult(RunAndReturnExitCode, _ => 1);

        }

        private static int RunAndReturnExitCode(Options opts)
        {              
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, opts);            
            var serviceProvider = serviceCollection.BuildServiceProvider();

            try
            {
                var result = RunSync(() =>
                {
                    var tflService = serviceProvider.GetService<ITflService>();
                    return tflService.GetRoadAsync(opts.RoadId);
                });
                DisplayResult(opts.RoadId, result);
                return result.Successful ? 0 : result.Error == Errors.InvalidId ? 2 : 3;
            }
            catch (Exception e)
            {
                WriteLine($"Unexpected error: {e}");
                return 4;
            }
        }

        private static void DisplayResult(string roadId, Result<TflApiPresentationEntitiesRoadCorridor> result)
        {
            if (result.Successful)
            {
                WriteLine($"The status of the {result.Value.DisplayName} is as follows");
                WriteLine($"\tRoad Status is {result.Value.StatusSeverity}");
                WriteLine($"\tRoad Status Description is {result.Value.StatusSeverityDescription}");               
            }
            else
            {
                if (result.Error == Errors.InvalidId)
                {
                    WriteLine($"{roadId} is not a valid road id");
                }
                else
                {
                    WriteLine($"Error getting results for {roadId}");                    
                }
            }
        }

        private static void ConfigureServices(ServiceCollection serviceCollection, Options opts)
        {
            // api url could be in config or on commandline if required for separate environments
            serviceCollection.AddSingleton<IApiConfig>(new ApiConfig("https://api.tfl.gov.uk",opts.AppId, opts.AppKey));
            serviceCollection.AddHttpClient<ITflService,TflService>()
                .ConfigurePrimaryHttpMessageHandler(handler =>
                new HttpClientHandler
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });
            
            if (opts.Verbose) 
            {
                serviceCollection.AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.AddDebug();
                });      
            }
        }
    }
}