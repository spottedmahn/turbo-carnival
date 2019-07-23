using Microsoft.Extensions.Options;
using System;

namespace ModernConsoleAppTemplate.SampleService
{
    public class MyService : IMyService
    {
        private readonly MyOptions myOptions;

        public MyService(IOptions<MyOptions> optionsAccessor)
        {
            myOptions = optionsAccessor.Value;
        }

        public void DoWork()
        {
            Console.WriteLine($"Demo Custom Configuration: {myOptions.Demo}");
        }
    }
}
