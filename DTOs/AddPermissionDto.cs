using Authorize.Enums;

namespace Authorize.DTOs;

public class AddPermissionDto
{
    public required Guid RoleId { get; set; }
    public required Guid ResourceId { get; set; }
    public required MthodesTypes Method { get; set; }
}
