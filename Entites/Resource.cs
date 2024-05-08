namespace Authorize.Entities
{
    public class Resource: BaseEntity
    {
        public required string Url { get; set; }
        public ICollection<Permission>? Permissions { get; set; }
    }
}