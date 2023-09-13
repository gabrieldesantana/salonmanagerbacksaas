using Microsoft.AspNetCore.Mvc;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Services;
using Serilog;


namespace SalonManager.Application.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class SalonServicesController : ControllerBase
{
    private readonly ISalonServiceService _service;
    public SalonServicesController(ISalonServiceService service)
    {
        _service = service;
    }

    // GET api/[controller]
    [ProducesResponseType((200), Type = typeof(List<SalonService>))]
    [ProducesResponseType((400))]
    [HttpGet("")]
    public async Task<IActionResult> GetAllAsync() 
    {
        try
        {
            Log.Information("#### Obtendo todos os serviços ####");
            var services = await _service.GetAllAsync();
            return Ok(services);
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            return StatusCode(400, exception.Message);
        }
    }


    // GET api/[controller]/{id}
    [ProducesResponseType((200), Type = typeof(SalonService))]
    [ProducesResponseType((400))]
    [ProducesResponseType((404))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id) 
    {
        try
        {
            Log.Information($"#### Obtendo o serviço de ID = {id} ####");

            var service = await _service.GetByIdAsync(id);

            if (service is null) return NotFound();

            return Ok(service);
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            return StatusCode(400, exception.Message);
        }
    }


    // POST api/[controller]
    [ProducesResponseType((200), Type = typeof(SalonService))]
    [ProducesResponseType((400))]
    [HttpPost("")]
    public async Task<IActionResult> PostAsync(InputSalonServiceModel inputModel)
    {
        try
        {
            Log.Information($"#### Inserindo um novo serviço ####");

            if (inputModel is null) return BadRequest();

            await _service.InsertAsync(inputModel);

            return Ok(inputModel);
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            return StatusCode(400, exception.Message);
        }
    }


    // PUT api/[controller]
    [ProducesResponseType((204))]
    [ProducesResponseType((400))]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, EditSalonServiceModel editModel) 
    {
        try
        {
            Log.Information($"#### Atualizando o serviço de ID = {id} ####");

            if (editModel is null) return BadRequest();

            var model = await _service.UpdateAsync(id, editModel);

            if (model is null) return BadRequest();
            return NoContent();
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return StatusCode(400, ex.Message);
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
            Log.Information($"#### Excluindo o serviço de ID = {id} ####");

            var wasDelete = await _service.DeleteAsync(id);

            if (!wasDelete) return BadRequest();
            return NoContent();
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            return StatusCode(400, exception.Message);
        }
    }
}
