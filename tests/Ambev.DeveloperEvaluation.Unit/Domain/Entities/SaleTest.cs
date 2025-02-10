using System;
using System.Collections.Generic;
using System.Linq;
using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.UnitTests.Domain.Entities
{
    public class SaleTests
    {
        [Fact]
        public void TotalSaleAmountBeforeDiscount_ShouldCalculateCorrectly()
        {
            // Arrange
            var sale = new Sale
            {
                Items = new List<SaleItem>
                {
                    new SaleItem { Quantity = 2, Price = 10.0m },
                    new SaleItem { Quantity = 3, Price = 20.0m }
                }
            };

            // Act
            var totalAmountBeforeDiscount = sale.TotalSaleAmountBeforeDiscount;

            // Assert
            totalAmountBeforeDiscount.Should().Be(80.0m);
        }

        [Fact]
        public void TotalSaleAmountWithDiscount_ShouldCalculateCorrectly()
        {
            // Arrange
            var sale = new Sale
            {
                Items = new List<SaleItem>
                {
                    new SaleItem { Quantity = 2, Price = 10.0m, DiscountedPrice = 8.0m },
                    new SaleItem { Quantity = 3, Price = 20.0m, DiscountedPrice = 16.0m }
                }
            };

            // Act
            var totalAmountWithDiscount = sale.TotalSaleAmountWithDiscount;

            // Assert
            totalAmountWithDiscount.Should().Be(64.0m);
        }

        [Fact]
        public void ItemsCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var sale = new Sale
            {
                Items = new List<SaleItem>
                {
                    new SaleItem { Quantity = 2, Price = 10.0m },
                    new SaleItem { Quantity = 3, Price = 20.0m }
                }
            };

            // Act
            var itemsCount = sale.ItemsCount;

            // Assert
            itemsCount.Should().Be(2);
        }

        [Fact]
        public void Validate_ShouldReturnValidResult_WhenSaleIsValid()
        {
            // Arrange
            var sale = new Sale
            {
                Number = 12345,
                Date = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                CustomerName = "John Doe",
                CustomerEmail = "john.doe@example.com",
                BranchId = Guid.NewGuid(),
                BranchName = "Main Branch",
                Items = new List<SaleItem>
                {
                    new SaleItem { Id = Guid.NewGuid(), Quantity = 2, Price = 10.0m }
                }
            };

            // Act
            var validationResult = sale.Validate();

            // Assert
            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Validate_ShouldReturnInvalidResult_WhenSaleIsInvalid()
        {
            // Arrange
            var sale = new Sale
            {
                Number = 12345,
                Date = DateTime.UtcNow,
                CustomerId = Guid.Empty, // Invalid CustomerId
                CustomerName = string.Empty, // Invalid CustomerName
                CustomerEmail = "invalid-email", // Invalid CustomerEmail
                BranchId = Guid.Empty, // Invalid BranchId
                BranchName = string.Empty, // Invalid BranchName
                Items = new List<SaleItem>
                {
                    new SaleItem { Quantity = -1, Price = -10.0m } // Invalid SaleItem
                }
            };

            // Act
            var validationResult = sale.Validate();

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public void Cancel_ShouldSetIsCancelledToTrueAndUpdateUpdatedAt()
        {
            // Arrange
            var sale = new Sale();

            // Act
            sale.Cancel();

            // Assert
            sale.IsCancelled.Should().BeTrue();
            sale.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }
    }
}
