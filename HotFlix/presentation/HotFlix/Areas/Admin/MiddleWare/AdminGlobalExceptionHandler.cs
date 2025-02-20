namespace HotFlix.Areas.Admin.MiddleWare
{
    public class AdminGlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public AdminGlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {

                context.Response.Redirect($"/home/error?errormessage={e.Message}");
            }
        }
    }
}
