using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using odata.Extensions;
using odata.models;
using odata.repository.Repositories;

namespace odata.Controllers.V1
{
    public sealed class DegreeController : ODataController
    {
        private readonly IRepository<Degree> repository;
        private readonly IValidator<Degree> validator;

        public DegreeController(IRepository<Degree> repository, IValidator<Degree> validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid key, CancellationToken cancellationToken)
        {
            var exists = await repository.ExistsAsync(key, cancellationToken);

            if (!exists)
            {
                return NotFound();
            }

            await repository.DeleteAsync(key, cancellationToken);

            return NoContent();
        }

        [HttpGet]
        [EnableQuery]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SingleResult<Degree>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public SingleResult<Degree> Get(Guid key)
        {
            var model = repository.Get(key);

            return SingleResult.Create<Degree>(model);
        }

        [HttpGet]
        [EnableQuery]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SingleResult<Degree>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IQueryable<Degree>> Get()
        {
            return Ok(repository.Get());
        }

        [HttpPatch]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Patch(Guid key, Delta<Degree> model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var originalModel = await repository.Get(key).SingleOrDefaultAsync(cancellationToken);

            if (originalModel is null)
            {
                return NotFound();
            }

            model.Patch(originalModel);

            await repository.UpdateAsync(originalModel, cancellationToken);

            return Updated(originalModel);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Degree))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Degree>> Post([FromBody] Degree model, CancellationToken cancellationToken)
        {
            var validatorResult = await validator.ValidateAsync(model, cancellationToken);

            if (!validatorResult.IsValid)
            {
                validatorResult.AddToModelState(ModelState);

                return BadRequest(ModelState);
            }

            await repository.AddAsync(model, cancellationToken);

            return Created(model);
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid key, Degree model, CancellationToken cancellationToken)
        {
            var validatorResult = await validator.ValidateAsync(model, cancellationToken);

            if (!validatorResult.IsValid)
            {
                validatorResult.AddToModelState(ModelState);

                return BadRequest(ModelState);
            }

            if (key != model.Id)
            {
                return BadRequest();
            }

            var exists = await repository.ExistsAsync(key, cancellationToken);

            if (!exists)
            {
                return NotFound();
            }

            await repository.UpdateAsync(model, cancellationToken);

            return Updated(model);
        }
    }
}