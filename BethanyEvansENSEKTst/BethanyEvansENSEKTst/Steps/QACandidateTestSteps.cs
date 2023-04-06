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
        private Buy _buyResponse;
        private LoginResponse _loginResponse;
        private OrderResponse _orderResponse;
        private List<Orders> _ordersResponse;
        private string _orderId;

        public QACandidateTestSteps(ApiDriver apiDriver)
        {
            _apiDriver = apiDriver;
        }

        [Given(@"I call the Login endpoint")]
        public async Task GivenICallTheLoginEndpointAsync()
        {
            var loginRequest = new Login()
            {
                Username = "test",
                Password = "testing"
            };

            var loginResponse = await _apiDriver.PostLoginRequestAsync(loginRequest);

            var reader = await loginResponse.Content.ReadAsStringAsync();

            _loginResponse = JsonConvert.DeserializeObject<LoginResponse>(reader);

            loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Given(@"I call the Reset endpoint")]
        public async Task GivenICallTheResetEndpointAsync()
        {
            _apiDriver.AddAuthToken(_loginResponse.AccessToken);

            var resetRequest = await _apiDriver.PostResetRequestAsync();

            resetRequest.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Given(@"I call the Buy endpoint for energy type (.*) and quantity (.*)")]
        public async Task GivenICallTheBuyEndpointForEachEnergyTypeAndAsync(int energyType, int quantity)
        {
            var buyRequest = await _apiDriver.PutBuyRequestAsync(energyType, quantity);

            var reader = await buyRequest.Content.ReadAsStringAsync();

            _buyResponse = JsonConvert.DeserializeObject<Buy>(reader);

            buyRequest.StatusCode.Should().Be(HttpStatusCode.OK);

            _buyResponse.Message.Should().StartWith($"You have purchased {quantity}");

            var split = _buyResponse.Message.Split("is");

            char[] charsToTrim = { '.' };
            string cleanString = split[1].Trim(charsToTrim).Trim();
            _orderId = cleanString;
        }

        [Given(@"I call the Orders endpoint")]
        [When(@"I call the Orders endpoint")]
        public async Task WhenICallTheOrdersEndpointAsync()
        {
            var getOrdersRequest = await _apiDriver.GetOrdersAsync();

            var reader = await getOrdersRequest.Content.ReadAsStringAsync();

            _ordersResponse = JsonConvert.DeserializeObject<List<Orders>>(reader);

            getOrdersRequest.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Given(@"there is an order for each type of energy with quantity (.*) and energy type (.*)")]
        [Then(@"there is an order for each type of energy with quantity (.*) and energy type (.*)")]
        public void ThenThereIsAnOrderForEachTypeOfEnergyWithQuantityAndEnergy(int quantity, string energyType)
        {
            var recentOrders = _ordersResponse.Where(x => x.Id == _orderId);

            foreach (var order in recentOrders)
            {
                order.Quantity.Should().Be(quantity);
                order.Fuel.Should().Be(energyType);
            }
        }

        [Then(@"no Order data is retrieved for today")]
        public void ThenNoOrderDataIsRetrievedForToday()
        {
            var todaysOrders = _ordersResponse.Where(x => x.Time.Date == DateTime.Today);

            todaysOrders.Should().HaveCount(0);
        }

        [Then(@"there are (.*) orders placed before today")]
        public void ThenThereAreOrdersPlacedBeforeToday(int orderQuantity)
        {
            var previousOrders = _ordersResponse.Where(x => x.Time.Date != DateTime.Today);

            previousOrders.Should().HaveCount(orderQuantity);
        }

        [When(@"I call the Delete Orders endpoint")]
        public async Task WhenICallTheDeleteOrdersEndpointAsync()
        {
            var deleteOrders = await _apiDriver.DeleteOrderAsync(_orderId);

            deleteOrders.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"the order is deleted from the Orders")]
        public void ThenTheOrderIsDeletedFromTheOrders()
        {
            var recentOrders = _ordersResponse.Where(x => x.Id == _orderId);

            recentOrders.Should().HaveCount(0);
        }

        [When(@"I call the Get Order endpoint")]
        public async Task WhenICallTheGetOrderEndpointAsync()
        {
            var getOrder = await _apiDriver.GetOrderAsync(_orderId);

            var reader = await getOrder.Content.ReadAsStringAsync();

            _orderResponse = JsonConvert.DeserializeObject<OrderResponse>(reader);

            getOrder.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"the correct order is returned")]
        public void ThenTheCorrectOrderIsReturned()
        {
            var recentOrders = _ordersResponse.Where(x => x.Id == _orderId);

            recentOrders.Should().HaveCount(1);

            var energyId = "";
            var quantity = 0;

            foreach (var order in recentOrders)
            {
                energyId = order.Fuel;
                quantity = order.Quantity;
            }

            switch (energyId)
            {
                case "Gas":
                    _orderResponse.Energy_Id.Should().Be(1);
                    break;
                case "Nuclear":
                    _orderResponse.Energy_Id.Should().Be(2);
                    break;
                case "Electric":
                    _orderResponse.Energy_Id.Should().Be(3);
                    break;
                case "Oil":
                    _orderResponse.Energy_Id.Should().Be(4);
                    break;
            }

            _orderResponse.Quantity.Should().Be(quantity);
        }
    }
}
