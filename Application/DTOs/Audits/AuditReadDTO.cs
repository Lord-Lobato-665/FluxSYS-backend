namespace FluxSYS_backend.Application.DTOs.Audits
{
    public class AuditReadDTO
    {
        public int Id_audit { get; set; }
        public string Date_insert { get; set; }
        public string Date_update { get; set; }
        public string Date_delete { get; set; }
        public string Date_restore { get; set; }
        public int Amount_modify { get; set; }
        public string Name_user { get; set; }
        public string Name_department { get; set; }
        public string Name_module { get; set; }
        public string Name_company { get; set; }
        public bool Delete_log_audits { get; set; }
    }
}