using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pichincha.Api.Exceptions;
using System.Net;

namespace Pichincha.Api.Middleware
{
    public class ErrorDeatils
    {
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ExceptionHandlingMiddleware
    {
        public RequestDelegate RequestDelegate;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.RequestDelegate = requestDelegate;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await RequestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception.ToString());

            string resultError = string.Empty;
            context.Response.ContentType = "application/json";
            var statusCode = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case DbUpdateException:
                    statusCode = HttpStatusCode.InternalServerError;
                    resultError = JsonConvert.SerializeObject(new ErrorDeatils
                    {
                        ErrorMessage = exception.InnerException.Message ?? exception.Message,
                        ErrorType = exception.GetType().Name
                    });
                    context.Response.StatusCode = (int)statusCode;
                    break;

                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    resultError = JsonConvert.SerializeObject(validationException.Errors);
                    break;

                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    resultError = JsonConvert.SerializeObject(notFoundException.Errors);
                    break;

                default:

                    resultError = JsonConvert.SerializeObject(new ErrorDeatils
                    {
                        ErrorMessage = exception.Message,
                        ErrorType = exception.GetType().Name
                    });
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(resultError);
        }
    }
}
