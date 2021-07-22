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
                var user = context.HttpContext.User.Claims.First(m => m.Type == ClaimTypes.NameIdentifier).Value;
                //_cache.Set("Role" + user, this.ActionName, TimeSpan.FromHours(1));

                //var xx = _cache.Get("Role" + user);

                //var Service =  context.HttpContext.RequestServices.GetService<IUserRepository>(); *///.GetService(typeof(UserRepository));


                HasPermission(context.HttpContext, _actionName);
                if (string.IsNullOrEmpty(user)) context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                else
                {
                    var ListRole = context.HttpContext.User.Claims.First(m => m.Type == ClaimTypes.Role).Value;
                    if (!string.IsNullOrEmpty(ListRole))
                    {
                        var ArrayRole = ListRole.Split(',');
                        if (!ArrayRole.Any(m => m.Equals(_actionName)))
                        {
                            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                        }
                    }
                    else
                    {
                        context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                    }
                }
            }
            catch (Exception)
            {
                context.Result = new JsonResult(new { message = "Error" }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        private void HasPermission(HttpContext httpContext, string ActionName)
        {
            //var services = (IUserRepository)httpContext.RequestServices.GetService(typeof(IUserRepository));
            var Service = httpContext.RequestServices.GetService<IUserRepository>();
            

        }
    }
}
