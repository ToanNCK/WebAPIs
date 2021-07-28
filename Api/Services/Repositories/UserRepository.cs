using Entities.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DTO;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SaleManagerContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public UserRepository(SaleManagerContext context, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }

        public int? UserIdLogger => Int32.Parse(_contextAccessor.HttpContext.User.Claims.First(m => m.Type == ClaimTypes.NameIdentifier).Value);
        public string UserAccountLogger => _contextAccessor.HttpContext.User.Claims.First(m => m.Type == ClaimTypes.Name).Value;

        public IList<UsersDTO> GetAllUser()
        {
            var data = _context.Users.ToList();
            var output = _mapper.Map<IList<UsersDTO>>(data);
            return output;
        }

        public UsersDTO GetInforUserLogin(LoginRequestDTO input)
        {
            var hashPassword = MD5(input.Password, input.Account);

            var data = _context.Users.FirstOrDefault(m => m.Account.Equals(input.Account) && m.Password.Equals(hashPassword));
            var output = _mapper.Map<UsersDTO>(data);
            return output;
        }

        public UsersDTO GetById(int id)
        {
            var data = _context.Users.FirstOrDefault(m => m.Id == id);
            var output = _mapper.Map<UsersDTO>(data);
            return output;
        }
        public bool CreateUsers(UsersDTO input)
        {
            try
            {
                input.CreateDate = DateTime.Now;
                input.Active = 1;
                input.CreateBy = UserIdLogger;
                input.Password = MD5(input.Password, input.Account);

                var data = _mapper.Map<User>(input);
                _context.Add(data);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditUsers(UsersDTO input)
        {
            try
            {
                input.EditDate = DateTime.Now;
                input.EditBy = UserIdLogger;

                var data = _mapper.Map<User>(input);
                var item = GetById(input.Id);
                _context.Entry(item).CurrentValues.SetValues(data);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUsers(int id)
        {
            try
            {
                var data = GetById(id);
                _context.Remove(data);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public string MD5(string password, string account)
        {
            var subaccount = account.Substring(0, account.Length > 3 ? 3 : account.Length);
            MD5 md5 = new MD5CryptoServiceProvider();
            var hashBytes = BitConverter.ToString(md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password + HashCodeMD5.Code + subaccount)));


            var output = string.Join("", hashBytes.Split('-'));


            return output.ToString();
        }

    }
}
