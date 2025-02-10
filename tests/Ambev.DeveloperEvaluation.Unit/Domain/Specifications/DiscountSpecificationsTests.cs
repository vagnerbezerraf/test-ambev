using Ambev.DeveloperEvaluation.Domain.Specifications.Discount;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.UnitTests.Domain.Specifications.Discount
{
    public class DiscountSpecificationsTests
    {
        [Fact]
        public void NoDiscountSpecification_ShouldReturnTrue_WhenItemCountIsLessThan4()
        {
            // Arrange
            var specification = new NoDiscountSpecification();

            // Act
            var result = specification.IsSatisfiedBy(3);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void NoDiscountSpecification_ShouldReturnFalse_WhenItemCountIs4OrMore()
        {
            // Arrange
            var specification = new NoDiscountSpecification();

            // Act
            var result = specification.IsSatisfiedBy(4);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void NoDiscountSpecification_ShouldReturnZeroDiscountRate()
        {
            // Arrange
            var specification = new NoDiscountSpecification();

            // Act
            var discountRate = specification.GetDiscountRate();

            // Assert
            discountRate.Should().Be(0);
        }

        [Fact]
        public void FourItemsPlusDiscountSpecification_ShouldReturnTrue_WhenItemCountIsBetween4And9()
        {
            // Arrange
            var specification = new FourItemsPlusDiscountSpecification();

            // Act
            var result = specification.IsSatisfiedBy(5);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void FourItemsPlusDiscountSpecification_ShouldReturnFalse_WhenItemCountIsLessThan4OrMoreThan9()
        {
            // Arrange
            var specification = new FourItemsPlusDiscountSpecification();

            // Act
            var result1 = specification.IsSatisfiedBy(3);
            var result2 = specification.IsSatisfiedBy(10);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public void FourItemsPlusDiscountSpecification_ShouldReturnTenPercentDiscountRate()
        {
            // Arrange
            var specification = new FourItemsPlusDiscountSpecification();

            // Act
            var discountRate = specification.GetDiscountRate();

            // Assert
            discountRate.Should().Be(0.1m);
        }

        [Fact]
        public void TenToTwentyItemsDiscountSpecification_ShouldReturnTrue_WhenItemCountIsBetween10And20()
        {
            // Arrange
            var specification = new TenToTwentyItemsDiscountSpecification();

            // Act
            var result = specification.IsSatisfiedBy(15);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void TenToTwentyItemsDiscountSpecification_ShouldReturnFalse_WhenItemCountIsLessThan10OrMoreThan20()
        {
            // Arrange
            var specification = new TenToTwentyItemsDiscountSpecification();

            // Act
            var result1 = specification.IsSatisfiedBy(9);
            var result2 = specification.IsSatisfiedBy(21);

            // Assert
            result1.Should().BeFalse();
            result2.Should().BeFalse();
        }

        [Fact]
        public void TenToTwentyItemsDiscountSpecification_ShouldReturnTwentyPercentDiscountRate()
        {
            // Arrange
            var specification = new TenToTwentyItemsDiscountSpecification();

            // Act
            var discountRate = specification.GetDiscountRate();

            // Assert
            discountRate.Should().Be(0.2m);
        }
    }
}
