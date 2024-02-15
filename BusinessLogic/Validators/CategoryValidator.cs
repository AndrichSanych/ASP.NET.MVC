using BusinessLogic.DTOs;
using DataAccess.Data.Entities;
using FluentValidation;

namespace BusinessLogic.Validators

{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            this.RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .Matches("[A-Z].*").WithMessage("{PropetryName} must start with uppercase letter.");

        }
    }
}
