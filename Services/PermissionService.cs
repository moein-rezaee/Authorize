using Authorize.Context;
using Authorize.DTOs;
using Authorize.Entities;
using Authorize.Interfaces;
using Authorize.Repositories;


namespace Authorize.Services
{
    public class PermissionService(IUnitOfWorkRepository db)
    {
        private readonly IUnitOfWorkRepository _db = db;

        public bool Check(CheckPermissionDto dto)
        {
            bool hasPermission = false;
            Role? role = _db.Roles.Find(dto.RoleId);
            if (role != null)
            {
                Resource? resource = _db.Resources.Find(i => i.Url == dto.Resource);
                if (resource != null)
                {
                    hasPermission = _db.Permissions.Any(i => i.RoleId == dto.RoleId
                                                                  && i.ResourceId == resource.Id
                                                                  && i.Method == dto.Method);
                }
            }
            return hasPermission;
        }

    }
}