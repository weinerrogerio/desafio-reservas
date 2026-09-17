namespace WorkspaceReservas.Utils
{
    public class ServiceResult<T>
    {
        public bool Success { get; private set; }
        public T? Data { get; private set; }
        public Dictionary<string, string[]> Errors { get; private set; } = new();

        public static ServiceResult<T> Ok(T data) => new() { Success = true, Data = data };

        public static ServiceResult<T> Fail(string campo, string mensagem) => new()
        {
            Success = false,
            Errors = new Dictionary<string, string[]> { { campo, new[] { mensagem } } }
        };
    }
}

