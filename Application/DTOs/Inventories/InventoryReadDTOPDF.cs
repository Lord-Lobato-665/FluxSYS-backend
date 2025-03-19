namespace FluxSYS_backend.Application.DTOs.Inventories;

public class InventoryReadDTOPDF
{
    public string Fecha { get; set; }
    public string Hora { get; set; }
    public virtual ICollection<InventoryReadDTO> Inventories { get; set; }
}