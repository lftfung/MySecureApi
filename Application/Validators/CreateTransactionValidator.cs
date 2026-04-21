using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Application.DTOs;


namespace Application.Validators
{
    public class CreateTransactionValidator: AbstractValidator<CreateTransactionDto>
    {
        public CreateTransactionValidator() {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be over 0");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category cannot be empty")
                .MaximumLength(50).WithMessage("Category too long");

            RuleFor(x => x.Date)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("The date cannot be later than today");
        }
        

    }
}
