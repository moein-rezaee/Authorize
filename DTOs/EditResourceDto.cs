namespace Authorize.DTOs;

public class EditResourceDto
{
    public required Guid Id { get; set; }
    public required Guid OrganizationId { get; set; }
    public required string Url { get; set; }
}
