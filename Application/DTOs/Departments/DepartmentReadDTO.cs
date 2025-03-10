namespace FluxSYS_backend.Application.DTOs.Departments;

public class DepartmentReadDTO
{
    public int Id_department { get; set; }
    public string Name_deparment { get; set; }
    public string Name_company { get; set; }
    public bool Delete_log_department { get; set; }
}
