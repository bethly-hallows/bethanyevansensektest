using BethanyEvansENSEKTest.Settings;
using BoDi;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace BethanyEvansENSEKTest.Hooks
{
    public class HookConfiguration
    {
        public void Configure(IObjectContainer objectContainer)
        {
            var configuration = new ConfigurationBuilder()
             .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
             .AddJsonFile("appsettings.json")
             .Build();

            var settings = new EnsekApiSettings();
            configuration.GetSection("EnsekApiSettings").Bind(settings);

            objectContainer.RegisterInstanceAs(settings);
        }
    }
}