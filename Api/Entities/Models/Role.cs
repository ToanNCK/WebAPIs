using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Role
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public string DisplayName { get; set; }
        public string GroupName { get; set; }
    }
}
