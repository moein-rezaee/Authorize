using Authorize.Enums;

namespace Authorize.DTOs;

public class AddPermissionDto
{
    public required Guid RoleId { get; set; }
    public required Guid ResourceId { get; set; }
    public required MethodsTypes Method { get; set; }
}
