using BethanyEvansENSEKTest.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BethanyEvansENSEKTest.Steps
{
    [Binding]

    public class QACandidateTestSteps
    {
        private readonly ApiDriver _apiDriver;

        public QACandidateTestSteps(ApiDriver apiDriver)
        {
            _apiDriver = apiDriver;
        }

        [Given(@"I call the Reset endpoint")]
        public async Task GivenICallTheResetEndpointAsync()
        {
            await _apiDriver.PostResetRequestAsync();
        }

        [When(@"I call the Energy endpoint")]
        public void WhenICallTheEnergyEndpoint()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I call the Orders endpoint")]
        public void WhenICallTheOrdersEndpoint()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"no data is retrieved")]
        public void ThenNoDataIsRetrieved()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
