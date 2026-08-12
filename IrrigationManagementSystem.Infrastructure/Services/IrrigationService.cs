using IrrigationManagementSystem.Application.Interfaces;
using IrrigationManagementSystem.Domain.Entities;
using IrrigationManagementSystem.Infrastructure.Data;

namespace IrrigationManagementSystem.Infrastructure.Services;

public class IrrigationService(IrrigationDbContext dbContext) : IIrrigationService
{
    public IEnumerable<IrrigationScheme> GetSchemes()
        => dbContext.IrrigationSchemes.OrderBy(scheme => scheme.Name).ToList();

    public IEnumerable<Village> GetVillages()
        => dbContext.Villages.OrderBy(village => village.Order).ToList();

    public IEnumerable<SubCanal> GetSubCanals()
        => dbContext.SubCanals.OrderBy(subCanal => subCanal.Name).ToList();

    public IEnumerable<Farmer> GetFarmers()
        => dbContext.Farmers.OrderBy(farmer => farmer.Name).ToList();

    public IrrigationScheme? GetScheme(Guid id)
        => dbContext.IrrigationSchemes.Find(id);

    public IrrigationScheme CreateScheme(IrrigationScheme scheme)
    {
        scheme.Id = scheme.Id == Guid.Empty ? Guid.NewGuid() : scheme.Id;
        dbContext.IrrigationSchemes.Add(scheme);
        dbContext.SaveChanges();
        return scheme;
    }

    public IrrigationScheme? UpdateScheme(Guid id, IrrigationScheme scheme)
    {
        var existing = dbContext.IrrigationSchemes.Find(id);
        if (existing is null) return null;

        existing.Name = scheme.Name;
        existing.RiverName = scheme.RiverName;
        existing.Description = scheme.Description;
        dbContext.SaveChanges();
        return existing;
    }

    public bool DeleteScheme(Guid id)
        => Delete(dbContext.IrrigationSchemes.Find(id));

    public Village? GetVillage(Guid id)
        => dbContext.Villages.Find(id);

    public Village CreateVillage(Village village)
    {
        village.Id = village.Id == Guid.Empty ? Guid.NewGuid() : village.Id;
        dbContext.Villages.Add(village);
        dbContext.SaveChanges();
        return village;
    }

    public Village? UpdateVillage(Guid id, Village village)
    {
        var existing = dbContext.Villages.Find(id);
        if (existing is null) return null;

        existing.Name = village.Name;
        existing.Order = village.Order;
        existing.IrrigationSchemeId = village.IrrigationSchemeId;
        dbContext.SaveChanges();
        return existing;
    }

    public bool DeleteVillage(Guid id)
        => Delete(dbContext.Villages.Find(id));

    public SubCanal? GetSubCanal(Guid id)
        => dbContext.SubCanals.Find(id);

    public SubCanal CreateSubCanal(SubCanal subCanal)
    {
        subCanal.Id = subCanal.Id == Guid.Empty ? Guid.NewGuid() : subCanal.Id;
        dbContext.SubCanals.Add(subCanal);
        dbContext.SaveChanges();
        return subCanal;
    }

    public SubCanal? UpdateSubCanal(Guid id, SubCanal subCanal)
    {
        var existing = dbContext.SubCanals.Find(id);
        if (existing is null) return null;

        existing.Name = subCanal.Name;
        existing.VillageId = subCanal.VillageId;
        existing.NumberOfFarmers = subCanal.NumberOfFarmers;
        dbContext.SaveChanges();
        return existing;
    }

    public bool DeleteSubCanal(Guid id)
        => Delete(dbContext.SubCanals.Find(id));

    public Farmer? GetFarmer(Guid id)
        => dbContext.Farmers.Find(id);

    public Farmer CreateFarmer(Farmer farmer)
    {
        farmer.Id = farmer.Id == Guid.Empty ? Guid.NewGuid() : farmer.Id;
        dbContext.Farmers.Add(farmer);
        dbContext.SaveChanges();
        return farmer;
    }

    public Farmer? UpdateFarmer(Guid id, Farmer farmer)
    {
        var existing = dbContext.Farmers.Find(id);
        if (existing is null) return null;

        existing.Name = farmer.Name;
        existing.VillageId = farmer.VillageId;
        existing.SubCanalId = farmer.SubCanalId;
        dbContext.SaveChanges();
        return existing;
    }

    public bool DeleteFarmer(Guid id)
        => Delete(dbContext.Farmers.Find(id));

    private bool Delete<TEntity>(TEntity? entity) where TEntity : class
    {
        if (entity is null) return false;

        dbContext.Remove(entity);
        dbContext.SaveChanges();
        return true;
    }
}
