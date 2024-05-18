using Authorize.Enums;

namespace Authorize.Entities
{
    public class Permission: BaseEntity
    {
        public required Guid RoleId { get; set; }
        public Role? Role { get; set; }
        
        public required Guid ResourceId { get; set; }
        public Resource? Resource { get; set; }

        public required MethodsTypes Method { get; set; } = MethodsTypes.GET; 
    }
}
