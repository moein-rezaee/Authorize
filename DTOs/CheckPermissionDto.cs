using Authorize.Enums;

namespace Authorize.DTOs
{
    public class CheckPermissionDto
    {

        public required Guid RoleId { get; set; }
        public required string Resource { get; set; }
        public required MethodsTypes Method { get; set; }
    }
}
