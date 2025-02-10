namespace Ambev.DeveloperEvaluation.Domain.Specifications.Discount;

/// <summary>
/// Represents a discount specification.
/// </summary>
public interface IDiscountSpecification
{
    bool IsSatisfiedBy(int itemCount);
    decimal GetDiscountRate();
}
