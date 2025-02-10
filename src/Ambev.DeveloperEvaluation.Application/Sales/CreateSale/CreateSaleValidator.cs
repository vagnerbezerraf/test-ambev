﻿using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for <see cref="CreateSaleCommand"> that defines validation rules for user creation command.
/// </summary>
public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleValidator"/> with defined validation rules.
    /// </summary>
    /// /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">SaleDate: Must not be in the future</list>
    /// <list type="bullet">CustomerId and BranchId: Should both be valid</list>
    /// <list type="bullet">CustomerName and BranchName: Must both be within 3 to 100 characters (inclusive)</list>
    /// <list type="bullet">Items: Must not be empty and each item must be validated by <see cref="SaleItemRequestValidator"/></list>
    /// </remarks>
    public CreateSaleValidator()
    {
        RuleFor(sale => sale.Date)
            .NotEmpty().WithMessage("Sale date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

        RuleFor(sale => sale.CustomerId)
            .Must(id => id != Guid.Empty).WithMessage("Sale must have a valid Customer ID");

        RuleFor(sale => sale.CustomerName)
            .NotEmpty().WithMessage("Customer name must not be empty.")
            .Length(3, 100).WithMessage("Customer name must be at least 3 characters long and cannot be longer than 100 characters.");

        RuleFor(sale => sale.CustomerEmail).SetValidator(new EmailValidator());

        RuleFor(sale => sale.BranchId)
            .Must(id => id != Guid.Empty).WithMessage("Sale must have a valid Branch ID");

        RuleFor(sale => sale.BranchName)
            .NotEmpty().WithMessage("Branch name must not be empty.")
            .Length(3, 100).WithMessage("Branch name must be at least 3 characters long and cannot be longer than 100 characters.");

        RuleFor(sale => sale.Items)
            .NotEmpty()
            .WithMessage("At least one sale item is required.")
            .ForEach(item => item.SetValidator(new CreateSaleItemCommandValidator()));
    }
}

/// <summary>
/// Validator for <see cref="CreateSaleItemCommand"/> that defines validation rules items whitin a sale.
/// </summary>
public class CreateSaleItemCommandValidator : AbstractValidator<CreateSaleItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleItemCommandValidator"> with defined validation rules.
    /// </summary>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">ProductId: Should be valid</list>
    /// <list type="bullet">ProductName: Must be within 3 to 100 characters (inclusive)</list>
    /// <list type="bullet">Quantity: Min 1 and max 20</list>
    /// <list type="bullet">UnitPrice: Must be greater than 0</list>
    /// </remarks>
    public CreateSaleItemCommandValidator()
    {
        RuleFor(item => item.ProductId)
            .Must(id => id != Guid.Empty).WithMessage("Sale item must have a valid Product ID");

        RuleFor(item => item.ProductName)
            .NotEmpty().WithMessage("Product name must not be empty.")
            .Length(3, 100).WithMessage("Product name must be at least 3 characters long and cannot be longer than 100 characters.");

        RuleFor(saleItem => saleItem.Quantity)
            .InclusiveBetween(1, 20).WithMessage("Quantity must be at least 1 and cannot be higher than 20.");

        RuleFor(saleItem => saleItem.UnitPrice)
            .GreaterThan(0).WithMessage("Item price must be higher than 0");
    }
}
