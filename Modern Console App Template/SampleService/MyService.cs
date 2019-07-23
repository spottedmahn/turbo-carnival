using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ModernConsoleAppTemplate.SampleService
{
    public class MyService : IMyService
    {
        private readonly MyOptions myOptions;
        private readonly ILogger<MyService> logger;

        public MyService(IOptions<MyOptions> optionsAccessor
            , ILogger<MyService> logger)
        {
            myOptions = optionsAccessor.Value;
            this.logger = logger;
        }

        public void DoWork()
        {
            logger.LogDebug("Starting to do work...");

            logger.LogInformation($"Demo Custom Configuration: {myOptions.Demo}");

            logger.LogDebug("Done doing work...");
        }
    }
}
