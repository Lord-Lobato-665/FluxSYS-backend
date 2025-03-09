using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxSYS_backend.Domain.Models.PrincipalModels
{
    [Table("error_logs")]
    public class ErrorLogs
    {
        [Key]
        public int Id_error_log { get; set; }
        public string Message_error {  get; set; }
        public string Stacktrace_error { get; set; }
        public string Source_error { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
