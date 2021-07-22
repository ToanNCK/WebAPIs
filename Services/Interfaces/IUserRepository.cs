using DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IUserRepository
    {
        public IList<UsersDTO> GetAllUser();
        public UsersDTO GetInforUserLogin(LoginRequestDTO input);
        public UsersDTO GetById(int id);
        public int? UserIdLoger => null ;
        public string UserAccountLogger => string.Empty;
        public bool CreateUsers(UsersDTO input);
        public bool EditUsers(UsersDTO input);
        public bool DeleteUsers(int id);
    }
}
