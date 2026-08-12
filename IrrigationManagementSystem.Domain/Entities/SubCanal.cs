namespace IrrigationManagementSystem.Domain.Entities;

public class SubCanal
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Guid VillageId { get; set; }

    public int NumberOfFarmers { get; set; }
}