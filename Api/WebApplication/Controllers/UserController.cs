using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Repositories;

namespace WebApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;
        public IConfiguration _configuration;
        public UserController(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Login(LoginRequestDTO input)
        {
            try
            {
                var data = _userRepository.GetInforUserLogin(input);
                if (data != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["appSettings:Secret"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim(ClaimTypes.NameIdentifier, data.Id.ToString()),
                        new Claim(ClaimTypes.Name, data.Account.ToString()),
                        }),
                        Expires = DateTime.UtcNow.AddDays(Int32.Parse(_configuration["appSettings:ExpiresDay"])),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);
                    var rs = new LoginResponseDTO(data.Id, data.Account, data.UserName, tokenString);
                    return Json(new ResponseData(StatusCodes.Status200OK, "Đăng nhập thành công", rs));
                }
                else
                {
                    return Json(new ResponseData(StatusCodes.Status500InternalServerError, "Tài khoản hoặc mật khẩu chưa đúng. vui lòng nhập lại"));
                }
            }
            catch (Exception)
            {
                return Json(new ResponseData(StatusCodes.Status500InternalServerError, "Lỗi trong quá trình đăng nhập, vui lòng thử lại"));
            }


        }

        [HttpGet]
        [Authorize(ActionName: EnumRole.Get_Users)]
        public JsonResult GetAllUser()
        {
            var data = _userRepository.GetAllUser();
            return Json(data);
        }

        [HttpPost]
        [Authorize(ActionName: EnumRole.Create_Users)]
        public JsonResult CreateUser(UsersDTO input)
        {
            if (_userRepository.CreateUsers(input)) return Json(new ResponseData(StatusCodes.Status200OK, "Thêm mới thành công"));
            return Json(new ResponseData(StatusCodes.Status404NotFound, "Thêm mới thất bại"));
        }

        [HttpPost]
        [Authorize(ActionName: EnumRole.Edit_Users)]
        public JsonResult EditUsers(UsersDTO input)
        {
            if (_userRepository.EditUsers(input)) return Json(new ResponseData(StatusCodes.Status200OK, "Cập nhật thành công"));
            return Json(new ResponseData(StatusCodes.Status404NotFound, "Cập nhật thất bại"));
        }

        [HttpDelete]
        [Authorize(ActionName: EnumRole.Delete_Users)]
        public JsonResult DeleteUsers(int id)
        {
            if (_userRepository.DeleteUsers(id)) return Json(new ResponseData(StatusCodes.Status200OK, "Cập nhật thành công"));
            return Json(new ResponseData(StatusCodes.Status404NotFound, "Cập nhật thất bại"));
        }

    }
}
