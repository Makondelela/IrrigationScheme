using IrrigationManagementSystem.Domain.Entities;

namespace IrrigationManagementSystem.Application.Interfaces;

public interface IIrrigationService
{
    IEnumerable<IrrigationScheme> GetSchemes();

    IEnumerable<Village> GetVillages();

    IEnumerable<SubCanal> GetSubCanals();

    IEnumerable<Farmer> GetFarmers();

    IrrigationScheme? GetScheme(Guid id);
    IrrigationScheme CreateScheme(IrrigationScheme scheme);
    IrrigationScheme? UpdateScheme(Guid id, IrrigationScheme scheme);
    bool DeleteScheme(Guid id);

    Village? GetVillage(Guid id);
    Village CreateVillage(Village village);
    Village? UpdateVillage(Guid id, Village village);
    bool DeleteVillage(Guid id);

    SubCanal? GetSubCanal(Guid id);
    SubCanal CreateSubCanal(SubCanal subCanal);
    SubCanal? UpdateSubCanal(Guid id, SubCanal subCanal);
    bool DeleteSubCanal(Guid id);

    Farmer? GetFarmer(Guid id);
    Farmer CreateFarmer(Farmer farmer);
    Farmer? UpdateFarmer(Guid id, Farmer farmer);
    bool DeleteFarmer(Guid id);
}