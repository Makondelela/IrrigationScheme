using IrrigationManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IrrigationManagementSystem.API.Controllers;

[ApiController]
[Route("api/villages")]
public class VillagesController(IIrrigationService service) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(service.GetVillages());
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
        => service.GetVillage(id) is { } village ? Ok(village) : NotFound();

    [HttpPost]
    public IActionResult Create(IrrigationManagementSystem.Domain.Entities.Village village)
    {
        var created = service.CreateVillage(village);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, IrrigationManagementSystem.Domain.Entities.Village village)
        => service.UpdateVillage(id, village) is { } updated ? Ok(updated) : NotFound();

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
        => service.DeleteVillage(id) ? NoContent() : NotFound();
}
