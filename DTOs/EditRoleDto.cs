namespace Authorize.DTOs;

public class EditRoleDto
{
    public required Guid Id { get; set; }
    public required Guid OrganizationId { get; set; }
    public required string Name { get; set; }
}
