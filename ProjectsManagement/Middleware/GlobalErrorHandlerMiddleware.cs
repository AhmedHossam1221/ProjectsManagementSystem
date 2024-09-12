using ProjectsManagement.Data;
using ProjectsManagement.ViewModels;

namespace ProjectsManagement.Middleware
{
    public class GlobalErrorHandlerMiddleware
    {
        RequestDelegate _next;

        public GlobalErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string message = "Error Occured";
                ErrorCode errorCode = ErrorCode.UnKnown;

                if (ex is BusinessException businessException)
                {
                    message = businessException.Message;
                    errorCode = businessException.ErrorCode;
                }
                else
                {
                    File.WriteAllText("D:\\Log.txt", $"Error happened: {ex.Message}");
                }

                var result = ResultViewModel.Faliure(message);

                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
