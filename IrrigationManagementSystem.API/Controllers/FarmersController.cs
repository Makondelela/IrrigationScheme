using IrrigationManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IrrigationManagementSystem.API.Controllers;

[ApiController]
[Route("api/farmers")]
public class FarmersController(IIrrigationService service) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(service.GetFarmers());
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
        => service.GetFarmer(id) is { } farmer ? Ok(farmer) : NotFound();

    [HttpPost]
    public IActionResult Create(IrrigationManagementSystem.Domain.Entities.Farmer farmer)
    {
        var created = service.CreateFarmer(farmer);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, IrrigationManagementSystem.Domain.Entities.Farmer farmer)
        => service.UpdateFarmer(id, farmer) is { } updated ? Ok(updated) : NotFound();

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
        => service.DeleteFarmer(id) ? NoContent() : NotFound();
}
