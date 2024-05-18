using FluentValidation;
using Authorize.DTOs;

namespace Authorize.Validations;

public class EditResourceValidator : AbstractValidator<EditResourceDto>
{
    public EditResourceValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty()
            .NotNull();

        RuleFor(i => i.Url)
            .NotEmpty()
            .NotNull()
            .Length(3, 30);

        RuleFor(i => i.OrganizationId)
            .NotEmpty()
            .NotNull();
    }

}
