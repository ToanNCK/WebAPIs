using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Interfaces;
using Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string _actionName { get; set; }
        public AuthorizeAttribute(string ActionName)
        {
            _actionName = ActionName;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous) return;

            try
            {
                var userid = context.HttpContext.User.Claims.First(m => m.Type == ClaimTypes.NameIdentifier).Value;
                var username = context.HttpContext.User.Claims.First(m => m.Type == ClaimTypes.Name).Value;

                if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(username)) context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };

                if (!HasPermission(context.HttpContext, _actionName, username))
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
            catch (Exception)
            {
                context.Result = new JsonResult(new { message = "Error" }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        private bool HasPermission(HttpContext httpContext, string ActionName, string UserName)
        {
            var Service = httpContext.RequestServices.GetService<IUserRoleRepository>();
            if (Service.CacheRoleByUserLogger().Any(m => m.ActionName == ActionName && m.UserName == UserName))
                return true;
            return false;

        }
    }
}
