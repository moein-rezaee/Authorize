namespace Authorize.Entities
{
    public class Resource: BaseEntity
    {
        public required Guid OrganizationId { get; set; }
        public required string Url { get; set; }
        
        public ICollection<Permission>? Permissions { get; set; }
        public ICollection<RoleResource>? RolesResources { get; set; }
    }
}