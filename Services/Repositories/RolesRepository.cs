using Entities.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Services.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly SaleManagerContext _context;
        public RolesRepository(SaleManagerContext context)
        {
            _context = context;
        }

        public void AutoAddRole()
        {
            foreach (var item in new EnumRole().GetType().GetFields())
            {
                if (!_context.Roles.Any(m => m.ActionName == item.Name))
                {
                    Role role = new Role();
                    role.ActionName = item.Name;
                    role.DisplayName = item.GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>().FirstOrDefault().Name;
                    role.GroupName = item.GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>().FirstOrDefault().GroupName;
                    _context.Roles.Add(role);
                }
                _context.SaveChanges();
            }
        }

    }
}
