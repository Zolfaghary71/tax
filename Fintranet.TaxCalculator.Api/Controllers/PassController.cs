using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Fintranet.TaxCalculator.Application.Features.Passes.Command.CalculateTaxes;
using Fintranet.TaxCalculator.Application.Features.Passes.Command.Create;
using Fintranet.TaxCalculator.Application.Features.Passes.Command.Delete;
using Fintranet.TaxCalculator.Application.Features.Passes.Command.Update;
using Fintranet.TaxCalculator.Application.Features.Passes.Queries.GetAll;
using Fintranet.TaxCalculator.Application.Features.Passes.Queries.GetById;

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
        public async Task<IActionResult> GetPassById(Guid id)
        {
            var query = new GetPassByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePass(UpdatePassCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePass(Guid id)
        {
            var command = new DeletePassCommand { Id = id };
            var result = await _mediator.Send(command);
            if (result==null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPasses()
        {
            var result = await _mediator.Send(new GetAllPassesQuery());
            return Ok(result);
        }
        [HttpPost("calculate-taxes")]
        public async Task<IActionResult> CalculateTaxes([FromBody] CalculateTaxesCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}