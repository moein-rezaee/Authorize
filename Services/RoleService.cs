using Authorize.Context;


namespace Authorize.Services
{
    public class RoleService(AuthorizeContextDb db)
    {
        private readonly AuthorizeContextDb _db = db;

    }
}
