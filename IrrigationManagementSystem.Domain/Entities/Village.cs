namespace IrrigationManagementSystem.Domain.Entities;

public class Village
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Order { get; set; }

    public Guid IrrigationSchemeId { get; set; }
}