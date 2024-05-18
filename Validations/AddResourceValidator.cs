using FluentValidation;
using Authorize.DTOs;

namespace Authorize.Validations;

public class AddResourceValidator : AbstractValidator<AddResourceDto>
{
    public AddResourceValidator()
    {
        RuleFor(i => i.Url)
            .NotEmpty()
            .NotNull()
            .Length(3, 30);

        RuleFor(i => i.OrganizationId)
            .NotEmpty()
            .NotNull();
    }

}
