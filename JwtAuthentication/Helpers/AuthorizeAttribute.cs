using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using JwtAuthentication.DataEntity;
using System.Linq;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public ICollection<string> _roles { get; set; }

    public AuthorizeAttribute(params string[] roles)
    {
        _roles = roles.Select(x => x.ToLower()).ToList();
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (User)context.HttpContext.Items["User"];
        if (user == null)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
        else
        {
            var userRoles = user.UserRoles.Select(x => x.Role.RoleName).ToList();
            var isValid = false;
            userRoles.ForEach(role =>
            {
                if (_roles.Contains(role.ToLower()))
                {
                    isValid = true;
                }
            });
            if (!isValid)
            {
                context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}