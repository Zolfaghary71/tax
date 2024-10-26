using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Fintranet.TaxCalculator.Application.Features.Pass.Command.Create;
using Fintranet.TaxCalculator.Application.Features.Pass.Command.Delete;
using Fintranet.TaxCalculator.Application.Features.Pass.Command.Update;
using Fintranet.TaxCalculator.Application.Features.Pass.Queries;
using Fintranet.TaxCalculator.Application.Features.Pass.Queries.GetById;

namespace Fintranet.TaxCalculator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PassController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePass([FromBody] CreatePassCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPassById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPassById(int id)
        {
            var query = new GetPassByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePass(int id, [FromBody] UpdatePassCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePass(int id)
        {
            var command = new DeletePassCommand { Id = id };
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPasses()
        {
            var query = new GetAllPassesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}