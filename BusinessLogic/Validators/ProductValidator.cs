using FluentValidation;
using DataAccess.Data.Entities;
using BusinessLogic.DTOs;

namespace BusinessLogic.Validators

{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .Matches("[A-Z].*").WithMessage("{PropetryName} must start with uppercase letter.");

            RuleFor(x => x.CategoryId)
                .NotEmpty();

            RuleFor(x => x.Price)
               .NotEmpty()
               .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} can not be negative.");

            RuleFor(x => x.Discount)
               .NotEmpty()
               .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} can not be negative.");

            RuleFor(x => x.Description)
               .Length(10, 4000)
               .Matches("[A-Z].*").WithMessage("{PropetryName} must start with uppercase letter.");

            RuleFor(x => x.ImageUrl)
               .NotEmpty()
               .Matches("[A-Z].*").WithMessage("{PropetryName} must start with uppercase letter.");
        }

        private static bool LinkMustBeUri(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return false;
            }

            Uri outUri;
            return Uri.TryCreate(link, UriKind.Absolute, out outUri)
                && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
        }

    }
}
