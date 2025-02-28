using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.WebApi;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
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
            Random random = new Random();
            int randomNumber = random.Next(100, 1000);
            var requestUser = new CreateUserRequest()
            {
                Email = $"test{randomNumber}@example.com",
                Username = "John Doe",
                Password = "aA{123456}@",
                Phone = "+5511999999999",
                Status = Domain.Enums.UserStatus.Active,
                Role = Domain.Enums.UserRole.Customer
            };            

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
                    new CreateSaleItemRequest { 
                        ProductId = Guid.NewGuid(), 
                        Quantity = 5, 
                        UnitPrice = 10.0m,
                        ProductName = "Product A"
                        
                    }
                }
            };

            // Act
            var responseUser = await _client.PostAsJsonAsync("/api/users", requestUser);
            var responseUserData = await responseUser.Content.ReadFromJsonAsync<ApiResponseWithData<CreateUserResponse>>();
            request.CustomerId = responseUserData.Data.Id;

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
