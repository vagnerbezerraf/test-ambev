namespace Ambev.DeveloperEvaluation.Domain.Specifications.Discount;

/// <summary>
/// Represents a discount specification that does not apply any discount.
/// </summary>
public class NoDiscountSpecification : IDiscountSpecification
{
    public bool IsSatisfiedBy(int itemCount) => itemCount < 4;

    public decimal GetDiscountRate() => 0;
}
