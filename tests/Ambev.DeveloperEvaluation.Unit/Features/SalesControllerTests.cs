using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Ambev.DeveloperEvaluation.IntegrationTests.Features.Sales
{
    public class SalesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public SalesControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateSale_ShouldReturnCreatedSale()
        {
            // Arrange
            var request = new CreateSaleRequest
            {
                Date = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                CustomerName = "John Doe",
                CustomerEmail = "john.doe@example.com",
                BranchId = Guid.NewGuid(),
                BranchName = "Main Branch",
                Items = new List<CreateSaleItemRequest>
                {
                    new CreateSaleItemRequest { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 10.0m }
                }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/sales", request);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            var responseData = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();
            responseData.Should().NotBeNull();
            responseData!.Success.Should().BeTrue();
            responseData.Data.Should().NotBeNull();
            responseData.Data!.CustomerName.Should().Be(request.CustomerName);
        }
    }
}
