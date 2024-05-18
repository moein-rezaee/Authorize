namespace Authorize.DTOs;

public class AddResourceDto
{
    public required Guid OrganizationId { get; set; }
    public required string Url { get; set; }
}
