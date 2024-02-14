using FluentValidation;
using ASP.NET.MVC.Data.Entities;
namespace ASP.NET.MVC.Validators

{
    public class CategoryValidator : AbstractValidator<Category>
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
