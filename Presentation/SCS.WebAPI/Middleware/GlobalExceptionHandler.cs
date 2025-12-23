//using Microsoft.AspNetCore.Diagnostics;
//using Microsoft.AspNetCore.Mvc;
//using Scs.Application.Exceptions;
//using System.Net;

//namespace SCS.WebAPI.Middleware;

//public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
//{
//    public async ValueTask<bool> TryHandleAsync(
//        HttpContext httpContext,
//        Exception exception,
//        CancellationToken cancellationToken)
//    {
//        logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

//        // 1. Map exceptions to status codes
//        var (statusCode, title) = exception switch
//        {
//            NotFoundException => (StatusCodes.Status404NotFound, "Resource Not Found"),
//            IdentityRegistrationException => (StatusCodes.Status400BadRequest, "Registration Error"),
//            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
//            // Add more custom exceptions here as you create them
//            _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
//        };

//        // 2. Create the ProblemDetails response (RFC 7807)
//        var problemDetails = new ProblemDetails
//        {
//            Status = statusCode,
//            Title = title,
//            Detail = exception.Message,
//            Instance = httpContext.Request.Path
//        };

//        // 3. Special handling for Identity errors (extracting your List<string> Errors)
//        if (exception is IdentityRegistrationException identityEx)
//        {
//            problemDetails.Extensions.Add("identityErrors", identityEx.Errors);
//        }

//        // 4. Set response headers and body
//        httpContext.Response.StatusCode = statusCode;
//        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

//        return true; // Tells the pipeline we've handled the exception
//    }
//}