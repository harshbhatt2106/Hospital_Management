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
            string[] publicPaths =
                {
                    "/admin/login",
                    "/password/forgetpassword",
                    "/admin/verifygmail",
                    "/admin/sendotp",
                    "/admin/verifyotp",
                    "/password/ResetPassword"
                };

            string path = context.Request.Path.Value?.ToLowerInvariant() ?? "/admin/login";

            if (publicPaths.Any(p => path.Equals(p, StringComparison.OrdinalIgnoreCase)))
            {
                await _requestDelegate(context);
                return;
            }
            if (path == "/error/general")
            {
                await _requestDelegate(context);
                return;
            }
            else
            {
                int? _userid = SessionUtility.GetCurrentUserID();
                if (_userid == 0)
                {
                    context.Response.Redirect("/Admin/Login");
                    return;
                }
            }
            await _requestDelegate(context);
        }
    }
}
