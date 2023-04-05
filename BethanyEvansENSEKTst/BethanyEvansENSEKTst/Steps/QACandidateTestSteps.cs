using BethanyEvansENSEKTest.Drivers;
using BethanyEvansENSEKTest.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BethanyEvansENSEKTest.Steps
{
    [Binding]

    public class QACandidateTestSteps
    {
        private readonly ApiDriver _apiDriver;
        private Energy _energyResponse;
        private Buy _buyResponse;
        private List<Orders> _ordersResponse;
        private string _orderId;

        public QACandidateTestSteps(ApiDriver apiDriver)
        {
            _apiDriver = apiDriver;
        }

        [Given(@"I call the Reset endpoint")]
        public async Task GivenICallTheResetEndpointAsync()
        {
            var resetRequest = await _apiDriver.PostResetRequestAsync();
            resetRequest.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Given(@"I call the Energy endpoint")]
        [When(@"I call the Energy endpoint")]
        public async Task WhenICallTheEnergyEndpointAsync()
        {
            var getEnergyRequest = await _apiDriver.GetEnergyAsync();

            var reader = await getEnergyRequest.Content.ReadAsStringAsync();

            _energyResponse = JsonConvert.DeserializeObject<Energy>(reader);

            getEnergyRequest.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Given(@"I call the Buy endpoint for each (.*) and (.*)")]
        public async Task GivenICallTheBuyEndpointForEachEnergyTypeAndAsync(int energyType, int quantity)
        {
            var buyRequest = await _apiDriver.PutBuyRequestAsync(energyType, quantity);

            var reader = await buyRequest.Content.ReadAsStringAsync();

            _buyResponse = JsonConvert.DeserializeObject<Buy>(reader);

            buyRequest.StatusCode.Should().Be(HttpStatusCode.OK);
            _buyResponse.Message.Should().StartWith($"You have purchased {quantity}");

            var split = _buyResponse.Message.Split("is");

            int index = split[1].LastIndexOf('.');
            split[1].Remove(index, 1);
            _orderId = split[1];
        }

        [When(@"I call the Orders endpoint")]
        public async Task WhenICallTheOrdersEndpointAsync()
        {
            var getOrdersRequest = await _apiDriver.GetOrdersAsync();

            var reader = await getOrdersRequest.Content.ReadAsStringAsync();

            _ordersResponse = JsonConvert.DeserializeObject<List<Orders>>(reader);

            getOrdersRequest.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"there is an order for each type of energy with quantity (.*)")]
        public void ThenThereIsAnOrderForEachTypeOfEnergyWithQuantity(int quantity)
        {
            var currentDateTime = DateTime.UtcNow;

            var recentOrders = _ordersResponse.Where(x => x.Id == _orderId);

            foreach (var order in recentOrders)
            {
                order.Quantity.Should().Be(quantity);
            }
        }

        [Then(@"no data is retrieved")]
        public void ThenNoDataIsRetrieved()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
