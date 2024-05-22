using FluentValidation;
using Authorize.DTOs;

namespace Authorize.Validations;

public class CheckPermissionValidator : AbstractValidator<CheckPermissionDto>
{
    public CheckPermissionValidator()
    {
        RuleFor(i => i.Resource)
            .NotEmpty()
            .NotNull()
            .Length(3, 100);

        RuleFor(i => i.RoleId)
            .NotEmpty()
            .NotNull();

        RuleFor(i => i.Method)
            .NotEmpty()
            .NotNull();
    }

}
