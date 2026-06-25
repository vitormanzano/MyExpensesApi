using Microsoft.AspNetCore.Mvc;
using MyExpenses.Errors;

namespace MyExpenses.Results;

public static  class ResultExtensions
{
    public static IActionResult ToActionResult(this Error error, ControllerBase controller)
        => error.Type switch 
        {
            ErrorType.NotFound => controller.NotFound(error.Message),
            ErrorType.Conflict => controller.Conflict(error.Message),
            ErrorType.Validation => controller.BadRequest(error.Message),
            ErrorType.Unauthorized => controller.Unauthorized(),
            _ => controller.StatusCode(500, error.Message),
        };
}
