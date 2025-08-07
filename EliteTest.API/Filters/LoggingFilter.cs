
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EliteTest.API.Filters;

public class LoggingFilter : IActionFilter
{
    private readonly ILogger<LoggingFilter> _logger;
    private Stopwatch _stopwatch;

    public LoggingFilter(ILogger<LoggingFilter> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _stopwatch = new Stopwatch();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch = Stopwatch.StartNew();

        string actionName = context.ActionDescriptor.DisplayName ?? "Unknown Action";
        string method = context.HttpContext.Request.Method;
        string path = context.HttpContext.Request.Path;

        _logger.LogInformation($"Action {actionName} started with method {method} at path {path}");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
         _stopwatch.Stop();

        var statusCode = context.HttpContext.Response.StatusCode;
        var actionName = context.ActionDescriptor.DisplayName;

        _logger.LogInformation("Completed request: {ActionName} => Status Code: {StatusCode} (Duration: {Duration} ms)",
            actionName, statusCode, _stopwatch.ElapsedMilliseconds);
    }
}