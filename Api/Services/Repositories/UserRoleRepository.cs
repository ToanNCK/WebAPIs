using DTO;
using Entities.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace Services.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly SaleManagerContext _context;
        private readonly UserRepository _user;
        private readonly IMemoryCache _cache;

        public UserRoleRepository(SaleManagerContext context, UserRepository user, IMemoryCache cache)
        {
            _context = context;
            _user = user;
            _cache = cache;
        }

        public List<RoleDTO> GetAllRoleByUserID(int id)
        {
            var AllRoleByUsers = (from A in _context.Roles
                                  join B in _context.UserRoles on new { a = A.Id, b = id } equals new { a = B.RoleId, b = B.UserId } into AB
                                  from B in AB.DefaultIfEmpty()
                                  select new RoleDTO
                                  {
                                      ActionName = A.ActionName,
                                      DisplayName = A.DisplayName,
                                      GroupName = A.GroupName,
                                      IsCheck = B != null ? true : false,
                                  }).OrderBy(m => m.GroupName).ToList();
            return AllRoleByUsers;
        }

        public List<PermissionDTO> CacheRoleByUserLogger()
        {
            List<PermissionDTO> ListRole = new List<PermissionDTO>();
            if (_cache.Get("ListPermission" + _user.UserIdLogger) == null)
            {
                ListRole = (from A in _context.Roles
                            join B in _context.UserRoles on A.Id equals B.RoleId
                            where B.UserId == _user.UserIdLogger
                            select new PermissionDTO
                            {
                                ActionName = A.ActionName,
                                UserName = _user.UserAccountLogger
                            }).ToList();
                if (ListRole.Count > 0) _cache.Set("ListPermission" + _user.UserIdLogger, ListRole, TimeSpan.FromHours(4));
            }
            else
            {
                ListRole = (List<PermissionDTO>)_cache.Get("ListPermission" + _user.UserIdLogger);
            }
            return ListRole;
        }
    }
}
