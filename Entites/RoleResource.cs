namespace Authorize.Entities
{
    public class RoleResource: BaseEntity
    {
        public required Guid ResourceId { get; set; }
        public Resource? Resource { get; set; }
        
        public required Guid RoleId { get; set; }
        public Role? Role { get; set; }
    }
}