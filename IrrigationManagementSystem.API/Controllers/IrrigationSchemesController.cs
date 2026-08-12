using IrrigationManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IrrigationManagementSystem.API.Controllers;

[ApiController]
[Route("api/irrigationschemes")]
public class IrrigationSchemesController(IIrrigationService service) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(service.GetSchemes());
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
        => service.GetScheme(id) is { } scheme ? Ok(scheme) : NotFound();

    [HttpPost]
    public IActionResult Create(IrrigationManagementSystem.Domain.Entities.IrrigationScheme scheme)
    {
        var created = service.CreateScheme(scheme);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, IrrigationManagementSystem.Domain.Entities.IrrigationScheme scheme)
        => service.UpdateScheme(id, scheme) is { } updated ? Ok(updated) : NotFound();

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
        => service.DeleteScheme(id) ? NoContent() : NotFound();
}
