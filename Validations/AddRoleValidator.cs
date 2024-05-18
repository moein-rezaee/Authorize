using FluentValidation;
using Authorize.DTOs;

namespace Authorize.Validations;
public class AddRoleValidator : AbstractValidator<AddRoleDto>
{
    public AddRoleValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty()
            .NotNull()
            .Length(3, 30);

        RuleFor(i => i.OrganizationId)
            .NotEmpty()
            .NotNull();
    }

}
