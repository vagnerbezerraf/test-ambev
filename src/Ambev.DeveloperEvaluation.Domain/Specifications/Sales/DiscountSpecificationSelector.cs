using System.Collections.Generic;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Domain.Specifications.Discount
{
    public static class DiscountSpecificationSelector
    {
        private static readonly List<IDiscountSpecification> Specifications = new()
        {
            new NoDiscountSpecification(),
            new FourItemsPlusDiscountSpecification(),
            new TenToTwentyItemsDiscountSpecification()
        };

        public static IDiscountSpecification GetDiscountSpecification(int itemCount)
        {
            return Specifications.First(spec => spec.IsSatisfiedBy(itemCount));
        }
    }
}
