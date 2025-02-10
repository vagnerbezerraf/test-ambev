﻿using Ambev.DeveloperEvaluation.Domain.Entities;

using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.Number)
            .NotEqual(0).WithMessage("Sale Number must not be 0.");

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
            .Must(id => id != Guid.Empty)
            .WithMessage("Sale must have a valid Branch ID");

        RuleFor(sale => sale.BranchName)
            .NotEmpty().WithMessage("Branch name must not be empty.")
            .Length(3, 100).WithMessage("Branch name must be at least 3 characters long and cannot be longer than 100 characters.");

        RuleFor(sale => sale.Items)
            .NotEmpty().WithMessage("Sale must have at least one item.");

        RuleForEach(sale => sale.Items).SetValidator(new SaleItemValidator());
    }
}
