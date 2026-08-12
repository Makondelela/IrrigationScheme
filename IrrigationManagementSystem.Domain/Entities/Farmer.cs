namespace IrrigationManagementSystem.Domain.Entities;

public class Farmer
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Guid VillageId { get; set; }

    public Guid SubCanalId { get; set; }
}
