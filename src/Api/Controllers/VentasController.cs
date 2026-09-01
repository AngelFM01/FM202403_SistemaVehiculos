using Core.Feature.Ventas.Commands;
using Core.Feature.Ventas.Queries;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("ventas")]
public sealed class VentasController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Venta>>> GetAll(CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetVentasQuery(), cancellationToken));

    [HttpGet("{ventaId:long}")]
    public async Task<ActionResult<Venta>> GetById(long ventaId, CancellationToken cancellationToken)
    {
        var venta = await mediator.Send(new GetVentaByIdQuery { VentaId = ventaId }, cancellationToken);
        return venta is null ? NotFound() : Ok(venta);
    }

    // Caso de uso: Realizar Venta Vehiculo
    [HttpPost]
    public async Task<ActionResult<Venta>> Create([FromBody] CreateVentaCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var venta = await mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { ventaId = venta.VentaId }, venta);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ProblemDetails { Detail = ex.Message, Status = StatusCodes.Status404NotFound });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new ProblemDetails { Detail = ex.Message, Status = StatusCodes.Status409Conflict });
        }
    }

    [HttpPut("{ventaId:long}")]
    public async Task<IActionResult> Update(long ventaId, [FromBody] UpdateVentaCommand command, CancellationToken cancellationToken)
    {
        command.VentaId = ventaId;
        var updated = await mediator.Send(command, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{ventaId:long}")]
    public async Task<IActionResult> Delete(long ventaId, CancellationToken cancellationToken) =>
        await mediator.Send(new DeleteVentaCommand { VentaId = ventaId }, cancellationToken) ? NoContent() : NotFound();
}
