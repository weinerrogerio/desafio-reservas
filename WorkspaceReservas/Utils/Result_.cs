namespace WorkspaceReservas.Utils
{
    public class Result_<T>
    {
        public bool IsSuccess { get; private set; }
        public T Value { get; private set; }
        public Dictionary<string, string[]> Errors { get; private set; }

        private Result_(bool isSuccess, T value, Dictionary<string, string[]> errors)
        {
            IsSuccess = isSuccess;
            Value = value;
            Errors = errors;
        }

        public static Result_<T> Success(T value) => new(true, value, null);

        public static Result_<T> Failure(string campo, string mensagem) =>
            new(false, default, new Dictionary<string, string[]> { { campo, new[] { mensagem } } });

        public static Result_<T> Failure(Dictionary<string, string[]> errors) =>
            new(false, default, errors);
    }


}