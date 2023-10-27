using Microsoft.AspNetCore.Mvc;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Services;
using Serilog;

namespace SalonManager.Application.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class GoalsController : ControllerBase
{
    private readonly IGoalService _service;
    public GoalsController(IGoalService service)
    {
        _service = service;
    }

    // GET api/[controller]
    [ProducesResponseType((200), Type = typeof(List<Goal>))]
    [ProducesResponseType((400))]
    [HttpGet("")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            Log.Information("#### Obtendo todas as metas ####");
            var goals = await _service.GetAllAsync();
            return Ok(goals);
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }


    // GET api/[controller]/{id}
    [ProducesResponseType((200), Type = typeof(Goal))]
    [ProducesResponseType((400))]
    [ProducesResponseType((404))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            Log.Information($"#### Obtendo a meta de ID: {id} ####");

            var goal = await _service.GetByIdAsync(id);

            if (goal is null)
            {
                Log.Error($"**** Não foi possível localizar a meta de ID: {id} ");
                return NotFound();
            }

            return Ok(goal);
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }


    // POST api/[controller]
    [ProducesResponseType((200), Type = typeof(Goal))]
    [ProducesResponseType((400))]
    [HttpPost("")]
    public async Task<IActionResult> PostAsync(InputGoalModel inputModel)
    {
        try
        {
            Log.Information($"#### Inserindo uma nova meta ####");

            if (inputModel is null)
            {
                Log.Error("**** As informações da Model não foram preenchidas");
                return BadRequest();
            }

            await _service.InsertAsync(inputModel);

            return Ok(inputModel);
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }


    // PUT api/[controller]
    [ProducesResponseType((204))]
    [ProducesResponseType((400))]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, EditGoalModel editModel)
    {
        try
        {
            Log.Information($"#### Atualizando a meta de ID: {id} ####");

            if (editModel is null)
            {
                Log.Error("**** As informações da Model não foram preenchidas");
                return BadRequest();
            }

            var model = await _service.UpdateAsync(id, editModel);

            if (model is null)
            {
                Log.Error("**** Houve um problema ao processar essa requisição");
                return BadRequest();
            }

            return NoContent();
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }


    // DELETE api/[controller]/{id}
    [ProducesResponseType((204))]
    [ProducesResponseType((400))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            Log.Information($"#### Excluindo a meta de ID: {id} ####");

            var wasDelete = await _service.DeleteAsync(id);

            if (!wasDelete)
            {
                Log.Error("**** Houve um problema ao processar essa requisição");
                return BadRequest();
            }

            return NoContent();
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }
}
