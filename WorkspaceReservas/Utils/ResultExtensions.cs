using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace WorkspaceReservas.Utils
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(
        this Result<T> result,
        ControllerBase controller,
        Func<T, IActionResult>? onSuccess = null)
        {
            if ( result.IsSuccess )
                return onSuccess?.Invoke(result.Value) ?? controller.Ok(result.Value);

            var errors = result.Errors.Select(e => e.Message);

            if ( result.HasError<NotFoundError>() )
                return controller.NotFound(new { errors });               // 404

            if ( result.HasError<BusinessRuleError>() )
                return controller.UnprocessableEntity(new { errors });    // 422 (Unprocessable Entity)

            if ( result.HasError<ConflictError>() )
                return controller.Conflict(new { errors }); // 409 (Conflict)

            return controller.BadRequest(new { errors });                 // 400
        }
    }
}
