using FluentValidation;
using Authorize.DTOs;

namespace Authorize.Validations;

public class EditPermissionValidator : AbstractValidator<EditPermissionDto>
{
    public EditPermissionValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty()
            .NotNull();

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
