namespace FluxSYS_backend.Application.DTOs.Positions;

public class PositionReadDTO
{
    public int Id_position {  get; set; }
    public string Name_position { get; set; }
    public string Name_company { get; set; }
    public bool Delete_log_position { get; set; }
}
