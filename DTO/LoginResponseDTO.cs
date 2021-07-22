using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class LoginResponseDTO
    {
        public LoginResponseDTO()
        {

        }
        public LoginResponseDTO(int _id, string _account, string _username, string _token)
        {
            this.Id = _id;
            this.Account = _account;
            this.UserName = _username;
            this.Token = _token;
        }

        public int Id { get; set; }
        public string Account { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }

    }
}
