using Core.Feature.Vehiculos.Commands;
using Core.Feature.Vehiculos.Queries;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("vehiculos")]
public sealed class VehiculosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Vehiculo>>> GetAll([FromQuery] long? marcaId, CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetVehiculosQuery { MarcaId = marcaId }, cancellationToken));

    [HttpGet("{vehiculoId:long}")]
    public async Task<ActionResult<Vehiculo>> GetById(long vehiculoId, CancellationToken cancellationToken)
    {
        var vehiculo = await mediator.Send(new GetVehiculoByIdQuery { VehiculoId = vehiculoId }, cancellationToken);
        return vehiculo is null ? NotFound() : Ok(vehiculo);
    }

    [HttpPost]
    public async Task<ActionResult<Vehiculo>> Create([FromBody] CreateVehiculoCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var vehiculo = await mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { vehiculoId = vehiculo.VehiculoId }, vehiculo);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ProblemDetails { Detail = ex.Message, Status = StatusCodes.Status404NotFound });
        }
    }

    [HttpPut("{vehiculoId:long}")]
    public async Task<IActionResult> Update(long vehiculoId, [FromBody] UpdateVehiculoCommand command, CancellationToken cancellationToken)
    {
        command.VehiculoId = vehiculoId;
        try
        {
            var updated = await mediator.Send(command, cancellationToken);
            return updated ? NoContent() : NotFound();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ProblemDetails { Detail = ex.Message, Status = StatusCodes.Status404NotFound });
        }
    }

    [HttpDelete("{vehiculoId:long}")]
    public async Task<IActionResult> Delete(long vehiculoId, CancellationToken cancellationToken) =>
        await mediator.Send(new DeleteVehiculoCommand { VehiculoId = vehiculoId }, cancellationToken) ? NoContent() : NotFound();
}
