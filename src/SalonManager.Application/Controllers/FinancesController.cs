using Microsoft.AspNetCore.Mvc;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Services;
using Serilog;
using System;


namespace SalonManager.Application.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class FinancesController : ControllerBase
{
    private readonly IFinanceService _service;
    public FinancesController(IFinanceService service)
    {
        _service = service;
    }

    // GET api/[controller]
    [ProducesResponseType((200), Type = typeof(List<Finance>))]
    [ProducesResponseType((400))]
    [HttpGet("")]
    public async Task<IActionResult> GetAllAsync() 
    {
        try
        {
            Log.Information("#### Obtendo todos os registros financeiros ####");
            var finances = await _service.GetAllAsync();
            return Ok(finances);
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }


    // GET api/[controller]/{id}
    [ProducesResponseType((200), Type = typeof(Finance))]
    [ProducesResponseType((400))]
    [ProducesResponseType((404))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id) 
    {
        try
        {
            Log.Information($"#### Obtendo o registro financeiro de ID: {id} ####");

            var finance = await _service.GetByIdAsync(id);

            if (finance is null)
            {
                Log.Error($"**** Não foi possível localizar o registro financeiro de ID: {id} ");
                return NotFound();
            }

            return Ok(finance);
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }


    // POST api/[controller]
    [ProducesResponseType((200), Type = typeof(Finance))]
    [ProducesResponseType((400))]
    [HttpPost("")]
    public async Task<IActionResult> PostAsync(InputFinanceModel inputModel)
    {
        try
        {
            Log.Information($"#### Inserindo um novo registro financeiro ####");

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
    [ProducesResponseType((204), Type = typeof(Finance))]
    [ProducesResponseType((400))]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, EditFinanceModel editModel) 
    {
        try
        {
            Log.Information($"#### Atualizando o registro financeiro de ID = {id} ####");

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
            Log.Information($"#### Excluindo o registro financeiro de ID = {id} ####");

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
