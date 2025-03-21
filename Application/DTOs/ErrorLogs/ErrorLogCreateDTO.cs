namespace FluxSYS_backend.Application.DTOs.ErrorLogs
{
    public class ErrorLogCreateDTO
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
    }
}
