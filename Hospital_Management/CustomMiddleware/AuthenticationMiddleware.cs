using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace Hospital_Management.CustomMiddleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public AuthenticationMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.Value?.ToLower();
            
            if (path == "/admin/login")
            {
                await _requestDelegate(context);
                return;
            }
            int? _userid = context.Session.GetInt32("UserID");
            if (_userid == null)
            {                
                context.Response.Redirect("/Admin/Login");
                return;
            }
            await _requestDelegate(context);
        }
    }
}
