using FluentResults;

namespace WorkspaceReservas.Utils
{
    public class Errors
    {
    }

    public class NotFoundError : Error
    {
        public NotFoundError(string message) : base(message) { }
    }

    public class BusinessRuleError : Error
    {
        public BusinessRuleError(string message) : base(message) { }
    }

    public class ConflictError : Error
    {
        public ConflictError(string message) : base(message) { }
    }


}
