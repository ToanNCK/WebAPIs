using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IUserRoleRepository
    {
        public List<RoleDTO> GetAllRoleByUserID(int id);
        public List<PermissionDTO> CacheRoleByUserLogger();
    }
}
