using Authorize.Enums;

namespace Authorize.DTOs;

public class EditPermissionDto
{
    public required Guid Id { get; set; }
    public required Guid RoleId { get; set; }
    public required Guid ResourceId { get; set; }
    public required MethodsTypes Method { get; set; }
}
