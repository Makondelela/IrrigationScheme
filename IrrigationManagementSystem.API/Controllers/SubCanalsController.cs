using IrrigationManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IrrigationManagementSystem.API.Controllers;

[ApiController]
[Route("api/subcanals")]
public class SubCanalsController(IIrrigationService service) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(service.GetSubCanals());
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
        => service.GetSubCanal(id) is { } subCanal ? Ok(subCanal) : NotFound();

    [HttpPost]
    public IActionResult Create(IrrigationManagementSystem.Domain.Entities.SubCanal subCanal)
    {
        var created = service.CreateSubCanal(subCanal);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, IrrigationManagementSystem.Domain.Entities.SubCanal subCanal)
        => service.UpdateSubCanal(id, subCanal) is { } updated ? Ok(updated) : NotFound();

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
        => service.DeleteSubCanal(id) ? NoContent() : NotFound();
}
