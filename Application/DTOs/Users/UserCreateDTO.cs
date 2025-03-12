namespace FluxSYS_backend.Application.DTOs.Users
{
    public class UserCreateDTO
    {
        public string Name_user { get; set; }
        public string Mail_user { get; set; }
        public long Phone_user { get; set; }
        public string Password_user { get; set; }
        public int Id_rol_Id { get; set; }
        public int Id_position_Id { get; set; }
        public int Id_department_Id { get; set; }
        public int Id_company_Id { get; set; }
    }
}