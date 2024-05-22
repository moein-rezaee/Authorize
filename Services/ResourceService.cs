using Authorize.Context;


namespace Authorize.Services
{
    public class ResourceService(AuthorizeContextDb db)
    {
        private readonly AuthorizeContextDb _db = db;

    }
}
