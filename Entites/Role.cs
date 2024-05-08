namespace Authorize.Entities
{
    public class Role: BaseEntity
    {
        public required string Name { get; set; }
        public ICollection<RoleResource>? RolesResource { get; set; }
        public ICollection<Permission>? Permissions { get; set; }
    }
}