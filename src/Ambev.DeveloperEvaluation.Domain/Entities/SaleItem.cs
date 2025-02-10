﻿using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item that it is part of a <see cref="Sale"/>.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the sale ID.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the product ID.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product quantity.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the product unit price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the discount.
    /// </summary>
    public decimal DiscountedPrice { get; set; }

    /// <summary>
    /// Gets the date and time when the item was created.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Gets the total amount of the item.
    /// </summary>
    public decimal TotalAmountBeforeDiscount => Quantity * Price;

    /// <summary>
    /// Gets the total amount of the item with the discount applied.
    /// </summary>
    public decimal TotalAmountWithDiscount => Quantity * DiscountedPrice;

    /// <summary>
    /// Performs validation of the sale item entity using the SaleItemValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Sale data</list>
    /// <list type="bullet">Product data</list>
    /// <list type="bullet">Quantity and unit price validity</list>
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    public void ApplyDiscount(decimal discountRate)
    {
        DiscountedPrice = Price * (1 - discountRate);
    }
}