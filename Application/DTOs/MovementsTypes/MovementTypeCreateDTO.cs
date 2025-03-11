namespace FluxSYS_backend.Application.DTOs.MovementsTypes;

public class MovementTypeCreateDTO
{
    public string Name_movement_type { get; set; }
    public int Id_company_Id { get; set; }
    public int Id_clasification_movement_Id { get; set; }
}
