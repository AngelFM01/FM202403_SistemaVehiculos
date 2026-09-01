using Core.Feature.Marcas.Commands;
using Core.Feature.Marcas.Queries;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("marcas")]
public sealed class MarcasController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Marca>>> GetAll(CancellationToken cancellationToken) =>
        Ok(await mediator.Send(new GetMarcasQuery(), cancellationToken));

    [HttpGet("{marcaId:long}")]
    public async Task<ActionResult<Marca>> GetById(long marcaId, CancellationToken cancellationToken)
    {
        var marca = await mediator.Send(new GetMarcaByIdQuery { MarcaId = marcaId }, cancellationToken);
        return marca is null ? NotFound() : Ok(marca);
    }

    [HttpPost]
    public async Task<ActionResult<Marca>> Create([FromBody] CreateMarcaCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var marca = await mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { marcaId = marca.MarcaId }, marca);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new ProblemDetails { Detail = ex.Message, Status = StatusCodes.Status409Conflict });
        }
    }

    [HttpPut("{marcaId:long}")]
    public async Task<IActionResult> Update(long marcaId, [FromBody] UpdateMarcaCommand command, CancellationToken cancellationToken)
    {
        command.MarcaId = marcaId;
        try
        {
            var updated = await mediator.Send(command, cancellationToken);
            return updated ? NoContent() : NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new ProblemDetails { Detail = ex.Message, Status = StatusCodes.Status409Conflict });
        }
    }

    [HttpDelete("{marcaId:long}")]
    public async Task<IActionResult> Delete(long marcaId, CancellationToken cancellationToken) =>
        await mediator.Send(new DeleteMarcaCommand { MarcaId = marcaId }, cancellationToken) ? NoContent() : NotFound();
}
