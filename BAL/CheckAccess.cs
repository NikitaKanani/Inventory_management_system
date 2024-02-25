﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using static System.Formats.Asn1.AsnWriter;

namespace Inventory_management_system.BAL
{
    public class CheckAccess : ActionFilterAttribute, IAuthorizationFilter
    {
        //When User ID is not availale or removed from session,
        // it will redirect to login page
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("UserID") == null)
                filterContext.Result = new RedirectResult("~/SEC_User/Login");
        }
        // Once we logout (session is cleared) then we can not go back to previous screen
        // We must login to proceed further.
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers["Cache-Control"] = "no-cache,no - store, must - revalidate";
            context.HttpContext.Response.Headers["Expires"] = "-1";
            context.HttpContext.Response.Headers["Pragma"] = "no-cache";
            base.OnResultExecuting(context);
        }
    }
}

