using System;
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.UnitTests.Domain.Entities
{
    public class SaleItemTests
    {
        [Fact]
        public void TotalAmountBeforeDiscount_ShouldCalculateCorrectly()
        {
            // Arrange
            var saleItem = new SaleItem
            {
                Quantity = 5,
                Price = 10.0m
            };

            // Act
            var totalAmountBeforeDiscount = saleItem.TotalAmountBeforeDiscount;

            // Assert
            totalAmountBeforeDiscount.Should().Be(50.0m);
        }

        [Fact]
        public void TotalAmountWithDiscount_ShouldCalculateCorrectly()
        {
            // Arrange
            var saleItem = new SaleItem
            {
                Quantity = 5,
                Price = 10.0m,
                DiscountedPrice = 8.0m
            };

            // Act
            var totalAmountWithDiscount = saleItem.TotalAmountWithDiscount;

            // Assert
            totalAmountWithDiscount.Should().Be(40.0m);
        }

        [Fact]
        public void ApplyDiscount_ShouldSetDiscountedPriceCorrectly()
        {
            // Arrange
            var saleItem = new SaleItem
            {
                Price = 10.0m
            };
            var discountRate = 0.2m;

            // Act
            saleItem.ApplyDiscount(discountRate);

            // Assert
            saleItem.DiscountedPrice.Should().Be(8.0m);
        }

        [Fact]
        public void Validate_ShouldReturnValidResult_WhenSaleItemIsValid()
        {
            // Arrange
            var saleItem = new SaleItem
            {
                SaleId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                ProductName = "Product A",
                Quantity = 5,
                Price = 10.0m,
                DiscountedPrice = 8.0m,
                Created = DateTime.UtcNow
            };

            // Act
            var validationResult = saleItem.Validate();

            // Assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Validate_ShouldReturnInvalidResult_WhenSaleItemIsInvalid()
        {
            // Arrange
            var saleItem = new SaleItem
            {
                SaleId = Guid.Empty, // Invalid SaleId
                ProductId = Guid.Empty, // Invalid ProductId
                ProductName = string.Empty, // Invalid ProductName
                Quantity = -1, // Invalid Quantity
                Price = -10.0m, // Invalid Price
                DiscountedPrice = -8.0m, // Invalid DiscountedPrice
                Created = DateTime.UtcNow
            };

            // Act
            var validationResult = saleItem.Validate();

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().NotBeEmpty();
        }
    }
}
