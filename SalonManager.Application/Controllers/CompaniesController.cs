using Microsoft.AspNetCore.Mvc;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Services;
using Serilog;


namespace SalonManager.Application.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _service;
    public CompaniesController(ICompanyService service)
    {
        _service = service;
    }

    // GET api/[controller]
    [ProducesResponseType((200), Type = typeof(List<Company>))]
    [ProducesResponseType((400))]
    [HttpGet("{tenantId}")]
    public async Task<IActionResult> GetAllAsync(string? tenantId = "")
    {
        try
        {
            Log.Information("#### Obtendo todas as empresas ####");
            var companies = await _service.GetAllAsync(tenantId);
            return Ok(companies);
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }


    // GET api/[controller]/{id}
    [ProducesResponseType((200), Type = typeof(Company))]
    [ProducesResponseType((400))]
    [ProducesResponseType((404))]
    [HttpGet("{tenantId}/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id, string? tenantId = "")
    {
        try
        {
            Log.Information($"#### Obtendo a empresa de ID: {id} ####");

            var company = await _service.GetByIdAsync(id, tenantId);

            if (company is null)
            {
                Log.Error($"**** Não foi possível localizar o registro empresa de ID: {id} ");
                return NotFound();
            }

            return Ok(company);
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }


    // POST api/[controller]
    [ProducesResponseType((200), Type = typeof(Company))]
    [ProducesResponseType((400))]
    [HttpPost("")]
    public async Task<IActionResult> PostAsync(InputCompanyModel inputModel)
    {
        try
        {
            Log.Information($"#### Inserindo uma nova empresa ####");

            if (inputModel is null)
            {
                Log.Error("**** As informações da Model não foram preenchidas");
                return BadRequest();
            }

            var user = await _service.InsertAsync(inputModel);

            if (user is null)
                return BadRequest();

            return Ok(inputModel);
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }


    // PUT api/[controller]
    [ProducesResponseType((204), Type = typeof(Company))]
    [ProducesResponseType((400))]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, EditCompanyModel editModel)
    {
        try
        {
            Log.Information($"#### Atualizando a empresa de ID = {id} ####");

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
            Log.Information($"#### Excluindo a empresa de ID = {id} ####");

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
