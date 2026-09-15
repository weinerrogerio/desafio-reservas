namespace WorkspaceReservas.Utils
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T Value { get; private set; }
        public Dictionary<string, string[]> Errors { get; private set; }

        private Result(bool isSuccess, T value, Dictionary<string, string[]> errors)
        {
            IsSuccess = isSuccess;
            Value = value;
            Errors = errors;
        }

        public static Result<T> Success(T value) => new(true, value, null);

        public static Result<T> Failure(string campo, string mensagem) =>
            new(false, default, new Dictionary<string, string[]> { { campo, new[] { mensagem } } });

        public static Result<T> Failure(Dictionary<string, string[]> errors) =>
            new(false, default, errors);
    }

}