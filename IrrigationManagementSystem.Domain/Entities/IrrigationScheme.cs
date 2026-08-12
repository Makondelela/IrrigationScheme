namespace IrrigationManagementSystem.Domain.Entities;

public class IrrigationScheme
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string RiverName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}