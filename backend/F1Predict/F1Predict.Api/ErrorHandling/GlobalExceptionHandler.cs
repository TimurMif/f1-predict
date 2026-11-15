using F1Predict.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace F1Predict.Api.ErrorHandling;

/// <summary>
/// Превращает исключения приложения в ответы с нужным HTTP-статусом (формат ProblemDetails).
/// </summary>
public class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCode, title) = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, "Не найдено"),
            ConflictException => (StatusCodes.Status409Conflict, "Конфликт"),
            _ => (StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера")
        };

        var isServerError = statusCode == StatusCodes.Status500InternalServerError;
        if (isServerError)
            logger.LogError(exception, "Необработанное исключение");

        httpContext.Response.StatusCode = statusCode;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                // текст исключения показываем только для «ожидаемых» ошибок, а не для 500
                Detail = isServerError ? null : exception.Message
            }
        });
    }
}