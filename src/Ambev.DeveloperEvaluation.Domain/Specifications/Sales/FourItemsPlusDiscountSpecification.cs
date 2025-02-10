namespace Ambev.DeveloperEvaluation.Domain.Specifications.Discount;

/// <summary>
/// Discount specification for purchases with more than 4 identical items.
/// </summary>
public class FourItemsPlusDiscountSpecification : IDiscountSpecification
{
    public bool IsSatisfiedBy(int itemCount) => itemCount >= 4 && itemCount < 10;

    public decimal GetDiscountRate() => 0.1m;
}
