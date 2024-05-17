namespace Authorize.DTOs;

public class AddRoleDto
{
    public required Guid OrganizationId { get; set; }
    public required string Name { get; set; }
}
