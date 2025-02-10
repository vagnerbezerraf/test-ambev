namespace Ambev.DeveloperEvaluation.Domain.Specifications.Discount;

/// <summary>
/// Discount specification for purchases with 10 to 20 identical items.
/// </summary>
public class TenToTwentyItemsDiscountSpecification : IDiscountSpecification
{
    public bool IsSatisfiedBy(int itemCount) => itemCount >= 10 && itemCount <= 20;

    public decimal GetDiscountRate() => 0.2m;
}
