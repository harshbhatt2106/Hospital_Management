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
                    "/admin/forgetpassword",
                    "/admin/verifygmail",
                    "/admin/sendotp",
                    "/admin/verifyotp",
                    "/admin/ResetPassword"
                };

            string path = context.Request.Path.Value?.ToLowerInvariant() ?? "/admin/login";

            if (publicPaths.Any(p => path.Equals(p,StringComparison.OrdinalIgnoreCase)))
            {
                await _requestDelegate(context);
                return;
            }

            else
            {
                int? _userid = context.Session.GetInt32("UserID");
                if (_userid == null)
                {
                    context.Response.Redirect("/Admin/Login");
                    return;
                }
            }
            await _requestDelegate(context);
        }
    }
}
