namespace Authorize.Entities
{
    public class Role: BaseEntity
    {
        public required Guid OrganizationId { get; set; }
        public required string Name { get; set; }
        
        public ICollection<Permission>? Permissions { get; set; }
    }
}