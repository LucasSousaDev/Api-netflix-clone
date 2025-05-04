using System.Net;

namespace movies_api.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
		private RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
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
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var statusCode = exception switch
			{
				TimeoutException          => HttpStatusCode.RequestTimeout,
				InvalidOperationException => HttpStatusCode.BadRequest,
				KeyNotFoundException      => HttpStatusCode.NotFound,
				IndexOutOfRangeException  => HttpStatusCode.BadRequest,
				_ => HttpStatusCode.InternalServerError,
			};
			
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)statusCode;

			return context.Response.WriteAsJsonAsync(new
			{
				context.Response.StatusCode,
				exception.Message
			});
		}
	}
}