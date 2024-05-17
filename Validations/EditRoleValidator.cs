using FluentValidation;
using Authorize.DTOs;

namespace Authorize.Validations;

public class EditRoleValidator : AbstractValidator<EditRoleDto>
{
    public EditRoleValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty()
            .NotNull();

        RuleFor(i => i.Name)
            .NotEmpty()
            .NotNull()
            .Length(3, 30);

        RuleFor(i => i.OrganizationId)
            .NotEmpty()
            .NotNull();
    }

}
