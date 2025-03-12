namespace FluxSYS_backend.Application.DTOs.Users
{
    public class UserReadDTO
    {
        public int Id_user { get; set; }
        public string Name_user { get; set; }
        public string Mail_user { get; set; }
        public long Phone_user { get; set; }
        public string Name_role { get; set; }
        public string Name_position { get; set; }
        public string Name_department { get; set; }
        public string Name_company { get; set; }
        public string Name_module { get; set; }
        public DateTime? Date_insert { get; set; }
        public DateTime? Date_update { get; set; }
        public DateTime? Date_delete { get; set; }
        public DateTime? Date_restore { get; set; }
        public bool Delete_log_user { get; set; }
    }
}