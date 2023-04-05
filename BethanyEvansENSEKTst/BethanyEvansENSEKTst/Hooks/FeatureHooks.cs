using TechTalk.SpecFlow;

namespace BethanyEvansENSEKTest.Hooks
{
    [Binding]
    public class FeatureHooks
    {
        [BeforeFeature]
        public static void ConfigureDependencies(FeatureContext featureContext)
        {
            var hookConfig = new HookConfiguration();

            hookConfig.Configure(featureContext.FeatureContainer);
        }
   
    }
}
