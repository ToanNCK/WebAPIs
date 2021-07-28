using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class UsersDTO
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int? Gender { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public int AccountType { get; set; }
        public int Active { get; set; }
        public string Description { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? EditBy { get; set; }
        public DateTime? EditDate { get; set; }
    }
}
