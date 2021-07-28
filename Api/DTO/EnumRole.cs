using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class EnumRole
    {
        [Display(Name = "Danh sách người dùng", GroupName = "Users")]
        public const string Get_Users = "Get_Users";
        [Display(Name = "Thêm mới người dùng", GroupName = "Users")]
        public const string Create_Users = "Create_Users";
        [Display(Name = "Cập nhật người dùng", GroupName = "Users")]
        public const string Edit_Users = "Edit_Users";
        [Display(Name = "Xóa người dùng", GroupName = "Users")]
        public const string Delete_Users = "Delete_Users";
    }
}
