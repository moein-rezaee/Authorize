using FluentValidation;
using Authorize.DTOs;

namespace Authorize.Validations;

public class AddPermissionValidator : AbstractValidator<AddPermissionDto>
{
    public AddPermissionValidator()
    {
        RuleFor(i => i.ResourceId)
            .NotEmpty()
            .NotNull();

        RuleFor(i => i.RoleId)
            .NotEmpty()
            .NotNull();

        RuleFor(i => i.Method)
            .NotEmpty()
            .NotNull();
    }

}
