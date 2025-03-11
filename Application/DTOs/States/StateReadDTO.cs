namespace FluxSYS_backend.Application.DTOs.States;

public class StateReadDTO
{
    public int Id_state { get; set; }
    public string Name_state { get; set; }
    public string Name_company { get; set; }
    public bool Delete_log_state { get; set; }
}
